﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeneratorAPI;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.GeneratorAPI {
    [TestFixture]
    internal class GeneratorTests {
        [SetUp]
        public void Initiate() {
            var randomizer = new Randomizer(Seed);
            _generator = new Generator<string>(randomizer.GetString);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private Generator<string> _generator;
        private const int Seed = 100;

        [Test(
            Description = "Verifies that Generator.Create uses same instance"
        )]
        public void Create_Use_Same_Instance() {
            var generator = Generator.Create(new Randomizer());
            Assert.AreSame(generator.Take(), generator.Take());
        }

        [Test(
            Description = "Verifies that Generator.Create uses same instance"
        )]
        public void Constructor_Not_Same_Instance() {
            var generator = new Generator<Random>(() => new Random());
            Assert.AreNotSame(generator.Take(), generator.Take());
        }

        [Test(
            Description = "Check to see that constructor throw exception when null is used"
        )]
        public void Constructor_Throw_Exception_When_Null() {
            Assert.Throws<ArgumentNullException>(() => new Generator<string>(null), "Argument cannot be null");
        }

        [Test(
            Description = "Check that Do gets various elements and not the same element"
        )]
        public void Do_Gets_Various_Elements() {
            var container = new List<string>();
            var result = _generator
                .Do(container.Add)
                .Take(10);
            var randomizer = new Randomizer(Seed);
            var expected = new Generator<string>(() => randomizer.GetString())
                .Take(10);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Checks that the Action is only invoked if take is called"
        )]
        public void Do_Is_Invoked_After_Take_Is_invoked() {
            string result = null;
            _generator
                .Do(s => result = s)
                .Take();

            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Checks that the Action is only invoked if take is called"
        )]
        public void Do_Is_Not_Invoked_Before_Take_Is_Invoked() {
            string result = null;
            _generator.Do(s => result = s);
            Assert.IsNull(result);
        }

        [Test(
            Description = "Check that Do throws exception if the Action<T> is null"
        )]
        public void Do_Null_Argument() {
            Assert.Throws<ArgumentNullException>(() => _generator.Do(null));
        }

        [Test(
            Description = "Check that Select does not return null"
        )]
        public void Select_Does_Not_Return_Null() {
            var result = _generator.Select(s => s.Length);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Checks that the Func is only invoked if take is called"
        )]
        public void Select_Is_Invoked_After_Take_Is_Invoked() {
            string temp = null;
            _generator
                .Select(s => temp = s)
                .Take();
            Assert.IsNotNull(temp);
        }

        [Test(
            Description = "Checks that the Func is only invoked if take is called"
        )]
        public void Select_Is_Not_Invoked_Before_Take_Is_Invoked() {
            string temp = null;
            _generator
                .Select(s => temp = s);
            Assert.IsNull(temp);
        }

        [Test(
            Description = "Check that passing null does not work"
        )]
        public void Select_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Select<string>(null));
        }

        [Test(
            Description = "Check that Select works like extension method Select On IEnumerable<T>"
        )]
        public void Select_String_Length() {
            var randomizer = new Randomizer(Seed);
            const int count = 10;
            var result = _generator
                .Select(s => s.Length)
                .Take(count);
            var expected = Enumerable
                .Range(0, count)
                .Select(i => randomizer.GetString())
                .Select(s => s.Length);
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Check if nested generation using argument with composer can be flattened"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Flatten_Nested_Generation() {
            var number = 0;
            var result = _generator
                .SelectMany(s => new Generator<string>(() => s + number++), (s, s1) => s + s1)
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
            Description = "Check that passing null to both arguments will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_Both_Arg() {
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany<string, string>(null, null));
        }

        [Test(
            Description = "Check that passing null to first argument will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_First_Arg() {
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany<string, string>(null, (s, s1) => s + s1));
        }

        [Test(
            Description = "Check that passing null to second argument will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_Second_Arg() {
            var number = 0;
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, string>(s => new Generator<string>(() => s + number++), null)
            );
        }

        [Test(
            Description = "Checks that the Func is only invoked if take is called"
        )]
        public void SelectMany_Is_Invoked_After_Take_Is_Invoked() {
            string result = null;
            _generator
                .SelectMany(s => new Generator<string>(() => result = s), (s, s1) => s + s1)
                .Take();
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Checks that the Func is only invoked if take is called"
        )]
        public void SelectMany_Is_Not_Invoked_Before_Take_Is_Invoked() {
            string result = null;
            _generator
                .SelectMany(s => new Generator<string>(() => result = s), (s, s1) => s + s1);
            Assert.IsNull(result);
        }

        [Test(
            Description = "Check that passing null does not work"
        )]
        public void SelectMany_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany<string>(null));
        }

        [Test(
            Description = "Check if nested generation can be flattened"
        )]
        public void SelectMany_Single_Arg_Flatten_Nested_Generation() {
            var randomizer = new Randomizer(Seed);
            //Nested Generation
            var generation = new Generator<Generator<string>>(
                () => new Generator<string>(
                    () => randomizer.GetString()
                )
            );
            var result = generation.SelectMany(g => g);
            var takeResult = result.Take(10);
            var takeExpected = _generator.Take(10);

            Assert.AreEqual(takeExpected, takeResult);
        }

        [Test(
            Description = "Check if nested generation using argument with composer can be flattened"
        )]
        public void SelectMany_Single_Arg_Using_Arg_Flatten_Nested_Generation() {
            var number = 0;
            var result = _generator
                .SelectMany(s => new Generator<string>(() => s + number++))
                .Take(10);
            var randomizer = new Randomizer(Seed);

            var expected = Enumerable
                .Range(0, 10)
                .Select(i => randomizer.GetString() + i);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Check that Take gives the expected ammount of elements"
        )]
        public void Take_Gives_Expected_Ammount() {
            const int count = 10;
            var result = _generator.Take(count);
            Assert.AreEqual(count, result.Count());
        }

        [Test(
            Description = "Check that Take without parameter gives expected result"
        )]
        public void Take_Gives_Expected_Elements() {
            const int count = 10;
            var result = _generator.Take(count);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable.Range(0, count).Select(i => randomizer.GetString());
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Check that take with a parameter does not return null"
        )]
        public void Take_Is_Not_Null() {
            var result = _generator.Take(10);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Check that Take without parameter gives expected result"
        )]
        public void Take_No_Param_Gives_Expected_Element() {
            var result = _generator.Take();
            var expected = new Randomizer(Seed).GetString();
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Check that take without a parameter does not return null"
        )]
        public void Take_No_Param_Is_Not_Null() {
            var result = _generator.Take();
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Check that where does not return null"
        )]
        public void Where_Does_Not_Return_Null() {
            var result = _generator.Where(s => s.Length > 1);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Checks that the Func is only invoked if take is called"
        )]
        public void Where_Is_Invoked_After_Take_Is_Invoked() {
            string result = null;
            _generator.Where(s => {
                result = s;
                return true;
            });
            Assert.IsNull(result);
        }

        [Test(
            Description = "Checks that the Func is only invoked if take is called"
        )]
        public void Where_Is_Not_Invoked_Before_Take_Is_Invoked() {
            string result = null;
            _generator.Where(s => {
                result = s;
                return true;
            }).Take();
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Check to see that Where is required for the assertion to succeed"
        )]
        public void Where_Is_Required_For_String_Contains_Letter_A() {
            const int count = 10;
            var result = _generator
                .Take(count)
                .ToArray();
            Assert.AreEqual(false, result.All(s => s.Contains("A")));
        }

        [Test(
            Description = "Check that passing null does not work"
        )]
        public void Where_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Where(null));
        }

        [Test(
            Description = "Check to see that where only returns data fiting the predicate"
        )]
        public void Where_String_Contains_Letter_A() {
            const int count = 10;
            var result = _generator
                .Where(s => s.Contains("A"))
                .Take(count)
                .ToArray();
            Assert.AreEqual(true, result.All(s => s.Contains("A")));
        }

        [Test(
            Description = "Check that Zip does not return null"
        )]
        public void Zip_Does_Not_Return_Null() {
            var resultRandomizer = new Randomizer(Seed);
            var resultGeneration = new Generator<int>(() => resultRandomizer.Next());
            var result = _generator
                .Zip(resultGeneration, (s, i) => s + i)
                .Take(100);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Check if Generations of string and number can be ziped together"
        )]
        public void Zip_Int_String() {
            var resultRandomizer = new Randomizer(Seed);
            var resultGeneration = new Generator<int>(() => resultRandomizer.Next());
            var result = _generator
                .Zip(resultGeneration, (s, i) => s + i)
                .Take(100);

            var expectedRandomizerOne = new Randomizer(Seed);
            var expectedRandomizerTwo = new Randomizer(Seed);
            var expected = new Generator<string>(
                () => expectedRandomizerOne.GetString() + expectedRandomizerTwo.Next()
            ).Take(100);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Checks that the Func is only invoked if take is called"
        )]
        public void Zip_Is_Invoked_After_Take_Is_Invoked() {
            string result = null;
            var randomizer = new Randomizer(Seed);
            var generation = new Generator<int>(() => randomizer.Next());
            _generator.Zip(generation, (s, i) => result = s + i).Take();
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Checks that the Func is only invoked if take is called"
        )]
        public void Zip_Is_Not_Invoked_Before_Take_Is_Invoked() {
            string result = null;
            var randomizer = new Randomizer(Seed);
            var generation = new Generator<int>(() => randomizer.Next());
            _generator.Zip(generation, (s, i) => result = s + i);
            Assert.IsNull(result);
        }

        [Test(
            Description = "Check that passing null for both argument does not work"
        )]
        public void Zip_Null_Both_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Zip<string, int>(null, null));
        }

        [Test(
            Description = "Check that passing null for first argument does not work"
        )]
        public void Zip_Null_First_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Zip<string, int>(null, (s, i) => s + i));
        }

        [Test(
            Description = "Check that passing null for second does not work"
        )]
        public void Zip_Null_Second_Param_Throws() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.Zip<string, int>(new Generator<int>(() => 1), null));
        }
    }
}