using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using log4net;

namespace spotchempdf
{
    public class Range
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

    public class RangeType
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RangeType));

        public Dictionary<string, Range> ranges { get; set; } = new Dictionary<string, Range>();

        public static RangeType createDefaults()
        {
            log.Debug("Adding default ranges");

            RangeType rr = new RangeType();
            // Values for DOGS
            rr.Add("BUN", (float)2.4989999, (float)8.568, "mmol/L");     // Blood Urea Nitrogen: 7-24 mg/dL -> 2.4989999-8.568 mmol/L
            rr.Add("Glu", (float)4.44, (float)6.66, "mmol/L");           // Glucose: 80-120 g/dL -> 4.44-6.66 mmol/L
            rr.Add("ALP", (float)0.0510, (float)1.190, "µkat/L");        // Alkaline Phosphatase: 3-70 U/L
            rr.Add("T-Pro", (float)54, (float)80, "g/L");                // Total Protein: 5.4-8 g/dL
            rr.Add("GPT", (float)0.068, (float)1.53, "µkat/L");          // Alanine transaminase or glutamate pyruvate transaminase ALT/GPT: 4-90 U/L
            rr.Add("Cre", (float)61.88, (float)123.76, "µmol/L");        // Creatinine: 0.7-1.4 mg/dL
            rr.Add("Ca", (float)2.25, (float)2.85, "mmol/L");            // Ca2+: 9-11.4 mg/dL
            rr.Add("MCH", (float)0, (float)0, "pg");                     // 
            rr.Add("RBC", (float)0, (float)0, "x10^12L");                // 
            rr.Add("HCT", (float)0, (float)0, "%");                      //
            rr.Add("MCV", (float)0, (float)0, "fl");                     //
            return rr;
        }

        public void Add(string name, float min, float max, string unit)
        {
            if (!ranges.ContainsKey(name))
                ranges.Add(name, new Range(min, max, unit));
            else
                log.Debug("Range " + name + " is already in the list.");
        }

    }

    public class ReadingRanges
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ReadingRanges));

        public Dictionary<string, RangeType> rangeTypes { get; set; } = new Dictionary<string, RangeType>();

        public void Add(string name, RangeType type)
        {
            if (!rangeTypes.ContainsKey(name))
                rangeTypes.Add(name, type);
            else
                log.Debug("RangeType " + name + " is already in the list.");
        }

        public string toJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        public static ReadingRanges FromJSON(string json)
        {
            ReadingRanges rr;
            rr = JsonConvert.DeserializeObject<ReadingRanges>(json);
            return rr;
        }

        public void Save(string fileName)
        {
            string bakFile = fileName + ".bak";
            log.Debug("Creating backup file " + bakFile);
            try
            {
                File.Copy(fileName, bakFile, true);
            } catch (Exception ex)
            {
                log.Error("Failed creating backup file " + bakFile+" ex="+ex.Message);
            }

            log.Debug("Saving ranges to " + fileName);
            if (fileName != null)
            {
                string content = this.toJSON();
                try
                {
                    using (StreamWriter outputFile = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                    {
                        outputFile.Write(content);
                    }
                } catch (Exception ex)
                {
                    log.Error("Failed writing range file " + fileName+" ex="+ex.Message);
                    log.Info("Restoring range file from backup file " + bakFile);
                    try
                    {
                        File.Copy(bakFile, fileName, true);
                    } catch (Exception ex2)
                    {
                        log.Error("Failed restoring range file from backup file " + bakFile + " ex=" + ex2.Message);
                    }
                    throw ex;
                }

            }
            else
                log.Debug("File not specified - nothing saved");

        }

        public static ReadingRanges Load(string fileName)
        {
            log.Debug("Loading reading ranges from " + fileName);
            string contents = File.ReadAllText(fileName, System.Text.Encoding.UTF8);
            ReadingRanges rr = ReadingRanges.FromJSON(contents);

            return rr;
        }

        public void SaveDefaults(string fileName)
        {
            log.Debug("Creating default ranges to "+fileName);
            ReadingRanges rr = new ReadingRanges();
            rr.Add("Pes", RangeType.createDefaults());
            log.Debug("Saving default ranges to " + fileName);
            rr.Save(fileName);

        }


    }
}
