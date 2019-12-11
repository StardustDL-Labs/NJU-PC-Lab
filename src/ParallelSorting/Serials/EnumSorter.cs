using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class EnumSorter : ISorter
    {
        public Task<int[]> Sort(int[] seq)
        {
            int[] result = new int[seq.Length];
            foreach(var val in seq)
                result[seq.Count(x => x < val)] = val;
            return Task.FromResult(result);
        }
    }
}
