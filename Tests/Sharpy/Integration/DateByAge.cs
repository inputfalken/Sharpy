using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Core;
using Sharpy.Core.Linq;
using Sharpy.Implementation;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class DateByAge {
        [Test]
        public void Arg_MinusOne() {
            //Will throw exception if argument is less than 0
            Assert.Throws<ArgumentException>(
                () => Generator.Create(new Provider())
                    .Select(generator => generator.DateByAge(-1))
                    .Generate());
        }

        [Test]
        public void Arg_Twenty() {
            var result = Generator.Create(new Provider())
                .Select(generator => generator.DateByAge(20))
                .Generate();

            Assert.AreEqual(result.Year, DateGenerator.CurrentLocalDate.Year - 20);
        }

        [Test]
        [Repeat(10)]
        public void Arg_Zero() {
            var result = Generator.Create(new Provider())
                .Select(generator => generator.DateByAge(0))
                .Generate();

            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateGenerator.CurrentLocalDate > result);
        }
    }
}