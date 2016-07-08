using System;
using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Types
{
    public static class HelperClass
    {
        private static readonly Random Random = new Random();

        public static int Randomer(int limit) {
            lock (Random)
                return Random.Next(limit);
        }
    }
}