using System.Linq;
using DataGen.Types.String;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class StringFilterTests {
        [Test]
        public void StringFilter_Contains() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "foo", "foobar", "barfoo" };
            var result = new StringFilter(strings).Contains("foo");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_DoesNotContain() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "bar" };
            var result = new StringFilter(strings).DoesNotContain("foo");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_StartsWith() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "foo", "foobar" };
            var result = new StringFilter(strings).StartsWith("foo");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_DoesNotStartWith() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "bar", "barfoo" };
            var result = new StringFilter(strings).DoesNotStartWith("foo");
            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}