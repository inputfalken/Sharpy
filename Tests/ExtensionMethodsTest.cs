using System.Collections.Generic;
using System.Linq;
using DataGen;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class ExtensionMethodsTest {
        [Test]
        public void CreatePattern_Arg_IntList_WithDivisionFromTwelve() {
            var expected = new List<double> { 12, 6, 4 };
            var result = new List<double>().CreatePattern(3, (i, i1) => i / i1, 12);
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [Test]
        public void CreatePattern_Arg_IntList_WithMulitplikationFromFive() {
            var expected = new List<int> { 5, 10, 15, 20, 25 };
            var result = new List<int>().CreatePattern(5, (i, i1) => i * i1, 5);
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [Test]
        public void CreatePattern_Arg_IntList_WithMultiplikationFromThree() {
            var expected = new List<int> { 3, 6, 9, 12, 15 };
            var result = new List<int>().CreatePattern(5, (i, i1) => i * i1, 3);
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [Test]
        public void CreatePattern_Arg_StringList() {
            var expected = new List<string> { "foo", "foofoo", "foofoofoo", "foofoofoofoo" };
            var result = new List<string>().CreatePattern(4, (s, i) =>
                Enumerable.Repeat(s, i).Aggregate((s1, s2) => s1 + s2), "foo");
            Assert.IsTrue(expected.SequenceEqual(result));
        }
    }
}