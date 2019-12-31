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
                case ArrayType.Ordered:
                    Array.Sort(RawArray);
                    break;
            }
        }
    }
}
