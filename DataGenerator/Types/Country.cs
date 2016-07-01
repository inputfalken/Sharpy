using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Types
{
    public class Country
    {
        private static readonly List<string> Titles = ReadFromFile("Data/Types/Country/country.txt");

        private static readonly IReadOnlyList<string> SeoCodes =
            ReadFromFile("Data/Types/Country/seoCode.txt");


        public Country(IFetchable<string> ifFetchable) {
            var fetchWithIndex = ifFetchable.FetchWithIndex(Titles);
            Title = fetchWithIndex.Item1;
            IsoCode = SeoCodes[fetchWithIndex.Item2];
        }

        private string Title { get; }
        private string IsoCode { get; }

        private static List<string> ReadFromFile(string filePath) {
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