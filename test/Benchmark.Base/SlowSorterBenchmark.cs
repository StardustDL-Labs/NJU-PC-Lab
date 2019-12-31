using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Threading.Tasks;

namespace Benchmark.Base
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [RPlotExporter]
    public class SlowSorterBenchmark : BaseSorterBenchmark
    {
        [Params(ArrayType.Random, ArrayType.Ordered)]
        public ArrayType Type { get; set; }

        [Params(100, 1000)]
        public int Length { get; set; }

        [GlobalSetup]
        public void GlobalSetup() => GenerateRandomly(Length, Type);

        [Benchmark(Baseline = true)]
        public async Task SystemArraySorter() => _ = await new ParallelSorting.Systems.ArraySorter().Sort(RawArray);

        [Benchmark]
        public async Task SerialEnumSorter() => _ = await new ParallelSorting.Serials.EnumSorter().Sort(RawArray);

        [Benchmark]
        public async Task ParallelEnumSorter() => _ = await new ParallelSorting.Parallels.EnumSorter().Sort(RawArray);
    }
}
