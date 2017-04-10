using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Drawing;

using System.Runtime.CompilerServices;
using System.Threading;
using log4net;
using log4net.Config;

using System.Deployment.Application;



namespace spotchempdf
{


    public partial class FrmMain : Form, IBufferProcessor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FrmMain));

        // object containing loaded configuration
        Config cfg = new Config();

        // PDF writer object
        PDFWriter pdfw;

        // initialize random number generator
        Random r = new Random();

        // serial port receiver
        SerialReceiver sr = new SerialReceiver();

        // buffer for data received via serial port
        byte[] spBuffer = new byte[1024];

        // current position in spBuffer
        int spBOffset = 0;

        // count of readings received via serial port
        int receivedCount = 0;


        // dictionary containing all loaded readings
        ConcurrentDictionary<string, Reading> loadedReadings = new ConcurrentDictionary<string, Reading>();

        // list used to show loaded readings on the screen - it has to be synchronized with loadedReadings
        List<dynamic> readingsList = new List<dynamic>();
        
        // object containing data of reading selected in readingList
        Reading selectedReading;

        // true if reading has been updated, e.g. animal name has been entered
        bool readingUpdated;

        // locking object for saving updated reading
        Object updateLock = new Object();


        // object containng all available reading ranges for all types of animals
        ReadingRanges readingRanges = new ReadingRanges();


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

            // configure logging
            log4net.GlobalContext.Properties["LogFileName"] = cfg.logFolder + @"\SpotchemPDF.log";
            if (File.Exists(cfg.configFolder + @"\" + cfg.logConfigFile))
                XmlConfigurator.Configure(new System.IO.FileInfo(cfg.configFolder + @"\" + cfg.logConfigFile));
            else
                BasicConfigurator.Configure();

            // restore window position
            this.Location = new System.Drawing.Point(cfg.mainWindow.x, cfg.mainWindow.y);
            InitializeComponent();

            // clear listbox showing readings
            lstReadings.Items.Clear();

            this.Text = "SpotchemPDF "+PublishedVersion;

            // create application folders
            cfg.createFolders();


            log.Info("********** Starting SpotchemPDF "+PublishedVersion);

            // if none exist, create range configuration file
            if (!File.Exists(cfg.configFolder + @"\" + cfg.rangesConfigFile))
                readingRanges.SaveDefaults(cfg.configFolder + @"\" + cfg.rangesConfigFile);

            // load reading ranges for different types of animals
            readingRanges = ReadingRanges.Load(cfg.configFolder + @"\" + cfg.rangesConfigFile);
            reloadAnimalTypes("");

            // set checkbox
            showAfterSave.Checked = cfg.openPDFAfterSave;

            // create & configure new PDF writer
            pdfw = new PDFWriter(readingRanges);

            // make sure correct ouput folder is display after loading configuration
            updatePDFPath(cfg.outputFolder);

            // try opening serial port
            log.Info("Opening serial port");
            sr.OpenSerial(cfg.comPort);
            sr.setBufferProcessor(this);
            updateSerialStatus();

            timer1.Interval = 500; // 500ms between keystrokes is considered as interval long enough to save updates

            // load readings already existing in readings folder
            LoadReadings(cfg.readingsFolder);

        }

        public string PublishedVersion
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
                else
                    return "Not Published";
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void LoadReadings(string path)
        {
            string[] filePaths = Directory.GetFiles(path);

            log.Debug("Loading readings from " + path);
            loadedReadings.Clear();
            readingsList.Clear();

            foreach (string fileName in Directory.EnumerateFiles(path, "*.json"))
            {
                try
                {
                    Reading rd = Reading.fromJSONFile(fileName);

                    if (loadedReadings.TryAdd(rd.GetUUID(), rd))

                    {
                        readingsList.Add(new { Value = rd.GetUUID(), Text = rd.GetTitle(), fname = fileName });
                        log.Debug("Adding reading: Value=" + rd.GetUUID() + " Text=" + rd.GetTitle());
                    }
                    else
                        log.Warn("Failed to add reading " + rd.GetTitle());
                } catch (Exception ex)
                {
                    log.Debug("Failed to load reading from file=" + fileName + "  ex=" + ex.Message);
                }
            }

            lstReadings.DataSource = null;
            if (readingsList.Count > 0)
            {
                lstReadings.DisplayMember = "Text";
                lstReadings.ValueMember = "Value";
                lstReadings.DataSource = readingsList;
            }

            lstReadings.Invalidate();
           

            log.Info("Loaded " + loadedReadings.Count + " readings.");

            //updateSelectedReading();

        }

        private void SaveReadingToPDF(Reading r, string fileName, RangeType rangeType)
        {
            // save reading to PDF
            string s = pdfw.savePDF(r, fileName, cfg.openPDFAfterSave, cfg.provider, rangeType);
            log.Info("SavePDF: Reading(" + r.GetUUID() + ") saving to file=" + s);

            // move reading to archive
            s = readingsList.Find(x => x.Value == r.GetUUID()).fname;
            s = Path.GetFileName(s);
            string sa = cfg.archiveFolder + @"\" + s;
            if (File.Exists(sa))
            {
                log.Debug("Reading already archived. Adding version.");
                int sfx = 0;
                while (File.Exists(sa + "." + sfx)) sfx++;
                sa = sa + "." + sfx;
            }
            try
            {
                File.Move(cfg.readingsFolder + @"\" + s, sa);
                log.Info("Reading file=" + s + " moved to file=" + sa);
            }
            catch (IOException ex)
            {
                log.Warn("SavePDF: Error while moving file to archve folder. " + ex.Message);
            }
        }


        private void btnSavePDF_Click(object sender, EventArgs e)
        {
            Reading r;
            if (lstReadings.Items.Count == 0 || lstReadings.SelectedIndex > lstReadings.Items.Count || lstReadings.SelectedIndex < 0)
            {
                log.Debug("SavePDF: Empty list or nothing selected.");
                return;
            }

            if (animalType.Text.Trim().Length == 0)
            {
                MessageBox.Show("Najprv vyberte druh zvieraťa.");
                return;
            }


            log.Debug("Going to save " + lstReadings.SelectedItem + " value=" + getSelectedValue());
            // get selected item
            
            if (!loadedReadings.TryGetValue(getSelectedValue(), out r))
                log.Warn("SavePDF: Unable to find reading UUID=" + getSelectedValue());
            else
            {
                RangeType rt;
                if (!readingRanges.rangeTypes.TryGetValue(r.animalType, out rt))
                {
                    log.Info("Ranges not found for animal type = " + r.animalType);
                    rt = new RangeType();
                }

                SaveReadingToPDF(r, cfg.outputFolder + @"\" + r.GetUUID(),rt);

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
                    log.Debug("Saving "+(i+1)+" bytes of raw data to " + s);
                    File.WriteAllBytes(s, m);

                    // parse data to readings
                    log.Debug("Parsing received data");
                    List<Reading> lrd = Reading.parse(m);
                    log.Info("Received " + lrd.Count + " readings.");
                    receivedCount += lrd.Count;

                    // show number of received messages
                    this.rcvCount.Text = receivedCount.ToString();
                    /*
                    if (this.rcvCount.InvokeRequired)
                    {
                        this.rcvCount.BeginInvoke((MethodInvoker)delegate () { this.rcvCount.Text = this.receivedCount.ToString(); ; });
                    }
                    else
                    {
                        this.rcvCount.Text = this.receivedCount.ToString(); ;
                    }
                    this.rcvCount.Invalidate();
                    */

                    // save parsed readings
                    log.Debug("Saving readings to " + cfg.readingsFolder);
                    lrd.ForEach(rd => rd.Save(cfg.readingsFolder));

                    // reload available reading
                    LoadReadings(cfg.readingsFolder);
                    
                }
            }
        }

        private void changePDFPath()
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
            //Reading.CreateFakeReadings(5,cfg.readingsFolder);
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
            {
                lblConnStatus.ForeColor = SystemColors.Highlight;
                lblConnStatus.Text = "OK";

            }
            else
            {
                lblConnStatus.ForeColor = Color.Red;
                lblConnStatus.Text = "Chyba";
            }

            lblConnStatus.Text += " (" + sr.getStatusString() + ")";

        }

        private void configureSerialPort()
        {
            log.Debug("Opening serial port configuration window.");
            frmSerialSettings frm = new frmSerialSettings(cfg.comPort);

            // set saved window coordinates
            frm.Location = new System.Drawing.Point(cfg.portSetWindow.x, cfg.portSetWindow.y);

            // configure using current parameters
            frm.setParams(cfg.comPort.name, cfg.comPort.baudRate);

            frm.ShowDialog();

            // remember changed window location
            cfg.portSetWindow.x = frm.Location.X;
            cfg.portSetWindow.y = frm.Location.Y;

            if (frm.OKclicked && frm.getPortName().Length > 0)
            {
                cfg.comPort.name = frm.getPortName();
                log.Info("Serial port changed to " + cfg.comPort.name);

                cfg.comPort.baudRate = frm.getBaudRate();

                cfg.Save();
                log.Debug("Configuration saved.");

                log.Debug("Closing old and opening new port.");
                sr.Close();
                sr.OpenSerial(cfg.comPort);

                updateSerialStatus();

            }
        }

        private void changeProviderDetails()
        {
            log.Debug("Opening provider details configuration window.");
            frmProviderAddress frm = new frmProviderAddress();

            // set saved window coordinates
            frm.Location = new System.Drawing.Point(cfg.providerDetailsWindow.x, cfg.providerDetailsWindow.y);
            frm.setData(cfg.provider);

            frm.ShowDialog();

            cfg.providerDetailsWindow.x = frm.Location.X;
            cfg.providerDetailsWindow.y = frm.Location.Y;

            if (frm.DialogResult == DialogResult.OK)
            {
                log.Debug("Provider details changed.");
                frm.getData(out cfg.provider);

                cfg.Save();
                log.Debug("Configuration saved.");

            }
        }

        private void reloadAnimalTypes(string name)
        {
            animalType.DataSource = null;
            animalType.Enabled = false;

            if (readingRanges.rangeTypes.Count == 0) 
                return;

            animalType.DataSource = new List<string>(readingRanges.rangeTypes.Keys);

            if (animalType.Items.Count > 0)
                animalType.Enabled = true;

            if (name.Length > 0)
            {
                int i = animalType.Items.IndexOf(name);
                if (i >= 0)
                {
                    animalType.SelectedIndex = i;
                    animalType.Text = name;
                }
            }

        }

        private void editRanges()
        {
            log.Debug("Opening edit ranges window");
            frmEditRanges frm = new frmEditRanges(cfg.configFolder+@"\"+cfg.rangesConfigFile);

            // set saved window coordinates
            frm.Location = new System.Drawing.Point(cfg.editRangesWindow.x, cfg.editRangesWindow.y);

            frm.ShowDialog();

            readingRanges = ReadingRanges.Load(cfg.configFolder + @"\" + cfg.rangesConfigFile);

            reloadAnimalTypes(animalType.Text);

            cfg.editRangesWindow.x = frm.Location.X;
            cfg.editRangesWindow.y = frm.Location.Y;

            cfg.Save();
            log.Debug("Configuration saved.");

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
            cfg.Save();
        }

        private void saveUpdates()
        {
            lock (updateLock)
            {

                if (readingUpdated && selectedReading != null)
                {
                    String key = selectedReading.GetUUID();
                    lock (selectedReading)
                    {
                        selectedReading.clientId = clientId.Text;
                        selectedReading.clientName = clientName.Text;
                        selectedReading.animalName = animalName.Text;
                        selectedReading.animalType = animalType.Text;
                        selectedReading.animalAge = int.Parse(animalAge.Text);

                        loadedReadings[key].clientId = selectedReading.clientId;
                        loadedReadings[key].clientName = selectedReading.clientName;
                        loadedReadings[key].animalType = selectedReading.animalType;
                        loadedReadings[key].animalName = selectedReading.animalName;
                        loadedReadings[key].animalAge = selectedReading.animalAge;

                        loadedReadings[key].Save(cfg.readingsFolder);


                    }

                }

                timer1.Stop();
                readingUpdated = false;
            }

        }

        private void readingModified_TextChanged(object sender, EventArgs e)
        {
            lock (updateLock)
            {
                readingUpdated = true;
                timer1.Start();
            }

        }

        private void updateSelectedReading()
        {            
            if (lstReadings.SelectedIndex < 0 )
            {
                selectedReading = null;
                log.Info("UpdateSelected: List of readings is empty or nothing is selected. count="+lstReadings.Items.Count + " selected="+lstReadings.SelectedIndex);

                clientId.Enabled = false;
                clientName.Enabled = false;

                animalName.Enabled = false;
                animalAge.Enabled = false;
                animalType.Enabled = false;

                btnSave.Enabled = false;

                clientId.Text = "";
                clientName.Text = "";
                animalName.Text = "";
                animalType.Text = "";
                animalAge.Text = "";

                return;
            }


            // get selected item
            try
            {
                log.Debug("Selecting reading UUID=" + getSelectedValue()+" Index="+lstReadings.SelectedIndex);
                selectedReading = loadedReadings[getSelectedValue()];

                clientId.Enabled = true;
                clientName.Enabled = true;

                animalName.Enabled = true;
                animalAge.Enabled = true;
                animalType.Enabled = true;

                btnSave.Enabled = true;

                clientId.Text = selectedReading.clientId;
                clientName.Text = selectedReading.clientName;
                animalName.Text = selectedReading.animalName;
                animalType.Text = selectedReading.animalType;
                animalAge.Text = selectedReading.animalAge.ToString();

            }
            catch (KeyNotFoundException ex)
            {
                log.Warn("UpdateSelected: Unable to find reading UUID=" + getSelectedValue()+" ex="+ex.Message+" stack="+ex.StackTrace);
                clientId.Text = "";
                clientName.Text = "";
                animalName.Text = "";
                animalType.Text = "";
                animalAge.Text = "";

            }

        }

        // not sure why standard SelectedValue was returning wrong data while SelectedItem was showing correct ones
        private string getSelectedValue()
        {
            string s = "";
            if (lstReadings.SelectedIndex >= 0)
                s = (string)lstReadings.SelectedItem.GetType().GetProperty("Value").GetValue(lstReadings.SelectedItem, null);
            return s;
        }

        private void lstReadings_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateSelectedReading();

            if (lstReadings.SelectedIndex >= 0)
                log.Debug("Selected reading=" + lstReadings.SelectedItem+" value="+ getSelectedValue());

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            saveUpdates();
        }

        private void changeSerialPortMenuItem_Click(object sender, EventArgs e)
        {
            configureSerialPort();
        }

        private void changeOutputFolderMenutItem_Click(object sender, EventArgs e)
        {
            changePDFPath();
        }

        private void loadReadingsMenuItem_Click(object sender, EventArgs e)
        {
            LoadReadings(cfg.readingsFolder);
        }

        private void providerDetailsMenuItem_Click(object sender, EventArgs e)
        {
            changeProviderDetails();
        }

        private void editRangesMenuItem_Click(object sender, EventArgs e)
        {
            editRanges();
        }

        private void animalType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lock (updateLock)
            {
                readingUpdated = true;
                timer1.Start();
            }

        }
    }
}
