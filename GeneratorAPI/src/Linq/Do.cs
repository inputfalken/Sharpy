using System;
using static GeneratorAPI.Generator;

namespace GeneratorAPI.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Exposes the element from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <param name="generator">The <paramref name="generator" /> whose elements is passed to <see cref="Action{T}" />.</param>
        /// <param name="action">The <see cref="Action{T}" /> which all generations will be passed to.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="action" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has been exposed to <see cref="Action{T}" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.Do" source="Examples\Generator.cs" />
        /// </example>
        public static IGenerator<TSource> Do<TSource>(this IGenerator<TSource> generator, Action<TSource> action) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => {
                var generation = generator.Generate();
                action(generation);
                return generation;
            });
        }
    }
}