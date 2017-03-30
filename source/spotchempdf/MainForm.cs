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


    public partial class FrmMain : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FrmMain));

        ConcurrentDictionary<string, Reading> readings = new ConcurrentDictionary<string, Reading>();
        Random r = new Random();

        Config cfg = new Config();

      

        ReadingRanges readingRanges = new ReadingRanges();

        byte[] spBuffer = new byte[1024];
        int spBOffset = 0;

        List<dynamic> ReadingsList = new List<dynamic>();

        PDFWriter pdfw;
        SerialReceiver sr = new SerialReceiver();

        

        public FrmMain()
        {
            string s;
            InitializeComponent();

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

            // create application folders
            cfg.createFolders();


            // configure logging
            log4net.GlobalContext.Properties["LogFileName"] = cfg.logFolder + @"\SpotchemPDF.log";
            if (File.Exists(cfg.configFolder + @"\"+cfg.logConfigFile))
                XmlConfigurator.Configure(new System.IO.FileInfo(cfg.configFolder + @"\" + cfg.logConfigFile));
            else
                BasicConfigurator.Configure();

            // if none exist, create range configuration file
            if (!File.Exists(cfg.configFolder + @"\" + cfg.rangesConfigFile))
                readingRanges.SaveFakeRanges(cfg.configFolder + @"\" + cfg.rangesConfigFile);

            readingRanges = ReadingRanges.Load(cfg.configFolder + @"\" + cfg.rangesConfigFile);

            // create & configure new PDF writer
            pdfw = new PDFWriter(readingRanges);

            // make sure correct ouput folder is display after loading configuration
            updatePDFPath(cfg.outputFolder);

            log.Info("Opening serial port");
            sr.OpenSerial(cfg.comPort);
            updateSerialStatus();

            LoadReadings(cfg.readingsFolder);

        }

        private void LoadReadings(string path)
        {
            string[] filePaths = Directory.GetFiles(path);

            log.Debug("Loading readings from " + path);
            readings.Clear();
            ReadingsList.Clear();
            lstReadings.Refresh();

            foreach (string file in Directory.EnumerateFiles(path, "*.*"))
            {
                string contents = File.ReadAllText(file);
                Reading rd = Reading.FromJSON(contents);
                if (readings.TryAdd(rd.GetUUID(), rd))
                    ReadingsList.Add(new { Id = rd.GetUUID(), Name = rd.GetTitle() });
                else
                    log.Warn("Failed to load reading " + rd.GetTitle());

            }
            lstReadings.DataSource = null;
            lstReadings.DataSource = ReadingsList;
            lstReadings.DisplayMember = "Name";
            lstReadings.ValueMember = "Id";

            log.Info("Loaded " + readings.Count + " readings.");

        }


        private void btnSavePDF_Click(object sender, EventArgs e)
        {
            Reading r;
            // get selected item
            String s = lstReadings.SelectedItem.ToString();
            if (!readings.TryGetValue(lstReadings.SelectedValue.ToString(), out r))
                log.Warn("Unable to find reading UUID=" + lstReadings.SelectedValue.ToString());
            pdfw.savePDF(r, cfg.outputFolder + @"\" + r.GetUUID(), cfg.openPDFAfterSave);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void processBuffer(byte[] buffer, int length)
        {
            Array.Copy(buffer, 0, spBuffer, spBOffset, length);
            spBOffset += length;

            // first byte should be <STX> 0x02
            if (spBuffer[0] != (byte)0x02)
            {
                log.Warn("Bad data received. Looking for STX.");
                int i = 0;
                while (i < spBuffer.Length && spBuffer[i] != (byte)0x02) i++;
                if (i < spBuffer.Length && spBuffer[i] == (byte)0x02)
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

            // locate <ETX> 
        }

        private void btnChangePDFPath(object sender, EventArgs e)
        {
            frmFlbBrowse.Description = "Vyber priečinok pre PDF súbory";
            frmFlbBrowse.SelectedPath = cfg.outputFolder;
            frmFlbBrowse.ShowDialog();
            cfg.outputFolder = frmFlbBrowse.SelectedPath;
            updatePDFPath(cfg.outputFolder);
            cfg.Save();
        }

        private void FrmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // close serial port
            sr.Close();
        }

        private void btnCreateFake_Click(object sender, EventArgs e)
        {
            Reading.CreateFakeReadings(5,cfg.readingsFolder);
            LoadReadings(cfg.readingsFolder);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
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
            frmSerialSettings frmPortSet = new frmSerialSettings(cfg.comPort);
            frmPortSet.ShowDialog();
            if (frmPortSet.OKclicked && frmPortSet.getPortName().Length > 0)
            {
                cfg.comPort.name = frmPortSet.getPortName();
                cfg.Save();
                updateSerialStatus();
                log.Info("Serial port changed to " + cfg.comPort.name);
            }

        }
    }
}
