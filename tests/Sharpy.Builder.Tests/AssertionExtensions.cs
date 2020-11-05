using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Sharpy.Builder.Tests
{
    public static class AssertionExtensions
    {
        public static void AssertNotAllValuesAreTheSame<T>(this ICollection<T> collection)
        {
            Assert.False(
                collection
                    .Zip(collection.Skip(1), (x, y) => new {First = x, second = y})
                    .All(x => x.First.Equals(x.second)),
                "All values are the same from the collection."
            );
        }
    }
}