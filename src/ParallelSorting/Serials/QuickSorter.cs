using System;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class QuickSorter : ISorter
    {
        public const int RecursiveBound = 50;

        public static int Partition(Memory<int> arr)
        {
            Span<int> sarr = arr.Span;
            int h = 0, t = sarr.Length - 1, key = sarr[h];
            while (h < t)
            {
                while (h < t && sarr[t] > key) t--;
                sarr[h] = sarr[t];
                while (h < t && sarr[h] <= key) h++;
                sarr[t] = sarr[h];
            }
            sarr[h] = key;
            return h;
        }

        public Task<Memory<int>> Sort(ReadOnlyMemory<int> seq)
        {
            static void inner(Memory<int> arr, Random random)
            {
                if (arr.Length <= 1) return;
                if (arr.Length <= RecursiveBound)
                {
                    InsertSorter.InsertSort(arr);
                    return;
                }

                int k = random.Next(arr.Length);
                Utils.Swap(arr, 0, k);

                int p = Partition(arr);
                inner(arr[..p], random);
                inner(arr[(p + 1)..], random);
            }
            Memory<int> result = new Memory<int>(new int[seq.Length]);
            seq.CopyTo(result);
            inner(result, new Random());
            return Task.FromResult(result);
        }
    }
}
