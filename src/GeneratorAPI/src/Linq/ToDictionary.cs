using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Creates an <see cref="Dictionary{TKey,TValue}" /> from a <see cref="IGenerator{T}" /> according to specified
        ///         key selector and element selector functions.
        ///     </para>
        /// </summary>
        /// <param name="generator">A <paramref name="generator" /> to create a <see cref="Dictionary{TKey,TValue}" /> from.</param>
        /// <param name="count">The number of elements to be returned in the <see cref="Dictionary{TKey,TValue}" />.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="generator" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TValue">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
        /// <returns>
        ///     A <see cref="Dictionary{TKey,TValue}" /> that contains values of type TElement selected from the input
        ///     <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of how you can create a <see cref="Dictionary{TKey,TValue}" /> from a generator.
        ///     </para>
        ///     <code language='c#'>
        ///         Dictionary&lt;int,int&gt; dict = Factory.Incrementer(0).ToDictionary(100, (int ks) => ks, (int es) => es);
        ///     </code>
        /// </example>
        public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(this IGenerator<TSource> generator,
            int count, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector) {
            return generator.Take(count).ToDictionary(keySelector, elementSelector);
        }
    }
}