using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class CollectionRandomizerTests
    {
        private static readonly ICollectionElementProvider CollectionElementProvider =
            new CollectionRandomizer(new Random());

        [Test]
        public void Span()
        {
            var args = new Span<string>(new[] {"hello", "there", "foo"});

            Assert.IsTrue(args.Contains(CollectionElementProvider.FromSpan(args)));
        }

        [Test]
        public void ReadOnlySpan()
        {
            var args = new ReadOnlySpan<string>(new[] {"hello", "there", "foo"});

            Assert.IsTrue(args.Contains(CollectionElementProvider.FromSpan(args)));
        }

        [Test]
        [Repeat(100)]
        public void Array()
        {
            var args = new[] {"hello", "there", "foo"};

            Assert.IsTrue(args.Contains(CollectionElementProvider.FromList(args)));
        }

        [Test]
        [Repeat(100)]
        public void List()
        {
            var args = new[] {"hello", "there", "foo"};
            Assert.IsTrue(args.Contains(CollectionElementProvider.FromList(args)));
        }

        [Test]
        public void Empty_List_Throws()
        {
            Assert.Throws<ArgumentException>(() => CollectionElementProvider.FromList(System.Array.Empty<int>()));
        }
    }
}