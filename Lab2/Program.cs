using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IList<string> words = new List<string>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nMENU OPTION");
                Console.WriteLine("1) Import Words from File");
                Console.WriteLine("2) Bubble Sort words");
                Console.WriteLine("3) LINQ Sort words");
                Console.WriteLine("4) Count distinct words");
                Console.WriteLine("5) Take first 10 words");
                Console.WriteLine("6) Reverse each word");
                Console.WriteLine("7) Words ending with 'a'");
                Console.WriteLine("8) Words starting with 'm'");
                Console.WriteLine("9) Words longer than 5 and contain 's'");
                Console.WriteLine("X) Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine() ?? "";

                try
                {
                    switch (option.ToUpper())
                    {
                        case "1":
                            words = ImportWords("Words.txt");
                            Console.WriteLine($"Words loaded: {words.Count}");
                            break;

                        case "2":
                            TimeSort(() => BubbleSort(words), "Bubble Sort");
                            break;

                        case "3":
                            TimeSort(() => LINQSort(words), "LINQ Sort");
                            break;

                        case "4":
                            Console.WriteLine($"Distinct words: {words.Distinct().Count()}");
                            break;

                        case "5":
                            words.Take(10).ToList().ForEach(Console.WriteLine);
                            break;

                        case "6":
                            words.Select(w => new string(w.Reverse().ToArray()))
                                 .ToList()
                                 .ForEach(Console.WriteLine);
                            break;

                        case "7":
                            var endA = words.Where(w => w.EndsWith("a")).ToList();
                            endA.ForEach(Console.WriteLine);
                            Console.WriteLine($"Count: {endA.Count}");
                            break;

                        case "8":
                            var startM = words.Where(w => w.StartsWith("m")).ToList();
                            startM.ForEach(Console.WriteLine);
                            Console.WriteLine($"Count: {startM.Count}");
                            break;

                        case "9":
                            var filtered = words.Where(w => w.Length > 5 && w.Contains("s")).ToList();
                            filtered.ForEach(Console.WriteLine);
                            Console.WriteLine($"Count: {filtered.Count}");
                            break;

                        case "X":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static IList<string> ImportWords(string path)
        {
            IList<string> words = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string? sentence = reader.ReadLine();
                    if (sentence != null)
                    {
                        words.Add(sentence);
                    }
                }
            }
            return words;
        }

        static IList<string> BubbleSort(IList<string> words)
        {
            List<string> sorted = new List<string>(words);

            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = 0; j < sorted.Count - i - 1; j++)
                {
                    if (string.Compare(sorted[j], sorted[j + 1]) > 0)
                    {
                        string temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }
            return sorted;
        }

        static IList<string> LINQSort(IList<string> words)
        {
            return words.OrderBy(w => w).ToList();
        }

        static void TimeSort(Func<IList<string>> sortMethod, string name)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sortMethod();
            sw.Stop();

            Console.WriteLine($"{name} took {sw.ElapsedMilliseconds} ms");
        }
    }
}
        

    