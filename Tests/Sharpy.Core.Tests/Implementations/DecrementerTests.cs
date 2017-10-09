using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.Core.Tests.Implementations {
    [TestFixture]
    internal class DecrementerTests {
        private static IEnumerable<int> GetExpectedDecrementationEnumerable(int start, int count) => Enumerable
            .Range((start * -1 + count) * -1 + 1, count)
            .Reverse();

        [Test]
        public void Int_MinValue_Throws() {
            var result = Generator.Decrementer(int.MinValue).Take(500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Long_MinValue_Throws() {
            var result = Generator.Decrementer(long.MinValue).Take(500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Start_Int_MaxValue_Does_Not_Throw() {
            var result = Generator.Decrementer(int.MaxValue).Take(500);
            Assert.DoesNotThrow(() => result.ToArray());
        }

        [Test]
        public void Start_Int_MinValue_Plus_Thousand() {
            const int start = int.MinValue + 1000;
            const int count = 500;
            var result = Generator.Decrementer(start).Take(count);

            var expected = new List<int>();
            for (var i = start; i > start - count; i--) expected.Add(i);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Start_Long_MaxValue_Does_Not_Throw() {
            var result = Generator.Decrementer(long.MaxValue).Take(500);
            Assert.DoesNotThrow(() => result.ToArray());
        }

        [Test]
        public void Start_Long_MinValue_Plus_Thousand() {
            const long start = long.MinValue + 1000;
            const int count = 500;
            var result = Generator.Decrementer(start).Take(count);

            var expected = new List<long>();
            for (var i = start; i > start - count; i--) expected.Add(i);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void To_Int_Min_Value() {
            var count = 500;
            var start = int.MinValue + count;
            var result = Generator.Decrementer(start).Take(count)
                .ToArray();
            var expected = new List<int>();
            for (var i = start; i > start - count; i--) expected.Add(i);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void To_Less_Than_Int_Min_Value_Throws() {
            const int start = int.MinValue + 500;
            var result = Generator.Decrementer(start).Take(501);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void To_Less_Than_Long_Min_Value_Throws() {
            const long start = long.MinValue + 500;
            var result = Generator.Decrementer(start).Take(501);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void To_Long_Min_Value() {
            var count = 500;
            var start = long.MinValue + count;
            var result = Generator.Decrementer(start).Take(count)
                .ToArray();
            var expected = new List<long>();
            for (var i = start; i > start - count; i--) expected.Add(i);
            Assert.AreEqual(expected, result);
        }
    }
}