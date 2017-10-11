using System;
using NUnit.Framework;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.CoreTests.ExtensionTests {
    [TestFixture]
    public class SelectTests {
        [SetUp]
        public void Initiate() {
            _generator = Generator.Incrementer(0);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<int> _generator;

        [Test(
            Description = "Verify that counter increments for each element"
        )]
        public void Counter_Increments_For_Each_Generation() {
            var result = _generator
                .Select((s, i) => i);
            Assert.AreEqual(0, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(2, result.Generate());
            Assert.AreEqual(3, result.Generate());
            Assert.AreEqual(4, result.Generate());
        }

        [Test(
            Description = "Verify that counter starts on zero"
        )]
        public void Counter_Starts_Zero() {
            var result = _generator
                .Select((s, i) => i);
            Assert.AreEqual(0, result.Generate());
        }

        [Test(
            Description = "Verify that Select does not return null"
        )]
        public void Does_Not_Return_Null() {
            var result = _generator.Select(i => i);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verifys that the Select is only invoked if Generate is invoked"
        )]
        public void Is_Evaluated_After_Take_Is_Invoked() {
            var invoked = false;
            var generator = _generator
                .Select(s => invoked = true);
            // Not evaluated
            Assert.IsFalse(invoked);
            // Evaluated
            generator.Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that Select with null Generator and Argument throws exception"
        )]
        public void Null_Generator_And_Selector_Throws() {
            Func<string, int> selector = null;
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Select(selector));
        }

        [Test(
            Description = "Verify that Select with null Generator throws exception"
        )]
        public void Null_Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Select(s => s.Length));
        }

        [Test(
            Description = "Verify that null Func given to Select throws exception"
        )]
        public void Null_Selector_Throws() {
            Func<int, int> selector = null;
            Assert.Throws<ArgumentNullException>(() => _generator.Select(selector));
        }

        [Test(
            Description = "Verify that mapping works"
        )]
        public void Selector_Returns_String_Length() {
            var result = _generator
                .Select(s => s.ToString().Length);

            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(1, result.Generate());
            Assert.AreEqual(2, result.Generate());
        }
    }
}