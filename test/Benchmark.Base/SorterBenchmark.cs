using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Threading.Tasks;

namespace Benchmark.Base
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [RPlotExporter]
    public class SorterBenchmark : BaseSorterBenchmark
    {
        [Params(ArrayType.Random, ArrayType.Asc, ArrayType.Desc)]
        public ArrayType Type { get; set; }

        [Params(1000, 10000, 10000, 1000000, 10000000)]
        public int Length { get; set; }

        [GlobalSetup]
        public void GlobalSetup() => GenerateRandomly(Length, Type);

        [Benchmark(Baseline = true)]
        public async Task SystemArraySorter() => _ = await new ParallelSorting.Systems.ArraySorter().Sort(RawArray);

        [Benchmark]
        public async Task SystemLinqSorter() => _ = await new ParallelSorting.Systems.LinqSorter().Sort(RawArray);

        [Benchmark]
        public async Task SerialMergeSorter() => _ = await new ParallelSorting.Serials.MergeSorter().Sort(RawArray);

        [Benchmark]
        public async Task SerialQuickSorter() => _ = await new ParallelSorting.Serials.QuickSorter().Sort(RawArray);

        [Benchmark]
        public async Task ParallelMergeSorter() => _ = await new ParallelSorting.Parallels.MergeSorter().Sort(RawArray);

        [Benchmark]
        public async Task ParallelQuickSorter() => _ = await new ParallelSorting.Parallels.QuickSorter().Sort(RawArray);
    }
}
