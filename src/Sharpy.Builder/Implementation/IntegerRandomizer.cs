using System;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     <para>
    ///         Randomizes <see cref="int" /> elements by using <see cref="Random" />.
    ///     </para>
    /// </summary>
    public sealed class IntegerRandomizer : IIntegerProvider {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="IntegerRandomizer" />.
        /// </summary>
        public IntegerRandomizer(Random random) => _random = random;

        /// <inheritdoc />
        public int Integer(int max) => _random.Next(max);

        /// <inheritdoc />
        public int Integer(int min, int max) {
            if (max < min)
                throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.Next(min, max);
        }

        /// <inheritdoc />
        public int Integer() => _random.Next();
    }
}