using System;
using System.Linq;

namespace GeneratorAPI.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Creates an array from a <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">A <see cref="IGenerator{T}" /> to create an <see cref="Array" /> from.</param>
        /// <param name="length">The number of elements to be returned in the <see cref="Array" />.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>
        ///     An array that contains elements generated from the input <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.ToArray" source="Examples\Generator.cs" />
        /// </example>
        public static TSource[] ToArray<TSource>(this IGenerator<TSource> generator, int length) {
            return generator.Take(length).ToArray();
        }
    }
}