using ParallelSorting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Benchmark.FromFile
{
    class Program
    {
        static string FindAssetRandomFile()
        {
            string dir = Directory.GetCurrentDirectory();
            do
            {
                string target = Path.Join(dir, "assets", "random.txt");
                if (File.Exists(target))
                {
                    return target;
                }
                dir = Path.GetDirectoryName(dir);
            } while (!string.IsNullOrEmpty(dir));
            throw new FileNotFoundException("No assets/random.txt file.");
        }

        static int[] LoadList(string file) => File.ReadAllText(file).Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x.Trim())).ToArray();

        static TimeSpan MeasureTime(Action action)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            action();
            watch.Stop();
            return watch.Elapsed;
        }

        static TimeSpan UseSorter(ISorter sorter, int[] input, FileStream outputFile)
        {
            int[] result = Array.Empty<int>();
            var time = MeasureTime(() => result = sorter.Sort(input).Result);
            using StreamWriter sw = new StreamWriter(outputFile);
            foreach (var v in result)
            {
                sw.Write(v);
                sw.Write(' ');
            }
            return time;
        }

        static void Main(string[] args)
        {
            string randomFile = FindAssetRandomFile();
            Console.WriteLine($"Input file: {randomFile}");
            var arr = LoadList(randomFile);
            Console.WriteLine($"Data length: {arr.Length} integers");

            ISorter[] sorters = new ISorter[]
            {
                new ParallelSorting.Serials.EnumSorter(),
                new ParallelSorting.Serials.MergeSorter(),
                new ParallelSorting.Serials.QuickSorter(),
                new ParallelSorting.Parallels.EnumSorter(),
                new ParallelSorting.Parallels.MergeSorter(),
                new ParallelSorting.Parallels.QuickSorter()
            };

            for (int i = 0; i < sorters.Length; i++)
            {
                var outputFile = $"output{i}.txt";
                using FileStream fs = File.OpenWrite(Path.Join(Path.GetDirectoryName(randomFile), outputFile));
                var name = sorters[i].GetType().FullName;
                name = name.Substring(name.IndexOf('.'));
                var time = UseSorter(sorters[i], arr, fs);
                Console.WriteLine($"{name}: {time.TotalSeconds} s -> assets/{outputFile}");
            }
        }
    }
}
