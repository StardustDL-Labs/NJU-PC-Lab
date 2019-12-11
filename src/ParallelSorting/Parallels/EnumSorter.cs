using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class EnumSorter : ISorter
    {
        public Task<int[]> Sort(int[] seq)
        {
            int[] result = new int[seq.Length];
            Parallel.ForEach(seq, val => result[seq.Count(x => x < val)] = val);
            return Task.FromResult(result);
        }
    }
}
