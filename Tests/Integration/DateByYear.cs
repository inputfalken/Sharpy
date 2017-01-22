using System;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void Arg_MinusOne() {
            var randomGenerator = new Generator();
            Assert.Throws<ArgumentException>(() => randomGenerator.Generate(generator => generator.DateByYear(-1)));
        }

        [Test]
        public void Arg_TwoThousand() {
            var randomGenerator = new Generator();
            var result = randomGenerator.Generate(generator => generator.DateByYear(2000));
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void Arg_TwoThousandTen() {
            var randomGenerator = new Generator();
            var result = randomGenerator.Generate(generator => generator.DateByYear(2010));
            Assert.AreEqual(result.Year, 2010);
        }
    }
}