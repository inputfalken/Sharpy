using System;
using RandomExtended;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <inheritdoc />
    public sealed class DecimalRandomizer : IDecimalProvider
    {
        private readonly Random _random;

        public DecimalRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public decimal Decimal(in decimal max)
        {
            return Decimal(0, in max);
        }

        /// <inheritdoc />
        public decimal Decimal()
        {
            return Decimal(0, decimal.MaxValue);
        }

        /// <inheritdoc />
        public decimal Decimal(in decimal min, in decimal max)
        {
            return _random.Decimal(in min, in max);
        }
    }
}