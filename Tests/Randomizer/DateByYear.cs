﻿using System;
using NUnit.Framework;
using Sharpy;

namespace Tests.source {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void DateByYearTwoThousand() {
            var randomGenerator = Generator.Create();
            var localDate = randomGenerator.Generate(source => source.DateByYear(2000));
            Assert.AreEqual(2000, localDate.Year);
        }

        [Test]
        public void RandomDateByYearMinusOne() {
            var randomGenerator = Generator.Create();
            Assert.Throws<ArgumentException>(() => randomGenerator.Generate(source => source.DateByYear(-1)));
        }

        [Test]
        public void RandomDateByYearTwoThousand() {
            var randomGenerator = Generator.Create();
            var result = randomGenerator.Generate(source => source.DateByYear(2000));
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void RandomDateByYearTwoThousandTen() {
            var randomGenerator = Generator.Create();
            var result = randomGenerator.Generate(source => source.DateByYear(2010));
            Assert.AreEqual(result.Year, 2010);
        }
    }
}