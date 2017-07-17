using System;

namespace GeneratorAPI.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Denies generated elements until the specified condition in the predicate is matched.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">The type of elements from argument <paramref name="generator" />.</typeparam>
        /// <param name="generator">A <see cref="IGenerator{T}" /> to generate elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="threshold">The amount of attempts before an exception is thrown.</param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will continue from the matched predicate.
        /// </returns>
        public static IGenerator<TSource> SkipWhile<TSource>(this IGenerator<TSource> generator,
            Func<TSource, bool> predicate, int threshold = 10000) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var skip = new Lazy<TSource>(() => {
                for (var i = 0; i < threshold; i++) {
                    var generate = generator.Generate();
                    if (predicate(generate)) continue;
                    return generate;
                }
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts.");
            });

            // This function is needed to avoid implicitly captured predicate.
            return SkipWhileGenerator(skip, generator);
        }

        private static IGenerator<TSource> SkipWhileGenerator<TSource>(Lazy<TSource> lazy,
            IGenerator<TSource> generator) {
            return Generator.Function(() => lazy.IsValueCreated ? generator.Generate() : lazy.Value);
        }
    }
}