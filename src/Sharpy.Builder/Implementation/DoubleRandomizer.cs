using System;
using RandomExtended;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Randomizes <see cref="double" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class DoubleRandomizer : IDoubleProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="DoubleRandomizer" />.
        /// </summary>
        public DoubleRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public double Double()
        {
            return _random.NextDouble();
        }

        /// <inheritdoc />
        public double Double(in double max)
        {
            return Double(0, max);
        }

        /// <inheritdoc />
        public double Double(in double min, in double max)
        {
            return _random.Double(min, max);
        }
    }
}