using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.source {
    [TestFixture]
    public class Long {
        private const int Length = 1000000;

        [Test]
        public void DoubleArgumentMinusThousandAndMinusTwoThousand() {
            const int min = -1000;
            const int max = -2000;
            var longs = Generator.Create().GenerateMany(source => source.Long(min, max), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => Generator.Create().Generate(source => source.Long(min, max)));
        }

        [Test]
        public void DoubleArgumentMinusThousandAndTwoThousand() {
            const int min = -1000;
            const int max = 2000;
            var longs = Generator.Create().GenerateMany(source => source.Long(min, max), Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance = Generator.Create().Generate(source => source.Long(min, max));
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void DoubleArgumentMinusTwoThousandAndMinusThousand() {
            const int min = -2000;
            const int max = -1000;
            var longs = Generator.Create().GenerateMany(source => source.Long(min, max), Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance = Generator.Create().Generate(source => source.Long(min, max));
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void DoubleArgumentMinValueAndZero() {
            var longs = Generator.Create().GenerateMany(source => source.Long(long.MinValue, 0), Length);
            Assert.IsTrue(longs.All(l => l < 0));
            var longInstance = Generator.Create().Generate(source => source.Long(long.MinValue, 0));

            Assert.IsTrue(longInstance < 0);
        }


        [Test]
        public void DoubleArgumentThousandAndTwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var longs = Generator.Create().GenerateMany(source => source.Long(min, max), Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance = Generator.Create().Generate(source => source.Long(min, max));
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void DoubleArgumentZeroMaxValue() {
            var longs = Generator.Create().GenerateMany(source => source.Long(0, long.MaxValue), Length);
            Assert.IsTrue(longs.All(l => l > 0));

            var longInstance = Generator.Create().Generate(source => source.Long(0, long.MaxValue));
            Assert.IsTrue(longInstance > 0);
        }

        [Test]
        public void NoArgument() {
            var longs = Generator.Create().GenerateMany(source => source.Long(), Length);
            Assert.IsTrue(longs.All(l => l > long.MinValue && l < long.MaxValue));

            var longInstance = Generator.Create().Generate(source => source.Long());
            Assert.IsTrue(longInstance > long.MinValue && longInstance < long.MaxValue);
        }

        [Test]
        public void SingleArgumentLessThanZero() {
            var longs = Generator.Create().GenerateMany(source => source.Long(-1), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => Generator.Create().Generate(source => source.Long(-1)));
        }

        [Test]
        public void SingleArgumentMaxValue() {
            var longs = Generator.Create().GenerateMany(source => source.Long(long.MaxValue), Length);
            Assert.IsTrue(longs.All(l => l >= 0));

            var longInstance = Generator.Create().Generate(source => source.Long(long.MaxValue));
            Assert.IsTrue(longInstance >= 0);
        }

        [Test]
        public void SingleArgumentThousand() {
            const int max = 1000;
            var longs = Generator.Create().GenerateMany(source => source.Long(max), Length);
            Assert.IsTrue(longs.All(l => l >= 0 && l < max));

            var longInstance = Generator.Create().Generate(source => source.Long(max));
            Assert.IsTrue(longInstance >= 0 && longInstance < max);
        }

        [Test]
        public void SingleArgumentZero() {
            var longs = Generator.Create().GenerateMany(source => source.Long(0), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());


            Assert.Throws<ArgumentOutOfRangeException>(
                () => Generator.Create().Generate(source => source.Long(0)));
        }
    }
}