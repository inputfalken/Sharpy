using System;
using System.Collections.Generic;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Randomizes <see cref="IReadOnlyList{T}" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class CollectionRandomizer : ICollectionElementProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="CollectionRandomizer" />.
        /// </summary>
        public CollectionRandomizer(Random random) => _random = random ?? throw new ArgumentNullException(nameof(random));

        /// <summary>
        ///     Returns a randomized element from the argument <paramref name="list" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the elements inside the  <paramref name="list" /> argument.
        /// </typeparam>
        /// <param name="list">
        ///     The collection of elements.
        /// </param>
        /// <returns>
        ///     One of the elements inside argument <paramref name="list" />.
        /// </returns>
        public T FromList<T>(IReadOnlyList<T> list) => _random.ListElement(list);

        /// <inheritdoc />
        public T FromSpan<T>(ReadOnlySpan<T> span)
        {
            return _random.SpanElement(span);
        }

        /// <inheritdoc />
        public T FromSpan<T>(Span<T> span)
        {
            return _random.SpanElement(span);
        }
    }
}