using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Sharpy.Builder.Tests
{
    public static class Assertion
    {
        public const int MainSeed = 100;
        public const int SecondarySeed = MainSeed + 1;
        public const int Amount = 10000000;

        public static void AssertNotAllValuesAreTheSame<T>(this ICollection<T> collection)
        {
            Assert.False(
                collection
                    .Zip(collection.Skip(1), (x, y) => new {First = x, second = y})
                    .All(x => x.First.Equals(x.second)),
                "All values are the same from the collection."
            );
        }


        public static void IsDistributed<T, TResult>(
            T source, Func<T, TResult> fn,
            Action<List<IGrouping<TResult, TResult>>> action
        )
        {
            var buildArray = BuildArray(source, fn);
            AssertNotAllValuesAreTheSame(buildArray);
            var result = buildArray.GroupBy(x => x).ToList();
            action(result);
            foreach (var grouping in result)
                Assert.IsNotEmpty(grouping);
        }

        public static void AreNotEqual<T, TResult>(
            T expected,
            T result,
            Func<T, TResult> fn
        )
        {
            Assert.AreNotEqual(BuildArray(expected, fn), BuildArray(result, fn));
        }

        public static void AreEqual<T, TResult>(
            T expected,
            T result,
            Func<T, TResult> fn
        )
        {
            Assert.AreEqual(BuildArray(expected, fn), BuildArray(result, fn));
        }

        private static TResult[] BuildArray<T, TResult>(this T source, Func<T, TResult> fn)
        {
            var array = new TResult[Amount];

            for (var i = 0; i < Amount; i++)
                array[i] = fn(source);

            return array;
        }
    }
}