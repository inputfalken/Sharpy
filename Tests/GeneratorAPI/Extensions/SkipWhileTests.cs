using System;
using GeneratorAPI;
using GeneratorAPI.Linq;
using NUnit.Framework;

namespace Tests.GeneratorAPI.Extensions {
    [TestFixture]
    public class SkipWhileTests {
        [SetUp]
        public void Initiate() {
            _generator = Generator.Factory.Incrementer(0);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<int> _generator;

        [Test]
        public void Is_Evaluated_After_Generate_Is_Invoked() {
            var invoked = false;
            var skipWhile = _generator.SkipWhile(i => {
                invoked = true;
                return false;
            });
            // Not evaluated
            Assert.IsFalse(invoked);
            // Evaluated
            skipWhile.Generate();
            Assert.IsTrue(invoked);
        }

        [Test]
        public void Includes_First_False_Case() {
            var skipWhile = _generator.SkipWhile(i => i < 20);
            Assert.AreEqual(20, skipWhile.Generate());
        }

        [Test]
        public void Gives_Rest_Of_Generations() {
            var skipWhile = _generator.SkipWhile(i => i < 20);
            Assert.AreEqual(20, skipWhile.Generate());
            Assert.AreEqual(21, skipWhile.Generate());
            Assert.AreEqual(22, skipWhile.Generate());
            Assert.AreEqual(23, skipWhile.Generate());
            Assert.AreEqual(24, skipWhile.Generate());
            Assert.AreEqual(25, skipWhile.Generate());
            Assert.AreEqual(26, skipWhile.Generate());
            Assert.AreEqual(27, skipWhile.Generate());
        }

        [Test]
        public void Bad_Predicate_Throws() {
            var skipWhile = _generator.SkipWhile(i => true);
            Assert.Throws<ArgumentException>(() => skipWhile.Generate());
        }

        [Test]
        public void Null_Predicate_Throws() {
            Func<int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => _generator.SkipWhile(predicate));
        }

        [Test]
        public void Null_Generator_Throws() {
            _generator = null;
            Assert.Throws<ArgumentNullException>(() => _generator.SkipWhile(i => false));
        }
    }
}