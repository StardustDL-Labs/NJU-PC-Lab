using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Columns;

namespace Benchmark.Base
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .With(ConfigOptions.JoinSummary)
                .With(CategoriesColumn.Default)
                .With(MemoryDiagnoser.Default)
                .With(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));
            _ = BenchmarkRunner.Run(typeof(Program).Assembly, config);
        }
    }
}
