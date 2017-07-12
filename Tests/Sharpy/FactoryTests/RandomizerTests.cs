using System;
using GeneratorAPI;
using GeneratorAPI.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Sharpy.FactoryTests {
    [TestFixture]
    internal class RandomizerTests {
        [Test(
            Description = "Verify that Randomizer behaves like System.Random"
        )]
        public void With_Min_Max_Behaves_Like_Same_As_Random() {
            const int seed = 100;
            const int length = 1000000;
            const int maxValue = 100000;
            const int minValue = 1000;
            var result = Factory.Randomizer(minValue, maxValue, seed)
                .ToArray(length);
            var expected = Generator
                .Create(new Random(seed))
                .Select(rnd => rnd.Next(minValue, maxValue))
                .ToArray(length);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that Randomizer behaves like System.Random"
        )]
        public void Without_Min_Max_Behaves_Like_Same_As_Random() {
            const int seed = 100;
            const int length = 1000000;
            var result = Factory.Randomizer(seed: seed)
                .ToArray(length);
            var expected = Generator
                .Create(new Random(seed))
                .Select(rnd => rnd.Next())
                .ToArray(length);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that Randomizer behaves like System.Random"
        )]
        public void Without_MinValue_Behaves_Like_Random() {
            const int seed = 100;
            const int length = 1000000;
            const int maxValue = 1000;
            var result = Factory.Randomizer(maxValue, seed: seed)
                .ToArray(length);
            var expected = Generator
                .Create(new Random(seed))
                .Select(rnd => rnd.Next(maxValue))
                .ToArray(length);

            Assert.AreEqual(expected, result);
        }
    }
}