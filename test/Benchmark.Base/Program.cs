using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Linq;

namespace Benchmark.Base
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = DefaultConfig.Instance.With(ConfigOptions.DisableOptimizationsValidator);
            _ = BenchmarkRunner.Run<SorterBenchmark>(config);
            _ = BenchmarkRunner.Run<SlowSorterBenchmark>(config);
        }
    }
}
