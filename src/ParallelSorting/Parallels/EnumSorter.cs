using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class EnumSorter : ISorter
    {
        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            Memory<int> result = new int[seq.Length];
            int[] copy = Utils.MemoryToArray(seq);

            _ = Parallel.ForEach(copy,
                val =>
                {
                    int count = 0;
                    foreach (var t in copy)
                    {
                        if (t < val)
                            count++;
                    }
                    result.Span[count] = val;
                });

            return Task.FromResult(result);
        }
    }
}
