using System;
using System.Collections.Generic;
using System.Linq;
using GeneratorAPI.Implementations;
using static GeneratorAPI.Generator;

namespace GeneratorAPI.Extensions {
    /// <summary>
    /// </summary>
    public static class EnumerableExtensions {
        /// <summary>
        ///     <para>
        ///         Invokes <see cref="IGenerator{T}.Generate" /> for each full iteration of <see cref="IEnumerable{T}" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">The <paramref name="generator" />.</param>
        /// <param name="enumerableSelector">The function to consume <see cref="IEnumerable{T}" />.</param>
        /// <typeparam name="TSource">The type of <paramref name="generator" />.</typeparam>
        /// <typeparam name="TResult">The type of the <see cref="IEnumerable{T}" />.</typeparam>
        /// <returns>
        ///     TODO
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="enumerableSelector" /> is null</exception>
        public static IGenerator<TResult> SelectMany<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, IEnumerable<TResult>> enumerableSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (enumerableSelector == null) throw new ArgumentNullException(nameof(enumerableSelector));
            var sequence = new Seq<TResult>(() => enumerableSelector(generator.Generate()));
            return Function(() => sequence.Generate());
        }

        /// <summary>
        ///     <para>
        ///         Invokes <see cref="IGenerator{T}.Generate" /> for each full iteration of <see cref="IEnumerable{T}" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">The <paramref name="generator" />.</param>
        /// <param name="enumerableSelector">The function to consume <see cref="IEnumerable{T}" />.</param>
        /// <param name="resultSelector">The function to compose .</param>
        /// <typeparam name="TSource">The type of <paramref name="generator" />.</typeparam>
        /// <typeparam name="TResult">The type of the <see cref="IEnumerable{T}" />.</typeparam>
        /// <typeparam name="TCompose">The type of the <see cref="IEnumerable{T}" />.</typeparam>
        /// <returns>
        ///     TODO
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="enumerableSelector" /> is null</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="resultSelector" /> is null</exception>
        public static IGenerator<TCompose> SelectMany<TSource, TResult, TCompose>(
            this IGenerator<TSource> generator,
            Func<TSource, IEnumerable<TResult>> enumerableSelector, Func<TSource, TResult, TCompose> resultSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (enumerableSelector == null) throw new ArgumentNullException(nameof(enumerableSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            return generator.SelectMany(source => enumerableSelector(source)
                .Select(result => resultSelector(source, result)));
        }
    }
}