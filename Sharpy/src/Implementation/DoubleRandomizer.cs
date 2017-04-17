using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>Randomizes Doubles.</para>
    /// </summary>
    public class DoubleRandomizer : IDoubleProvider {
        private readonly Random _random;

        /// <summary>
        ///     <para>Randomizes doubles with the random supplied.</para>
        /// </summary>
        /// <param name="random"></param>
        public DoubleRandomizer(Random random) => _random = random;

        /// <summary>
        ///     <para>Returns a randomized double within 0 and 1.</para>
        /// </summary>
        /// <returns></returns>
        public double Double() => _random.NextDouble();

        /// <summary>
        ///     <para>Returns a randomized double within 0 and max.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Double(double max) => Double(0, max);

        /// <summary>
        ///     <para>Returns a randomized double within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Double(double min, double max) {
            if (max <= min) throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.NextDouble() * (max - min) + min;
        }
    }
}