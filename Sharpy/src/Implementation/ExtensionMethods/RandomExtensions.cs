using System;

namespace Sharpy.Implementation.ExtensionMethods {
    internal static class RandomExtensionMethods {
        internal static double NextDouble(this Random random, double max) => NextDouble(random, 0, max);

        internal static double NextDouble(this Random random, double min, double max) {
            if (max <= min) throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return random.NextDouble()*(max - min) + min;
        }
    }
}