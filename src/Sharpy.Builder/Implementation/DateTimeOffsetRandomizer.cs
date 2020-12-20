using System;
using RandomExtended;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <inheritdoc />
    public class DateTimeOffsetRandomizer : IDateTimeOffsetProvider
    {
        private readonly Random _random;

        public DateTimeOffsetRandomizer(Random random)
        {
            _random = random;
        }

        ///<inheritdoc />
        public DateTimeOffset DateTimeOffset()
        {
            return DateTimeOffset(System.DateTimeOffset.MinValue, System.DateTimeOffset.MaxValue);
        }

        ///<inheritdoc />
        public DateTimeOffset DateTimeOffset(in DateTimeOffset max)
        {
            return DateTimeOffset(System.DateTimeOffset.MinValue, max);
        }

        ///<inheritdoc />
        public DateTimeOffset DateTimeOffset(in DateTimeOffset min, in DateTimeOffset max)
        {
            return _random.DateTimeOffset(min, max);
        }
    }
}