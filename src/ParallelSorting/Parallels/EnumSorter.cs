using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class EnumSorter : ISorter
    {
        public Task<Memory<int>> Sort(ReadOnlyMemory<int> seq)
        {
            Memory<int> result = new Memory<int>(new int[seq.Length]);
            int[] copy = Utils.MemoryToArray(seq);

            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount * 2
            };
            _ = Parallel.ForEach(copy, options,
                val =>
                {
                    int count = 0;
                    foreach (var t in seq.Span)
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
