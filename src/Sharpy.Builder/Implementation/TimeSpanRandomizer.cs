using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation
{
    /// <inheritdoc />
    public sealed class TimeSpanRandomizer : ITimeSpanProvider
    {
        private readonly Random _random;

        public TimeSpanRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public TimeSpan TimeSpan()
        {
            return TimeSpan(System.TimeSpan.Zero, System.TimeSpan.MaxValue);
        }


        /// <inheritdoc />
        public TimeSpan TimeSpan(TimeSpan max)
        {
            return TimeSpan(System.TimeSpan.Zero, max);
        }

        /// <inheritdoc />
        public TimeSpan TimeSpan(TimeSpan min, TimeSpan max)
        {
            return _random.TimeSpan(min, max);
        }
    }
}