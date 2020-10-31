using System;
using System.Linq;

namespace Sharpy.Core.Linq
{
    public static partial class Extensions
    {
        /// <summary>
        ///     <para>
        ///         Creates an <see cref="Array" /> from a <see cref="IGenerator{T}" />.
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
        ///         int[] arr = Generator.Incrementer(0).ToArray(100);
        ///     </code>
        /// </example>
        public static TSource[] ToArray<TSource>(this IGenerator<TSource> generator, int length)
        {
            if (generator is null)
                throw new ArgumentNullException(nameof(generator));
            
            if (length < 0)
                throw new ArgumentException("Can not pass negative values.");
            
            if (length == 0)
                return Array.Empty<TSource>();

            var sources = new TSource[length];
            for (var i = 0; i < length; i++) 
                sources[i] = generator.Generate();

            return sources;
        }
    }
}