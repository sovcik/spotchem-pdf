using System;
using System.IO;
using System.IO.Ports;
using Newtonsoft.Json;
using log4net;

namespace spotchempdf
{
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

        public string readingsFolder, logFolder, outputFolder;
        [JsonIgnore]
        public string configFolder;
        public string logConfigFile = "log4net.xml";
        public string rangesConfigFile = "ranges.json";
        [JsonIgnore]
        public string configFile = "config.json";

        public bool openPDFAfterSave = true;

        public Config()
        {
            string AppName = "SpotchemPDF";
            string AppDataFld = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            AppDataFld = AppDataFld + @"\" + AppName;

            readingsFolder = AppDataFld + @"\Readings";
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
            string contents = File.ReadAllText(cfgFile);
            Config c = FromJSON(contents);

            return c;
        }

        public void Save()
        {
            string cfgFile = configFolder + @"\" + configFile;
            log.Debug("Saving configuration to " + cfgFile);
            string content = this.toJSON();
            using (StreamWriter outputFile = new StreamWriter(cfgFile))
            {
                outputFile.WriteAsync(content);
            }
        }

        public void createFolders()
        {
            Directory.CreateDirectory(readingsFolder);
            Directory.CreateDirectory(logFolder);
            Directory.CreateDirectory(configFolder);
            Directory.CreateDirectory(outputFolder);
        }


    }


}
