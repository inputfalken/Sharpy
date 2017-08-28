using System;

namespace Sharpy.Generator.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> by flattening a <see cref="IGenerator{T}" /> with another
        ///         <see cref="IGenerator{T}" /> for each invocation of <see cref="IGenerator{T}.Generate" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements from <paramref name="generator" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The return type of function <paramref name="selector" />.
        /// </typeparam>
        /// <param name="generator">
        ///     The generator to be flattened.
        /// </param>
        /// <param name="selector">
        ///     A transform function to apply to each element.
        /// </param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="selector" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements have been flattened by another <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<TResult> SelectMany<TSource, TResult>(
            this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TResult>> selector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return Generator.Function(() => selector(generator.Generate()).Generate());
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> by flattening a <see cref="IGenerator{T}" /> with another
        ///         <see cref="IGenerator{T}" /> for each invocation of <see cref="IGenerator{T}.Generate" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements from <paramref name="generator" />.
        /// </typeparam>
        /// <typeparam name="TGenerator">
        ///     The return type of function <paramref name="selector" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The return type from function <paramref name="resultSelector" />.
        /// </typeparam>
        /// <param name="generator">
        ///     The generator to be flattened.
        /// </param>
        /// <param name="selector">
        ///     A transform function to apply to each element.
        /// </param>
        /// <param name="resultSelector">
        ///     A transform function with the result from argument <paramref name="selector" /> and the element from
        ///     <paramref name="generator" />.
        /// </param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="selector" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements have been flattened by an <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<TResult> SelectMany<TSource, TGenerator, TResult>(
            this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TGenerator>> selector,
            Func<TSource, TGenerator, TResult> resultSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            return generator.SelectMany(
                a => selector(a).SelectMany(r => Generator.Function(() => resultSelector(a, r))));
        }
    }
}