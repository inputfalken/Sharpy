using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Implementation;

namespace Tests.Generator {
    [TestFixture]
    public class DateByAge {
        [Test]
        public void Arg_MinusOne() {
            //Will throw exception if argument is less than 0
            Assert.Throws<ArgumentException>(
                () => new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.DateByAge(-1)));
        }

        [Test]
        public void Arg_Twenty() {
            var result = new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.DateByAge(20));

            Assert.AreEqual(result.Year, DateGenerator.CurrentLocalDate.Year - 20);
        }

        [Test]
        [Repeat(10)]
        public void Arg_Zero() {
            var result = new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.DateByAge(0));
            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateGenerator.CurrentLocalDate > result);
        }
    }
}