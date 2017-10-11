using System;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Randomizes <see cref="double" /> elements by using <see cref="Random" />.
    /// </summary>
    public class DoubleRandomizer : IDoubleProvider {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="DoubleRandomizer" />.
        /// </summary>
        public DoubleRandomizer(Random random) => _random = random;

        /// <inheritdoc />
        public double Double() => _random.NextDouble();

        /// <inheritdoc />
        public double Double(double max) => Double(0, max);

        /// <inheritdoc />
        public double Double(double min, double max) {
            if (max <= min) throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.NextDouble() * (max - min) + min;
        }
    }
}