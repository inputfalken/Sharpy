using System;
using System.Linq;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class NumberGenerator {
        [Test]
        public void CreateRandomNumber_Args_Length5_PreNumber30_UniqueFalse() {
            var phoneNumberGenerator = new Sharpy.Randomizer.Generators.NumberGenerator(new Random(), 4);
            //Runs the randomnumber 1000 times and checks if any number is ever repeated
            Assert.IsFalse(Enumerable.Range(0, 1000)
                    .Select(i => phoneNumberGenerator.RandomNumber())
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1)
            );
        }

        [Test]
        public void CreateRandomNumber_Args_Length5_PreNumber30_UniqueTrue() {
            var phoneNumberGenerator = new Sharpy.Randomizer.Generators.NumberGenerator(new Random(), 4, true);
            //Runs the randomnumber 1000 times and checks if any number is ever repeated
            Assert.IsTrue(Enumerable.Range(0, 1000)
                    .Select(i => phoneNumberGenerator.RandomNumber())
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1)
            );
        }
    }
}