using System.Linq;
using DataGen.Types.String;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class StringFilterTests {
        [Test]
        public void StringFilter_Arg_Contains() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "foo", "foobar", "barfoo" };
            var result = new StringFilter(strings).Contains("foo");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void StringFilter_Arg_StartsWith() {
            string[] strings = { "bar", "foo", "foobar", "barfoo" };
            string[] expected = { "foo", "foobar" };
            var result = new StringFilter(strings).StartsWith("foo");
            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}