using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Types
{
    internal static class DataGenHelperClass
    {
        private static readonly Random Random = new Random();
        private static string Path { get; } = "Data/Types/";

        public static IReadOnlyList<string> ReadFromFile(string filePath) {
            var list = new List<string>();
            using (var reader = new StreamReader(Path + filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                    list.Add(line);

                return list;
            }
        }

        public static T FetchRandomItem<T>(IReadOnlyList<T> iReadOnlyList)
            => iReadOnlyList[Randomer(iReadOnlyList.Count)];

        private static int Randomer(int limit) {
            lock (Random)
                return Random.Next(limit);
        }
    }
}