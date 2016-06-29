using System.Collections.Generic;

namespace DataGenerator.Types
{
    public class Country
    {
        static Country() {
            Titles = DataGenHelperClass.ReadFromFile("Country/country.txt");
            SeoCodes = DataGenHelperClass.ReadFromFile("Country/seoCode.txt");
        }

        public Country() {
            var fetchRandomItem = DataGenHelperClass.FetchRandomBundledWithIndex(Titles);
            Title = fetchRandomItem.Item1;
            IsoCode = SeoCodes[fetchRandomItem.Item2];
        }

        private static IReadOnlyList<string> Titles { get; }

        private static IReadOnlyList<string> SeoCodes { get; }
        public string Title { get; }

        public string IsoCode { get; }
    }
}