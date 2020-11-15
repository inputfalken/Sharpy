using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
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
        public DateTimeOffset DateTimeOffset(DateTimeOffset max)
        {
            return DateTimeOffset(System.DateTimeOffset.MinValue, max);
        }

        ///<inheritdoc />
        public DateTimeOffset DateTimeOffset(DateTimeOffset min, DateTimeOffset max)
        {
            return _random.DateTimeOffset(min, max);
        }
    }
}