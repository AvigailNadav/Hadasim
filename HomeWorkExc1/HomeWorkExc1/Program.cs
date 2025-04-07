using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeWorkExc1
{
    internal class Program
    {
        public static List<List<string>> ReadErrorsFromFile(string filePath, int numOfParts)
        {
            List<List<string>> AllPartsList = new List<List<string>>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The file does not exist");
                return AllPartsList;
            }
            List<string> allLines = File.ReadAllLines(filePath).ToList();
            int numOfLines = allLines.Count();
            int linesForAPart = (int)Math.Ceiling((double)numOfLines / numOfParts);

            for (int i = 0; i < numOfParts; i++)
            {
                List<string> partOfList = allLines.Skip(i * linesForAPart).Take(linesForAPart).ToList();
                AllPartsList.Add(partOfList);
            }
            return AllPartsList;
        }
        public static Dictionary<string, int> CountErrorsFrequencies(List<string> errorList)
        {
            Dictionary<string, int> countErrors = new Dictionary<string, int>();
            foreach (var error in errorList)
            {
                if (error.Contains("Error:"))
                {
                    string[] parts = error.Split(',');
                    if (parts.Length > 1)
                    {
                        string[] errorParts = parts[1].Split(':');
                        if (errorParts.Length > 1)
                        {
                            string errorCode = errorParts[1].Trim();
                            if (countErrors.TryGetValue(errorCode, out int count))
                            {
                                countErrors[errorCode] = count + 1;
                            }
                            else
                            {
                                countErrors[errorCode] = 1;
                            }
                        }
                    }
                }
            }
            return countErrors;
        }
        public static Dictionary<string, int> MergeDictionaries(List<Dictionary<string, int>> dictionaries)
        {
            Dictionary<string, int> mergedDictionaries = new Dictionary<string, int>();
            foreach (var dict in dictionaries)
            {
                foreach (var error in dict)
                {
                    if (mergedDictionaries.TryGetValue(error.Key, out int count))
                    {
                        mergedDictionaries[error.Key] = error.Value + count;
                    }
                    else
                    {
                        mergedDictionaries[error.Key] = error.Value;
                    }
                }
            }
            return mergedDictionaries;
        }
        public static List<KeyValuePair<string,int>> TopNErrors(Dictionary<string,int> mergedDictionaries,int n)
        {
            List<KeyValuePair<string, int>> topN = new List<KeyValuePair<string, int>>();
            topN = mergedDictionaries.OrderByDescending(e => e.Value).Take(n).ToList();
            return topN;
        }
        static void Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
            int numberOfParts = 10;

            List<List<string>> allParts = ReadErrorsFromFile(filePath, numberOfParts);

            List<Dictionary<string, int>> errorDitionaries = new List<Dictionary<string, int>>();
            foreach(var part in allParts)
            {
                errorDitionaries.Add(CountErrorsFrequencies(part));
            }

            Dictionary<string, int> mergedDictionary = MergeDictionaries(errorDitionaries);

            Console.WriteLine("Enter the number of top errors to display");
            int n= int.Parse(Console.ReadLine());
            List<KeyValuePair<string, int>> topNErrors = TopNErrors(mergedDictionary, n);
            foreach(var error in topNErrors)
            {
                Console.WriteLine($"Error: {error.Key} Count: {error.Value}");
            }
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}

