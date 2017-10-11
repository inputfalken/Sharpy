using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.CoreTests.Implementations {
    [TestFixture]
    internal class IncrementerTests {
        [Test]
        public void Int_MinValue_Does_Not_Throw() {
            var result = Generator.Incrementer(int.MinValue).Take(500);
            Assert.DoesNotThrow(() => result.ToArray());
        }

        [Test]
        public void Long_MinValue_Does_Not_Throw() {
            var result = Generator.Incrementer(long.MinValue).Take(500);
            Assert.DoesNotThrow(() => result.ToArray());
        }

        [Test]
        public void Start_Below_Int_MinValue_Throws() {
            var start = int.MinValue;
            var result = Generator.Incrementer(start - 1).Take(1);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Start_Below_Long_MinValue_Throws() {
            var start = long.MinValue;
            var result = Generator.Incrementer(start - 1).Take(1);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Start_Int_MaxValue_Minus_Thousand() {
            var start = int.MaxValue - 1000;
            var count = 500;
            var result = Generator.Incrementer(start).Take(count);
            var expected = new List<int>();
            for (var i = start; i < start + count; i++) expected.Add(i);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Start_Int_MaxValue_Throws() {
            var result = Generator.Incrementer(int.MaxValue).Take(500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Start_Long_MaxValue_Minus_Thousand() {
            var start = long.MaxValue - 1000;
            var count = 500;
            var result = Generator.Incrementer(start).Take(count);
            var expected = new List<long>();
            for (var i = start; i < start + count; i++) expected.Add(i);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Start_Long_MaxValue_Throws() {
            var result = Generator.Incrementer(long.MaxValue).Take(500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void To_Int_Max_Value() {
            var count = 500;
            var start = int.MaxValue - count;
            var result = Generator.Incrementer(start).Take(count);
            var expected = new List<int>();
            for (var i = start; i < start + count; i++) expected.Add(i);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void To_Long_Max_Value() {
            var count = 500;
            var start = long.MaxValue - count;
            var result = Generator.Incrementer(start).Take(count);
            var expected = new List<long>();
            for (var i = start; i < start + count; i++) expected.Add(i);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void To_More_Than_Int_Max_Value_Throws() {
            const int start = int.MaxValue - 500;
            var result = Generator.Incrementer(start).Take(501);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }
    }
}