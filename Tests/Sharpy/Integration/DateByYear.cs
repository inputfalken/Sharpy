﻿using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Generator;
using Sharpy.Generator.Linq;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void Arg_MinusOne() {
            var randomGenerator = Generator.Create(new Provider());
            Assert.Throws<ArgumentException>(
                () => randomGenerator.Select(generator => generator.DateByYear(-1)).Generate());
        }

        [Test]
        public void Arg_TwoThousand() {
            var randomGenerator = Generator.Create(new Provider());
            var result = randomGenerator.Select(generator => generator.DateByYear(2000)).Generate();
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void Arg_TwoThousandTen() {
            var randomGenerator = Generator.Create(new Provider());
            var result = randomGenerator.Select(generator => generator.DateByYear(2010)).Generate();
            Assert.AreEqual(result.Year, 2010);
        }
    }
}