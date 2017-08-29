using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.GeneratorAPI.LinqTests {
    [TestFixture]
    public class TakeTests {
        [SetUp]
        public void Initiate() {
            _generator = Factory.Incrementer(0);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<int> _generator;

        [Test(
            Description = "Verify that take with negative argument throws exception"
        )]
        public void Arg_Negative() {
            Assert.Throws<ArgumentException>(() => _generator.Take(-1));
        }

        [Test(
            Description = "Verify that take with argument zero throws exception"
        )]
        public void Arg_Zero() {
            Assert.Throws<ArgumentException>(() => _generator.Take(0));
        }

        [Test(
            Description = "Verify that Take gives the expected ammount of elements"
        )]
        public void Gives_Expected_Amount() {
            const int count = 10;
            var result = _generator.Take(count);
            Assert.AreEqual(count, result.Count());
        }

        [Test(
            Description = "Verify that Take gives expected result"
        )]
        public void Gives_Expected_Elements() {
            const int count = 10;
            var result = _generator.Take(count);
            var expected = Enumerable.Range(0, count);
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that take with  does not return null"
        )]
        public void Is_Not_Null() {
            var result = _generator.Take(10);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verify that null generator throws excpetion when take is invoked"
        )]
        public void Null_Generator_Throws() {
            IGenerator<string> nullGenerator = null;
            Assert.Throws<ArgumentNullException>(() => nullGenerator.Take(1));
        }
    }
}