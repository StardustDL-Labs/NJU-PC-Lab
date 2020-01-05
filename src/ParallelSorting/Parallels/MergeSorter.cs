using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class MergeSorter : ISorter
    {
        public const int DefaultRecursiveBound = 200;

        private int RecursiveBound { get; }

        private Action<Memory<int>> BasicSort { get; }

        public MergeSorter() : this(basicSort: null) { }

        public MergeSorter(int recursiveBound = DefaultRecursiveBound, Action<Memory<int>> basicSort = null)
        {
            RecursiveBound = recursiveBound;
            BasicSort = basicSort ?? Serials.ShellSorter.Sort;
        }

        private void InnerSort(Memory<int> arr, Memory<int> temp)
        {
            if (arr.Length <= RecursiveBound)
            {
                BasicSort(arr);
                return;
            }

            int mid = arr.Length / 2;
            Parallel.Invoke(
                () => InnerSort(arr[..mid], temp[..mid]),
                () => InnerSort(arr[mid..], temp[mid..]));
            Serials.MergeSorter.Merge(arr[..mid], arr[mid..], arr, temp);
        }

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            InnerSort(result, new int[seq.Length]);
            return Task.FromResult(result);
        }
    }
}
