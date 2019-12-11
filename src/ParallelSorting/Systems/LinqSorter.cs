using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting.Systems
{
    public class LinqSorter : ISorter
    {
        public Task<int[]> Sort(int[] seq) => Task.FromResult(seq.OrderBy(x => x).ToArray());
    }
}
