using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     Randomizes <see cref="bool" /> by <see cref="Random" />.
    /// </summary>
    public class BoolRandomizer : IBoolProvider {
        private readonly Random _random;

        /// <summary>
        /// </summary>
        /// <param name="random"></param>
        public BoolRandomizer(Random random) => _random = random;

        /// <inheritdoc />
        public bool Bool() => _random.Next(2) != 0;
    }
}