using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Randomizer {
    [TestFixture]
    public class Long {
        private const int Length = 1000000;

        [Test]
        public void DoubleArgumentMinusThousandAndMinusTwoThousand() {
            const int min = -1000;
            const int max = -2000;
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(min, max), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => RandomGenerator.Create().Generate(randomizer => randomizer.Long(min, max)));
        }

        [Test]
        public void DoubleArgumentMinusThousandAndTwoThousand() {
            const int min = -1000;
            const int max = 2000;
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(min, max), Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance = RandomGenerator.Create().Generate(randomizer => randomizer.Long(min, max));
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void DoubleArgumentMinusTwoThousandAndMinusThousand() {
            const int min = -2000;
            const int max = -1000;
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(min, max), Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance = RandomGenerator.Create().Generate(randomizer => randomizer.Long(min, max));
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void DoubleArgumentMinValueAndZero() {
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(long.MinValue, 0), Length);
            Assert.IsTrue(longs.All(l => l < 0));
            var longInstance = RandomGenerator.Create().Generate(randomizer => randomizer.Long(long.MinValue, 0));

            Assert.IsTrue(longInstance < 0);
        }


        [Test]
        public void DoubleArgumentThousandAndTwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(min, max), Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance = RandomGenerator.Create().Generate(randomizer => randomizer.Long(min, max));
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void DoubleArgumentZeroMaxValue() {
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(0, long.MaxValue), Length);
            Assert.IsTrue(longs.All(l => l > 0));

            var longInstance = RandomGenerator.Create().Generate(randomizer => randomizer.Long(0, long.MaxValue));
            Assert.IsTrue(longInstance > 0);
        }

        [Test]
        public void NoArgument() {
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(), Length);
            Assert.IsTrue(longs.All(l => l > long.MinValue && l < long.MaxValue));

            var longInstance = RandomGenerator.Create().Generate(randomizer => randomizer.Long());
            Assert.IsTrue(longInstance > long.MinValue && longInstance < long.MaxValue);
        }

        [Test]
        public void SingleArgumentLessThanZero() {
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(-1), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => RandomGenerator.Create().Generate(randomizer => randomizer.Long(-1)));
        }

        [Test]
        public void SingleArgumentMaxValue() {
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(long.MaxValue), Length);
            Assert.IsTrue(longs.All(l => l >= 0));

            var longInstance = RandomGenerator.Create().Generate(randomizer => randomizer.Long(long.MaxValue));
            Assert.IsTrue(longInstance >= 0);
        }

        [Test]
        public void SingleArgumentThousand() {
            const int max = 1000;
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(max), Length);
            Assert.IsTrue(longs.All(l => l >= 0 && l < max));

            var longInstance = RandomGenerator.Create().Generate(randomizer => randomizer.Long(max));
            Assert.IsTrue(longInstance >= 0 && longInstance < max);
        }

        [Test]
        public void SingleArgumentZero() {
            var longs = RandomGenerator.Create().GenerateMany(randomizer => randomizer.Long(0), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());


            Assert.Throws<ArgumentOutOfRangeException>(
                () => RandomGenerator.Create().Generate(randomizer => randomizer.Long(0)));
        }
    }
}