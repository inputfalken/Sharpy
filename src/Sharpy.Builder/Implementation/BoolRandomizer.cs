using System;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Randomizes <see cref="bool" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class BoolRandomizer : IBoolProvider {
        private readonly Random _random;

        /// <summary>
        ///     Creates <see cref="BoolRandomizer" />.
        /// </summary>
        public BoolRandomizer(Random random) => _random = random;

        /// <inheritdoc />
        public bool Bool() => _random.Next(2) != 0;
    }
}