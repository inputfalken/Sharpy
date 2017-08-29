using System;
using System.Threading;
using NUnit.Framework;
using Sharpy;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.FactoryTests {
    [TestFixture]
    internal class RandomizerTests {
        [Test]
        public void Int32_Max_Greater_Than_Min_Does_Not_Throw() {
            const int min = 0;
            const int max = 1;
            Assert.DoesNotThrow(() => Factory.Randomizer(min, max));
        }

        [Test]
        public void Int32_Min_Equal_To_Max_Throws() {
            const int min = 1;
            const int max = 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => Factory.Randomizer(min, max));
        }

        [Test]
        public void Int32_Min_Greater_Than_Max_Throws() {
            const int min = 1;
            const int max = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => Factory.Randomizer(min, max));
        }

        [Test]
        public void Int32_Using_Seed_Gives_Same_Result() {
            const int seed = 100;
            const int length = 1000000;
            const int maxValue = 100000;
            const int minValue = 1000;
            var result = Factory.Randomizer(minValue, maxValue, seed).ToArray(length);
            // So the seed can change
            Thread.Sleep(200);
            var expected = Factory.Randomizer(minValue, maxValue, seed).ToArray(length);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Int32_With_Different_Seed_Does_Not_Gives_Same_Result() {
            const int length = 1000000;
            const int maxValue = 100000;
            const int minValue = 1000;
            var result = Factory.Randomizer(minValue, maxValue, 100).ToArray(length);
            var expected = Factory.Randomizer(minValue, maxValue, 200).ToArray(length);

            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void Int32_Without_Seed_Does_Not_Gives_Same_Result() {
            const int length = 1000000;
            const int maxValue = 100000;
            const int minValue = 1000;
            var result = Factory.Randomizer(minValue, maxValue).ToArray(length);
            // So the seed can change
            Thread.Sleep(200);
            var expected = Factory.Randomizer(minValue, maxValue).ToArray(length);

            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void Int64_Max_Greater_Than_Min_Does_Not_Throw() {
            const long min = 0;
            const long max = 1;
            Assert.DoesNotThrow(() => Factory.Randomizer(min, max));
        }

        [Test]
        public void Int64_Min_Equal_To_Max_Throws() {
            const long min = 1;
            const long max = 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => Factory.Randomizer(min, max));
        }

        [Test]
        public void Int64_Min_Greater_Than_Max_Throws() {
            const long min = 1;
            const long max = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => Factory.Randomizer(min, max));
        }

        [Test]
        public void Int64_Using_Seed_Gives_Same_Result() {
            const int seed = 100;
            const int length = 1000000;
            const long maxValue = long.MaxValue - 20000000;
            const long minValue = 0 + long.MaxValue - 30000000;
            var result = Factory.Randomizer(minValue, maxValue, seed).ToArray(length);
            // So the seed can change
            Thread.Sleep(200);
            var expected = Factory.Randomizer(minValue, maxValue, seed).ToArray(length);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Int64_With_Different_Seed_Does_Not_Gives_Same_Result() {
            const int length = 1000000;
            const long maxValue = long.MaxValue - 20000000;
            const long minValue = 0 + long.MaxValue - 30000000;
            var result = Factory.Randomizer(minValue, maxValue, 100).ToArray(length);
            var expected = Factory.Randomizer(minValue, maxValue, 200).ToArray(length);

            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void Int64_Without_Seed_Does_Not_Gives_Same_Result() {
            const int length = 1000000;
            const long maxValue = long.MaxValue - 20000000;
            const long minValue = 0 + long.MaxValue - 30000000;
            var result = Factory.Randomizer(minValue, maxValue).ToArray(length);
            // So the seed can change
            Thread.Sleep(200);
            var expected = Factory.Randomizer(minValue, maxValue).ToArray(length);

            Assert.AreNotEqual(expected, result);
        }
    }
}