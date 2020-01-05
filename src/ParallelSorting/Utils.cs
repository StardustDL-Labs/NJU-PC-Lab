using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelSorting
{
    public static class Utils
    {
        public static void Swap<T>(Span<T> arr, int i, int j)
        {
            if (i == j) return;
            T x = arr[i];
            arr[i] = arr[j];
            arr[j] = x;
        }

        public static void Swap<T>(Memory<T> arr, int i, int j) => Swap(arr.Span, i, j);

        public static T[] MemoryToArray<T>(in Memory<T> memory)
        {
            T[] copy = new T[memory.Length];
            memory.CopyTo(copy);
            return copy;
        }

        public static T[] MemoryToArray<T>(in ReadOnlyMemory<T> memory)
        {
            T[] copy = new T[memory.Length];
            memory.CopyTo(copy);
            return copy;
        }

        public static void FillDistinct(Span<int> arr, Func<int> generator)
        {
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                int val;
                do
                {
                    val = generator();
                } while (set.Contains(val));
                arr[i] = val;
                _ = set.Add(val);
            }
        }

        public static void FillDistinctRandom(Span<int> arr)
        {
            Random rand = new Random();
            FillDistinct(arr, () => rand.Next());
        }      
    }
}
