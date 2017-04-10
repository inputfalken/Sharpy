using System;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void Arg_MinusOne() {
            var randomGenerator =  Productor.Yield(new Provider());
            Assert.Throws<ArgumentException>(
                () => randomGenerator.Generate(generator => generator.DateByYear(-1)).Produce());
        }

        [Test]
        public void Arg_TwoThousand() {
            var randomGenerator =  Productor.Yield(new Provider());
            var result = randomGenerator.Generate(generator => generator.DateByYear(2000)).Produce();
            Assert.AreEqual(result.Year, 2000);
        }

        [Test]
        public void Arg_TwoThousandTen() {
            var randomGenerator =  Productor.Yield(new Provider());
            var result = randomGenerator.Generate(generator => generator.DateByYear(2010)).Produce();
            Assert.AreEqual(result.Year, 2010);
        }
    }
}