using System;

namespace DataGen.Types {
    internal static class HelperClass {
        private static Random Random { get; set; } = new Random();

        internal static int Randomizer(int limit) {
            lock (Random)
                return Random.Next(limit);
        }

        internal static int Randomizer(int min, int max) {
            lock (Random)
                return Random.Next(min, max);
        }

        internal static string FirstletterTolower(string text) => char.ToLowerInvariant(text[0]) + text.Substring(1);

        internal static void SetRandomizer(Random random) => Random = random;
    }
}