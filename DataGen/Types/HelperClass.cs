using System;

namespace DataGen.Types {
    internal static class HelperClass {
        private static Random Random { get; set; }

        internal static int SetRandomizer(int limit) {
            lock (Random)
                return Random.Next(limit);
        }

        internal static int SetRandomizer(int min, int max) {
            lock (Random)
                return Random.Next(min, max);
        }

        internal static void SetRandomizer(Random random) => Random = random;
    }
}