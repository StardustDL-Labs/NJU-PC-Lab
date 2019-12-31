using System;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class InsertSorter : ISorter
    {
        public static void InsertSort(Memory<int> arr)
        {
            Span<int> span = arr.Span;
            for (int i = 1; i < span.Length; i++)
            {
                int v = span[i];
                int pos = span[..i].BinarySearch(v);
                if (pos < 0) pos = ~pos;
                for (int j = i; j > pos; j--)
                    span[j] = span[j - 1];
                span[pos] = v;
            }
        }

        public Task<Memory<int>> Sort(ReadOnlyMemory<int> seq)
        {
            Memory<int> result = new Memory<int>(new int[seq.Length]);
            seq.CopyTo(result);
            InsertSort(result);
            return Task.FromResult(result);
        }
    }
}
