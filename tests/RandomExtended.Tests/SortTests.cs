using System;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class SortTests
    {
        private static readonly Random Random = new();

        [Test]
        public void Sort()
        {
            var enumerable = Enumerable.Range(1, 100).ToList();
            var result = Random.Sort(enumerable).ToList();
            
            // Order has changed
            Assert.AreNotEqual(enumerable, result);

            var result2 = Random.Sort(enumerable).ToList();
            
            Assert.AreNotEqual(enumerable, result2);
            Assert.AreNotEqual(result, result2);
        }
    }
}