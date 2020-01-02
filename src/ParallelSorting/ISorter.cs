using System;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public interface ISorter
    {
        Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq);
    }
}