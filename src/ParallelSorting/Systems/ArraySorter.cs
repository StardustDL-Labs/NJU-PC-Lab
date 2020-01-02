using System;
using System.Threading.Tasks;

namespace ParallelSorting.Systems
{
    public class ArraySorter : ISorter
    {
        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            int[] result = Utils.MemoryToArray(seq);
            Array.Sort(result);
            return Task.FromResult<Memory<int>>(result);
        }
    }
}
