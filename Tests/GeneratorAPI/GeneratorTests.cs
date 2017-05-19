using System;
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
            _generator = Generator.Create(randomizer.GetString);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<string> _generator;
        private const int Seed = 100;

        [Test(
            Description = "Verifies that Generator.Create uses same instance"
        )]
        public void Constructor_Not_Same_Instance() {
            var generator = Generator.Create(() => new Random());
            Assert.AreNotSame(generator.Take(), generator.Take());
        }

        [Test(
            Description = "Verify to see that constructor throw exception when null is used"
        )]
        public void Constructor_Throw_Exception_When_Null() {
            Assert.Throws<ArgumentNullException>(() => Generator.Create<string>(null), "Argument cannot be null");
        }

        [Test(
            Description = "Verify that Create is lazy"
        )]
        public void Create_Lazy_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            Generator.Create(() => {
                invoked = true;
                return new Randomizer();
            }).Take();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that Create is lazy"
        )]
        public void Create_Lazy_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            Generator.Create(() => {
                invoked = true;
                return new Randomizer();
            });
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verifies that Generator.Create uses same instance"
        )]
        public void Create_Lazy_Use_Same_Instance() {
            var generator = Generator.CreateLazy(() => new Randomizer());
            Assert.AreSame(generator.Take(), generator.Take());
        }

        [Test(
            Description = "Verifies that Generator.Create uses same instance"
        )]
        public void Create_Use_Same_Instance() {
            var generator = Generator.CreateWithProvider(new Randomizer());
            Assert.AreSame(generator.Take(), generator.Take());
        }

        [Test(
            Description = "Verify that Do gets various elements and not the same element"
        )]
        public void Do_Gets_Various_Elements() {
            var container = new List<string>();
            var result = _generator
                .Do(container.Add)
                .Take(10);
            var randomizer = new Randomizer(Seed);
            var expected = Generator.Create(() => randomizer.GetString())
                .Take(10);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verifys that the Action is only invoked if take is called"
        )]
        public void Do_Is_Invoked_After_Take_Is_invoked() {
            var invoked = false;
            _generator
                .Do(s => invoked = true)
                .Take();

            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verifys that the Action is only invoked if take is called"
        )]
        public void Do_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            _generator.Do(s => invoked = true);
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that Do throws exception if the Action<T> is null"
        )]
        public void Do_Null_Argument() {
            Assert.Throws<ArgumentNullException>(() => _generator.Do(null));
        }

        [Test(
            Description = "Verify that Select does not return null"
        )]
        public void Select_Does_Not_Return_Null() {
            var result = _generator.Select(s => s.Length);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if take is called"
        )]
        public void Select_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            _generator
                .Select(s => invoked = true)
                .Take();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if take is called"
        )]
        public void Select_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            _generator
                .Select(s => invoked = true);
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that passing null does not work"
        )]
        public void Select_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Select<string, string>(null));
        }

        [Test(
            Description = "Verify that Select works like extension method Select On IEnumerable<T>"
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
            Description = "Verify if nested generation using argument with composer can be flattened"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Flatten_Nested_Generation() {
            var number = 0;
            var result = _generator
                .SelectMany(s => Generator.Create(() => s + number++), (s, s1) => s + s1)
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
            Description = "Verify that passing null to both arguments will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_Both_Arg() {
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany<string, string, string>(null, null));
        }

        [Test(
            Description = "Verify that passing null to first argument will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_First_Arg() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, string, string>(null, (s, s1) => s + s1));
        }

        [Test(
            Description = "Verify that passing null to second argument will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_Second_Arg() {
            var number = 0;
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, string, string>(s => Generator.Create(() => s + number++),
                    null)
            );
        }

        [Test(
            Description = "Verifys that the Func is only invoked if take is called"
        )]
        public void SelectMany_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            _generator
                .SelectMany(s => Generator.Create(() => invoked = true), (s, s1) => s + s1)
                .Take();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if take is called"
        )]
        public void SelectMany_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            _generator
                .SelectMany(s => Generator.Create(() => invoked = true), (s, s1) => s + s1);
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that passing null does not work"
        )]
        public void SelectMany_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany<string, string>(null));
        }

        [Test(
            Description = "Verify if nested generation can be flattened"
        )]
        public void SelectMany_Single_Arg_Flatten_Nested_Generation() {
            var randomizer = new Randomizer(Seed);
            //Nested Generation
            var generation = Generator.Create(
                () => Generator.Create(
                    () => randomizer.GetString()
                )
            );
            var result = generation.SelectMany(g => g);
            var takeResult = result.Take(10);
            var takeExpected = _generator.Take(10);

            Assert.AreEqual(takeExpected, takeResult);
        }

        [Test(
            Description = "Verify if nested generation using argument with composer can be flattened"
        )]
        public void SelectMany_Single_Arg_Using_Arg_Flatten_Nested_Generation() {
            var number = 0;
            var result = _generator
                .SelectMany(s => Generator.Create(() => s + number++))
                .Take(10);
            var randomizer = new Randomizer(Seed);

            var expected = Enumerable
                .Range(0, 10)
                .Select(i => randomizer.GetString() + i);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that take with negative argument throws exception"
        )]
        public void Take_Arg_Negative() {
            Assert.Throws<ArgumentException>(() => _generator.Take(-1));
        }

        [Test(
            Description = "Verify that take with argument zero throws exception"
        )]
        public void Take_Arg_Zero() {
            Assert.Throws<ArgumentException>(() => _generator.Take(0));
        }

        [Test(
            Description = "Verify that Take gives the expected ammount of elements"
        )]
        public void Take_Gives_Expected_Ammount() {
            const int count = 10;
            var result = _generator.Take(count);
            Assert.AreEqual(count, result.Count());
        }

        [Test(
            Description = "Verify that Take without parameter gives expected result"
        )]
        public void Take_Gives_Expected_Elements() {
            const int count = 10;
            var result = _generator.Take(count);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable.Range(0, count).Select(i => randomizer.GetString());
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that take with a parameter does not return null"
        )]
        public void Take_Is_Not_Null() {
            var result = _generator.Take(10);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verify that Take without parameter gives expected result"
        )]
        public void Take_No_Param_Gives_Expected_Element() {
            var result = _generator.Take();
            var expected = new Randomizer(Seed).GetString();
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that take without a parameter does not return null"
        )]
        public void Take_No_Param_Is_Not_Null() {
            var result = _generator.Take();
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verify that where does not return null"
        )]
        public void Where_Does_Not_Return_Null() {
            var result = _generator.Where(s => s.Length > 1);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if take is called"
        )]
        public void Where_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            _generator.Where(s => {
                invoked = true;
                return true;
            });
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if take is called"
        )]
        public void Where_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            _generator.Where(s => {
                invoked = true;
                return true;
            }).Take();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify to see that Where is required for the assertion to succeed"
        )]
        public void Where_Is_Required_For_String_Contains_Letter_A() {
            const int count = 10;
            var result = _generator
                .Take(count)
                .ToArray();
            Assert.AreEqual(false, result.All(s => s.Contains("A")));
        }

        [Test(
            Description = "Verify that passing null does not work"
        )]
        public void Where_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Where(null));
        }

        [Test(
            Description = "Verify to see that where only returns data fiting the predicate"
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
            Description = "Verify that Zip does not return null"
        )]
        public void Zip_Does_Not_Return_Null() {
            var resultRandomizer = new Randomizer(Seed);
            var resultGeneration = Generator.Create(() => resultRandomizer.Next());
            var result = _generator
                .Zip(resultGeneration, (s, i) => s + i)
                .Take(100);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verify if Generations of string and number can be ziped together"
        )]
        public void Zip_Int_String() {
            var resultRandomizer = new Randomizer(Seed);
            var resultGeneration = Generator.Create(() => resultRandomizer.Next());
            var result = _generator
                .Zip(resultGeneration, (s, i) => s + i)
                .Take(100);

            var expectedRandomizerOne = new Randomizer(Seed);
            var expectedRandomizerTwo = new Randomizer(Seed);
            var expected = Generator.Create(
                () => expectedRandomizerOne.GetString() + expectedRandomizerTwo.Next()
            ).Take(100);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if take is called"
        )]
        public void Zip_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            var randomizer = new Randomizer(Seed);
            var generation = Generator.Create(() => randomizer.Next());
            _generator.Zip(generation, (s, i) => invoked = true).Take();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if take is called"
        )]
        public void Zip_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            var randomizer = new Randomizer(Seed);
            var generation = Generator.Create(() => randomizer.Next());
            _generator.Zip(generation, (s, i) => invoked = true);
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that passing null for both argument does not work"
        )]
        public void Zip_Null_Both_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Zip<string, int, string>(null, null));
        }

        [Test(
            Description = "Verify that passing null for first argument does not work"
        )]
        public void Zip_Null_First_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Zip<string, int, string>(null, (s, i) => s + i));
        }

        [Test(
            Description = "Verify that passing null for second does not work"
        )]
        public void Zip_Null_Second_Param_Throws() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.Zip<string, int, string>(Generator.Create(() => 1), null));
        }
    }
}