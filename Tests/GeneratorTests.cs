using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Types;

//Todo set a seed and let all tests be ran from that seed so i can expect values...

namespace Tests {
    /// <summary>
    ///     These tests are all used with a seed so the result are always the same.
    /// </summary>
    [TestFixture]
    public class GeneratorTests {
        private const int Seed = 100;

        [Test]
        public void Seed_With_Bools() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the bools expected
            var generator = new Generator<bool>(randomizer => randomizer.Bool());
            generator.Seed(Seed);
            var random = new Random(Seed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(2) != 0);
            var result = generator.GenerateEnumerable(1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }


        [Test]
        public void Seed_With_Number() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int limit = 100;
            var generator = new Generator<int>(randomizer => randomizer.Number(limit));
            generator.Seed(Seed);
            var random = new Random(Seed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(limit));
            var result = generator.GenerateEnumerable(1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NamesAreNotNull() {
            var generator = new Generator<string>(randomizer => randomizer.Name());
            var strings = generator.GenerateEnumerable(20).ToArray();
            Assert.IsFalse(strings.All(string.IsNullOrEmpty));
            Assert.IsFalse(strings.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void IteratorWithEnumerable() {
            var iteration = 0;
            var generator = new Generator<bool>((randomizer, i) => iteration++ == i);
            var result = generator.GenerateEnumerable(20).ToArray();
            Assert.IsTrue(result.All(b => b));
        }

        [Test]
        public void IteratorWithGenerate() {
            var iteration = 0;
            var generator = new Generator<bool>((randomizer, i) => iteration++ == i);
            for (var i = 0; i < 10; i++)
                Assert.IsTrue(generator.Generate());
        }
    }
}