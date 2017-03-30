using System;
using System.IO;
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

        [JsonProperty]
        public string name { get; set; }

        [JsonProperty]
        public char abnormalMark { get; set; }

        [JsonProperty]
        public float value { get; set; }

        [JsonProperty]
        public string unit { get; set; }

        [JsonProperty]
        public int temp { get; set; }


        public ReadingItem(string name, char mark, float value, string unit, int temp)
        {
            this.name = name;
            this.abnormalMark = mark;
            this.value = value;
            this.unit = unit;
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
                m = (char)0x1F;
            else if (value > min)
                m = (char)0x1E;

            return new ReadingItem(test, m, value, unit, temp);
        }


    }

    [JsonObject]
    public class Reading
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Reading));

        [JsonProperty]
        public DateTime date { get; set; }

        [JsonProperty]
        public string id { get; set; }

        [JsonProperty]
        public string multiName { get; set; }

        [JsonProperty]
        public ConcurrentDictionary<string, ReadingItem> items = new ConcurrentDictionary<string, ReadingItem>();

        public Reading(string id, DateTime date, string multiName)
        {
            this.id = id;
            this.date = date;
            this.multiName = multiName.Trim();
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
            return id + " " + date.ToString("yyyyMMdd") + " " + date.ToString("HH:mm") + " " + multiName;
        }

        public string GetTitle()
        {
            return id + "    " + date.ToString("yyyy-MM-dd") + "    " + date.ToString("HH:mm");
        }

        public string GetUUID()
        {
            return id + "-" + date.ToString("yyyyMMdd") + "-" + date.ToString("HHmm");
        }


        public string ToJSON()
        {
            log.Debug("Items="+items.Count);
            string s = JsonConvert.SerializeObject(this, Formatting.Indented);
            return s;
        }

        public static Reading FromJSON(string json)
        {
            Reading rd;
            rd = JsonConvert.DeserializeObject<Reading>(json);
            return rd;
        }

        public void Save(string path)
        {
            string fname = id + @"-" + date.ToString("yyyyMMdd") + @"-" + date.ToString("HHmm")+@".json";
            SaveToFile(path + @"\" + fname, ToJSON());

        }

        public static async void SaveToFile(string fname, string content)
        {
            using (StreamWriter outputFile = new StreamWriter(fname))
            {
                await outputFile.WriteAsync(content);
            }

        }

        public static Reading GetFake(int randomSeed)
        {
            Random r = new Random(randomSeed);
            Reading rd;
            ReadingRanges ranges = ReadingRanges.createFake();
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



    }
}