using System;
using System.Collections.Generic;

namespace Sharpy.Generator.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Returns generations from a <see cref="IGenerator{T}" /> as long as a specified condition is true.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="generator" />.</typeparam>
        /// <param name="generator">The <paramref name="generator" /> to generate from.</param>
        /// <param name="predicate">A function to test each generated element for a condition.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> whose generated elements passed the <paramref name="predicate" />.</returns>
        /// <remarks>
        ///     This method will run forever if the <paramref name="predicate" /> always returns true.
        /// </remarks>
        public static IEnumerable<TSource> TakeWhile<TSource>(this IGenerator<TSource> generator,
            Func<TSource, bool> predicate) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return TakeWhileIterator(generator, predicate);
        }

        private static IEnumerable<TSource> TakeWhileIterator<TSource>(IGenerator<TSource> generator,
            Func<TSource, bool> predicate) {
            while (true) {
                var generation = generator.Generate();
                if (predicate(generation)) yield return generation;
                else break;
            }
        }
    }
}