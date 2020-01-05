using System;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class ShellSorter : ISorter
    {
        public static void Sort(Memory<int> arr)
        {
            Span<int> span = arr.Span;
            for (int gap = span.Length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < span.Length; i++)
                {
                    int target = span[i], j = i - gap;
                    for (; j >= 0 && target < span[j]; j -= gap)
                    {
                        span[j + gap] = span[j];
                    }
                    span[j + gap] = target;
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
