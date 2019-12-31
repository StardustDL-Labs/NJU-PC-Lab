using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelSorting
{
    public static class Utils
    {
        public static void Swap<T>(T[] arr, int i, int j)
        {
            T x = arr[i];
            arr[i] = arr[j];
            arr[j] = x;
        }

        public static T[] MemoryToArray<T>(Memory<T> memory)
        {
            T[] copy = new T[memory.Length];
            memory.CopyTo(new Memory<T>(copy));
            return copy;
        }

        public static T[] MemoryToArray<T>(ReadOnlyMemory<T> memory)
        {
            T[] copy = new T[memory.Length];
            memory.CopyTo(new Memory<T>(copy));
            return copy;
        }

        public static void Swap<T>(Span<T> arr, int i, int j)
        {
            T x = arr[i];
            arr[i] = arr[j];
            arr[j] = x;
        }

        public static void Swap<T>(Memory<T> arr, int i, int j) => Swap(arr.Span, i, j);

        public static void FillDistinct(int[] arr, Func<int> generator)
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

        public static void FillDistinctRandom(int[] arr)
        {
            Random rand = new Random();
            FillDistinct(arr, () => rand.Next());
        }
    }
}
