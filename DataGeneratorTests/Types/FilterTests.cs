using System.Collections.Generic;
using System.Linq;
using DataGen.Types;
using DataGen.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types {
    [TestClass]
    public class FilterTests {
        [TestMethod]
        public void RepeatedDataTestString() {
            IEnumerable<string> testList = new[] { "bob", "bob", "John", "doe", "doe", "doe" };
            IEnumerable<string> expected = new[] { "bob", "John", "doe" };
            var result = Filter<string>.RepeatedData(testList);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public void RepeatedDataTestInt() {
            IEnumerable<int> testList = new[] { 1, 2, 3, 4, 65, 6, 7, 7, 8, 8, 9, 10, 10, 10, 10 };
            IEnumerable<int> expected = new[] { 1, 2, 3, 4, 65, 6, 7, 8, 9, 10 };
            var result = Filter<int>.RepeatedData(testList);
            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}