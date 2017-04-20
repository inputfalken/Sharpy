using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>Randomizes Integers.</para>
    /// </summary>
    public class IntRandomizer : IIntegerProvider {
        private readonly Random _random;

        /// <summary>
        ///     <para>Randomizes integers with the random supplied.</para>
        /// </summary>
        /// <param name="random"></param>
        public IntRandomizer(Random random) {
            _random = random;
        }

        /// <summary>
        ///     <para>Returns a randomized integer within 0 - max.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Integer(int max) {
            return _random.Next(max);
        }

        /// <summary>
        ///     <para>Returns a randomized integer within min and max</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Integer(int min, int max) {
            if (max <= min)
                throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.Next(min, max);
        }

        /// <summary>
        ///     <para>Returns a randomized integer within 0 and int.MaxValue</para>
        /// </summary>
        /// <returns></returns>
        public int Integer() {
            return _random.Next();
        }
    }
}