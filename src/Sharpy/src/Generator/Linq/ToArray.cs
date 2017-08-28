using System.Linq;

namespace Sharpy.Generator.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Creates an array from a <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">A <see cref="IGenerator{T}" /> to create an <see cref="System.Array" /> from.</param>
        /// <param name="length">The number of elements to be returned in the <see cref="System.Array" />.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>
        ///     An array that contains elements generated from the input <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of how you can create an <see cref="System.Array" /> from a generator.
        ///     </para>
        ///     <code language='c#'>
        ///         int[] arr = Factory.Incrementer(0).ToArray(100);
        ///     </code>
        /// </example>
        public static TSource[] ToArray<TSource>(this IGenerator<TSource> generator, int length) {
            return generator.Take(length).ToArray();
        }
    }
}