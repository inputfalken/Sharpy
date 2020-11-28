using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core.Linq;

namespace Sharpy.Core.Tests.ExtensionTests
{
    [TestFixture]
    public class ToDictionaryTests
    {
        [SetUp]
        public void Initiate()
        {
            _generator = Generator.Incrementer(0);
        }

        [TearDown]
        public void Dispose()
        {
            _generator = null;
        }

        private IGenerator<int> _generator;

        [Test(
            Description = "Verify that ToDictionary with negative value throws ArgumentException"
        )]
        public void Negative_Length_Throws()
        {
            Assert.Throws<ArgumentException>(() => _generator.ToDictionary(-1, i => i, i => i));
        }


        [Test(
            Description = "Verify the expeceted amount of elements is returned."
        )]
        public void Returns_Expected_Amount()
        {
            const int count = 20;
            var result = _generator
                .ToDictionary(20, i => i, i => i);
            Assert.AreEqual(count, result.Count);
        }

        [Test(
            Description = "Verify that ToDictionary returns the expected elements"
        )]
        public void Returns_Expected_Elements()
        {
            var result = _generator
                .ToDictionary(20, i => i, i => i);
            var expected = Enumerable
                .Range(0, 20)
                .ToDictionary(i => i, i => i);
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that ToDictionary with zero value return empty dictionary"
        )]
        public void Zero_Length_Throws()
        {
            Assert.IsEmpty(_generator.ToDictionary(0, s => s, s => s));
        }
    }
}