using System;

namespace DataGen.Types {
    public static class HelperClass {
        private static Random Random { get; set; }

        public static int SetRandomizer(int limit) {
            lock (Random)
                return Random.Next(limit);
        }

        public static int SetRandomizer(int min, int max) {
            lock (Random)
                return Random.Next(min, max);
        }

        public static void SetRandomizer(Random random) => Random = random;
    }
}