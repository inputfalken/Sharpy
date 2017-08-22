using System;

namespace GeneratorAPI.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Releases the number given to <paramref name="amount" /> immediately from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <remarks>
        ///     <para>
        ///         This method is not lazy evaluated, for lazy evaluation see <see cref="Skip{TSource}" />.
        ///     </para>
        /// </remarks>
        /// <param name="generator">The <paramref name="generator" /> whose generations will be released.</param>
        /// <param name="amount">The number of generations to be released.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentException">Argument <paramref name="amount" /> is less than 0.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has been released by the number equal to argument
        ///     <paramref name="amount" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of releasing 20 elements.
        ///         The result will be a <see cref="IGenerator{T}" /> who has invoked <see cref="IGenerator{T}.Generate" /> 20
        ///         times.
        ///     </para>
        ///     <code language="c#">
        ///         IGenerator&lt;int&gt; releasedGenerator = Factory.FirstName().Release(20);
        ///     </code>
        /// </example>
        public static IGenerator<TSource> Release<TSource>(this IGenerator<TSource> generator, int amount) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (amount == 0) return generator;
            if (amount < 0) throw new ArgumentException($"{nameof(amount)} can't be below ${amount}");
            return ReleaseIterator(generator, amount);
        }

        private static IGenerator<T> ReleaseIterator<T>(IGenerator<T> generator, int count) {
            for (var i = 0; i < count; i++) generator.Generate();
            return generator;
        }
    }
}