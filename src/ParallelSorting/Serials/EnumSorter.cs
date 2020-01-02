using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class EnumSorter : ISorter
    {
        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            Memory<int> result = new int[seq.Length];
            ReadOnlySpan<int> sseq = seq.Span;
            Span<int> sresult = result.Span;
            foreach (var val in sseq)
            {
                int count = 0;
                foreach (var t in sseq)
                {
                    if (t < val)
                        count++;
                }

                sresult[count] = val;
            }
            return Task.FromResult(result);
        }
    }
}
