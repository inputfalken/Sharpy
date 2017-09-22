using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>
    ///         Randomizes <see cref="int" /> by <see cref="Random" />.
    ///     </para>
    /// </summary>
    public class IntRandomizer : IIntegerProvider {
        private readonly Random _random;

        /// <summary>
        ///     <para>
        ///         Randomizes integers using argument <paramref name="random" />.
        ///     </para>
        /// </summary>
        /// <param name="random"></param>
        public IntRandomizer(Random random) => _random = random;

        /// <inheritdoc cref="IIntegerProvider.Integer(int)" />
        public int Integer(int max) => _random.Next(max);

        /// <inheritdoc cref="IIntegerProvider.Integer(int,int)" />
        public int Integer(int min, int max) {
            if (max <= min)
                throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.Next(min, max);
        }

        /// <inheritdoc cref="IIntegerProvider.Integer()" />
        public int Integer() => _random.Next();
    }
}