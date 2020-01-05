using System;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class HeapSorter : ISorter
    {
        public static void Sort(Memory<int> arr)
        {
            static void heapify(Span<int> span)
            {
                int pa = 0, id;
                while (true)
                {
                    id = pa;
                    int l = pa * 2 + 1, r = l + 1;
                    if (l < span.Length)
                    {
                        if (span[l] > span[id])
                        {
                            id = l;
                        }
                        if (r < span.Length && span[r] > span[id])
                        {
                            id = r;
                        }
                    }
                    if (id == pa)
                        break;
                    Utils.Swap(span, pa, id);
                    pa = id;
                }
            }
            Span<int> span = arr.Span;
            for (int i = span.Length / 2; i >= 0; i--)
            {
                heapify(span[i..]);
            }
            for (int i = span.Length - 1; i >= 1; i--)
            {
                Utils.Swap(span, 0, i);
                heapify(span[0..i]);
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
