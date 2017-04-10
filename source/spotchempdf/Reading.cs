using System;
using System.IO;
using System.Globalization;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

using log4net;


namespace spotchempdf
{
    public class ReadingItem
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ReadingItem));

        public string name { get; set; }
        public char abnormalMark { get; set; } = ' ';
        public float value { get; set; } = -9999;
        public string unit { get; set; } = "";
        public int temp { get; set; }
        public string error { get; set; } = "";

        public ReadingItem(string name, char mark, float value, string unit, char temp)
        {
            this.name = name;
            this.abnormalMark = mark;
            this.value = value;
            this.unit = unit;
            this.temp = tempToNum(temp);
        }

        [JsonConstructor]
        public ReadingItem(string name, char mark, float value, string unit, int temp)
        {
            this.name = name;
            this.abnormalMark = mark;
            this.value = value;
            this.unit = unit;
            this.temp = temp;
        }

        public ReadingItem(string name, string error, int temp)
        {
            this.name = name;
            this.error = error;
            this.temp = temp;
        }


        int tempToNum(char t)
        {
            switch (t)
            {
                case ' ':
                    return 37;
                case '+':
                    return 30;
                case '*':
                    return 25;
                default:
                    return -1;
            }
        }

        public string toString()
        {
            return name + " " + value + " " + unit + " " + abnormalMark + " t=" + temp;
        }

        public string toJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static ReadingItem getFake(string test, float value, float min, float max, string unit, int temp)
        {
            char m = ' ';
            
            if (value < min)
                m = (char)Reading.ASCII_US;
            else if (value > min)
                m = (char)Reading.ASCII_RS;

            return new ReadingItem(test, m, value, unit, temp);
        }


    }

    [JsonObject]
    public class Reading
    {
        [JsonIgnore]
        public const byte ASCII_STX     = 0x02;
        [JsonIgnore]
        public const byte ASCII_ETX     = 0x03;
        [JsonIgnore]
        public const byte ASCII_ETB     = 0x17;
        [JsonIgnore]
        public const byte ASCII_US      = 0x1F;
        [JsonIgnore]
        public const byte ASCII_RS      = 0x1E;
        [JsonIgnore]
        public const byte ASCII_BLANK   = 0x20;

        private static readonly ILog log = LogManager.GetLogger(typeof(Reading));

        public DateTime date { get; set; }
        public string id { get; set; }
        public string multiName { get; set; } = "";
        public ConcurrentDictionary<string, ReadingItem> items = new ConcurrentDictionary<string, ReadingItem>();

        public string clientId { get; set; } = "";
        public string clientName { get; set; } = "";
        public string animalName { get; set; } = "";
        public int animalAge { get; set; } = 0;
        public string animalType { get; set; } = "";

        private static Object FileWriteLock = new Object();

        [JsonConstructor]
        public Reading(string id, DateTime date)
        {
            this.id = id;
            this.date = date;
        }

        public Reading(string id, DateTime date, string multiName)
        {
            this.id = id;
            this.date = date;
            this.multiName = multiName.Trim();
        }

        public void setMultiName(string name)
        {
            this.multiName = name;
        }

        public bool isMulti()
        {
            return multiName.Length == 0;
        }

        public bool AddItem(ReadingItem item)
        {
            return items.TryAdd(item.name, item);

        }

        override public string ToString()
        {
            return "id="+id + " date=" + date.ToString("yyyyMMdd") + " time=" + date.ToString("HH:mm") + " multi=" + multiName;
        }

        public string GetTitle()
        {
            return date.ToString("yyyy-MM-dd") + "   " + date.ToString("HH:mm") + "    " + id;
        }

        public string GetUUID()
        {
            return date.ToString("yyyyMMdd") + "-" + date.ToString("HHmm") + "-" + id;
        }


        public string ToJSON()
        {
            string s = JsonConvert.SerializeObject(this, Formatting.Indented);
            return s;
        }

        public static Reading FromJSON(string json)
        {
            Reading rd;
            rd = JsonConvert.DeserializeObject<Reading>(json);
            return rd;
        }

        public static Reading fromJSONFile(string fname)
        {
            string s = "";
            lock (FileWriteLock)
            {
                s = File.ReadAllText(fname);
            }
            Reading rd = Reading.FromJSON(s);
            return rd;
        }


        public void Save(string path)
        {

            string fname = id.Trim() + @"-" + date.ToString("yyyyMMdd") + @"-" + date.ToString("HHmm")+@".json";
            fname = path + @"\" + fname;
            try
            {
                SaveToFile(fname, ToJSON());
                log.Debug("Reading Saved: file = " + fname+ "  reading=" + this);
            } catch (Exception ex)
            {
                log.Error("Error saving reading to file. " + ex.Message);
            }

        }

        public static void SaveToFile(string fname, string content)
        {
            lock (FileWriteLock)
            {
                System.IO.File.WriteAllText(fname, content);
            }

        }

        /*
        public static Reading GetFake(int randomSeed)
        {
            Random r = new Random(randomSeed);
            Reading rd;
            ReadingRanges ranges = ReadingRanges.createDefaults();
            string[] tests = { "AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIII", "JJJJ", "KKKK" };
            int i;

            ranges.ranges.Keys.CopyTo(tests,0);
            
            rd = new Reading(r.Next(99, 99999).ToString().PadLeft(5,'0'), DateTime.Now.AddMinutes((r.NextDouble()*3000)-1000), "multi1");
            for (i = 0; i < 5; i++)
            {
                string t = tests[r.Next(0, 10)];
                Range ra; // = new Range(0,0,"x");
                ranges.ranges.TryGetValue(t, out ra);
                if (ra == null) ra = new Range();
                float v = ((r.Next(300) - 100) / 300) * (ra.max - ra.min) + ra.min;
                if (!rd.AddItem(ReadingItem.getFake(t,v,ra.min, ra.max,ra.unit,r.Next(20,37))))
                    log.Debug("Failed to add item");
            }

            log.Debug("created items=" + rd.items.Count);
            return rd;
        }

        public static void CreateFakeReadings(int c, string folder)
        {
            Reading rd;

            while (c > 0)
            {
                rd = Reading.GetFake(c * 13);
                c--;
                rd.Save(folder);
            }
        }
        */

        private static string b2S(byte[] b, int indexFrom, int count)
        {
            byte[] b1 = new byte[count];
            Array.Copy(b, indexFrom, b1, 0, count);
            return System.Text.Encoding.ASCII.GetString(b1);
        }

        public static List<Reading> parse(byte[] msg)
        {
            List<Reading> lrd = new List<Reading>();
            Reading rd;

            int off = 0;
            string s;
            float v;

            log.Debug("Parsing reading...");

            // STX is at offset 0

            // parse header
            off = 0;

            // get date + time
            s = b2S(msg, off + 1, 20);
            
            DateTime d = DateTime.ParseExact(s, "yy/MM/dd     HH:mm  ", CultureInfo.InvariantCulture);
            //DateTime d = new DateTime(2000 + msg[off+1]-48 * 10 + msg[off+2], msg[off+4] * 10 + msg[off+5], msg[off+7] * 10 + msg[off+8], msg[off+14] * 10 + msg[off+15], msg[off+17] * 10 + msg[off+18], 0);

            // get reading ID
            s = b2S(msg, off+27, 10).Trim();

            rd = new Reading(s, d);

            // read measurements 
            off += 44; // move past reading header

            // check if it is an multi-reagent strip - if yes, set its name
            if (b2S(msg, off + 1, 6).Trim().StartsWith("MULTI"))
                rd.setMultiName(b2S(msg, off + 8, 10));

            off += 22; // move past strip header

            // read items
            while (off+1 < msg.Length && msg[off+1] != Reading.ASCII_ETB && msg[off+1] != Reading.ASCII_ETX)
            {
                string name = b2S(msg, off + 1, 5).Trim();
                s = b2S(msg, off + 7, 3);
                switch (s)
                {
                    case "UND":
                    case "OVE":
                    case "CAN":
                    case "CAL":
                        char t = (char)msg[off + 20];
                        rd.AddItem(new ReadingItem(name, b2S(msg,off+7,13),t));
                        break;
                    default:
                        char am = (char)msg[off + 7];
                        s = b2S(msg, off + 8, 5);
                        try
                        {
                            v = float.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
                        } catch (FormatException)
                        {
                            log.Warn("Error while parsing value '" + s + "' as number.");
                            v = -9999;
                        } 
                        string unit = b2S(msg, off + 14, 6).Trim();
                        t = (char)msg[off + 20];
                        rd.AddItem(new ReadingItem(name, am, v, unit, t));
                        break;
                }
                off += 22;
                if (off+1 < msg.Length && msg[off+1] == Reading.ASCII_ETB)
                {
                    log.Info("ETB found");
                    off++;
                    if (off + 1 < msg.Length && msg[off + 1] == Reading.ASCII_STX)
                    {
                        log.Info("STX found");
                        off++;
                        // skip header (necessary data has been already read)
                        if (off+22 < msg.Length)
                            off += 22;
                        else
                        {
                            log.Error("Incomplete message.");
                            off = msg.Length;
                        }
                    }
                }
                    
            }

            lrd.Add(rd);

            return lrd;

        }



    }
}