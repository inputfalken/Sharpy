using System;
using System.Collections.Generic;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     Randomizes Argument and Elements by <see cref="Random" />.
    /// </summary>
    public class ListRandomizer : IElementProvider {
        private readonly Random _random;

        /// <summary>
        /// </summary>
        /// <param name="random"></param>
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