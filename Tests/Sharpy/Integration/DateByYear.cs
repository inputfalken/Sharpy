﻿using System;
using GeneratorAPI;
using NUnit.Framework;
using Sharpy;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void Arg_MinusOne() {
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider());
            Assert.Throws<ArgumentException>(
                () => randomGenerator.Generate(generator => generator.DateByYear(-1)).Take());
        }

        [Test]
        public void Arg_TwoThousand() {
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider());
            var result = randomGenerator.Generate(generator => generator.DateByYear(2000)).Take();
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void Arg_TwoThousandTen() {
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider());
            var result = randomGenerator.Generate(generator => generator.DateByYear(2010)).Take();
            Assert.AreEqual(result.Year, 2010);
        }
    }
}