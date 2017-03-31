using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.Collections.Concurrent;

using System.Runtime.CompilerServices;
using System.Threading;
using log4net;
using log4net.Config;



namespace spotchempdf
{


    public partial class FrmMain : Form, IBufferProcessor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FrmMain));

        ConcurrentDictionary<string, Reading> readings = new ConcurrentDictionary<string, Reading>();
        Random r = new Random();

        Config cfg = new Config();

        ReadingRanges readingRanges = new ReadingRanges();

        byte[] spBuffer = new byte[1024];
        int spBOffset = 0;

        List<dynamic> readingsList = new List<dynamic>();

        PDFWriter pdfw;
        SerialReceiver sr = new SerialReceiver();

        int receivedCount = 0;

        public FrmMain()
        {
            string s;

            // load configuration
            s = cfg.configFolder + @"\" + cfg.configFile;
            if (File.Exists(s))
            {
                log.Debug("Loading configuration from " + s);
                cfg = cfg.Load();
            }
            else
            {
                log.Debug("Creating new app configuration file");
                cfg.Save();
            }

            this.Location = new System.Drawing.Point(cfg.mainWindow.x, cfg.mainWindow.y);
            InitializeComponent();

            // create application folders
            cfg.createFolders();

            // configure logging
            log4net.GlobalContext.Properties["LogFileName"] = cfg.logFolder + @"\SpotchemPDF.log";
            if (File.Exists(cfg.configFolder + @"\"+cfg.logConfigFile))
                XmlConfigurator.Configure(new System.IO.FileInfo(cfg.configFolder + @"\" + cfg.logConfigFile));
            else
                BasicConfigurator.Configure();

            log.Info("********** Starting SpotchemPDF ********************** ");

            // if none exist, create range configuration file
            if (!File.Exists(cfg.configFolder + @"\" + cfg.rangesConfigFile))
                readingRanges.SaveFakeRanges(cfg.configFolder + @"\" + cfg.rangesConfigFile);

            readingRanges = ReadingRanges.Load(cfg.configFolder + @"\" + cfg.rangesConfigFile);

            // 
            showAfterSave.Checked = cfg.openPDFAfterSave;

            // create & configure new PDF writer
            pdfw = new PDFWriter(readingRanges);

            // make sure correct ouput folder is display after loading configuration
            updatePDFPath(cfg.outputFolder);

            log.Info("Opening serial port");
            sr.OpenSerial(cfg.comPort);
            sr.setBufferProcessor(this);
            updateSerialStatus();

            lstReadings.Items.Clear();

            LoadReadings(cfg.readingsFolder);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void LoadReadings(string path)
        {
            string[] filePaths = Directory.GetFiles(path);

            log.Debug("Loading readings from " + path);
            readings.Clear();
            readingsList.Clear();

            foreach (string file in Directory.EnumerateFiles(path, "*.json"))
            {
                Reading rd = Reading.fromJSONFile(file);
                if (readings.TryAdd(rd.GetUUID(), rd))
                    readingsList.Add(new { Id = rd.GetUUID(), Name = rd.GetTitle(), FName = file });
                else
                    log.Warn("Failed to load reading " + rd.GetTitle());

            }

            lstReadings.DataSource = null;
            if (readingsList.Count > 0)
            {
                lstReadings.DataSource = readingsList;
                lstReadings.DisplayMember = "Name";
                lstReadings.ValueMember = "Id";
            }

            lstReadings.Invalidate();

            log.Info("Loaded " + readings.Count + " readings.");

        }


        private void btnSavePDF_Click(object sender, EventArgs e)
        {
            Reading r;
            if (lstReadings.Items.Count == 0 || lstReadings.SelectedIndex > lstReadings.Items.Count)
                return;

            // get selected item
            String s = lstReadings.SelectedItem.ToString();
            if (!readings.TryGetValue(lstReadings.SelectedValue.ToString(), out r))
                log.Warn("Unable to find reading UUID=" + lstReadings.SelectedValue.ToString());
            else
            {
                // save reading to PDF
                s = pdfw.savePDF(r, cfg.outputFolder + @"\" + r.GetUUID(), cfg.openPDFAfterSave);
                log.Info("Reading(" + r.GetUUID() + ") saved to file=" + s);

                // move reading to archive
                s = readingsList.Find(x => x.Id == r.GetUUID()).FName;
                s = Path.GetFileName(s);
                try
                {
                    File.Move(cfg.readingsFolder + @"\" + s, cfg.archiveFolder + @"\" + s);
                } catch (IOException ex)
                {
                    log.Warn("Error while moving file to archve folder. " + ex.Message);
                }

                // remove reading from the list
                LoadReadings(cfg.readingsFolder);
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void processBuffer(byte[] buffer, int length)
        {
            Array.Copy(buffer, 0, spBuffer, spBOffset, length);
            spBOffset += length;

            // first byte should be <STX> 0x02
            if (spBuffer[0] != Reading.ASCII_STX)
            {
                log.Warn("Bad data received. Looking for STX.");
                int i = 0;
                while (i < spBuffer.Length && spBuffer[i] != Reading.ASCII_STX) i++;
                if (i < spBuffer.Length && spBuffer[i] == Reading.ASCII_STX)
                {
                    log.Debug("STX found. Offset=" + i);
                    Array.Copy(spBuffer, i, spBuffer, 0, spBuffer.Length - i);
                    spBOffset = spBOffset - i;
                }
                else
                {
                    log.Warn("STX not found in buffer. Discarding buffer data.");
                    spBOffset = 0;
                }

            }

            // locate <ETX> 0x03
            if (spBOffset > 0)
            {
                log.Debug("Data in buffer. Looking for ETX");
                int i = 0;
                while (i < spBuffer.Length && spBuffer[i] != Reading.ASCII_ETX) i++;
                if (i < spBuffer.Length && spBuffer[i] == Reading.ASCII_ETX)
                {
                    log.Debug("ETX found. Offset=" + i+". Going to process message.");
                    byte[] m = new byte[i + 1];
                    // extract single message
                    Array.Copy(spBuffer, m, i + 1);

                    // move buffer data to create space for next data
                    Array.Copy(spBuffer, i + 1, spBuffer, 0, spBuffer.Length - (i + 1));
                    spBOffset -= (i+1);

                    // save raw data
                    string s = cfg.readingsFolder + @"\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")+".txt";
                    log.Debug("Saving raw data to " + s);
                    File.WriteAllBytes(s, m);

                    // parse data to readings
                    log.Debug("Parsing received data");
                    List<Reading> lrd = Reading.parse(m);
                    log.Info("Received " + lrd.Count + " readings.");
                    receivedCount += lrd.Count;

                    if (this.rcvCount.InvokeRequired)
                    {
                        this.rcvCount.BeginInvoke((MethodInvoker)delegate () { this.rcvCount.Text = this.receivedCount.ToString(); ; });
                    }
                    else
                    {
                        this.rcvCount.Text = this.receivedCount.ToString(); ;
                    }
                    //this.rcvCount.Text = receivedCount.ToString();
                    this.rcvCount.Invalidate();

                    // save parsed readings
                    log.Debug("Saving readings to " + cfg.readingsFolder);
                    lrd.ForEach(rd => rd.Save(cfg.readingsFolder));

                    // reload available reading
                    LoadReadings(cfg.readingsFolder);
                    this.lstReadings.Invalidate();
                }
            }
        }

        private void btnChangePDFPath(object sender, EventArgs e)
        {
            log.Debug("Opening output folder selection dialog.");
            frmFlbBrowse.Description = "Vyber priečinok pre PDF súbory";
            frmFlbBrowse.SelectedPath = cfg.outputFolder;
            frmFlbBrowse.ShowDialog();
            cfg.outputFolder = frmFlbBrowse.SelectedPath;
            updatePDFPath(cfg.outputFolder);
            cfg.Save();
            log.Info("Output folder changed. New=" + cfg.outputFolder);
        }


        private void btnCreateFake_Click(object sender, EventArgs e)
        {
            log.Info("Creating fake readings.");
            Reading.CreateFakeReadings(5,cfg.readingsFolder);
            LoadReadings(cfg.readingsFolder);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            log.Info("Loading readings.");
            LoadReadings(cfg.readingsFolder);
        }

        private void updatePDFPath(string path)
        {
            tbSavePath.Text = path;
            if (tbSavePath.Text.Length > 0)
            {
                tbSavePath.SelectionStart = tbSavePath.Text.Length - 1;
                tbSavePath.SelectionLength = 0;
            }
        }

        public void updateSerialStatus()
        {
            if (sr.isOpen())
                lblConnStatus.Text = "OK";
            else
                lblConnStatus.Text = "Chyba";

            lblConnStatus.Text += " (" + sr.getStatusString() + ")";

        }

        private void btnConfigSerial_Click(object sender, EventArgs e)
        {
            log.Debug("Opening serial port configuration window.");
            frmSerialSettings frmPortSet = new frmSerialSettings(cfg.comPort);

            // set saved window coordinates
            frmPortSet.Location = new System.Drawing.Point(cfg.portSetWindow.x, cfg.portSetWindow.y);

            frmPortSet.ShowDialog();

            // remember changed window location
            cfg.portSetWindow.x = frmPortSet.Location.X;
            cfg.portSetWindow.y = frmPortSet.Location.Y;

            if (frmPortSet.OKclicked && frmPortSet.getPortName().Length > 0)
            {
                cfg.comPort.name = frmPortSet.getPortName();
                log.Info("Serial port changed to " + cfg.comPort.name);

                cfg.Save();
                log.Debug("Configuration saved.");

                log.Debug("Closing old and opening new port.");
                sr.Close();
                sr.OpenSerial(cfg.comPort);

                updateSerialStatus();
                
            }

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.Debug("Application is closing.");

            log.Debug("Saving window location (" + this.Location.X + "," + this.Location.Y + ")");
            cfg.mainWindow.x = this.Location.X;
            cfg.mainWindow.y = this.Location.Y;

            cfg.Save();

            // close serial port
            log.Debug("Closing serial port");
            sr.Close();
        }

        private void showAfterSave_CheckedChanged(object sender, EventArgs e)
        {
            cfg.openPDFAfterSave = ((CheckBox)sender).Checked;
        }
    }
}
