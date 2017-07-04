using System;
using GeneratorAPI;
using GeneratorAPI.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Implementation;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class DateByAge {
        [Test]
        public void Arg_MinusOne() {
            //Will throw exception if argument is less than 0
            Assert.Throws<ArgumentException>(
                () => Generator.Factory.Provider(new Provider())
                    .Select(generator => generator.DateByAge(-1))
                    .Generate());
        }

        [Test]
        public void Arg_Twenty() {
            var result = Generator.Factory.Provider(new Provider())
                .Select(generator => generator.DateByAge(20))
                .Generate();

            Assert.AreEqual(result.Year, DateGenerator.CurrentLocalDate.Year - 20);
        }

        [Test]
        [Repeat(10)]
        public void Arg_Zero() {
            var result = Generator.Factory.Provider(new Provider())
                .Select(generator => generator.DateByAge(0))
                .Generate();

            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateGenerator.CurrentLocalDate > result);
        }
    }
}