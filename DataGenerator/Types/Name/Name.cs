using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Types.Name
{
    internal abstract class Name
    {
        protected readonly IFetchable<string> Fetchable;

        protected Name(IFetchable<string> fetchable) {
            Fetchable = fetchable;
        }

        protected string Data { private get; set; }

        //TODO DRY UP READFROMFILE, CURRENTLY DUPLICATED
        protected static List<string> ReadFromFile(string filePath) {
            var list = new List<string>();
            using (var reader = new StreamReader(filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                    list.Add(line);

                return list;
            }
        }

        public override string ToString() => Data;
    }
}