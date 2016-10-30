using System;
using System.Linq;
using NodaTime;
using NUnit.Framework;
using Sharpy.Randomizer.Generators;

namespace Tests {
    [TestFixture]
    public class DateGeneratorTest {
        [Test]
        public void RandomDateByAgeTwentyMinusOne() {
            //Will throw exception if argument is less than 0
            var dateGenerator = new DateGenerator(new Random());
            Assert.Throws<ArgumentException>(() => dateGenerator.RandomDateByAge(-1));
        }

        [Test]
        public void RandomDateByAgeTwentyYears() {
            var dateGenerator = new DateGenerator(new Random());
            var result = dateGenerator.RandomDateByAge(20);
            Assert.AreEqual(result.Year, DateGenerator.CurrentLocalDate.Year - 20);
        }

        [Test]
        public void RandomDateByAgeZeroYears() {
            var dateGenerator = new DateGenerator(new Random());
            var result = dateGenerator.RandomDateByAge(0);
            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateGenerator.CurrentLocalDate > result);
        }

        [Test]
        public void RandomDateByYearTwoThousand() {
            var dateGenerator = new DateGenerator(new Random());
            var result = dateGenerator.RandomDateByYear(2000);
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void RandomDateByYearTwoThousandTen() {
            var dateGenerator = new DateGenerator(new Random());
            var result = dateGenerator.RandomDateByYear(2010);
            Assert.AreEqual(result.Year, 2010);
        }

        [Test]
        public void RandomDateByYearMinusOne() {
            var dateGenerator = new DateGenerator(new Random());
            Assert.Throws<ArgumentException>(() => dateGenerator.RandomDateByYear(-1));
        }
    }
}