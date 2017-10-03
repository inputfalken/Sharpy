using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.GeneratorAPI.LinqTests {
    internal class ToListTests {
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
            Description = "Verify that when argument less than zero, an exception is thrown"
        )]
        public void Negative_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToList(-1));
        }

        [Test(
            Description = "Verify the expeceted elements is returned."
        )]
        public void Returns_Expected_Elements() {
            var result = _generator.ToList(20);
            var expected = Enumerable.Range(0, 20);
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify the expeceted amount of elements is returned."
        )]
        public void Returns_Expected_Amount() {
            const int count = 20;
            var result = _generator.ToList(count);
            Assert.AreEqual(count, result.Count);
        }

        [Test(
            Description = "Verify that ArgumentNullException is thrown when generator is null."
        )]
        public void Null_Generator_Throws() {
            _generator = null;
            Assert.Throws<ArgumentNullException>(() => _generator.ToList(10));
        }

        [Test(
            Description = "Verify that when argument count is zero, an exception is thrown"
        )]
        public void Zero_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToList(0));
        }
    }
}