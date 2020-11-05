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
            return System.TimeSpan.FromMilliseconds(
                _random.Next(
                    System.TimeSpan.Zero.Milliseconds,
                    System.TimeSpan.MaxValue.Milliseconds
                )
            );
        }

        public TimeSpan TimeSpan(TimeSpan max)
        {
            return System.TimeSpan.FromMilliseconds(
                _random.Next(System.TimeSpan.Zero.Milliseconds, max.Milliseconds)
            );
        }

        public TimeSpan TimeSpan(TimeSpan min, TimeSpan max)
        {
            return System.TimeSpan.FromMilliseconds(
                _random.Next(min.Milliseconds, max.Milliseconds)
            );
        }
    }
}