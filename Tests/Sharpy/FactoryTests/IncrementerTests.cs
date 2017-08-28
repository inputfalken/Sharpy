using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Generator.Linq;

namespace Tests.Sharpy.FactoryTests {
    [TestFixture]
    internal class IncrementerTests {
        [Test]
        public void Int_MinValue_Does_Not_Throw() {
            var result = Extensions.Take<int>(Factory.Incrementer(int.MinValue), (int) 500);
            Assert.DoesNotThrow(() => result.ToArray());
        }


        [Test]
        public void Start_Below_Int_MinValue_Throws() {
            var start = int.MinValue;
            var result = Extensions.Take<int>(Factory.Incrementer(start - 1), (int) 1);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Start_Int_MaxValue_Throws() {
            var result = Extensions.Take<int>(Factory.Incrementer(int.MaxValue), (int) 500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Start_Minus_Twenty() {
            var result = Extensions.Take<int>(Factory.Incrementer(-20), (int) 500);
            var expected = Enumerable.Range(-20, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Start_Twenty() {
            var result = Extensions.Take<int>(Factory.Incrementer(20), (int) 500);
            var expected = Enumerable.Range(20, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void To_Int_Max_Value() {
            const int start = int.MaxValue - 500;
            var result = Extensions.Take<int>(Factory.Incrementer(start), (int) 500);
            var expected = Enumerable.Range(start, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void To_More_Than_Int_Max_Value() {
            const int start = int.MaxValue - 500;
            var result = Extensions.Take<int>(Factory.Incrementer(start), (int) 501);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }
    }
}