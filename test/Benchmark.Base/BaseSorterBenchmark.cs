using ParallelSorting;
using System;

namespace Benchmark.Base
{
    public class BaseSorterBenchmark
    {
        protected Memory<int> RawArray { get; set; }

        protected void GenerateRandomly(int length, ArrayType type)
        {
            var array = new int[length];
            Utils.FillDistinctRandom(new Span<int>(array));
            switch (type)
            {
                case ArrayType.Random:
                    break;
                case ArrayType.Ordered:
                    Array.Sort(array);
                    break;
            }
            RawArray = array;
        }
    }
}
