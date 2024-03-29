using System;
using RandomExtended;
using Sharpy.Builder.Providers;

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
        public TimeSpan TimeSpan(in TimeSpan max)
        {
            return TimeSpan(System.TimeSpan.Zero, in max);
        }

        /// <inheritdoc />
        public TimeSpan TimeSpan(in TimeSpan min, in TimeSpan max)
        {
            return _random.TimeSpan(in min, in max);
        }
    }
}