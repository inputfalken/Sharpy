using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>Randomizes Doubles.</para>
    /// </summary>
    public class DoubleRandomizer : IDoubleProvider {
        private readonly Random _random;

        /// <summary>
        ///     <para>
        ///         Randomizes doubles with the <see cref="Random" /> supplied.
        ///     </para>
        /// </summary>
        /// <param name="random">
        ///     The randomizer.
        /// </param>
        public DoubleRandomizer(Random random) => _random = random;

        /// <inheritdoc cref="IDoubleProvider.Double()" />
        public double Double() => _random.NextDouble();

        /// <inheritdoc cref="IDoubleProvider.Double(double)" />
        public double Double(double max) => Double(0, max);

        /// <inheritdoc cref="IDoubleProvider.Double(double, double)" />
        public double Double(double min, double max) {
            if (max <= min) throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.NextDouble() * (max - min) + min;
        }
    }
}