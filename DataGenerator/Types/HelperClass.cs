using System;
using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Types
{
    public static class HelperClass
    {
        private static readonly Random Random = new Random();


        public static List<string> ReadFromFile(string filePath) {
            var list = new List<string>();
            using (var reader = new StreamReader(filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                    list.Add(line);

                return list;
            }
        }

        public static int Randomer(int limit) {
            lock (Random)
                return Random.Next(limit);
        }
    }
}