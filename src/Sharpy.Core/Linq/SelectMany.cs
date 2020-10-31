using System;
using Sharpy.Core.Implementations;

namespace Sharpy.Core.Linq
{
    public static partial class Extensions
    {
        /// <summary>
        /// Flattens two <see cref="IGenerator{T}"/> into one.
        /// </summary>
        /// <param name="source">
        /// A <see cref="IGenerator{T}"/> to be flattened.
        /// </param>
        /// <param name="selector">
        /// A transform function.
        /// </param>
        /// <typeparam name="T">
        /// The type of the initial <see cref="IGenerator{T}"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the resulting <see cref="IGenerator{T}"/>.
        /// </typeparam>
        /// <returns>
        /// A Flattened <see cref="IGenerator{T}"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="source"/> is null or when <see cref="selector"/> is null.
        /// </exception>
        public static IGenerator<TResult> SelectMany<T, TResult>(
            this IGenerator<T> source,
            Func<T, IGenerator<TResult>> selector
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            return new Fun<TResult>(() => selector(source.Generate()).Generate());
        }

        /// <summary>
        /// Flattens two <see cref="IGenerator{T}"/> into one.
        /// </summary>
        /// <param name="source">
        /// A <see cref="IGenerator{T}"/> to be flattened.
        /// </param>
        /// <param name="selector">
        /// A transform function.
        /// </param>
        /// <param name="resultSelector">
        /// A function with the generation result from both <paramref name="source"/> and the result from <paramref name="selector"/> function.
        /// </param>
        /// <typeparam name="T">
        /// The type of the initial <see cref="IGenerator{T}"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type returned from <paramref name="resultSelector"/>.
        /// </typeparam>
        /// <typeparam name="TSelect"></typeparam>
        /// The type of the resulting <see cref="IGenerator{T}"/>.
        /// <returns>
        /// A Flattened <see cref="IGenerator{T}"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="source"/> is null or when <see cref="selector"/> is null.
        /// </exception>
        public static IGenerator<TResult> SelectMany<T, TSelect, TResult>(
            this IGenerator<T> source,
            Func<T, IGenerator<TSelect>> selector,
            Func<T, TSelect, TResult> resultSelector
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            if (resultSelector is null)
                throw new ArgumentNullException(nameof(resultSelector));

            return source.SelectMany(x => selector(x).Select(y => resultSelector(x, y)));
        }
    }
}