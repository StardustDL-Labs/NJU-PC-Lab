using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Benchmark.Base
{
    class Program
    {
        static string[] SplitName(string name)
        {
            StringBuilder sb = new StringBuilder();
            List<string> result = new List<string>();
            foreach (char c in name)
            {
                if (char.IsUpper(c))
                {
                    if (sb.Length > 0)
                    {
                        result.Add(sb.ToString());
                        _ = sb.Clear().Append(c);
                    }
                }
                else
                {
                    _ = sb.Append(c);
                }
            }
            if (sb.Length > 0)
            {
                result.Add(sb.ToString());
            }
            return result.ToArray();
        }

        static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .With(ConfigOptions.DisableOptimizationsValidator)
                .With(new TagColumn("Kind", name => SplitName(name)[0]))
                .With(new TagColumn("Method", name => SplitName(name)[1]));
            _ = BenchmarkRunner.Run<SorterBenchmark>(config);
            _ = BenchmarkRunner.Run<SlowSorterBenchmark>(config);
        }
    }
}
