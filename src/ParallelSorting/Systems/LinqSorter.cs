using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting.Systems
{
    public class LinqSorter : ISorter
    {
        public Task<Memory<int>> Sort(ReadOnlyMemory<int> seq)
        {
            int[] result = Utils.MemoryToArray(seq);
            return Task.FromResult(new Memory<int>(result.OrderBy(x => x).ToArray()));
        }
    }
}
