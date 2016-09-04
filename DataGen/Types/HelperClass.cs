using System;

namespace DataGen.Types {
    public static class HelperClass {
        private static Random Random { get; set; }

        public static int Randomizer(int limit) {
            lock (Random)
                return Random.Next(limit);
        }

        public static int Randomizer(int min, int max) {
            lock (Random)
                return Random.Next(min, max);
        }

        public static void Randomizer(Random random) => Random = random;
    }
}