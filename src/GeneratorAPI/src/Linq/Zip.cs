using System;
using static GeneratorAPI.Generator;

namespace GeneratorAPI.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Applies a specified function to the corresponding elements of two Generators, producing a Generator of the
        ///         results.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of the first generator.
        /// </typeparam>
        /// <typeparam name="TSecond">
        ///     The type of the elements of the second generator.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the result from argument <paramref name="resultSelector" />.
        /// </typeparam>
        /// <param name="first">The first generator to merge.</param>
        /// <param name="second">The second generator to merge.</param>
        /// <param name="resultSelector">A function that specifies how to merge the elements from the two generators.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="first" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="second" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> that contains merged elements of two input generators.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.Zip" source="Examples\Generator.cs" />
        /// </example>
        public static IGenerator<TResult> Zip<TSource, TSecond, TResult>(this IGenerator<TSource> first,
            IGenerator<TSecond> second,
            Func<TSource, TSecond, TResult> resultSelector) {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            return Function(() => resultSelector(first.Generate(), second.Generate()));
        }
    }
}