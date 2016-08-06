using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGen.Types;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class FilterTest {
        [Test]
        public void RepeatedDataTestString() {
            IEnumerable<string> testList = new[] { "bob", "bob", "John", "doe", "doe", "doe" };
            IEnumerable<string> expected = new[] { "bob", "John", "doe" };
            var result = Filter<string>.RemoveRepeatedData(testList);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void RepeatedDataTestInt() {
            IEnumerable<int> testList = new[] { 1, 2, 3, 4, 65, 6, 7, 7, 8, 8, 9, 10, 10, 10, 10 };
            IEnumerable<int> expected = new[] { 1, 2, 3, 4, 65, 6, 7, 8, 9, 10 };
            var result = Filter<int>.RemoveRepeatedData(testList);
            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}