using System;
using System.Linq;
using GeneratorAPI;
using NUnit.Framework;

namespace Tests.GeneratorAPI {
    [TestFixture]
    public class GeneratorFactoryTests {
        [Test]
        public void Incrementer_No_Arg() {
            var result = Generator
                .Factory
                .Incrementer()
                .Take(500);
            var expected = Enumerable.Range(0, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_Start_Twenty() {
            var result = Generator
                .Factory
                .Incrementer(20)
                .Take(500);
            var expected = Enumerable.Range(20, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_Start_Minus_Twenty() {
            var result = Generator
                .Factory
                .Incrementer(-20)
                .Take(500);
            var expected = Enumerable.Range(-20, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_Start_Int_MaxValue_Throws() {
            var result = Generator
                .Factory
                .Incrementer(int.MaxValue)
                .Take(500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Incrementer_Int_MinValue_Does_Not_Throw() {
            var result = Generator
                .Factory
                .Incrementer(int.MinValue)
                .Take(500);
            Assert.DoesNotThrow(() => result.ToArray());
        }

        [Test]
        public void Incrementer_Start_Below_Int_MinValue_Throws() {
            int start = int.MinValue;
            var result = Generator
                .Factory
                .Incrementer(start - 1)
                .Take(1);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Incrementer_To_Int_Max_Value() {
            const int start = int.MaxValue - 500;
            var result = Generator
                .Factory
                .Incrementer(start)
                .Take(500);
            var expected = Enumerable.Range(start, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_To_More_Than_Int_Max_Value() {
            const int start = int.MaxValue - 500;
            var result = Generator
                .Factory
                .Incrementer(start)
                .Take(501);
            Assert.Throws<OverflowException>(() => result.ToArray());
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
                .Randomizer(minValue, maxValue, seed)
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
        public void Randomizer_Without_MinValue_Behaves_Like_Random() {
            const int seed = 100;
            const int length = 1000000;
            const int maxValue = 1000;
            var result = Generator.Factory
                .Randomizer(maxValue, seed: seed)
                .ToArray(length);
            var expected = Generator
                .Create(new Random(seed))
                .Select(rnd => rnd.Next(maxValue))
                .ToArray(length);

            Assert.AreEqual(expected, result);
        }
    }
}