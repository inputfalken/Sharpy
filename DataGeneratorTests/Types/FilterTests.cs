using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DataGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types {
    [TestClass]
    public class FilterTests {
        [TestMethod]
        public void RepeatedDataTestString() {
            var testList = ImmutableList.Create("bob", "bob", "John", "doe", "doe", "doe");
            var expected = ImmutableList.Create("bob", "John", "doe");
            var result = Filter.RepeatedData(testList);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public void RepeatedDataTestInt() {
            var testList = ImmutableList.Create(1, 2, 3, 4, 65, 6, 7, 7, 8, 8, 9, 10, 10, 10, 10);
            var expected = ImmutableList.Create(1, 2, 3, 4, 65, 6, 7, 8, 9, 10);
            var result = Filter.RepeatedData(testList);
            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}