using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class MergeSorter : ISorter
    {
        public Task<int[]> Sort(int[] seq)
        {
            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount * 2
            };

            static void merge(int[] arr, int l, int mid, int r, int[] temp)
            {
                int i = l, j = mid, k = l;
                while (i < mid && j < r)
                {
                    temp[k++] = arr[i] <= arr[j] ? arr[i++] : arr[j++];
                }
                while (i < mid) temp[k++] = arr[i++];
                while (j < r) temp[k++] = arr[j++];
                Array.Copy(temp, l, arr, l, r - l);
            }

            static void inner(int[] arr, int l, int r, int[] temp, ParallelOptions options)
            {
                if (r - l <= 1) return;
                int mid = (l + r) / 2;
                Parallel.Invoke(options, () => inner(arr, l, mid, temp, options), () => inner(arr, mid, r, temp, options));
                merge(arr, l, mid, r, temp);
            }

            int[] result = new int[seq.Length];
            int[] temp = new int[seq.Length];
            seq.CopyTo(result, 0);
            inner(result, 0, result.Length, temp, options);
            return Task.FromResult(result);
        }
    }
}
