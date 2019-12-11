using System;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class EnumSorter : ISorter
    {
        public Task<int[]> Sort(int[] seq) => throw new NotImplementedException();
    }
}
