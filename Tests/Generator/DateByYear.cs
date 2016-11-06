using System;
using NUnit.Framework;

namespace Tests.Generator {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void DateByYearTwoThousand() {
            var randomGenerator = Sharpy.Generator.Create();
            var localDate = randomGenerator.Generate(source => source.DateByYear(2000));
            Assert.AreEqual(2000, localDate.Year);
        }

        [Test]
        public void RandomDateByYearMinusOne() {
            var randomGenerator = Sharpy.Generator.Create();
            Assert.Throws<ArgumentException>(() => randomGenerator.Generate(source => source.DateByYear(-1)));
        }

        [Test]
        public void RandomDateByYearTwoThousand() {
            var randomGenerator = Sharpy.Generator.Create();
            var result = randomGenerator.Generate(source => source.DateByYear(2000));
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void RandomDateByYearTwoThousandTen() {
            var randomGenerator = Sharpy.Generator.Create();
            var result = randomGenerator.Generate(source => source.DateByYear(2010));
            Assert.AreEqual(result.Year, 2010);
        }
    }
}