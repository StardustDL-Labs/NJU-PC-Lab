using System;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class QuickSorter : ISorter
    {
        public const int DefaultRecursiveBound = 200;

        private int RecursiveBound { get; }

        private Action<Memory<int>> BasicSort { get; }

        public QuickSorter() : this(basicSort: null) { }

        public QuickSorter(int recursiveBound = DefaultRecursiveBound, Action<Memory<int>> basicSort = null)
        {
            RecursiveBound = recursiveBound;
            BasicSort = basicSort ?? Serials.ShellSorter.Sort;
        }

        private void InnerSort(Memory<int> arr, Random random)
        {
            if (arr.Length <= RecursiveBound)
            {
                BasicSort(arr);
                return;
            }

            int k = random.Next(arr.Length);
            Utils.Swap(arr, 0, k);

            int p = Serials.QuickSorter.Partition(arr);

            Parallel.Invoke(
                () => InnerSort(arr[..p], random),
                () => InnerSort(arr[(p + 1)..], random));
        }

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            InnerSort(result, new Random());
            return Task.FromResult(result);
        }
    }
}
