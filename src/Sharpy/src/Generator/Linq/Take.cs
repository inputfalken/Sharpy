using System;
using System.Collections.Generic;

namespace Sharpy.Generator.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Creates an <see cref="IEnumerable{T}" /> from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of <paramref name="generator" />.
        /// </typeparam>
        /// <param name="generator">The <paramref name="generator" /> to generate from.</param>
        /// <param name="count">The number of elements to be returned in the <see cref="IEnumerable{T}" />.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentException">Argument <paramref name="count"></paramref> is less or equal to zero.</exception>
        /// <returns>
        ///     An <see cref="IEnumerable{T}" /> that contains the specified number of elements generated from the argument
        ///     <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of how you can create an <see cref="IEnumerable{T}" /> from a generator.
        ///     </para>
        ///     <code language='c#'>
        ///          IEenumerable&lt;int&gt; = Factory.Incrementer(0).Take(100);
        ///     </code>
        /// </example>
        public static IEnumerable<TSource> Take<TSource>(this IGenerator<TSource> generator, int count) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (count <= 0) throw new ArgumentException($"{nameof(count)} Must be more than zero");
            //Is needed so the above if statement is checked.
            return Iterator(count, generator);
        }

        private static IEnumerable<TSource> Iterator<TSource>(int count, IGenerator<TSource> generator) {
            for (var i = 0; i < count; i++) yield return generator.Generate();
        }
    }
}