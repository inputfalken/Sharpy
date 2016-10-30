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
    }
}