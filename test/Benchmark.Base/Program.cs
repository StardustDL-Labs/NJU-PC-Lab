using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

namespace Benchmark.Base
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Sorter>(DefaultConfig.Instance.With(ConfigOptions.DisableOptimizationsValidator));
        }
    }

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class Sorter
    {
        private int[] RawArray { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            RawArray = new int[30000];
            Random rand = new Random();
            for (int i = 0; i < RawArray.Length; i++) RawArray[i] = rand.Next();
        }

        [Benchmark(Baseline = true)]
        public async Task SystemArraySorter() => _ = await new SystemArraySorter().Sort(RawArray);

        [Benchmark]
        public async Task SystemLinqSorter() => _ = await new SystemLinqSorter().Sort(RawArray);

        [Benchmark]
        public async Task SerialMergeSorter() => _ = await new ParallelSorting.Serials.MergeSorter().Sort(RawArray);

        [Benchmark]
        public async Task SerialEnumSorter() => _ = await new ParallelSorting.Serials.EnumSorter().Sort(RawArray);

        [Benchmark]
        public async Task SerialQuickSorter() => _ = await new ParallelSorting.Serials.QuickSorter().Sort(RawArray);

        [Benchmark]
        public async Task ParallelMergeSorter() => _ = await new ParallelSorting.Parallels.MergeSorter().Sort(RawArray);

        [Benchmark]
        public async Task ParallelEnumSorter() => _ = await new ParallelSorting.Parallels.EnumSorter().Sort(RawArray);

        [Benchmark]
        public async Task ParallelQuickSorter() => _ = await new ParallelSorting.Parallels.QuickSorter().Sort(RawArray);
    }
}
