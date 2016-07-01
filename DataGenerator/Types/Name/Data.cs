using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Types.Name
{
    public abstract class Data
    {
        protected readonly IGenerator<string> Generator;

        protected Data(IGenerator<string> generator) {
            Generator = generator;
        }

        protected static List<string> ReadFromFile(string filePath) {
            var list = new List<string>();
            using (var reader = new StreamReader(filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                    list.Add(line);

                return list;
            }
        }
    }
}