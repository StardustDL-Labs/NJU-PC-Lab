using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using ParallelSorting;
using System;
using System.Linq;
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

    public enum ArrayType
    {
        Random,
        Asc,
        Desc
    }

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class Sorter
    {
        private int[] RawArray { get; set; }

        [Params(ArrayType.Random, ArrayType.Asc, ArrayType.Desc)]
        public ArrayType Type { get; set; }

        [Params(100, 1000)]
        public int Length { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            RawArray = new int[Length];
            Utils.FillDistinctRandom(RawArray);
            switch (Type)
            {
                case ArrayType.Random:
                    break;
                case ArrayType.Asc:
                    Array.Sort(RawArray);
                    break;
                case ArrayType.Desc:
                    Array.Sort(RawArray);
                    Array.Reverse(RawArray);
                    break;
            }
        }

        [Benchmark(Baseline = true)]
        public async Task SystemArraySorter() => _ = await new ParallelSorting.Systems.ArraySorter().Sort(RawArray);

        [Benchmark]
        public async Task SystemLinqSorter() => _ = await new ParallelSorting.Systems.LinqSorter().Sort(RawArray);

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
