using System;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class SelectSorter : ISorter
    {
        internal static void Sort(Memory<int> arr)
        {
            Span<int> span = arr.Span;
            for (int i = 0; i < span.Length; i++)
            {
                int id = i;
                for (int j = i + 1; j < span.Length; j++)
                {
                    if (span[j] < span[id])
                        id = j;
                }
                Utils.Swap(span, i, id);
            }
        }

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            Sort(result);
            return Task.FromResult(result);
        }
    }
}
