using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class ListTests
    {
        private static readonly Random Random = new();

        [Test]
        public void Array_One_Element_All_Values_Are_The_Same()
        {
            var args = new[] {1};
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = Random.ListElement(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void Array_Two_Elements_IsDistributed()
        {
            var args = new[] {1, 2};

            Assertion.IsDistributed(
                Random,
                x => x.ListElement(args),
                x => Assert.AreEqual(2, x.Count)
            );
        }

        [Test]
        public void Array_Three_Elements_IsDistributed()
        {
            var args = new[] {1, 2, 3};
            Assertion.IsDistributed(
                Random,
                x => x.ListElement(args),
                x => Assert.AreEqual(3, x.Count)
            );
        }

        [Test]
        public void List_One_Element_All_Values_Are_The_Same()
        {
            var args = new List<int> {1};
            var arr = new int[Assertion.Amount];
            for (var i = 0; i < Assertion.Amount; i++)
                arr[i] = Random.ListElement(args);

            Assert.True(arr.All(x => x == 1), "arr.All(x => x == 1)");
        }

        [Test]
        public void List_Two_Elements_IsDistributed()
        {
            var args = new List<int> {1, 2};
            Assertion.IsDistributed(
                Random,
                x => x.ListElement(args),
                x => Assert.AreEqual(2, x.Count)
            );
        }

        [Test]
        public void List_Three_Elements_IsDistributed()
        {
            var args = new List<int> {1, 2, 3};
            Assertion.IsDistributed(
                Random,
                x => x.ListElement(args),
                x => Assert.AreEqual(3, x.Count)
            );
        }

        [Test]
        public void Empty_List_Throws()
        {
            Assert.Throws<ArgumentException>(() => Random.ListElement(new List<int>()));
            Assert.Throws<ArgumentException>(() => Random.ListElement(Array.Empty<int>()));
        }
    }
}