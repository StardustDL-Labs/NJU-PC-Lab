using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Threading.Tasks;

namespace Benchmark.Base
{
    [BenchmarkCategory("Normal")]
    public class SorterBenchmark : BaseSorterBenchmark
    {
        [Params(ArrayType.Random, ArrayType.Ordered)]
        public ArrayType Type { get; set; }

        [Params(10000, 100000)]
        public int Length { get; set; }

        [GlobalSetup]
        public void GlobalSetup() => GenerateRandomly(Length, Type);

        [Benchmark(Baseline = true)]
        [BenchmarkCategory(nameof(SorterCategories.System), nameof(SorterAlgorithms.Array))]
        public async Task SystemArraySorter() => _ = await new ParallelSorting.Systems.ArraySorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.System), nameof(SorterAlgorithms.Linq))]
        public async Task SystemLinqSorter() => _ = await new ParallelSorting.Systems.LinqSorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Merge))]
        public async Task SerialMergeSorter() => _ = await new ParallelSorting.Serials.MergeSorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Quick))]
        public async Task SerialQuickSorter() => _ = await new ParallelSorting.Serials.QuickSorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Merge))]
        public async Task ParallelMergeSorter() => _ = await new ParallelSorting.Parallels.MergeSorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Quick))]
        public async Task ParallelQuickSorter() => _ = await new ParallelSorting.Parallels.QuickSorter().Sort(RawArray);
    }
}
