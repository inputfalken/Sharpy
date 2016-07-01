using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Types.Name
{
    public abstract class Data<T>
    {
        protected readonly IGenerator<T> Generator;

        protected Data(IGenerator<T> generator) {
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