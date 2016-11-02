using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Randomize {
    [TestFixture]
    public class Long {
        private const int Length = 1000000;

        [Test]
        public void DoubleArgumentZeroMaxValue() {
            var longs = RandomGenerator.Create().GenerateMany(randomize => randomize.Long(0, long.MaxValue), Length);
            Assert.IsTrue(longs.All(l => l > 0));
        }

        [Test]
        public void DoubleArgumentMinValueAndZero() {
            var longs = RandomGenerator.Create().GenerateMany(randomize => randomize.Long(long.MinValue, 0), Length);
            Assert.IsTrue(longs.All(l => l < 0));
        }

        [Test]
        public void NoArgument() {
            var longs = RandomGenerator.Create().GenerateMany(randomize => randomize.Long(), Length);
            Assert.IsTrue(longs.All(l => l > long.MinValue && l < long.MaxValue));
        }

        [Test]
        public void SingleArgumentMaxValue() {
            var longs = RandomGenerator.Create().GenerateMany(randomize => randomize.Long(long.MaxValue), Length);
            Assert.IsTrue(longs.All(l => l > long.MinValue && l < long.MaxValue));
        }

        [Test]
        public void SingleArgumentLessThanZero() {
            var longs = RandomGenerator.Create().GenerateMany(randomize => randomize.Long(-1), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());
        }

        [Test]
        public void SingleArgumentZero() {
            var longs = RandomGenerator.Create().GenerateMany(randomize => randomize.Long(0), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());
        }
    }
}