using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using log4net;

namespace spotchempdf
{
    class Range
    {
        public float min { get; set; }
        public float max { get; set; }
        public string unit { get; set; }

        public Range()
        {
            this.min = 0;
            this.max = 0;
            this.unit = "x";
        }

        public Range(float min, float max, string unit)
        {
            this.min = min;
            this.max = max;
            this.unit = unit;
        }
    }

    class ReadingRanges
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ReadingRanges));

        public Dictionary<string, Range> ranges { get; set; } = new Dictionary<string, Range>();

        public static ReadingRanges createFake()
        {
            ReadingRanges rr = new ReadingRanges();
            rr.Add("RBC", (float)5.65,(float)8.87,"x10^12/L");
            rr.Add("HCT", (float)37.3, (float)61.7,"%");
            rr.Add("HGB", (float)13.1, (float)20.5,"g/dL");
            rr.Add("MCV", (float)61.6, (float)73.5,"fL");
            rr.Add("MCH", (float)21.2, (float)25.9,"pg");
            rr.Add("MCHC", (float)32.0, (float)37.9,"g/dL");
            rr.Add("RDW", (float)13.6, (float)21.7,"%");

            return rr;
        }

        public string toJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public void Add(string name, float min, float max, string unit)
        {
            ranges.Add(name, new Range(min, max, unit));
        }

        public static ReadingRanges FromJSON(string json)
        {
            ReadingRanges rr;
            rr = JsonConvert.DeserializeObject<ReadingRanges>(json);
            return rr;
        }

        public void Save(string fileName)
        {
            log.Debug("Saving ranges to " + fileName);
            string content = this.toJSON();
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.WriteAsync(content);
            }

        }

        public static ReadingRanges Load(string fileName)
        {
            log.Debug("Loading reading ranges from " + fileName);
            string contents = File.ReadAllText(fileName);
            ReadingRanges rr = ReadingRanges.FromJSON(contents);

            return rr;
        }

        public void SaveFakeRanges(string fileName)
        {
            log.Debug("Creating fake ranges to "+fileName);
            ReadingRanges rr = ReadingRanges.createFake();
            log.Debug("Saving fake ranges to " + fileName);
            rr.Save(fileName);

        }


    }
}
