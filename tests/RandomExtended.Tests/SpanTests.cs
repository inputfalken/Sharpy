using System;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class SpanTests
    {
        private static readonly Random Random = new();

        [Test]
        public void Span_One_Element_All_Values_Are_The_Same()
        {
            var args = new Span<int>(new[] {1});

            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = Random.SpanElement(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void Span_Two_Elements_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.SpanElement(new Span<int>(new[] {1, 2})),
                x => Assert.AreEqual(2, x.Count)
            );
        }

        [Test]
        public void Span_Three_Elements_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.SpanElement(new Span<int>(new[] {1, 2, 3})),
                x => Assert.AreEqual(3, x.Count)
            );
        }

        [Test]
        public void ReadOnlySpan_One_Element_All_Values_Are_The_Same()
        {
            var args = new ReadOnlySpan<int>(new[] {1});

            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = Random.SpanElement(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void ReadOnlySpan_Two_Elements_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.SpanElement(new ReadOnlySpan<int>(new[] {1, 2})),
                x => Assert.AreEqual(2, x.Count)
            );
        }

        [Test]
        public void ReadOnlySpan_Three_Elements_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.SpanElement(new ReadOnlySpan<int>(new[] {1, 2, 3})),
                x => Assert.AreEqual(3, x.Count)
            );
        }

        [Test]
        public void Empty_Span_Throws()
        {
            Assert.Throws<ArgumentException>(() => Random.SpanElement(new Span<int>()));
            Assert.Throws<ArgumentException>(() => Random.SpanElement(new ReadOnlySpan<int>()));
        }
    }
}