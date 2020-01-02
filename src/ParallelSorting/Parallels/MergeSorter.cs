using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class MergeSorter : ISorter
    {
        private static int RecursiveBound => 100;

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount * 2
            };

            static void inner(Memory<int> arr, Memory<int> temp, ParallelOptions options)
            {
                if (arr.Length <= 1) return;
                if (arr.Length <= RecursiveBound)
                {
                    Serials.InsertSorter.Sort(arr);
                    return;
                }

                int mid = arr.Length / 2;
                Parallel.Invoke(options, 
                    () => inner(arr[..mid], temp[..mid], options), 
                    () => inner(arr[mid..], temp[mid..], options));
                Serials.MergeSorter.Merge(arr[..mid], arr[mid..], arr, temp);
            }

            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            inner(result, new int[seq.Length], options);
            return Task.FromResult(result);
        }
    }
}
