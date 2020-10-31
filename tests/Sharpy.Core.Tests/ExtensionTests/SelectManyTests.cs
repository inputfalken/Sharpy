using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core.Linq;

namespace Sharpy.Core.Tests.ExtensionTests
{
    [TestFixture]
    public class SelectManyTests
    {
        [SetUp]
        public void Initiate()
        {
            _generator = Generator.Incrementer(0);
            _flattenedGenerator = Generator.Incrementer(0);
        }

        [TearDown]
        public void Dispose()
        {
            _generator = null;
        }

        private IGenerator<int> _generator;
        private IGenerator<int> _flattenedGenerator;

        [Test(
            Description = "Verify that both generators are invoked for each generation call in the returned generator."
        )]
        public void SelectMany_Flattening_Invokes_Both_Generators_Every_Generation_Call()
        {
            var result = _generator
                .SelectMany(i => _flattenedGenerator, (x, y) => x - y)
                .Take(10);

            Assert.AreEqual(Enumerable.Repeat(0, 10), result);
        }

        [Test]
        public void SelectMany_Null_Generator_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    _generator = null;
                    _generator.SelectMany(x => _flattenedGenerator);
                }
            );
        }

    }
}