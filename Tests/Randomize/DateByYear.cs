using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Randomize.Generators;

namespace Tests.Randomize {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void DateByYearTwoThousand() {
            var randomGenerator = RandomGenerator.Create();
            var localDate = randomGenerator.Generate(randomize => randomize.DateByYear(2000));
            Assert.AreEqual(2000, localDate.Year);
        }

        [Test]
        public void RandomDateByYearMinusOne() {
            var randomGenerator = RandomGenerator.Create();
            Assert.Throws<ArgumentException>(() => randomGenerator.Generate(randomize => randomize.DateByYear(-1)));
        }

        [Test]
        public void RandomDateByYearTwoThousand() {
            var randomGenerator = RandomGenerator.Create();
            var result = randomGenerator.Generate(randomize => randomize.DateByYear(2000));
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void RandomDateByYearTwoThousandTen() {
            var randomGenerator = RandomGenerator.Create();
            var result = randomGenerator.Generate(randomize => randomize.DateByYear(2010));
            Assert.AreEqual(result.Year, 2010);
        }
    }
}