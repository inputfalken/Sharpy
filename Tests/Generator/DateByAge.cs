﻿using System;
using NUnit.Framework;
using Sharpy.Implementation.Generators;

namespace Tests.Generator {
    [TestFixture]
    public class DateByAge {
        [Test]
        public void RandomDateByAgeTwentyMinusOne() {
            //Will throw exception if argument is less than 0
            Assert.Throws<ArgumentException>(
                () => Sharpy.Generator.Create().Generate(generator => generator.DateByAge(-1)));
        }

        [Test]
        public void RandomDateByAgeTwentyYears() {
            var result = Sharpy.Generator.Create().Generate(generator => generator.DateByAge(20));

            Assert.AreEqual(result.Year, DateGenerator.CurrentLocalDate.Year - 20);
        }

        [Test]
        [Repeat(10)]
        public void RandomDateByAgeZeroYears() {
            var result = Sharpy.Generator.Create().Generate(generator => generator.DateByAge(0));
            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateGenerator.CurrentLocalDate > result);
        }
    }
}