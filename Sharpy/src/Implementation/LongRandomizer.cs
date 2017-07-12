using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <inheritdoc cref="ILongProvider"/>
    public class LongRandomizer : ILongProvider {
        private readonly Random _random;

        /// <summary>
        ///     <para>Randomizes longs with the random supplied.</para>
        /// </summary>
        /// <param name="random"></param>
        public LongRandomizer(Random random) {
            _random = random;
        }

        /// <summary>
        ///     Returns a random long from min (inclusive) to max (exclusive)
        /// </summary>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        public long Long(long min, long max) {
            if (max <= min)
                throw new ArgumentOutOfRangeException(nameof(max), "max must be > min!");

            //Working with ulong so that modulo works correctly with values > long.MaxValue
            var uRange = (ulong) (max - min);

            //Prevent a modulo bias; see http://stackoverflow.com/a/10984975/238419
            //for more information.
            //In the worst case, the expected number of calls is 2 (though usually it's
            //much closer to 1) so this loop doesn't really hurt performance at all.
            ulong ulongRand;
            do {
                var buf = new byte[8];
                _random.NextBytes(buf);
                ulongRand = (ulong) BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - (ulong.MaxValue % uRange + 1) % uRange);

            return (long) (ulongRand % uRange) + min;
        }

        /// <inheritdoc cref="ILongProvider.Long(long)"/>
        public long Long(long max) {
            return Long(0, max);
        }

        /// <inheritdoc cref="ILongProvider.Long()"/>
        public long Long() {
            return Long(long.MinValue, long.MaxValue);
        }
    }
}