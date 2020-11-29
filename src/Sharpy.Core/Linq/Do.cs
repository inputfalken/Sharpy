using System;

namespace Sharpy.Core.Linq
{
    public static partial class Extensions
    {
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
        ///     <para>
        ///         Here's an example of how you can expose each generated element.
        ///         The result is a generator which will call method <see cref="Console.WriteLine(int)" /> every time you generate.
        ///     </para>
        ///     <code language='c#'>
        ///          IGenerator&lt;int&gt; = Generator.Incrementer(0).Do((int x) => Console.WriteLine(x));
        ///     </code>
        /// </example>
        public static IGenerator<TSource> Do<TSource>(this IGenerator<TSource> generator, Action<TSource> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Generator.Function(() =>
            {
                var generation = generator.Generate();
                action(generation);
                return generation;
            });
        }
    }
}