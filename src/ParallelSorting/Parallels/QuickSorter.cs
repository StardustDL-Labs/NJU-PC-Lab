using System;
using System.Threading.Tasks;

namespace ParallelSorting.Parallels
{
    public class QuickSorter : ISorter
    {
        public Task<int[]> Sort(int[] seq)
        {
            static int partition(int[] arr, int l, int r)
            {
                int h = l, t = r - 1, key = arr[h];
                while (h < t)
                {
                    while (h < t && arr[t] > key) t--;
                    arr[h] = arr[t];
                    while (h < t && arr[h] <= key) h++;
                    arr[t] = arr[h];
                }
                arr[h] = key;
                return h;
            }
            static void inner(int[] arr, int l, int r, Random random)
            {
                if (r - l <= 1) return;

                int k = random.Next(l, r);
                Utils.Swap(arr, l, k);

                int p = partition(arr, l, r);

                Parallel.Invoke(() => inner(arr, l, p, random), () => inner(arr, p + 1, r, random));
            }
            int[] result = new int[seq.Length];
            seq.CopyTo(result, 0);
            inner(result, 0, result.Length, new Random());
            return Task.FromResult(result);
        }
    }
}
