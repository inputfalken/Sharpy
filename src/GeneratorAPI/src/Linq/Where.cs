using System;
using static GeneratorAPI.Generator;

namespace GeneratorAPI.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Filters a <see cref="IGenerator{T}" /> based on a predicate.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If the <paramref name="predicate" /> is not satisfied, a new generation will occur.
        ///     </para>
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type to be used as input in the <paramref name="predicate" />.
        /// </typeparam>
        /// <param name="generator">The <see cref="IGenerator{T}" /> to filter.</param>
        /// <param name="predicate">A function to test each generated element for a condition.</param>
        /// <param name="threshold">The number of attempts before throwing an exception.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="predicate" /> is null.</exception>
        /// <exception cref="ArgumentException">The condition can't be matched within the <paramref name="threshold" />.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has satisfied the condition.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of how you can set a condition for the <see cref="IGenerator{T}" />.
        ///         The result is a generator whose generations will match the condition given.
        ///     </para>
        ///     <code language='c#'>
        ///          IGenerator&lt;int&gt; conditionedGenerator = Factory.Generator.Incrementer(0).Where((int x) => x % 2 == 0);
        ///      </code>
        /// </example>
        public static IGenerator<TSource> Where<TSource>(this IGenerator<TSource> generator,
            Func<TSource, bool> predicate,
            int threshold = 100000) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => {
                for (var i = 0; i < threshold; i++) {
                    var generation = generator.Generate();
                    if (predicate(generation)) return generation;
                }
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts. ");
            });
        }
    }
}