using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class MergeSorter : ISorter
    {
        public const int RecursiveBound = 50;

        public static void Merge(ReadOnlyMemory<int> a, ReadOnlyMemory<int> b, Memory<int> result, Memory<int> temp)
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

        public Task<Memory<int>> Sort(ReadOnlyMemory<int> seq)
        {
            static void inner(Memory<int> arr, Memory<int> temp)
            {
                if (arr.Length <= 1) return;
                if (arr.Length <= RecursiveBound)
                {
                    InsertSorter.InsertSort(arr);
                    return;
                }

                int mid = arr.Length / 2;
                inner(arr[..mid], temp[..mid]);
                inner(arr[mid..], temp[mid..]);
                Merge(arr[..mid], arr[mid..], arr, temp);
            }

            var result = new Memory<int>(new int[seq.Length]);
            seq.CopyTo(result);
            var temp = new Memory<int>(new int[seq.Length]);
            inner(result, temp);
            return Task.FromResult(result);
        }
    }
}
