using System;

namespace DataGen.Types {
    public static class HelperClass {
        public static Random Random { private get; set; } = new Random();

        public static int Randomizer(int limit) {
            lock (Random)
                return Random.Next(limit);
        }

        public static int Randomizer(int min, int max) {
            lock (Random)
                return Random.Next(min, max);
        }
    }
}