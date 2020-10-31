using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpy.Core.Linq
{
    /// <summary>
    ///     Provides a set of static methods for <see cref="IGenerator{T}" />.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="List{T}" /> from a <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">The <see cref="List{T}" /> to create a <see cref="List{T}" /> from.</param>
        /// <param name="count">The number of elements to be returned in the <see cref="List{T}" />.</param>
        /// <typeparam name="TSource">The type of the elements of source..</typeparam>
        /// <returns>
        ///     A <see cref="List{T}" /> that contains elements generated from the input <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of how you can create a <see cref="List{T}" /> from a generator.
        ///     </para>
        ///     <code language='c#'>
        ///          list&lt;int&gt; list = Generator.Incrementer(0).ToList(100);
        ///     </code>
        /// </example>
        public static List<TSource> ToList<TSource>(this IGenerator<TSource> generator, int count)
        {
            if (generator is null)
                throw new ArgumentNullException(nameof(generator));

            if (count < 0)
                throw new ArgumentException("Can not be negative", nameof(count));

            var sources = new List<TSource>(count);

            if (count == 0)
                return sources;

            for (var i = 0; i < count; i++)
                sources.Add(generator.Generate());

            return sources;
        }
    }
}