using System;
using NUnit.Framework;
using Sharpy;

namespace Tests.Randomizer {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void DateByYearTwoThousand() {
            var randomGenerator = SharpyGenerator.Create();
            var localDate = randomGenerator.Generate(randomizer => randomizer.DateByYear(2000));
            Assert.AreEqual(2000, localDate.Year);
        }

        [Test]
        public void RandomDateByYearMinusOne() {
            var randomGenerator = SharpyGenerator.Create();
            Assert.Throws<ArgumentException>(() => randomGenerator.Generate(randomizer => randomizer.DateByYear(-1)));
        }

        [Test]
        public void RandomDateByYearTwoThousand() {
            var randomGenerator = SharpyGenerator.Create();
            var result = randomGenerator.Generate(randomizer => randomizer.DateByYear(2000));
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void RandomDateByYearTwoThousandTen() {
            var randomGenerator = SharpyGenerator.Create();
            var result = randomGenerator.Generate(randomizer => randomizer.DateByYear(2010));
            Assert.AreEqual(result.Year, 2010);
        }
    }
}