using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class ElementProviderTests
    {
        private static readonly IElementProvider ElementProvider = new ElementRandomizer(new Random());

        [Test]
        public void Two_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2),
                x => Assert.AreEqual(2, x.Count)
            );
        }

        [Test]
        public void Three_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2, 3),
                x => Assert.AreEqual(3, x.Count)
            );
        }

        [Test]
        public void Four_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2, 3, 4),
                x => Assert.AreEqual(4, x.Count)
            );
        }

        [Test]
        public void Five_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2, 3, 4, 5),
                x => Assert.AreEqual(5, x.Count)
            );
        }


        [Test]
        public void Six_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2, 3, 4, 5, 6),
                x => Assert.AreEqual(6, x.Count)
            );
        }

        [Test]
        public void Seven_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2, 3, 4, 5, 6, 7),
                x => Assert.AreEqual(7, x.Count)
            );
        }
        [Test]
        public void Eight_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2, 3, 4, 5, 6, 7, 8),
                x => Assert.AreEqual(8, x.Count)
            );
        }
        [Test]
        public void Nine_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2, 3, 4, 5, 6, 7, 8, 9),
                x => Assert.AreEqual(9, x.Count)
            );
        }
        [Test]
        public void Ten_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                ElementProvider,
                x => x.FromArgument(1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
                x => Assert.AreEqual(10, x.Count)
            );
        }

        [Test]
        public void Span_One_Element()
        {
            var args = new Span<int>(new[] {1});
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromSpan(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void Span_Two_Elements()
        {
            var args = new Span<int>(new[] {1, 2});
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromSpan(args);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
        }

        [Test]
        public void Span_Three_Elements()
        {
            var args = new Span<int>(new[] {1, 2, 3});
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromSpan(args);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
        }


        [Test]
        public void ReadOnlySpan_One_Element()
        {
            var args = new ReadOnlySpan<int>(new[] {1});
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromSpan(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void ReadOnlySpan_Two_Elements()
        {
            var args = new ReadOnlySpan<int>(new[] {1, 2});
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromSpan(args);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
        }

        [Test]
        public void ReadOnlySpan_Three_Elements()
        {
            var args = new ReadOnlySpan<int>(new[] {1, 2, 3});
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromSpan(args);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
        }

        [Test]
        public void Array_One_Element()
        {
            var args = new[] {1};
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromList(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void Array_Two_Elements()
        {
            var args = new[] {1, 2};

            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromList(args);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
        }

        [Test]
        public void Array_Three_Elements()
        {
            var args = new[] {1, 2, 3};
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromList(args);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
        }

        [Test]
        public void List_One_Element()
        {
            var args = new List<int> {1};
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromList(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void List_Two_Elements()
        {
            var args = new List<int> {1, 2};

            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromList(args);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
        }

        [Test]
        public void List_Three_Elements()
        {
            var args = new List<int> {1, 2, 3};
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = ElementProvider.FromList(args);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
        }

        [Test]
        public void Empty_List_Throws()
        {
            Assert.Throws<ArgumentException>(() => ElementProvider.FromList(new List<int>()));
            Assert.Throws<ArgumentException>(() => ElementProvider.FromList(System.Array.Empty<int>()));
        }

        [Test]
        public void Empty_Span_Throws()
        {
            Assert.Throws<ArgumentException>(() => ElementProvider.FromSpan(new Span<int>()));
            Assert.Throws<ArgumentException>(() => ElementProvider.FromSpan(new ReadOnlySpan<int>()));
        }
    }
}