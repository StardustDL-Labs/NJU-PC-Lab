using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class MergeSorter : ISorter
    {
        private static int RecursiveBound => 200;

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            static void inner(Memory<int> arr, Memory<int> temp)
            {
                if (arr.Length <= 1) return;
                if (arr.Length <= RecursiveBound)
                {
                    Serials.ShellSorter.Sort(arr);
                    return;
                }

                int mid = arr.Length / 2;
                Parallel.Invoke(
                    () => inner(arr[..mid], temp[..mid]),
                    () => inner(arr[mid..], temp[mid..]));
                Serials.MergeSorter.Merge(arr[..mid], arr[mid..], arr, temp);
            }

            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            inner(result, new int[seq.Length]);
            return Task.FromResult(result);
        }
    }
}
