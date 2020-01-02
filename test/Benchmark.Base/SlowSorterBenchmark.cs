using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Threading.Tasks;

namespace Benchmark.Base
{
    [BenchmarkCategory("Slow")]
    public class SlowSorterBenchmark : BaseSorterBenchmark
    {
        [Params(ArrayType.Random, ArrayType.Ordered)]
        public ArrayType Type { get; set; }

        [Params(100, 1000)]
        public int Length { get; set; }

        [GlobalSetup]
        public void GlobalSetup() => GenerateRandomly(Length, Type);

        [Benchmark(Baseline = true)]
        [BenchmarkCategory(nameof(SorterCategories.System), nameof(SorterAlgorithms.Array))]
        public async Task SystemArraySorter() => _ = await new ParallelSorting.Systems.ArraySorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Enum))]
        public async Task SerialEnumSorter() => _ = await new ParallelSorting.Serials.EnumSorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Enum))]
        public async Task ParallelEnumSorter() => _ = await new ParallelSorting.Parallels.EnumSorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Insert))]
        public async Task SerialInsertSorter() => _ = await new ParallelSorting.Serials.InsertSorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Select))]
        public async Task SerialSelectSorter() => _ = await new ParallelSorting.Serials.SelectSorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Bubble))]
        public async Task SerialBubbleSorter() => _ = await new ParallelSorting.Serials.BubbleSorter().Sort(RawArray);
    }
}
