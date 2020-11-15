using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class ListRandomizerTests
    {
        private static readonly IElementProvider ElementProvider = new ListRandomizer(new Random());

        [Test]
        [Repeat(100)]
        public void Array()
        {
            var args = new[] {"hello", "there", "foo"};

            Assert.IsTrue(args.Contains(ElementProvider.Element(args)));
        }

        [Test]
        [Repeat(100)]
        public void List()
        {
            var args = new[] {"hello", "there", "foo"};
            Assert.IsTrue(args.Contains(ElementProvider.Element(args)));
        }

        [Test]
        public void Null_List_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ElementProvider.Element<int>(null));
        }

        [Test]
        public void Empty_List_Throws()
        {
            Assert.Throws<ArgumentException>(() => ElementProvider.Element(System.Array.Empty<int>()));
        }
    }
}