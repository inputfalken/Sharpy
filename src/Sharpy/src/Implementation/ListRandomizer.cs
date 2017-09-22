using System;
using System.Collections.Generic;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     Randomizes Argument and Elements by <see cref="Random" />.
    /// </summary>
    public class ListRandomizer : IReadListElementProvider {
        private readonly Random _random;

        /// <summary>
        /// </summary>
        /// <param name="random"></param>
        public ListRandomizer(Random random) => _random = random ?? throw new ArgumentNullException(nameof(random));

        /// <inheritdoc />
        public T Element<T>(IReadOnlyList<T> list) => list.RandomItem(_random);

        /// <inheritdoc />
        public T Argument<T>(T first, T second, params T[] additional) {
            var res = _random.Next(-2, additional.Length);
            switch (res) {
                case -2: return first;
                case -1: return second;
                default: return additional[res];
            }
        }
    }
}