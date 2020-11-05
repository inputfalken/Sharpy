using System;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation
{
    public sealed class TimeSpanRandomizer : ITimeSpanProvider
    {
        private readonly Random _random;

        public TimeSpanRandomizer(Random random)
        {
            _random = random;
        }

        public TimeSpan TimeSpan()
        {
            return TimeSpan(System.TimeSpan.Zero, System.TimeSpan.MaxValue);
        }
        

        public TimeSpan TimeSpan(TimeSpan max)
        {
            return TimeSpan(System.TimeSpan.Zero, max);
        }

        public TimeSpan TimeSpan(TimeSpan min, TimeSpan max)
        {
            if (min > max)
                throw new ArgumentException($"Parameter '{nameof(min)}' can not be greater than '{nameof(max)}'.");

            return System.TimeSpan.FromMilliseconds(
                _random.Next(min.Milliseconds, max.Milliseconds)
            );
        }
    }
}