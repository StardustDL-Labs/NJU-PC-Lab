using System;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class QuickSorter : ISorter
    {
        private static int RecursiveBound => 100;

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount * 2
            };
            static void inner(Memory<int> arr, Random random, ParallelOptions options)
            {
                if (arr.Length <= 1) return;
                if (arr.Length <= RecursiveBound)
                {
                    Serials.InsertSorter.Sort(arr);
                    return;
                }

                int k = random.Next(arr.Length);
                Utils.Swap(arr, 0, k);

                int p = Serials.QuickSorter.Partition(arr);

                Parallel.Invoke(options,
                    () => inner(arr[..p], random, options),
                    () => inner(arr[(p + 1)..], random, options));
            }
            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            inner(result, new Random(), options);
            return Task.FromResult(result);
        }
    }
}
