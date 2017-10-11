using System;
using NUnit.Framework;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.Core.Tests.ExtensionTests {
    public class ZipTests {
        private IGenerator<int> _generator;

        [SetUp]
        public void Initiate() {
            _generator = Generator.Incrementer(0);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        [Test(
            Description = "Verify that Zip does not return null"
        )]
        public void Does_Not_Return_Null() {
            var second = Generator.Incrementer(0);
            var result = _generator
                .Zip(second, (s, i) => s + i);

            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verify if Generations of string and number can be ziped together"
        )]
        public void Int_Int() {
            var second = Generator.Incrementer(0);
            var result = _generator.Zip(second, (s, i) => s + i);

            Assert.AreEqual(0, result.Generate());
            Assert.AreEqual(2, result.Generate());
            Assert.AreEqual(4, result.Generate());
            Assert.AreEqual(6, result.Generate());
            Assert.AreEqual(8, result.Generate());
            Assert.AreEqual(10, result.Generate());
            Assert.AreEqual(12, result.Generate());
            Assert.AreEqual(14, result.Generate());
            Assert.AreEqual(16, result.Generate());
            Assert.AreEqual(18, result.Generate());
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void Is_Evaluated_After_Take_Is_Invoked() {
            var invoked = false;
            var second = Generator.Incrementer(0);
            var generator = _generator.Zip(second, (i, i1) => invoked = true);
            // Not evaluated
            Assert.IsFalse(invoked);
            // Evaluated
            generator.Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that Zip with null Generator and null first arg throws exception"
        )]
        public void Null__Generator_Null_First_Arg() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => { generator.Zip(_generator, (s, s1) => s + s1); });
        }

        [Test(
            Description = "Verify that Zip with null Generator and null second arg throws excpetion"
        )]
        public void Null__Generator_Null_Second_Arg() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(
                () => generator.Zip<string, string, string>(Generator.Function(() => ""), null));
        }

        [Test(
            Description = "Verify that Zip with null Generator throws exception"
        )]
        public void Null__Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Zip(Generator.Function(() => ""), (s, s1) => s + s1));
        }

        [Test(
            Description = "Verify that passing null for both argument does not work and throws exception"
        )]
        public void Null_Both_Param_Throws() {
            IGenerator<int> second = null;
            Func<int, int, int> resultSelector = null;
            Assert.Throws<ArgumentNullException>(() => _generator.Zip(second, resultSelector));
        }

        [Test(
            Description = "Verify that passing null for first argument does not work and throws exception"
        )]
        public void Null_First_Param_Throws() {
            IGenerator<int> second = null;
            Assert.Throws<ArgumentNullException>(
                () => _generator.Zip(second, (s, i) => s + i));
        }

        [Test(
            Description = "Verify that Zip with null Generator and Arguments throws excpetion"
        )]
        public void Null_Generator_And_Args_Throws() {
            IGenerator<string> first = null;
            IGenerator<int> second = null;
            Func<string, int, string> resultSelector = null;
            Assert.Throws<ArgumentNullException>(
                () => first.Zip(second, resultSelector));
        }

        [Test(
            Description = "Verify that passing null for second does not work and throws exception"
        )]
        public void Null_Second_Param_Throws() {
            Func<int, int, int> resultSelector = null;
            Assert.Throws<ArgumentNullException>(
                () => _generator.Zip(Generator.Incrementer(0), resultSelector));
        }
    }
}