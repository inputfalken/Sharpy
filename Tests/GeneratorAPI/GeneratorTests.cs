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
            _generator = Generator.Function(randomizer.GetString);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<string> _generator;
        private const int Seed = 100;

        [Test(
            Description = "Verify that lazy is not used before generate is invoked"
        )]
        public void Create_Lazy_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            Generator.Lazy(() => {
                invoked = true;
                return new Randomizer();
            }).Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that lazy is not used before generate is invoked"
        )]
        public void Create_Lazy_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            Generator.Lazy(() => {
                invoked = true;
                return new Randomizer();
            });
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that Generator.Lazy uses same instance"
        )]
        public void Create_Lazy_Use_Same_Instance() {
            var generator = Generator.Lazy(() => new Randomizer());
            Assert.AreSame(generator.Generate(), generator.Generate());
        }

        [Test(
            Description = "Verifies that Generator.Create uses same instance"
        )]
        public void Create_Use_Same_Instance() {
            var generator = Generator.Create(new Randomizer());
            Assert.AreSame(generator.Generate(), generator.Generate());
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
            var expected = Generator.Function(() => randomizer.GetString())
                .Take(10);

            Assert.AreEqual(expected, result);
        }


        [Test(
            Description = "Verify that the Action is only invoked if Generate is called"
        )]
        public void Do_Is_Invoked_After_Take_Is_invoked() {
            var invoked = false;
            _generator
                .Do(s => invoked = true)
                .Generate();

            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that the Action is only invoked if Generate is called"
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
            Description = "Verify that Do with null Generator and argument throws exception"
        )]
        public void Do_Null_Generator_And_Arg_Throws() {
            IGenerator<string> x = null;
            Assert.Throws<ArgumentNullException>(() => x.Do(null));
        }

        [Test(
            Description = "Verify that Do with null Generator throws exception"
        )]
        public void Do_Null_Generator_Throws() {
            IGenerator<string> x = null;
            Assert.Throws<ArgumentNullException>(() => x.Do(s => { }));
        }

        [Test(
            Description = "Verify that Generator.Function does not use the same instance"
        )]
        public void Function_Not_Same_Instance() {
            var generator = Generator.Function(() => new Random());
            Assert.AreNotSame(generator.Generate(), generator.Generate());
        }

        [Test(
            Description = "Verify that passing null to Function throws exception"
        )]
        public void Function_Throw_Exception_When_Null() {
            Assert.Throws<ArgumentNullException>(() => Generator.Function<string>(null), "Argument cannot be null");
        }

        [Test(
            Description = "Verify that Select does not return null"
        )]
        public void Select_Does_Not_Return_Null() {
            var result = _generator.Select(s => s.Length);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void Select_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            _generator
                .Select(s => invoked = true)
                .Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void Select_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            _generator
                .Select(s => invoked = true);
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that Select with null Generator and Argument throws exception"
        )]
        public void Select_Null_Generator_And_Arg_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Select<string, int>(null));
        }

        [Test(
            Description = "Verify that Select with null Generator throws"
        )]
        public void Select_Null_Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Select(s => s.Length));
        }

        [Test(
            Description = "Verify that null Func throws exception"
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
            Description = "Verify that SelectMany double arg with null Generator and null first arg"
        )]
        public void SelectMany_Double_Arg_Null_Generator_And_Arg_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(
                () => generator.SelectMany<string, int, string>(generatorSelector: null, composer: (s, i) => s + i));
        }

        [Test(
            Description = "Verify that SelectMany double arg with null Generator and null on args"
        )]
        public void SelectMany_Double_Arg_Null_Generator_And_Null_Args_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(
                () => generator.SelectMany<string, int, string>(generatorSelector: null, composer: null));
        }

        [Test(
            Description = "Verify that SelectMany double arg with null Generator throws"
        )]
        public void SelectMany_Double_Arg_Null_Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(
                () => generator.SelectMany(s => Generator.Create(s.Length), (s, i) => s + i));
        }

        [Test(
            Description = "Verify if nested generation using argument with composer can be flattened"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Flatten_Nested_Generation() {
            var number = 0;
            var result = _generator
                .SelectMany(s => Generator.Function(() => s + number++), (s, s1) => s + s1)
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
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, string, string>(generatorSelector: null, composer: null));
        }

        [Test(
            Description = "Verify that passing null to first argument will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_First_Arg() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, string, string>(generatorSelector: null,
                    composer: (s, s1) => s + s1));
        }

        [Test(
            Description = "Verify that passing null to second argument will throw exception"
        )]
        public void SelectMany_Double_Arg_Using_Arg_Null_Second_Arg() {
            var number = 0;
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, string, string>(s => Generator.Function(() => s + number++),
                    null)
            );
        }

        [Test(
            Description = "Verify that the randomized number changes when enumerable restarts"
        )]
        public void SelectMany_IEnumerable_Argument_Changes() {
            var result = Generator
                .Create(new Randomizer(Seed))
                .Select(rnd => rnd.Next(1, 100))
                //randomized number changes Enumerable repeats
                .SelectMany(randomizedNumer => Enumerable.Range(1, 5).Select(i => i + randomizedNumer))
                .Take(10);
            var randomizer = new Randomizer(Seed);
            var rndRes1 = randomizer.Next(1, 100);
            var rndRes2 = randomizer.Next(1, 100);
            var enumerable = Enumerable.Range(1, 5).Select(i => i + rndRes1)
                .Concat(Enumerable.Range(1, 5).Select(i => i + rndRes2));

            Assert.AreEqual(enumerable, result);
        }


        [Test(
            Description = "Verify that the randomized number changes when enumerable restarts"
        )]
        public void SelectMany_IEnumerable_Composer_Argument_Changes() {
            var result = Generator
                .Create(new Randomizer(Seed))
                .Select(rnd => rnd.Next(1, 100))
                //randomized number changes Enumerable repeats
                .SelectMany(randomizedNumer => Enumerable.Range(1, 5), (i, i1) => i + i1)
                .Take(10);
            var randomizer = new Randomizer(Seed);
            var rndRes1 = randomizer.Next(1, 100);
            var rndRes2 = randomizer.Next(1, 100);
            var enumerable = Enumerable.Range(1, 5).Select(i => i + rndRes1)
                .Concat(Enumerable.Range(1, 5).Select(i => i + rndRes2));

            Assert.AreEqual(enumerable, result);
        }

        [Test(
            Description = "Verify that null composer throws exception"
        )]
        public void SelectMany_IEnumerable_Composer_Null_Composer() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, int, string>(s => Enumerable.Range(0, 10), null));
        }

        [Test(
            Description = "Verify that both null composer and selector throws exception"
        )]
        public void SelectMany_IEnumerable_Composer_Null_Composer_And_Selector() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, int, string>(enumerableSelector: null, composer: null));
        }

        [Test(
            Description = "Verify that null Selector throws exception"
        )]
        public void SelectMany_IEnumerable_Composer_Null_Selector() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, int, string>(enumerableSelector: null, composer: (s, i) => s + i));
        }

        [Test]
        public void SelectMany_IEnumerable_Composer_Using_Randomizer() {
            var result = Generator
                .Create(new Randomizer(Seed))
                .SelectMany(rnd => Enumerable.Range(1, 10), (rnd, i) => i + rnd.Next(1, 100))
                .Take(10);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable
                .Range(1, 10)
                .Select(i => i + randomizer.Next(1, 100));
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that Enumerable turns into CircularEnumerable"
        )]
        public void SelectMany_IEnumerable_Is_Circular() {
            var result = _generator.SelectMany(s => Enumerable.Range(1, 5)).Take(15);
            var expected = Enumerable.Range(1, 5)
                .Concat(Enumerable.Range(1, 5))
                .Concat(Enumerable.Range(1, 5));

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that null Func throws exception"
        )]
        public void SelectMany_IEnumerable_Null() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany<string, int>(enumerableSelector: null));
        }

        [Test(
            Description = "Verify that SelectMany works as the expected LINQ expression"
        )]
        public void SelectMany_IEnumerable_Using_Randomizer() {
            var result = Generator
                .Create(new Randomizer(Seed))
                .SelectMany(rnd => Enumerable.Range(1, 10).Select(i => i + rnd.Next(1, 100)))
                .Take(10);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable
                .Range(1, 10)
                .Select(i => i + randomizer.Next(1, 100));
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void SelectMany_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            _generator
                .SelectMany(s => Generator.Function(() => invoked = true), (s, s1) => s + s1)
                .Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void SelectMany_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            _generator
                .SelectMany(s => Generator.Function(() => invoked = true), (s, s1) => s + s1);
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that SelectMany with null Generator and Argument throws exception"
        )]
        public void SelectMany_Null_Generator_And_Arg_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.SelectMany<string, int>(generatorSelector: null));
        }

        [Test(
            Description = "Verify that SelectMany with null Generator throws exception"
        )]
        public void SelectMany_Null_Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(
                () => generator.SelectMany(s => Generator.Create(s.Length)));
        }

        [Test(
            Description = "Verify that passing null does not work and throws exception"
        )]
        public void SelectMany_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany<string, string>(generatorSelector: null));
        }

        [Test(
            Description = "Verify if nested generation can be flattened"
        )]
        public void SelectMany_Single_Arg_Flatten_Nested_Generation() {
            var randomizer = new Randomizer(Seed);
            //Nested Generation
            var generation = Generator.Function(
                () => Generator.Function(
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
                .SelectMany(s => Generator.Function(() => s + number++))
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
            Description = "Verify that Take gives expected result"
        )]
        public void Take_Gives_Expected_Elements() {
            const int count = 10;
            var result = _generator.Take(count);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable.Range(0, count).Select(i => randomizer.GetString());
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that take with  does not return null"
        )]
        public void Take_Is_Not_Null() {
            var result = _generator.Take(10);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verify that Take gives expected result"
        )]
        public void Take_No_Param_Gives_Expected_Element() {
            var result = _generator.Generate();
            var expected = new Randomizer(Seed).GetString();
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that take does not return null"
        )]
        public void Take_No_Param_Is_Not_Null() {
            var result = _generator.Generate();
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verify that ToArray with negative value throws ArgumentException"
        )]
        public void ToArray_Negative_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToArray(-1));
        }

        [Test(
            Description = "Verify that ToArray returns the expected elements"
        )]
        public void ToArray_Returns_Expected_Elements() {
            var result = _generator.ToArray(20);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable.Range(0, 20).Select(i => randomizer.GetString()).ToArray();
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that ToArray with zero value throws ArgumentException"
        )]
        public void ToArray_Zero_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToArray(0));
        }

        [Test(
            Description = "Verify that Null KeySelector throws ArgumetNullException"
        )]
        public void ToDictionary_KeySelector_Null_Throws() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.ToDictionary<string, string, int>(1, null, s => s.Length));
        }

        [Test(
            Description = "Verify that ToDictionary with negative value throws ArgumentException"
        )]
        public void ToDictionary_Negative_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToDictionary(-1, s => s, s => s.Length));
        }

        [Test(
            Description = "Verify that ToDictionary returns the expected elements"
        )]
        public void ToDictionary_Returns_Expected_Elements() {
            var result = _generator.ToDictionary(20, s => s, s => s.Length);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable.Range(0, 20)
                .Select(i => randomizer.GetString())
                .ToDictionary(s => s, s => s.Length);
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that ValueSelector and KeySelector as null  throws ArgumetNullException"
        )]
        public void ToDictionary_ValueSelector_And_KeySelector_Null_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.ToDictionary<string, string, int>(1, null, null));
        }

        [Test(
            Description = "Verify that Null ValueSelector throws ArgumetNullException"
        )]
        public void ToDictionary_ValueSelector_Null_Throws() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.ToDictionary<string, string, int>(1, s => s, null));
        }

        [Test(
            Description = "Verify that ToDictionary with zero value throws ArgumentException"
        )]
        public void ToDictionary_Zero_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToDictionary(0, s => s, s => s.Length));
        }

        [Test(
            Description = "Verify that ToList with negative value throws ArgumentException"
        )]
        public void ToList_Negative_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToList(-1));
        }

        [Test(
            Description = "Verify that ToList returns the expected elements"
        )]
        public void ToList_Returns_Expected_Elements() {
            var result = _generator.ToList(20);
            var randomizer = new Randomizer(Seed);
            var expected = Enumerable.Range(0, 20).Select(i => randomizer.GetString()).ToList();
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that ToList with zero value throws ArgumentException"
        )]
        public void ToList_Zero_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToList(0));
        }

        [Test(
            Description = "Verify that where does not return null"
        )]
        public void Where_Does_Not_Return_Null() {
            var result = _generator.Where(s => s.Length > 1);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
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
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void Where_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            _generator.Where(s => {
                invoked = true;
                return true;
            }).Generate();
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
            Description = "Verify that Where with null Generator and Argument throws exception"
        )]
        public void Where_Null_Generator_And_Arg_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Where(null));
        }

        [Test(
            Description = "Verify that Where with null Generator throws exception"
        )]
        public void Where_Null_Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Where(s => s.Length == 0));
        }

        [Test(
            Description = "Verify that passing null does not work and throws exception"
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
            var resultGeneration = Generator.Function(() => resultRandomizer.Next());
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
            var resultGeneration = Generator.Function(() => resultRandomizer.Next());
            var result = _generator
                .Zip(resultGeneration, (s, i) => s + i)
                .Take(100);

            var expectedRandomizerOne = new Randomizer(Seed);
            var expectedRandomizerTwo = new Randomizer(Seed);
            var expected = Generator.Function(
                () => expectedRandomizerOne.GetString() + expectedRandomizerTwo.Next()
            ).Take(100);

            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void Zip_Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            var randomizer = new Randomizer(Seed);
            var generation = Generator.Function(() => randomizer.Next());
            _generator.Zip(generation, (s, i) => invoked = true).Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void Zip_Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            var randomizer = new Randomizer(Seed);
            var generation = Generator.Function(() => randomizer.Next());
            _generator.Zip(generation, (s, i) => invoked = true);
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that Zip with null Generator and null first arg throws exception"
        )]
        public void Zip_Null__Generator_Null_First_Arg() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => { generator.Zip(_generator, (s, s1) => s + s1); });
        }

        [Test(
            Description = "Verify that Zip with null Generator and null second arg throws excpetion"
        )]
        public void Zip_Null__Generator_Null_Second_Arg() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(
                () => generator.Zip<string, string, string>(Generator.Function(() => ""), null));
        }

        [Test(
            Description = "Verify that Zip with null Generator throws exception"
        )]
        public void Zip_Null__Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Zip(Generator.Function(() => ""), (s, s1) => s + s1));
        }

        [Test(
            Description = "Verify that passing null for both argument does not work and throws exception"
        )]
        public void Zip_Null_Both_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Zip<string, int, string>(null, fn: null));
        }

        [Test(
            Description = "Verify that passing null for first argument does not work and throws exception"
        )]
        public void Zip_Null_First_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Zip<string, int, string>(null, fn: (s, i) => s + i));
        }

        [Test(
            Description = "Verify that Zip with null Generator and Arguments throws excpetion"
        )]
        public void Zip_Null_Generator_And_Args_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Zip<string, int, string>(null, fn: null));
        }

        [Test(
            Description = "Verify that passing null for second does not work and throws exception"
        )]
        public void Zip_Null_Second_Param_Throws() {
            Assert.Throws<ArgumentNullException>(
                () => _generator.Zip<string, int, string>(Generator.Function(() => 1), null));
        }
    }
}