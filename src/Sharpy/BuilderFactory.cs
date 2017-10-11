using System;
using System.Collections.Generic;
using Sharpy.Core;
using Sharpy.Core.Linq;
using static Sharpy.Core.Generator;

namespace Sharpy {
    /// <summary>
    ///     Provides a set of static members to create objects by using <see cref="Builder.Builder" />.
    /// </summary>
    public static class BuilderFactory {
        /// <summary>
        ///     <para>
        ///         Turns the <see cref="Builder" /> into a <see cref="IGenerator{T}" />.
        ///     </para>
        ///     <para>
        ///         This also works for descenders of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of the <see cref="Builder" /> or a descendant.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The result from the parameter <paramref name="selector" />.
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="Builder" /> or a descendant.
        /// </param>
        /// <param name="selector">
        ///     The function whose result will be the type of the <see cref="IGenerator{T}" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose type is the result from the function argument <paramref name="selector" />.
        /// </returns>
        public static IGenerator<TResult> Generator<TBuilder, TResult>(this TBuilder source,
            Func<TBuilder, TResult> selector) where TBuilder : Builder.Builder {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return Create(source).Select(selector);
        }

        /// <summary>
        ///     <para>
        ///         Turns the <see cref="Builder" /> into a <see cref="IGenerator{T}" />.
        ///     </para>
        ///     <para>
        ///         This also works for descenders of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of the <see cref="Builder" /> or a descendant.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The result from the parameter <paramref name="selector" />.
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="Builder" /> or a descendant.
        /// </param>
        /// <param name="selector">
        ///     The function whose result will be the type of the <see cref="IGenerator{T}" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose type is the result from the function argument <paramref name="selector" />.
        /// </returns>
        public static IGenerator<TResult> Generator<TBuilder, TResult>(this TBuilder source,
            Func<TBuilder, int, TResult> selector) where TBuilder : Builder.Builder {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return Create(source).Select(selector);
        }

        /// <summary>
        ///     <para>
        ///         Turns the <see cref="Builder" /> into a <see cref="IEnumerable{T}" />.
        ///     </para>
        ///     <para>
        ///         This also works for descenders of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of the <see cref="Builder" /> or a descendant.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The result from the parameter <paramref name="selector" />.
        /// </typeparam>
        /// <param name="source">
        ///     The builder.
        /// </param>
        /// <param name="selector">
        ///     The function whose result will be the type of the <see cref="IEnumerable{T}" />.
        /// </param>
        /// <param name="count">
        ///     The <paramref name="count" /> of the returned <see cref="IEnumerable{T}" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> whose type is the result from the function argument <paramref name="selector" />.
        /// </returns>
        public static IEnumerable<TResult> Enumerable<TBuilder, TResult>(this TBuilder source,
            Func<TBuilder, TResult> selector, int count) where TBuilder : Builder.Builder {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            for (var i = 0; i < count; i++) yield return selector(source);
        }

        /// <summary>
        ///     <para>
        ///         Turns the <see cref="Builder" /> into a <see cref="IEnumerable{T}" />.
        ///     </para>
        ///     <para>
        ///         This also works for descenders of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of the <see cref="Builder" /> or a descendant.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The result from the parameter <paramref name="selector" />.
        /// </typeparam>
        /// <param name="source">
        ///     The builder.
        /// </param>
        /// <param name="selector">
        ///     The function whose result will be the type of the <see cref="IEnumerable{T}" />.
        /// </param>
        /// <param name="count">
        ///     The <paramref name="count" /> of the returned <see cref="IEnumerable{T}" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> whose type is the result from the function argument <paramref name="selector" />.
        /// </returns>
        public static IEnumerable<TResult> Enumerable<TBuilder, TResult>(this TBuilder source,
            Func<TBuilder, int, TResult> selector, int count) where TBuilder : Builder.Builder {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            for (var i = 0; i < count; i++) yield return selector(source, i);
        }
    }
}