using System;
using System.IO;
using System.IO.Ports;
using Newtonsoft.Json;
using log4net;

namespace spotchempdf
{
    public class Provider
    {
        public string name { get; set; } = "";
        public string address1 { get; set; } = "";
        public string address2 { get; set; } = "";
        public string address3 { get; set; } = "";
        public string address4 { get; set; } = "";
        public string contact1 { get; set; } = "";
        public string contact2 { get; set; } = "";

    }

    public class window
    {
        public int x = 200;
        public int y = 200;

        public window(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }


    public class COMport
    {
        public string name { get; set; } = "COM1";
        public int baudRate { get; set; } = 9600;
        [JsonIgnore]
        public Parity parity = Parity.Even;
        [JsonIgnore]
        public StopBits stopBits = StopBits.Two;
        [JsonIgnore]
        public int dataBits = 7;
        [JsonIgnore]
        public Handshake handshake { get; set; } = Handshake.RequestToSend;
        [JsonIgnore]
        public bool rtsEnable = true;

    }

    public class Config
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Config));

        public COMport comPort = new COMport();

        public string readingsFolder, logFolder, outputFolder, archiveFolder;
        [JsonIgnore]
        public string configFolder;
        public string logConfigFile = "log4net.xml";
        public string rangesConfigFile = "ranges.json";
        [JsonIgnore]
        public string configFile = "config.json";

        public bool openPDFAfterSave = true;

        public window mainWindow = new window(200, 200);
        public window portSetWindow = new window(250, 250);
        public window providerDetailsWindow = new window(250, 250);
        public window editRangesWindow = new window(250, 250);

        public Provider provider = new Provider();

        public Config()
        {
            string AppName = "SpotchemPDF";
            string AppDataFld = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            AppDataFld = AppDataFld + @"\" + AppName;

            readingsFolder = AppDataFld + @"\Readings";
            archiveFolder = readingsFolder + @"\Archive";
            logFolder = AppDataFld + @"\Log";
            configFolder = AppDataFld + @"\Configuration";
            outputFolder = AppDataFld;

        }

        public Config FromJSON(string json)
        {
            Config c;
            c = JsonConvert.DeserializeObject<Config>(json);
            return c;
        }

        public string toJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Config Load()
        {
            string cfgFile = configFolder + @"\" + configFile;
            log.Debug("Loading configuration from " + cfgFile);
            string contents = File.ReadAllText(cfgFile, System.Text.Encoding.UTF8);
            Config c = FromJSON(contents);

            return c;
        }

        public void Save()
        {
            string cfgFile = configFolder + @"\" + configFile;
            string cfgFileBak = cfgFile + ".bak";
            log.Debug("Saving configuration to " + cfgFile);
            string content = this.toJSON();
            Directory.CreateDirectory(configFolder);

            try
            {
                System.IO.File.Copy(cfgFile, cfgFileBak, true);
            } catch (Exception ex)
            {
                log.Error("Failed creating backup file: " + cfgFileBak+" ex="+ex.Message);
            }

            try
            {
                using (StreamWriter outputFile = new StreamWriter(cfgFile, false, System.Text.Encoding.UTF8))
                {
                    outputFile.Write(content);
                }
            } catch (Exception ex)
            {
                log.Error("Failed saving configuration. "+ex.Message);

                try
                {
                    System.IO.File.Copy(cfgFileBak, cfgFile, true);
                    log.Info("Configuration restored from backup file: ");
                } catch
                {
                    log.Error("Failed restoring backup file: " + cfgFileBak);
                }

                throw ex;
            }
        }

        public void createFolders()
        {
            Directory.CreateDirectory(readingsFolder);
            Directory.CreateDirectory(archiveFolder);
            Directory.CreateDirectory(logFolder);
            Directory.CreateDirectory(configFolder);
            Directory.CreateDirectory(outputFolder);
        }


    }


}
