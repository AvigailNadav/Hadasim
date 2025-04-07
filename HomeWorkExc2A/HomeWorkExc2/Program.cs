using CsvHelper;
using CsvHelper.Configuration;
using Parquet;
using Parquet.Schema;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace HomeWorkExc2
{
    internal class Program
    {
        public static async Task<Dictionary<string, double>> ReadParquetData(string filePath)
        {
            Dictionary<string, double> verifiedData = new Dictionary<string, double>();
            using (Stream fileStream = File.OpenRead(filePath))
            {
                using (var parquetReader = await ParquetReader.CreateAsync(fileStream, new ParquetOptions()))
                {
                    var fields = parquetReader.Schema.GetDataFields();

                    var timestampField = fields.First(f => f.Name == "timestamp");
                    var meanValueField = fields.First(f => f.Name == "mean_value");

                    for (int i = 0; i < parquetReader.RowGroupCount; i++)
                    {
                        using (var groupReader = parquetReader.OpenRowGroupReader(i))
                        {
                            var timestamps = (DateTime?[])(await groupReader.ReadColumnAsync((DataField)timestampField)).Data;
                            var meanValues = (double?[])(await groupReader.ReadColumnAsync((DataField)meanValueField)).Data;


                            for (int j = 0; j < timestamps.Length; j++)
                            {
                                string key = timestamps[j].Value.ToString("dd/MM/yyyy HH:mm");
                                verifiedData[key] = meanValues[j].Value;
                            }
                        }
                    }
                }
            }
            return verifiedData;
        }
        public static List<Tuple<DateTime, double>> TestingData(string filePath)
        {
            List<Tuple<DateTime, double>> verifiedData = new List<Tuple<DateTime, double>>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The file does not exist");
            }
            Dictionary<string, int> rowCount = new Dictionary<string, int>();
            using (StreamReader reader = new StreamReader(filePath))

            using (CsvReader csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
            {
                csv.Read();
                csv.ReadHeader();
                string header = string.Join(",", csv.HeaderRecord);

                while (csv.Read())
                {
                    string timeStamp = csv.GetField<string>("timestamp");
                    string value = csv.GetField<string>("value");
                    if (!DateTime.TryParseExact(timeStamp, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        Console.WriteLine($"The date: {timeStamp} in row: {csv.Parser.RawRow} is not correct!");
                        continue;
                    }
                    string rowKey = $"{parsedDate:dd/MM/yyyy HH:mm}-{value}";
                    if (rowCount.ContainsKey(rowKey))
                    {
                        rowCount[rowKey]++;
                    }
                    else
                    {
                        rowCount[rowKey] = 1;
                        if (double.TryParse(value, out double parsedValue))
                        {
                            if (double.IsNaN(parsedValue))
                            {
                                continue;
                            }
                            DateTime realDate = DateTime.ParseExact(timeStamp, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                            verifiedData.Add(Tuple.Create(realDate, parsedValue));
                        }
                    }
                }
                Console.WriteLine("test ended");
                return verifiedData;
            }
        }
        public static void SplitDataByDate(List<Tuple<DateTime, double>> verifiedData, string folderPath)
        {
            Dictionary<DateTime, List<Tuple<DateTime, double>>> dailyData = new Dictionary<DateTime, List<Tuple<DateTime, double>>>();

            foreach (var row in verifiedData)
            {
                DateTime dt = row.Item1;
                if (dailyData.ContainsKey(dt.Date))
                {
                    dailyData[dt.Date].Add(Tuple.Create(dt, row.Item2));
                }
                else
                {
                    dailyData[dt.Date] = new List<Tuple<DateTime, double>>();
                    dailyData[dt.Date].Add(Tuple.Create(dt, row.Item2));
                }
            }
            int index = 1;
            foreach (var data in dailyData)
            {
                string filePath = Path.Combine(folderPath, $"File-{index}");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var row in data.Value)
                    {
                        writer.WriteLine($"{row.Item1.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},{row.Item2}");
                    }
                }
                index++;
            }
            Console.WriteLine("splitted successefully");
        }
        public static Dictionary<int, double> AvgPerHour(string filePath)
        {
            Dictionary<int, Tuple<double, int>> avgValues = new Dictionary<int, Tuple<double, int>>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The file does not exist");
            }
            foreach (string line in File.ReadLines(filePath))
            {
                string[] splitedData = line.Split(',');
                DateTime dateInRow = DateTime.ParseExact(splitedData[0], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                double valueInRowDouble = double.Parse(splitedData[1]);
                int hour = dateInRow.Hour;
                if (!avgValues.ContainsKey(hour))
                {
                    avgValues[hour] = Tuple.Create(0.0, 0);
                }
                double newSum = avgValues[hour].Item1 + valueInRowDouble;
                int count = avgValues[hour].Item2 + 1;
                avgValues[hour] = Tuple.Create(newSum, count);
            }
            Dictionary<int, double> avgPerHour = new Dictionary<int, double>();
            foreach (var hour in avgValues)
            {
                double avg = (hour.Value.Item1) / (hour.Value.Item2);
                avgPerHour[hour.Key] = avg;
            }
            return avgPerHour;
        }
        public static async Task Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "time_series.csv");
            List<Tuple<DateTime, double>> testedData = TestingData(filePath);
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DailyDataFolder");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("The folder was created!");
            }

            SplitDataByDate(testedData, folderPath);
            string[] files = Directory.GetFiles(folderPath);
            Dictionary<string, double> combinedDictionary = new Dictionary<string, double>();
            foreach (var file in files)
            {
                string firstLine = File.ReadLines(file).FirstOrDefault();
                string[] splitedFirstRow = firstLine.Split(',');
                string[] formats = { "MM/dd/yyyy hh:mm tt", "dd/MM/yyyy HH:mm" };
                DateTime dateInRow;
                if (!DateTime.TryParseExact(splitedFirstRow[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateInRow))
                {
                    continue;
                }
                string date = dateInRow.ToString("dd/MM/yyyy");
                Dictionary<int, double> avgHour = new Dictionary<int, double>();
                avgHour = AvgPerHour(file);
                string dateAndHour;
                foreach (var hour in avgHour)
                {
                    dateAndHour = date + " " + hour.Key + ":00";
                    combinedDictionary[dateAndHour] = hour.Value;
                }
            }
            foreach (var data in combinedDictionary)
            {
                Console.WriteLine($"Date {data.Key} avg: {data.Value}");
            }
            Console.WriteLine("printed all values");

            Console.WriteLine("Reading parquet file");
            string parquetFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "time_series (4).parquet");
            if (!File.Exists(parquetFilePath))
            {
                Console.WriteLine("The file does not exist");
            }
            var parquetData=await ReadParquetData(parquetFilePath);
            foreach (var row in parquetData)
            {
                Console.WriteLine($"Timestamp: {row.Key}, Mean value: {row.Value}");
            }
        }
    }
}