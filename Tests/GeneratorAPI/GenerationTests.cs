using System;
using System.Collections.Generic;
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
            Description = "Check that Do gets various elements and not the same element"
        )]
        public void Do_Gets_Various_Elements() {
            var container = new List<string>();
            var result = _generation
                .Do(container.Add)
                .Take(10);
            var randomizer = new Randomizer(Seed);
            var expected = new Generation<string>(() => randomizer.GetString())
                .Take(10);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "Do should be invoked."
        )]
        public void Do_Is_Invoked_After_Take_Is_invoked() {
            string result = null;
            _generation
                .Do(s => result = s)
                .Take();

            Assert.IsNotNull(result);
        }

        [Test(
            Author = "Robert",
            Description = "Do should not do anything."
        )]
        public void Do_Is_Not_Invoked_Before_Take_Is_Invoked() {
            string result = null;
            _generation.Do(s => result = s);
            Assert.IsNull(result);
        }

        [Test(
            Author = "Robert",
            Description = "Check that Do throws exception if the Action<T> is null"
        )]
        public void Do_Null_Argument() {
            Assert.Throws<ArgumentNullException>(() => _generation.Do(null));
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
            Description = "Checks that the select Func is only invoked if take is called"
        )]
        public void Select_Is_Invoked_After_Take_Is_Invoked() {
            string temp = null;
            _generation
                .Select(s => temp = s)
                .Take();
            Assert.IsNotNull(temp);
        }

        [Test(
            Author = "Robert",
            Description = "Checks that the select Func is only invoked if take is called"
        )]
        public void Select_Is_Not_Invoked_Before_Take_Is_Invoked() {
            string temp = null;
            _generation
                .Select(s => temp = s);
            Assert.IsNull(temp);
        }

        [Test(
            Author = "Robert",
            Description = "Check that passing null does not work"
        )]
        public void Select_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generation.Select<string>(null));
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
            Description = "Check if nested generation using argument with composer can be flattened"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Flatten_Nested_Generation() {
            var number = 0;
            var result = _generation
                .SelectMany(s => new Generation<string>(() => s + number++), (s, s1) => s + s1)
                .Take(10);
            var randomizer = new Randomizer(Seed);

            var expected = Enumerable
                .Range(0, 10)
                .Select(i => {
                    var txt = randomizer.GetString();
                    return txt + txt + i;
                });

            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "Check that passing null to both arguments will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_Both_Arg() {
            Assert.Throws<ArgumentNullException>(() => _generation.SelectMany<string, string>(null, null));
        }

        [Test(
            Author = "Robert",
            Description = "Check that passing null to first argument will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_First_Arg() {
            Assert.Throws<ArgumentNullException>(() => _generation.SelectMany<string, string>(null, (s, s1) => s + s1));
        }

        [Test(
            Author = "Robert",
            Description = "Check that passing null to second argument will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_Second_Arg() {
            var number = 0;
            Assert.Throws<ArgumentNullException>(
                () => _generation.SelectMany<string, string>(s => new Generation<string>(() => s + number++), null)
            );
        }

        [Test(
            Author = "Robert",
            Description = "Check that passing null does not work"
        )]
        public void SelectMany_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generation.SelectMany<string>(null));
        }

        [Test(
            Author = "Robert",
            Description = "Check if nested generation can be flattened"
        )]
        public void SelectMany_Single_Arg_Flatten_Nested_Generation() {
            var randomizer = new Randomizer(Seed);
            //Nested Generation
            var generation = new Generation<Generation<string>>(
                () => new Generation<string>(
                    () => randomizer.GetString()
                )
            );
            var result = generation.SelectMany(g => g);
            var takeResult = result.Take(10);
            var takeExpected = _generation.Take(10);

            Assert.AreEqual(takeExpected, takeResult);
        }

        [Test(
            Author = "Robert",
            Description = "Check if nested generation using argument with composer can be flattened"
        )]
        public void SelectMany_Single_Arg_Using_Arg_Flatten_Nested_Generation() {
            var number = 0;
            var result = _generation
                .SelectMany(s => new Generation<string>(() => s + number++))
                .Take(10);
            var randomizer = new Randomizer(Seed);

            var expected = Enumerable
                .Range(0, 10)
                .Select(i => randomizer.GetString() + i);

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
        public void Take_Gives_Expected_Elements() {
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
            Description = "Check that passing null does not work"
        )]
        public void Where_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generation.Where(null));
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

        [Test(
            Author = "Robert",
            Description = "Check that passing null for both argument does not work"
        )]
        public void Zip_Null_Both_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generation.Zip<string, int>(null, null));
        }

        [Test(
            Author = "Robert",
            Description = "Check that passing null for first argument does not work"
        )]
        public void Zip_Null_First_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generation.Zip<string, int>(null, (s, i) => s + i));
        }

        [Test(
            Author = "Robert",
            Description = "Check that passing null for second does not work"
        )]
        public void Zip_Null_Second_Param_Throws() {
            Assert.Throws<ArgumentNullException>(
                () => _generation.Zip<string, int>(new Generation<int>(() => 1), null));
        }
    }
}