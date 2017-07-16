using System;
using static GeneratorAPI.Generator;

namespace GeneratorAPI.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Skips the number given to <paramref name="count" /> from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <param name="generator">The <paramref name="generator" /> whose generations will be skipped.</param>
        /// <param name="count">The number of generations to be released.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentException">Argument <paramref name="count" /> is less than 0.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has been skipped by the number equal to argument
        ///     <paramref name="count" />.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         This works the same way as <see cref="Release{TSource}" /> except that it's lazy evaluated.
        ///     </para>
        /// </remarks>
        public static IGenerator<TSource> Skip<TSource>(this IGenerator<TSource> generator, int count) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (count < 0) throw new ArgumentException($"{nameof(count)} Cant be negative");
            if (count == 0) return generator;
            var skipped = new Lazy<IGenerator<TSource>>(() => ReleaseIterator(generator, count));
            return Function(() => skipped.Value.Generate());
        }
    }
}