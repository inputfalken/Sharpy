using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NUnit.Framework;

namespace Sharpy.ReactiveBuilder.Tests {
    [TestFixture]
    internal class ReactiveBuilderTests {
        [Test]
        public void Not_Null() => Assert.NotNull(new Builder.Builder().Observable(b => b.Integer()));

        [Test]
        public void Not_Null_With_Counter() => Assert.NotNull(new Builder.Builder().Observable((b, i) => b.Integer()));

        [Test]
        public void Null_Builder_Throws() {
            Builder.Builder builder = null;
            Assert.Throws<ArgumentNullException>(() => builder.Observable(b => b.Integer()));
        }

        [Test]
        public void Null_Selector_Throws() {
            Func<Builder.Builder, int> selector = null;
            Assert.Throws<ArgumentNullException>(() => new Builder.Builder().Observable(selector));
        }

        [Test]
        public void Null_Selector_With_Counter_Throws() {
            Func<Builder.Builder, int, int> selector = null;
            Assert.Throws<ArgumentNullException>(() => new Builder.Builder().Observable(selector));
        }

        [Test]
        public void Building_Integers() {
            const int seed = 20;
            var builder = new Builder.Builder(seed);
            var expected = new List<int>();
            const int total = 2000;
            const int max = 1000;
            const int min = 100;
            for (var i = 0; i < total; i++) expected.Add(builder.Integer(min, max));
            var result = new Builder.Builder(seed)
                .Observable(b => b.Integer(min, max))
                .Take(total)
                .ToListObservable();
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void Building_Doubles() {
            const int seed = 20;
            var builder = new Builder.Builder(seed);
            var expected = new List<double>();
            const int total = 2000;
            const int max = 1000;
            const int min = 100;
            for (var i = 0; i < total; i++) expected.Add(builder.Double(min, max));
            var result = new Builder.Builder(seed)
                .Observable(b => b.Double(min, max))
                .Take(total)
                .ToListObservable();
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void Building_Booleans() {
            const int seed = 20;
            var builder = new Builder.Builder(seed);
            var expected = new List<bool>();
            const int total = 2000;
            for (var i = 0; i < total; i++) expected.Add(builder.Bool());
            var result = new Builder.Builder(seed)
                .Observable(b => b.Bool())
                .Take(total)
                .ToListObservable();
            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}