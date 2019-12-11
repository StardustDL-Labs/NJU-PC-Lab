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
            RawArray = new int[30000];
            Random rand = new Random();
            for (int i = 0; i < RawArray.Length; i++) RawArray[i] = rand.Next();
            OrderedArray = RawArray.OrderBy(x => x).ToArray();
        }

        [DataRow(typeof(ParallelSorting.Serials.QuickSorter))]
        [DataRow(typeof(ParallelSorting.Serials.EnumSorter))]
        [DataRow(typeof(ParallelSorting.Serials.MergeSorter))]
        [DataRow(typeof(ParallelSorting.Parallels.QuickSorter))]
        [DataRow(typeof(ParallelSorting.Parallels.EnumSorter))]
        [DataRow(typeof(ParallelSorting.Parallels.MergeSorter))]
        [TestMethod]
        public async Task CorrectSorter(Type type)
        {
            ISorter sorter = Activator.CreateInstance(type) as ISorter;
            var result = await sorter.Sort(RawArray);
            CollectionAssert.AreEqual(OrderedArray, result);
        }
    }
}
