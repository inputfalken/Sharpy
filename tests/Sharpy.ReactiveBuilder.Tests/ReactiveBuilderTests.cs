using System;
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
    }
}