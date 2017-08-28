﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Generator.Linq;

namespace Tests.Sharpy.FactoryTests {
    [TestFixture]
    internal class DecrementerTests {
        private static IEnumerable<int> GetExpectedDecrementationEnumerable(int start, int count) {
            // Closest way to to copy the behaviour of Decrementer.
            return Enumerable
                .Range((start * -1 + count) * -1 + 1, count)
                .Reverse();
        }

        [Test]
        public void Int_MinValue_Throws() {
            var result = Extensions.Take<int>(Factory.Decrementer(int.MinValue), (int) 500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }


        [Test]
        public void Start_Int_MaxValue_Does_Not_Throw() {
            var result = Extensions.Take<int>(Factory.Decrementer(int.MaxValue), (int) 500);
            Assert.DoesNotThrow(() => result.ToArray());
        }

        [Test]
        public void Start_Minus_Twenty() {
            const int start = -20;
            const int count = 500;
            var result = Extensions.Take<int>(Factory.Decrementer(start), (int) count);

            var expected = GetExpectedDecrementationEnumerable(start, count);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Start_Twenty() {
            const int start = 20;
            const int count = 500;
            var result = Extensions.Take<int>(Factory.Decrementer(start), (int) count);
            var expected = GetExpectedDecrementationEnumerable(start, count);
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void To_Int_Min_Value() {
            var count = 500;
            var start = int.MinValue + count;
            var result = Extensions.Take<int>(Factory.Decrementer(start), (int) count)
                .ToArray();
            var expected = GetExpectedDecrementationEnumerable(start, count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void To_Less_Than_Int_Min_Value() {
            const int start = int.MinValue + 500;
            var result = Extensions.Take<int>(Factory.Decrementer(start), (int) 501);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }
    }
}