using System;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>Randomizes longs.</para>
    /// </summary>
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
        ///     <para>Returns a randomized long within min and max</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long Long(long min, long max) => _random.NextLong(min, max);

        /// <summary>
        ///     <para>Returns a randomized long within 0 and max</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public long Long(long max) => _random.NextLong(max);

        /// <summary>
        ///     <para>Returns a randomized long within long.MinValue and long.MaxValue.</para>
        /// </summary>
        /// <returns></returns>
        public long Long() => _random.NextLong();
    }
}