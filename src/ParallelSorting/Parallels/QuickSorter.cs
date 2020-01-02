using System;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class QuickSorter : ISorter
    {
        private static int RecursiveBound => 200;

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            static void inner(Memory<int> arr, Random random)
            {
                if (arr.Length <= 1) return;
                if (arr.Length <= RecursiveBound)
                {
                    Serials.ShellSorter.Sort(arr);
                    return;
                }

                int k = random.Next(arr.Length);
                Utils.Swap(arr, 0, k);

                int p = Serials.QuickSorter.Partition(arr);

                Parallel.Invoke(
                    () => inner(arr[..p], random),
                    () => inner(arr[(p + 1)..], random));
            }
            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            inner(result, new Random());
            return Task.FromResult(result);
        }
    }
}
