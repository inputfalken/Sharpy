using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core.Linq;

namespace Sharpy.Core.Tests.ExtensionTests {
    [TestFixture]
    internal class ToArrayTests {
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
            Description = "Verify that when argument less than zero, an exception is thrown"
        )]
        public void Negative_Length_Throws() {
            Assert.Throws<ArgumentException>(() => _generator.ToArray(-1));
        }

        [Test(
            Description = "Verify that ArgumentNullException is thrown when generator is null."
        )]
        public void Null_Generator_Throws() {
            _generator = null;
            Assert.Throws<ArgumentNullException>(() => _generator.ToArray(10));
        }

        [Test(
            Description = "Verify the expeceted amount of elements is returned."
        )]
        public void Returns_Expected_Amount() {
            const int count = 20;
            var result = _generator.ToArray(count);
            Assert.AreEqual(count, result.Length);
        }

        [Test(
            Description = "Verify the expeceted elements is returned."
        )]
        public void Returns_Expected_Elements() {
            var result = _generator.ToArray(20);
            var expected = Enumerable.Range(0, 20);
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that when argument count is zero an empty array is returned."
        )]
        public void Zero_Length_Returns_EmptyArray() {
            Assert.AreEqual(Array.Empty<int>(), _generator.ToArray(0));
        }
    }
}