using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class MergeSorter : ISorter
    {
        private static int RecursiveBound => 100;

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

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            static void inner(Memory<int> arr, Memory<int> temp)
            {
                if (arr.Length <= 1) return;
                if (arr.Length <= RecursiveBound)
                {
                    ShellSorter.Sort(arr);
                    return;
                }

                int mid = arr.Length / 2;
                inner(arr[..mid], temp[..mid]);
                inner(arr[mid..], temp[mid..]);
                Merge(arr[..mid], arr[mid..], arr, temp);
            }

            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            inner(result, new int[seq.Length]);
            return Task.FromResult(result);
        }
    }
}
