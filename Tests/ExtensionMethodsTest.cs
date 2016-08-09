using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataGen;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class ExtensionMethodsTest {
        #region Sequence

        [Test]
        public void Sequence_Arg_IntList_WithMultiplikationFromThree() {
            var expected = new List<int> { 3, 6, 9, 12, 15 };
            var result = new List<int>().Sequence(5, (i, i1) => i * i1, 3);
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [Test]
        public void Sequence_Arg_IntList_WithMulitplikationFromFive() {
            var expected = new List<int> { 5, 10, 15, 20, 25 };
            var result = new List<int>().Sequence(ammount: 5, func: (i, i1) => i * i1, sequenceValue: 5);
            Assert.IsTrue(expected.SequenceEqual(result));
        }


        [Test]
        public void Sequence_Arg_IntList_WithDivisionFromTwelve() {
            var expected = new List<double> { 12, 6, 4 };
            var result = new List<double>().Sequence(ammount: 3, func: (i, i1) => i / i1, sequenceValue: 12);
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [Test]
        public void Sequence_Arg_StringList() {
            var expected = new List<string>() { "foo", "foofoo", "foofoofoo", "foofoofoofoo" };
            var result = new List<string>().Sequence(4, (s, i) =>
                    Enumerable.Repeat(s, i).Aggregate((s1, s2) => s1 + s2), "foo");
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        #endregion
    }
}