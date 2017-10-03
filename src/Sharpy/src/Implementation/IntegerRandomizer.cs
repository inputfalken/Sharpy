using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>
    ///         Randomizes <see cref="int" /> elements by using <see cref="Random" />.
    ///     </para>
    /// </summary>
    public class IntRandomizer : IIntegerProvider {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="IntRandomizer" />.
        /// </summary>
        public IntRandomizer(Random random) => _random = random;

        /// <inheritdoc />
        public int Integer(int max) => _random.Next(max);

        /// <inheritdoc />
        public int Integer(int min, int max) {
            if (max <= min)
                throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.Next(min, max);
        }

        /// <inheritdoc />
        public int Integer() => _random.Next();
    }
}