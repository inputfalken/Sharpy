using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core.Linq;

namespace Sharpy.Core.Tests.ExtensionTests {
    [TestFixture]
    internal class TakeWhileTests {
        [SetUp]
        public void Initiate() {
            _generator = Generator.Incrementer(0);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<int> _generator;

        [Test]
        public void False_Predicate_Returns_Empty_Enumerable() {
            var result = _generator.TakeWhile(i => false);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void Null_Generator_Throws() {
            _generator = null;
            Assert.Throws<ArgumentNullException>(() => _generator.TakeWhile(i => i < 20));
        }

        [Test]
        public void Null_Predicate_Throws() {
            Func<int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => _generator.TakeWhile(predicate));
        }

        [Test]
        public void Predicate_Gives_Expected_Elements() {
            var result = _generator.TakeWhile(i => i < 20);
            var expected = Enumerable.Range(0, 20);

            Assert.AreEqual(expected, result);
        }
    }
}