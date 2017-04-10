using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Implementation;

namespace Tests.Integration {
    [TestFixture]
    public class DateByAge {
        [Test]
        public void Arg_MinusOne() {
            //Will throw exception if argument is less than 0
            Assert.Throws<ArgumentException>(
                () => Productor.Yield(new Provider()).Generate(generator => generator.DateByAge(-1)).Produce());
        }

        [Test]
        public void Arg_Twenty() {
            var result = Productor.Yield(new Provider()).Generate(generator => generator.DateByAge(20)).Produce();

            Assert.AreEqual(result.Year, DateGenerator.CurrentLocalDate.Year - 20);
        }

        [Test]
        [Repeat(10)]
        public void Arg_Zero() {
            var result = Productor.Yield(new Provider()).Generate(generator => generator.DateByAge(0)).Produce();

            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateGenerator.CurrentLocalDate > result);
        }
    }
}