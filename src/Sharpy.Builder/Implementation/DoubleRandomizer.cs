using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Randomizes <see cref="double" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class DoubleRandomizer : IDoubleProvider {
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
        public double Double(double min, double max)
        {
            return _random.Double(min, max);
        }
    }
}