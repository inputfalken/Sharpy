using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataGenerator.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Types.Tests {
    [TestClass()]
    public class FilterTests {
        [TestMethod()]
        public void RepeatedDataTestString() {
            var list = new List<string>() { "bob", "bob", "John", "doe", "doe", "doe" };
            var expected = new List<string>() { "bob", "John", "doe" };

            Assert.IsTrue(Filter.RepeatedData(list).SequenceEqual(expected));
        }

        [TestMethod()]
        public void RepeatedDataTestInt() {
            var ints = new List<int>() { 1, 2, 3, 4, 65, 6, 7, 7, 8, 8, 9, 10, 10, 10, 10 };
            var expected = new List<int>() { 1, 2, 3, 4, 65, 6, 7, 8, 9, 10, };
            Assert.IsTrue(Filter.RepeatedData(ints).SequenceEqual(expected));
        }
    }
}