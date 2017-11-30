using System;
using System.Collections.Generic;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Randomizes <see cref="IReadOnlyList{T}" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class ListRandomizer : IElementProvider {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="ListRandomizer" />.
        /// </summary>
        public ListRandomizer(Random random) => _random = random ?? throw new ArgumentNullException(nameof(random));

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
        public T Element<T>(IReadOnlyList<T> list) => list.RandomItem(_random);
    }
}