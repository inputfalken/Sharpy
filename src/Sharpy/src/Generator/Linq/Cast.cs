using System;

namespace Sharpy.Generator.Linq {
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> by casting each element to <typeparamref name="TResult" /> when invoking
        ///         <see cref="IGenerator.Generate" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">
        ///     The <see cref="IGenerator" /> that contains the elements to be cast to type
        ///     <typeparamref name="TResult" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations have been casted to type <typeparamref name="TResult" />.
        /// </returns>
        /// <typeparam name="TResult">The type to be used when casting.</typeparam>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="InvalidCastException">
        ///     A generated element cannot be cast to type
        ///     <typeparamref name="TResult" />.
        /// </exception>
        /// <example>
        ///     <code language="C#" region="Generator.Cast" source="Examples\Generator.cs" />
        /// </example>
        public static IGenerator<TResult> Cast<TResult>(this IGenerator generator) {
            var result = generator as IGenerator<TResult>;
            if (result != null) return result;
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Generator.Function(() => (TResult) generator.Generate());
        }
    }
}