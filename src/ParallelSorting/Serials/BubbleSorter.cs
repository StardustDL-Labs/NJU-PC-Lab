using System;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class BubbleSorter : ISorter
    {
        internal static void Sort(Memory<int> arr)
        {
            Span<int> span = arr.Span;
            for (int i = 0; i < span.Length; i++)
            {
                for (int j = span.Length - 1; j > i; j--)
                {
                    if (span[j - 1] > span[j])
                        Utils.Swap(span, j - 1, j);
                }
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
