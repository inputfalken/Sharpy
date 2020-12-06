using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Sharpy.Builder.Tests
{
    public static class Assertion
    {
        public const int MainSeed = 100;
        private const int SecondarySeed = MainSeed + 1;
        public const int Amount = 100000;

        public static void IsDeterministic<T, TResult>(Func<int, T> factory, Func<T, TResult> fn)
        {
            Assert.AreEqual(EnumerableFactory(factory(MainSeed), fn), EnumerableFactory(factory(MainSeed), fn));
        }

        public static void IsNotDeterministic<T, TResult>(Func<int, T> factory, Func<T, TResult> fn)
        {
            Assert.AreNotEqual(EnumerableFactory(factory(MainSeed), fn), EnumerableFactory(factory(SecondarySeed), fn));
        }

        public static void AssertNotAllValuesAreTheSame<T>(this IEnumerable<T> collection)
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
            var results = new TResult[Amount];
            foreach (var (element, index) in EnumerableFactory(source, fn).Select((x,y) => (Element: x,Index:y )))
                results[index] = element;

            AssertNotAllValuesAreTheSame(results);
            var result = results.GroupBy(x => x).ToList();
            action(result);
            foreach (var grouping in result)
                Assert.IsNotEmpty(grouping);
        }

        private static IEnumerable<TResult> EnumerableFactory<T, TResult>(this T source, Func<T, TResult> fn)
        {
            for (var i = 0; i < Amount; i++)
                yield return fn(source);
        }
    }
}