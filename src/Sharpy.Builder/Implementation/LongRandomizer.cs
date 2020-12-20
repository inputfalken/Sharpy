using System;
using RandomExtended;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Randomizes <see cref="long" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class LongRandomizer : ILongProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="LongRandomizer" />.
        /// </summary>
        public LongRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public long Long(in long min, in long max)
        {
            return _random.Long(min, max);
        }

        /// <inheritdoc />
        public long Long(in long max)
        {
            return Long(0, max);
        }

        /// <inheritdoc />
        public long Long()
        {
            return Long(0, long.MaxValue);
        }
    }
}