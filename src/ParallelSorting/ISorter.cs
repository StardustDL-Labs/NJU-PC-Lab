using System;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public interface ISorter
    {
        Task<int[]> Sort(int[] seq);
    }
}