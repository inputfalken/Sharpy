using System;
using GeneratorAPI;
using NUnit.Framework;

namespace Tests.GeneratorAPI {
    [TestFixture]
    public class GeneratoeFactoryTests {
        [Test(
            Description = "Verify that Randomizer behaves like System.Random"
        )]
        public void Randomizer_Without_Min_Max_Behaves_Like_Same_As_Random() {
            const int seed = 100;
            const int length = 1000000;
            var result = Generator.Factory
                .Randomizer(seed: seed)
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
        public void Randomizer_With_Min_Max_Behaves_Like_Same_As_Random() {
            const int seed = 100;
            const int length = 1000000;
            const int maxValue = 100000;
            const int minValue = 1000;
            var result = Generator.Factory
                .Randomizer(minValue, max: maxValue, seed: seed)
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
        public void Randomizer_Without_MinValue_Behaves_Like_Random() {
            const int seed = 100;
            const int length = 1000000;
            const int maxValue = 1000;
            var result = Generator.Factory
                .Randomizer(max: maxValue, seed: seed)
                .ToArray(length);
            var expected = Generator
                .Create(new Random(seed))
                .Select(rnd => rnd.Next(maxValue: maxValue))
                .ToArray(length);

            Assert.AreEqual(expected, result);
        }
    }
}