using System;
using GeneratorAPI;
using GeneratorAPI.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.GeneratorAPI.LinqTests {
    [TestFixture]
    internal class SelectManyGeneratorTests {
        [SetUp]
        public void Initiate() {
            _generator = Factory.Incrementer(0);
            _selectorGenerator = Factory.Incrementer(0)
                .Select(i => i * 2);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
            _selectorGenerator = null;
        }

        private IGenerator<int> _generator;
        private IGenerator<int> _selectorGenerator;

        [Test(
            Description = "Verify that flattening a generator consumes the _selectorGenerator."
        )]
        public void Flatten() {
            var flatMapGenerator = _generator.SelectMany(i => _selectorGenerator);
            Assert.AreEqual(0, flatMapGenerator.Generate());
            Assert.AreEqual(2, flatMapGenerator.Generate());
            Assert.AreEqual(4, flatMapGenerator.Generate());
            Assert.AreEqual(6, flatMapGenerator.Generate());
        }

        [Test(
            Description = "Verify that SelectMany does not return null."
        )]
        public void Flatten_Does_Not_Return_Null() {
            var flatMapGenerator = _generator.SelectMany(i => _selectorGenerator);
            Assert.IsNotNull(flatMapGenerator);
        }

        [Test(
            Description = "Verify that flattening a generator consumes the _selectorGenerator."
        )]
        public void Flatten_Using_Selector_Argument() {
            var flatMapGenerator = _generator.SelectMany(Generator.Create);
            Assert.AreEqual(0, flatMapGenerator.Generate());
            Assert.AreEqual(1, flatMapGenerator.Generate());
            Assert.AreEqual(2, flatMapGenerator.Generate());
            Assert.AreEqual(3, flatMapGenerator.Generate());
        }

        [Test(
            Description =
                "Verify that flattening a generator consumes the _selectorGenerator. and using the resultSelector uses gives the generation result of the first generator."
        )]
        public void Flatten_With_ResultSelector() {
            var flatMapGenerator = _generator.SelectMany(i => _selectorGenerator, (i, i1) => i + i1);
            Assert.AreEqual(0, flatMapGenerator.Generate());
            Assert.AreEqual(3, flatMapGenerator.Generate());
            Assert.AreEqual(6, flatMapGenerator.Generate());
            Assert.AreEqual(9, flatMapGenerator.Generate());
        }

        [Test(
            Description = "Verify that SelectMany does not return null."
        )]
        public void Flatten_With_ResultSelector_Does_Not_Return_Null() {
            var flatMapGenerator = _generator.SelectMany(i => _selectorGenerator, (i, i1) => i + i1);
            Assert.IsNotNull(flatMapGenerator);
        }

        [Test]
        public void Null_Generator_Throws() {
            _generator = null;
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany(i => _selectorGenerator));
        }

        [Test]
        public void Null_Generator_With_ResultSelector_Throws() {
            _generator = null;
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany(i => _selectorGenerator, (i, i1) => i + i1));
        }


        [Test]
        public void Null_ResultSelector_Throws() {
            Func<int, int, int> resultSelector = null;
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany(i => _selectorGenerator, resultSelector));
        }

        [Test]
        public void Null_Selector_Throws() {
            Func<int, IGenerator<int>> selector = null;
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany(selector));
        }

        [Test]
        public void Null_Selector_With_ResultSelector_Throws() {
            Func<int, IGenerator<int>> selector = null;
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany(selector, (i, i1) => i + i1));
        }
    }
}