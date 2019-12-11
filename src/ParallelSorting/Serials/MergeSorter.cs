using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class MergeSorter : ISorter
    {
        public Task<int[]> Sort(int[] seq)
        {
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

            static void inner(int[] arr, int l, int r, int[] temp)
            {
                if (r - l <= 1) return;
                int mid = (l + r) / 2;
                inner(arr, l, mid, temp);
                inner(arr, mid, r, temp);
                merge(arr, l, mid, r, temp);
            }

            int[] result = new int[seq.Length];
            int[] temp = new int[seq.Length];
            seq.CopyTo(result, 0);
            inner(result, 0, result.Length, temp);
            return Task.FromResult(result);
        }
    }
}
