using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DataGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types {
    [TestClass()]
    public class FilterTests {
        [TestMethod()]
        public void RepeatedDataTestString() {
            var immutableList = new List<string>() { "bob", "bob", "John", "doe", "doe", "doe" }.ToImmutableList();
            var expected = new List<string>() { "bob", "John", "doe" }.ToImmutableList();

            Assert.IsTrue(Filter.RepeatedData(immutableList).SequenceEqual(expected));
        }

        [TestMethod()]
        public void RepeatedDataTestInt() {
            var ints = new List<int>() { 1, 2, 3, 4, 65, 6, 7, 7, 8, 8, 9, 10, 10, 10, 10 }.ToImmutableList();
            var expected = new List<int>() { 1, 2, 3, 4, 65, 6, 7, 8, 9, 10, }.ToImmutableList();
            Assert.IsTrue(Filter.RepeatedData(ints).SequenceEqual(expected));
        }
    }
}