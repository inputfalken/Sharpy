using System;
using System.Collections.Generic;
using System.Linq;
using GeneratorAPI;
using GeneratorAPI.Linq;
using NUnit.Framework;

namespace Tests.GeneratorAPI {
    [TestFixture]
    public class GeneratorFactoryTests {
        private static IEnumerable<int> GetExpectedDecrementationEnumerable(int start, int count) {
            // Closest way to to copy the behaviour of Decrementer.
            return Enumerable
                .Range((start * -1 + count) * -1 + 1, count)
                .Reverse();
        }

        [Test]
        public void Decrementer_Int_MinValue_Throws() {
            var result = Generator
                .Factory
                .Decrementer(int.MinValue)
                .Take(500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Decrementer_No_Arg() {
            const int count = 500;
            var result = Generator
                .Factory
                .Decrementer()
                .Take(count);
            var expected = GetExpectedDecrementationEnumerable(0, count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Decrementer_Start_Int_MaxValue_Does_Not_Throw() {
            var result = Generator
                .Factory.Decrementer(int.MaxValue)
                .Take(500);
            Assert.DoesNotThrow(() => result.ToArray());
        }

        [Test]
        public void Decrementer_Start_Minus_Twenty() {
            const int start = -20;
            const int count = 500;
            var result = Generator
                .Factory
                .Decrementer(start)
                .Take(count);

            var expected = GetExpectedDecrementationEnumerable(start, count);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Decrementer_Start_Twenty() {
            const int start = 20;
            const int count = 500;
            var result = Generator
                .Factory
                .Decrementer(start)
                .Take(count);
            var expected = GetExpectedDecrementationEnumerable(start, count);
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Decrementer_To_Int_Min_Value() {
            var count = 500;
            var start = int.MinValue + count;
            var result = Generator
                .Factory
                .Decrementer(start)
                .Take(count)
                .ToArray();
            var expected = GetExpectedDecrementationEnumerable(start, count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Decrementer_To_Less_Than_Int_Min_Value() {
            const int start = int.MinValue + 500;
            var result = Generator
                .Factory
                .Decrementer(start)
                .Take(501);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test(
            Description = "Verify that Guid generator does not return null"
        )]
        public void Guid_Generator_Is_Not_Null() {
            var result = Generator.Factory.Guid();
            Assert.IsNotNull(result);
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
        public void Incrementer_No_Arg() {
            var result = Generator
                .Factory
                .Incrementer()
                .Take(500);
            var expected = Enumerable.Range(0, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_Start_Below_Int_MinValue_Throws() {
            var start = int.MinValue;
            var result = Generator
                .Factory
                .Incrementer(start - 1)
                .Take(1);
            Assert.Throws<OverflowException>(() => result.ToArray());
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
        public void Incrementer_Start_Minus_Twenty() {
            var result = Generator
                .Factory
                .Incrementer(-20)
                .Take(500);
            var expected = Enumerable.Range(-20, 500);
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