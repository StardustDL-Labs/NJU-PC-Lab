using System;
using System.Threading.Tasks;

namespace ParallelSorting.Systems
{
    public class ArraySorter : ISorter
    {
        public Task<int[]> Sort(int[] seq)
        {
            int[] result = new int[seq.Length];
            seq.CopyTo(result, 0);
            Array.Sort(result);
            return Task.FromResult(result);
        }
    }
}
