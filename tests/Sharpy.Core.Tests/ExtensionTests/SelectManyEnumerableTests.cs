using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core.Linq;

namespace Sharpy.Core.Tests.ExtensionTests
{
    [TestFixture]
    public class SelectManyEnumerableTests
    {
        [SetUp]
        public void Initiate()
        {
            _generator = Generator.Incrementer(0);
            _list = new List<string> {"Foo", "Bar"};
        }

        [TearDown]
        public void Dispose()
        {
            _generator = null;
        }

        private IReadOnlyList<string> _list;
        private IGenerator<int> _generator;

        [Test]
        public void Flatten_Aggregated_List_Combined_With_Selector_Argument()
        {
            var result = _generator.SelectMany(i => _list.Aggregate((s, s1) => s + s1) + i);
            Assert.AreEqual('F', result.Generate());
            Assert.AreEqual('o', result.Generate());
            Assert.AreEqual('o', result.Generate());
            Assert.AreEqual('B', result.Generate());
            Assert.AreEqual('a', result.Generate());
            Assert.AreEqual('r', result.Generate());
            Assert.AreEqual('0', result.Generate());

            Assert.AreEqual('F', result.Generate());
            Assert.AreEqual('o', result.Generate());
            Assert.AreEqual('o', result.Generate());
            Assert.AreEqual('B', result.Generate());
            Assert.AreEqual('a', result.Generate());
            Assert.AreEqual('r', result.Generate());
            Assert.AreEqual('1', result.Generate());

            Assert.AreEqual('F', result.Generate());
            Assert.AreEqual('o', result.Generate());
            Assert.AreEqual('o', result.Generate());
            Assert.AreEqual('B', result.Generate());
            Assert.AreEqual('a', result.Generate());
            Assert.AreEqual('r', result.Generate());
            Assert.AreEqual('2', result.Generate());
        }

        [Test]
        public void Flatten_Does_Not_Return_Null()
        {
            var flatMapGenerator = _generator.SelectMany(i => _list);
            Assert.IsNotNull(flatMapGenerator);
        }

        [Test]
        public void Flatten_List()
        {
            var result = _generator.SelectMany(i => _list);
            Assert.AreEqual("Foo", result.Generate());
            Assert.AreEqual("Bar", result.Generate());
            Assert.AreEqual("Foo", result.Generate());
            Assert.AreEqual("Bar", result.Generate());
            Assert.AreEqual("Foo", result.Generate());
            Assert.AreEqual("Bar", result.Generate());
            Assert.AreEqual("Foo", result.Generate());
            Assert.AreEqual("Bar", result.Generate());
        }

        [Test]
        public void Flatten_With_ResultSelector_Does_Not_Return_Null()
        {
            var flatMapGenerator = _generator.SelectMany(i => _list, (i, s) => i + s);
            Assert.IsNotNull(flatMapGenerator);
        }

        [Test]
        public void Flatten_With_ResultSelector_Generates_Once_For_Each_Complete_Iteration_Of_List()
        {
            var result = _generator.SelectMany(i => _list, (i, s) => s + i);
            Assert.AreEqual("Foo0", result.Generate());
            Assert.AreEqual("Bar0", result.Generate());
            Assert.AreEqual("Foo1", result.Generate());
            Assert.AreEqual("Bar1", result.Generate());
            Assert.AreEqual("Foo2", result.Generate());
            Assert.AreEqual("Bar2", result.Generate());
            Assert.AreEqual("Foo3", result.Generate());
            Assert.AreEqual("Bar3", result.Generate());
        }

        [Test]
        public void Null_Generator_Throws()
        {
            _generator = null;
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany(i => _list));
        }

        [Test]
        public void Null_Generator_With_ResultSelector_Throws()
        {
            _generator = null;
            Assert.Throws<ArgumentNullException>(
                () => _generator.SelectMany(i => _list, (i, s) => i + s));
        }

        [Test]
        public void Null_ResultSelector_Throws()
        {
            Func<int, string, int> resultSelector = null;
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany(i => _list, resultSelector));
        }

        [Test]
        public void Null_Selector_Throws()
        {
            Func<int, IEnumerable<int>> selector = null;
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany(selector));
        }

        [Test]
        public void Null_Selector_With_ResultSelector_Throws()
        {
            Func<int, IEnumerable<int>> selector = null;
            Assert.Throws<ArgumentNullException>(() => _generator.SelectMany(selector, (i, i1) => i + i1));
        }
    }
}