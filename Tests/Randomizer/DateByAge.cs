using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Implementation.Generators;

namespace Tests.Randomizer {
    [TestFixture]
    public class DateByAge {
        [Test]
        public void RandomDateByAgeTwentyMinusOne() {
            //Will throw exception if argument is less than 0
            var generator = SharpyGenerator.Create();
            Assert.Throws<ArgumentException>(() => generator.Generate(randomizer => randomizer.DateByAge(-1)));
        }

        [Test]
        public void RandomDateByAgeTwentyYears() {
            var generator = SharpyGenerator.Create();
            var result = generator.Generate(randomizer => randomizer.DateByAge(20));

            Assert.AreEqual(result.Year, DateGenerator.CurrentLocalDate.Year - 20);
        }

        [Test]
        [Repeat(10)]
        public void RandomDateByAgeZeroYears() {
            var generator = SharpyGenerator.Create();
            var result = generator.Generate(randomizer => randomizer.DateByAge(0));
            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateGenerator.CurrentLocalDate > result);
        }
    }
}