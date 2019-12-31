using ParallelSorting;
using System;

namespace Benchmark.Base
{
    public class BaseSorterBenchmark
    {
        protected int[] RawArray { get; set; }

        protected void GenerateRandomly(int length, ArrayType type)
        {
            RawArray = new int[length];
            Utils.FillDistinctRandom(RawArray);
            switch (type)
            {
                case ArrayType.Random:
                    break;
                case ArrayType.Asc:
                    Array.Sort(RawArray);
                    break;
                case ArrayType.Desc:
                    Array.Sort(RawArray);
                    Array.Reverse(RawArray);
                    break;
            }
        }
    }
}
