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

        public static ReadingRanges createDefaults()
        {
            log.Debug("Adding default ranges");

            ReadingRanges rr = new ReadingRanges();
                                                                         // Values for DOGS
            rr.Add("BUN", (float)2.4989999, (float)8.568, "mmol/L");     // Blood Urea Nitrogen: 7-24 mg/dL -> 2.4989999-8.568 mmol/L
            rr.Add("Glu", (float)4.44, (float)6.66, "mmol/L");           // Glucose: 80-120 g/dL -> 4.44-6.66 mmol/L
            rr.Add("ALP", (float)0.0510, (float)1.190, "µkat/L");        // Alkaline Phosphatase: 3-70 U/L
            rr.Add("T-Pro", (float)54, (float)80, "g/L");                // Total Protein: 5.4-8 g/dL
            rr.Add("GPT", (float)0.068, (float)1.53, "µkat/L");          // Alanine transaminase or glutamate pyruvate transaminase ALT/GPT: 4-90 U/L
            rr.Add("Cre", (float)61.88, (float)123.76, "µmol/L");        // Creatinine: 0.7-1.4 mg/dL
            rr.Add("Ca",  (float)2.25, (float)2.85, "mmol/L");           // Ca2+: 9-11.4 mg/dL
            return rr;
        }

        public string toJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public void Add(string name, float min, float max, string unit)
        {
            if (!ranges.ContainsKey(name))
                ranges.Add(name, new Range(min, max, unit));
            else
                log.Debug("Range " + name + " is already in the list.");
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

        public void SaveDefaultRanges(string fileName)
        {
            log.Debug("Creating fake ranges to "+fileName);
            ReadingRanges rr = ReadingRanges.createDefaults();
            log.Debug("Saving fake ranges to " + fileName);
            rr.Save(fileName);

        }


    }
}
