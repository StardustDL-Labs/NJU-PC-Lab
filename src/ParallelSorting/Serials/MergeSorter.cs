using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class MergeSorter : ISorter
    {
        internal static void Merge(in ReadOnlyMemory<int> a, in ReadOnlyMemory<int> b, Memory<int> result, Memory<int> temp)
        {
            ReadOnlySpan<int> sa = a.Span, sb = b.Span;
            Span<int> stemp = temp.Span;
            int i = 0, j = 0, k = 0;
            while (i < sa.Length && j < sb.Length)
            {
                stemp[k++] = sa[i] <= sb[j] ? sa[i++] : sb[j++];
            }
            while (i < sa.Length) stemp[k++] = sa[i++];
            while (j < sb.Length) stemp[k++] = sb[j++];
            temp.CopyTo(result);
        }

        public const int DefaultRecursiveBound = 100;

        private int RecursiveBound { get; }

        private Action<Memory<int>> BasicSort { get; }

        public MergeSorter() : this(basicSort: null) { }

        public MergeSorter(int recursiveBound = DefaultRecursiveBound, Action<Memory<int>> basicSort = null)
        {
            RecursiveBound = recursiveBound;
            BasicSort = basicSort ?? ShellSorter.Sort;
        }

        private void InnerSort(Memory<int> arr, Memory<int> temp)
        {
            if (arr.Length <= RecursiveBound)
            {
                BasicSort(arr);
                return;
            }

            int mid = arr.Length / 2;
            InnerSort(arr[..mid], temp[..mid]);
            InnerSort(arr[mid..], temp[mid..]);
            Merge(arr[..mid], arr[mid..], arr, temp);
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
