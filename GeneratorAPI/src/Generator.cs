using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneratorAPI.Implementations;

namespace GeneratorAPI {
    /// <summary>
    ///     <para>Contains various methods for creating <see cref="IGenerator{T}" /></para>
    /// </summary>
    public static class Generator {
        /// <summary>
        ///     <para>Contains various methods for creating <see cref="IGenerator{T}" /></para>
        ///     <remarks>
        ///         <para>The point of this class is to contain extension methods from other libraries.</para>
        ///     </remarks>
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will use the same value
        ///         <typeparamref name="TSource" />.
        ///     </para>
        /// </summary>
        /// <param name="source">The value to generate from.</param>
        /// <typeparam name="TSource">The type of argument <paramref name="source" /></typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will use argument <paramref name="source" />.
        /// </returns>
        public static IGenerator<TSource> Create<TSource>(TSource source) {
            return new Fun<TSource>(() => source);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will use the same value
        ///         <typeparamref name="TSource" />.
        ///     </para>
        /// </summary>
        /// <param name="lazy">The lazy evaluated value.</param>
        /// <typeparam name="TSource">The type of <paramref name="lazy" />.</typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will use the lazy evaluated value from <paramref name="lazy" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="lazy" /> is null.</exception>
        public static IGenerator<TSource> Lazy<TSource>(Lazy<TSource> lazy) {
            if (lazy == null) throw new ArgumentNullException(nameof(lazy));
            return new Fun<TSource>(() => lazy.Value);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will use the same value
        ///         <typeparamref name="TSource" />.
        ///     </para>
        /// </summary>
        /// <param name="fn">The lazy evaluated value.</param>
        /// <typeparam name="TSource">The type of <paramref name="fn" />.</typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will use the lazy evaluated value from <paramref name="fn" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="fn" /> is null.</exception>
        public static IGenerator<TSource> Lazy<TSource>(Func<TSource> fn) {
            return Lazy(new Lazy<TSource>(fn));
        }


        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will
        ///         invoke the argument <paramref name="fn" />.
        ///     </para>
        /// </summary>
        /// <param name="fn">The function to invoke to be invoked for each generation.</param>
        /// <typeparam name="TSource">The type of the <paramref name="fn" />.</typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will invoke argument <paramref name="fn" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="fn" /> is null.</exception>
        public static IGenerator<TSource> Function<TSource>(Func<TSource> fn) {
            return new Fun<TSource>(fn);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> based on an <see cref="IEnumerable{T}" /> which resets if the end is
        ///         reached.
        ///     </para>
        /// </summary>
        /// <param name="enumerable">The <see cref="IEnumerable{T}" /> to create a <see cref="IGenerator{T}" /> from.</param>
        /// <typeparam name="TSource">The type of the <paramref name="enumerable" />.</typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements comes from argument <paramref name="enumerable" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable" /> is null.</exception>
        public static IGenerator<TSource> CircularSequence<TSource>(IEnumerable<TSource> enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return new Seq<TSource>(enumerable);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> with object as its type parameter; based on an
        ///         <see cref="IEnumerable" /> which resets if the end is reached.
        ///     </para>
        /// </summary>
        /// <param name="enumerable">The <see cref="IEnumerable" /> to create a IGenerator&lt;object&gt; from.</param>
        /// <returns>
        ///     <see cref="IGenerator" /> whose elements comes from argument <paramref name="enumerable" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable" /> is null.</exception>
        public static IGenerator<object> CircularSequence(IEnumerable enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            // TODO add proper implementation so cast can be skipped.
            return new Seq<object>(enumerable.Cast<object>());
        }
    }
}