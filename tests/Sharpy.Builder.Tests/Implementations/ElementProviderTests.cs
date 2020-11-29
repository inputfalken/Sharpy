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

        private const int Amount = 10000000;

        [Test]
        public void Two_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2);

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
        public void Three_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2, 3);

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
        public void Four_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2, 3, 4);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
        }

        [Test]
        public void Five_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2, 3, 4, 5);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.AreEqual(5, grouping[4].Key);

            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
            Assert.IsNotEmpty(grouping[4]);
        }

        [Test]
        public void Six_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2, 3, 4, 5, 6);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.AreEqual(5, grouping[4].Key);
            Assert.AreEqual(6, grouping[5].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
            Assert.IsNotEmpty(grouping[4]);
            Assert.IsNotEmpty(grouping[5]);
        }

        [Test]
        public void Seven_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2, 3, 4, 5, 6, 7);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.AreEqual(5, grouping[4].Key);
            Assert.AreEqual(6, grouping[5].Key);
            Assert.AreEqual(7, grouping[6].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
            Assert.IsNotEmpty(grouping[4]);
            Assert.IsNotEmpty(grouping[5]);
            Assert.IsNotEmpty(grouping[6]);
        }

        [Test]
        public void Eight_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2, 3, 4, 5, 6, 7, 8);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.AreEqual(5, grouping[4].Key);
            Assert.AreEqual(6, grouping[5].Key);
            Assert.AreEqual(7, grouping[6].Key);
            Assert.AreEqual(8, grouping[7].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
            Assert.IsNotEmpty(grouping[4]);
            Assert.IsNotEmpty(grouping[5]);
            Assert.IsNotEmpty(grouping[6]);
            Assert.IsNotEmpty(grouping[7]);
        }

        [Test]
        public void Nine_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2, 3, 4, 5, 6, 7, 8, 9);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.AreEqual(5, grouping[4].Key);
            Assert.AreEqual(6, grouping[5].Key);
            Assert.AreEqual(7, grouping[6].Key);
            Assert.AreEqual(8, grouping[7].Key);
            Assert.AreEqual(9, grouping[8].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
            Assert.IsNotEmpty(grouping[4]);
            Assert.IsNotEmpty(grouping[5]);
            Assert.IsNotEmpty(grouping[6]);
            Assert.IsNotEmpty(grouping[7]);
            Assert.IsNotEmpty(grouping[8]);
        }

        [Test]
        public void Ten_Arguments()
        {
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++) arr[i] = ElementProvider.FromArgument(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.AreEqual(5, grouping[4].Key);
            Assert.AreEqual(6, grouping[5].Key);
            Assert.AreEqual(7, grouping[6].Key);
            Assert.AreEqual(8, grouping[7].Key);
            Assert.AreEqual(9, grouping[8].Key);
            Assert.AreEqual(10, grouping[9].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
            Assert.IsNotEmpty(grouping[4]);
            Assert.IsNotEmpty(grouping[5]);
            Assert.IsNotEmpty(grouping[6]);
            Assert.IsNotEmpty(grouping[7]);
            Assert.IsNotEmpty(grouping[8]);
            Assert.IsNotEmpty(grouping[9]);
        }


        [Test]
        public void Span_One_Element()
        {
            var args = new Span<int>(new[] {1});
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
                arr[i] = ElementProvider.FromSpan(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void Span_Two_Elements()
        {
            var args = new Span<int>(new[] {1, 2});
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
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
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
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
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
                arr[i] = ElementProvider.FromSpan(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void ReadOnlySpan_Two_Elements()
        {
            var args = new ReadOnlySpan<int>(new[] {1, 2});
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
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
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
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
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
                arr[i] = ElementProvider.FromList(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void Array_Two_Elements()
        {
            var args = new[] {1, 2};

            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
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
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
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
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
                arr[i] = ElementProvider.FromList(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void List_Two_Elements()
        {
            var args = new List<int> {1, 2};

            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
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
            var arr = new int[Amount];
            for (var i = 0; i < Amount; i++)
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