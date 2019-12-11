using ParallelSorting;
using System.Linq;
using System.Threading.Tasks;

namespace Benchmark.Base
{
    class SystemLinqSorter : ISorter
    {
        public Task<int[]> Sort(int[] seq) => Task.FromResult(seq.OrderBy(x => x).ToArray());
    }
}
