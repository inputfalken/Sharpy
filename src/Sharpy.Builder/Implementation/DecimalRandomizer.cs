using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation
{
    public sealed class DecimalRandomizer : IDecimalProvider
    {
        private readonly Random _random;

        public DecimalRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public decimal Decimal(decimal max)
        {
            return Decimal(0, max);
        }

        /// <inheritdoc />
        public decimal Decimal()
        {
            return Decimal(0, decimal.MaxValue);
        }
        
        /// <inheritdoc />
        public decimal Decimal(decimal min, decimal max)
        {
            return _random.Decimal(min, max);
        }

    }
}