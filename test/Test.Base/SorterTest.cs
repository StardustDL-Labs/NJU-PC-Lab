using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParallelSorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Base
{
    [TestClass]
    public class SorterTest
    {
        private int[] RawArray { get; set; }

        private int[] OrderedArray { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RawArray = new int[10000];
            Utils.FillDistinctRandom(RawArray);
            OrderedArray = RawArray.OrderBy(x => x).ToArray();
        }


        [DataRow(typeof(ParallelSorting.Serials.QuickSorter))]
        [DataRow(typeof(ParallelSorting.Serials.EnumSorter))]
        [DataRow(typeof(ParallelSorting.Serials.MergeSorter))]
        [DataRow(typeof(ParallelSorting.Serials.InsertSorter))]
        [DataRow(typeof(ParallelSorting.Serials.SelectSorter))]
        [DataRow(typeof(ParallelSorting.Parallels.QuickSorter))]
        [DataRow(typeof(ParallelSorting.Parallels.EnumSorter))]
        [DataRow(typeof(ParallelSorting.Parallels.MergeSorter))]
        [DataRow(typeof(ParallelSorting.Systems.ArraySorter))]
        [DataRow(typeof(ParallelSorting.Systems.LinqSorter))]
        [TestMethod]
        public async Task CorrectSorter(Type type)
        {
            ISorter sorter = Activator.CreateInstance(type) as ISorter;
            var result = await sorter.Sort(RawArray);
            CollectionAssert.AreEqual(OrderedArray, Utils.MemoryToArray(result));
        }
    }
}
