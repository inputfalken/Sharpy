using System;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations {
    [TestFixture]
    public class LongRandomizerTests {
        private const int Amount = 100000;

        [Test]
        [Repeat(Amount)]
        public void NoArgument() {
            var longRandomizer = new LongRandomizer(new Random());
            var result = longRandomizer.Long();
            Assert.IsTrue(result > long.MinValue && result < long.MaxValue);
        }

        [Test]
        [Repeat(Amount)]
        public void One_Arg() {
            const long max = long.MaxValue - 1000;
            var longRandomizer = new LongRandomizer(new Random());
            var result = longRandomizer.Long(max);
            Assert.IsTrue(result >= 0 && result < max);
        }

        [Test]
        [Repeat(Amount)]
        public void One_Arg_MaxValue() {
            var longRandomizer = new LongRandomizer(new Random());
            var result = longRandomizer.Long(long.MaxValue);
            Assert.IsTrue(result >= 0 && result < long.MaxValue);
        }

        [Test]
        public void One_Arg_MinusOne_Throws() {
            var longRandomizer = new LongRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => longRandomizer.Long(-1));
        }

        [Test]
        [Repeat(Amount)]
        public void Two_Args_Max_More_Than_Min() {
            const long min = long.MaxValue - 3000;
            const long max = long.MaxValue - 2000;
            var longRandomizer = new LongRandomizer(new Random());
            var result = longRandomizer.Long(min, max);
            Assert.IsTrue(result >= min && result < max);
        }

        [Test]
        [Repeat(Amount)]
        public void Two_Args_Max_More_Than_Min_Both_Negative() {
            const int min = -2000;
            const int max = -1000;
            var longRandomizer = new LongRandomizer(new Random());
            var result = longRandomizer.Long(min, max);

            Assert.IsTrue(result >= min && result < max);
        }

        [Test]
        public void Two_Args_Maxx_Less_Than_Min_Throws() {
            const long min = long.MaxValue - 1000;
            const long max = long.MaxValue - 2000;
            var longRandomizer = new LongRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => longRandomizer.Long(min, max));
        }

        [Test]
        [Repeat(Amount)]
        public void Two_Args_MinValue_And_Zero() {
            var longRandomizer = new LongRandomizer(new Random());
            var result = longRandomizer.Long(long.MinValue, 0);
            Assert.IsTrue(result > long.MinValue && result < 0);
        }

        [Test]
        [Repeat(Amount)]
        public void Two_Args_Zero_And_MaxValue() {
            var longRandomizer = new LongRandomizer(new Random());
            var result = longRandomizer.Long(0, long.MaxValue);

            Assert.IsTrue(result > 0 && result < long.MaxValue);
        }
    }
}