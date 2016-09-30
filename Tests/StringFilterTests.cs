using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Types.String;

namespace Tests {
    [TestFixture]
    public class StringFilterTests {
        [Test]
        public void StringFilter_ByLength() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "bar", "foo" };
            var result = new StringFilter(strings).ByLength(3);
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_ByLength_With_ArgMinusOne() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            Assert.Throws<ArgumentOutOfRangeException>(() => new StringFilter(strings).ByLength(-1));
        }

        [Test]
        public void StringFilter_ByLength_With_ArgZero() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            Assert.Throws<ArgumentOutOfRangeException>(() => new StringFilter(strings).ByLength(0));
        }

        [Test]
        public void StringFilter_Contains_FourArgs() {
            string[] strings = { "bar", "foo", "foobar", "barfoo", "johnny", "doe" };
            string[] expected = { "bar", "foo", "foobar", "barfoo", "johnny", "doe" };
            var result = new StringFilter(strings).Contains("oo", "ar", "hn", "oe");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }


        [Test]
        public void StringFilter_Contains_ThreeArgs() {
            string[] strings = { "bar", "foo", "foobar", "barfoo", "johnny", "doe" };
            string[] expected = { "bar", "foo", "foobar", "barfoo", "johnny" };
            var result = new StringFilter(strings).Contains("oo", "ar", "ohn");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_Contains_TwoArgs() {
            string[] strings = { "bar", "foo", "foobar", "barfoo", "johnny", "doe" };
            string[] expected = { "bar", "foo", "foobar", "barfoo" };
            var result = new StringFilter(strings).Contains("oo", "ar");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_DoesNotContain() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "bar" };
            var result = new StringFilter(strings).DoesNotContain("foo");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_DoesNotStartWith() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "bar", "barfoo" };
            var result = new StringFilter(strings).DoesNotStartWith("foo");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_StartsWith_FourArg() {
            string[] strings = { "john", "doe", "foo", "bar", "lorem", "loremfoo", "doebar" };
            string[] expected = { "doe", "foo", "bar", "lorem", "loremfoo", "doebar" };
            var result = new StringFilter(strings).StartsWith("foo", "bar", "doe", "lorem");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_StartsWith_OneArg() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "foo", "foobar" };
            var result = new StringFilter(strings).StartsWith("foo");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_StartsWith_ThreeArg() {
            string[] strings = { "john", "doe", "foo", "bar", "lorem", "loremFoo", "doebar" };
            string[] expected = { "doe", "foo", "bar", "doebar" };
            var result = new StringFilter(strings).StartsWith("foo", "bar", "doe");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_StartsWith_TwoArg() {
            string[] strings = { "john", "doe", "foo", "bar", "lorem", "loremFoo", "doebar" };
            string[] expected = { "foo", "bar" };
            var result = new StringFilter(strings).StartsWith("foo", "bar");
            Assert.IsTrue(result.Fetcher.SequenceEqual(expected));
        }
    }
}