using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting.Serials
{
    public class QuickSorter : ISorter
    {
        internal static int Partition(Memory<int> arr)
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

        public const int DefaultRecursiveBound = 100;

        private int RecursiveBound { get; }

        private Action<Memory<int>> BasicSort { get; }

        public QuickSorter() : this(basicSort: null) { }

        public QuickSorter(int recursiveBound = DefaultRecursiveBound, Action<Memory<int>> basicSort = null)
        {
            RecursiveBound = recursiveBound;
            BasicSort = basicSort ?? ShellSorter.Sort;
        }

        private void InnerSort(Memory<int> arr, Random random)
        {
            if (arr.Length <= RecursiveBound)
            {
                BasicSort(arr);
                return;
            }

            int k = random.Next(arr.Length);
            Utils.Swap(arr, 0, k);

            int p = Partition(arr);
            InnerSort(arr[..p], random);
            InnerSort(arr[(p + 1)..], random);
        }

        public Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq)
        {
            Memory<int> result = new int[seq.Length];
            seq.CopyTo(result);
            InnerSort(result, new Random());
            return Task.FromResult(result);
        }
    }
}
