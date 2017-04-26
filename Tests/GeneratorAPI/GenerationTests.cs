using System;
using System.Linq;
using GeneratorAPI;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.GeneratorAPI {
    [TestFixture]
    internal class GenerationTests {
        [SetUp]
        public void Initiate() {
            var randomizer = new Randomizer(Seed);
            _generation = new Generation<string>(randomizer.GetString);
        }

        [TearDown]
        public void Dispose() {
            _generation = null;
        }

        private Generation<string> _generation;
        private const int Seed = 100;

        [Test(
            Author = "Robert",
            Description = "Check to see that constructor throw exception when null is used"
        )]
        public void Constructor_Throw_Exception_When_Null() {
            Assert.Throws<ArgumentNullException>(() => new Generation<string>(null), "Argument cannot be null");
        }

        [Test(
            Author = "Robert",
            Description = "Check that Select does not return null"
        )]
        public void Select_Does_Not_Return_Null() {
            var result = _generation.Select(s => s.Length);
            Assert.IsNotNull(result);
        }

        [Test(
            Author = "Robert",
            Description = "Check that Select works like extension method Select On IEnumerable<T>"
        )]
        public void Select_String_Length() {
            var randomizer = new Randomizer(Seed);
            const int count = 10;
            var result = _generation
                .Select(s => s.Length)
                .Take(count);
            var expected = Enumerable
                .Range(0, count)
                .Select(i => randomizer.GetString())
                .Select(s => s.Length);
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "Check that Take gives the expected ammount of elements"
        )]
        public void Take_Gives_Expected_Ammount() {
            const int count = 10;
            var result = _generation.Take(count);
            Assert.AreEqual(count, result.Count());
        }

        [Test(
            Author = "Robert",
            Description = "Check that Take without parameter gives expected result"
        )]
        public void Take_Gives_Expected_Element() {
            const int count = 10;
            var result = _generation.Take(count);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable.Range(0, count).Select(i => randomizer.GetString());
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "Check that take with a parameter does not return null"
        )]
        public void Take_Is_Not_Null() {
            var result = _generation.Take(10);
            Assert.IsNotNull(result);
        }

        [Test(
            Author = "Robert",
            Description = "Check that Take without parameter gives expected result"
        )]
        public void Take_No_Param_Gives_Expected_Element() {
            var result = _generation.Take();
            var expected = new Randomizer(Seed).GetString();
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "Check that take without a parameter does not return null"
        )]
        public void Take_No_Param_Is_Not_Null() {
            var result = _generation.Take();
            Assert.IsNotNull(result);
        }

        [Test(
            Author = "Robert",
            Description = "Check that where does not return null"
        )]
        public void Where_Does_Not_Return_Null() {
            var result = _generation.Where(s => s.Length > 1);
            Assert.IsNotNull(result);
        }

        [Test(
            Author = "Robert",
            Description = "Check to see that Where is required for the assertion to succeed"
        )]
        public void Where_Is_Required_For_String_Contains_Letter_A() {
            const int count = 10;
            var result = _generation
                .Take(count)
                .ToArray();
            Assert.AreEqual(false, result.All(s => s.Contains("A")));
        }

        [Test(
            Author = "Robert",
            Description = "Check to see that where only returns data fiting the predicate"
        )]
        public void Where_String_Contains_Letter_A() {
            const int count = 10;
            var result = _generation
                .Where(s => s.Contains("A"))
                .Take(count)
                .ToArray();
            Assert.AreEqual(true, result.All(s => s.Contains("A")));
        }

        [Test(
            Author = "Robert",
            Description = "Check that Zip does not return null"
        )]
        public void Zip_Does_Not_Return_Null() {
            var resultRandomizer = new Randomizer(Seed);
            var resultGeneration = new Generation<int>(() => resultRandomizer.Next());
            var result = _generation
                .Zip(resultGeneration, (s, i) => s + i)
                .Take(100);
            Assert.IsNotNull(result);
        }

        [Test(
            Author = "Robert",
            Description = "Check if Generations of string and number can be ziped together"
        )]
        public void Zip_Int_String() {
            var resultRandomizer = new Randomizer(Seed);
            var resultGeneration = new Generation<int>(() => resultRandomizer.Next());
            var result = _generation
                .Zip(resultGeneration, (s, i) => s + i)
                .Take(100);

            var expectedRandomizerOne = new Randomizer(Seed);
            var expectedRandomizerTwo = new Randomizer(Seed);
            var expected = new Generation<string>(
                () => expectedRandomizerOne.GetString() + expectedRandomizerTwo.Next()
            ).Take(100);

            Assert.AreEqual(expected, result);
        }
    }
}