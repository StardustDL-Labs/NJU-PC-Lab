using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

namespace Benchmark.Base
{
    [BenchmarkCategory("Sub")]
    public class SubSorterBenchmark : BaseSorterBenchmark
    {
        [Params(ArrayType.Random, ArrayType.Ordered)]
        public ArrayType Type { get; set; }

        [Params(100000)]
        public int Length { get; set; }

        [Params(100, 1000)]
        public int RecursiveBound { get; set; }

        [GlobalSetup]
        public void GlobalSetup() => GenerateRandomly(Length, Type);

        [Benchmark(Baseline = true)]
        [BenchmarkCategory(nameof(SorterCategories.System), nameof(SorterAlgorithms.Array))]
        public async Task SystemArraySorter() => _ = await new ParallelSorting.Systems.ArraySorter().Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Merge))]
        public async Task SerialMergeSorterWithInsertSort() => _ = await new ParallelSorting.Serials.MergeSorter(RecursiveBound, ParallelSorting.Serials.InsertSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Quick))]
        public async Task SerialQuickSorterWithInsertSort() => _ = await new ParallelSorting.Serials.QuickSorter(RecursiveBound, ParallelSorting.Serials.InsertSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Merge))]
        public async Task ParallelMergeSorterWithInsertSort() => _ = await new ParallelSorting.Parallels.MergeSorter(RecursiveBound, ParallelSorting.Serials.InsertSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Quick))]
        public async Task ParallelQuickSorterWithInsertSort() => _ = await new ParallelSorting.Parallels.QuickSorter(RecursiveBound, ParallelSorting.Serials.InsertSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Merge))]
        public async Task SerialMergeSorterWithShellSort() => _ = await new ParallelSorting.Serials.MergeSorter(RecursiveBound, ParallelSorting.Serials.ShellSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Quick))]
        public async Task SerialQuickSorterWithShellSort() => _ = await new ParallelSorting.Serials.QuickSorter(RecursiveBound, ParallelSorting.Serials.ShellSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Merge))]
        public async Task ParallelMergeSorterWithShellSort() => _ = await new ParallelSorting.Parallels.MergeSorter(RecursiveBound, ParallelSorting.Serials.ShellSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Quick))]
        public async Task ParallelQuickSorterWithShellSort() => _ = await new ParallelSorting.Parallels.QuickSorter(RecursiveBound, ParallelSorting.Serials.ShellSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Merge))]
        public async Task SerialMergeSorterWithHeapSort() => _ = await new ParallelSorting.Serials.MergeSorter(RecursiveBound, ParallelSorting.Serials.HeapSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Serial), nameof(SorterAlgorithms.Quick))]
        public async Task SerialQuickSorterWithHeapSort() => _ = await new ParallelSorting.Serials.QuickSorter(RecursiveBound, ParallelSorting.Serials.HeapSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Merge))]
        public async Task ParallelMergeSorterWithHeapSort() => _ = await new ParallelSorting.Parallels.MergeSorter(RecursiveBound, ParallelSorting.Serials.HeapSorter.Sort).Sort(RawArray);

        [Benchmark]
        [BenchmarkCategory(nameof(SorterCategories.Parallel), nameof(SorterAlgorithms.Quick))]
        public async Task ParallelQuickSorterWithHeapSort() => _ = await new ParallelSorting.Parallels.QuickSorter(RecursiveBound, ParallelSorting.Serials.HeapSorter.Sort).Sort(RawArray);
    }
}
