using System.Collections.Generic;

namespace DataGenerator.Types
{
    public class Country
    {
        static Country() {
            Titles = DataGenHelperClass.ReadFromFile("Country/country.txt");
            Initials = DataGenHelperClass.ReadFromFile("Country/seoCode.txt");
        }

        public Country() {
            var fetchRandomItem = DataGenHelperClass.FetchRandomBundledWithIndex(Titles);
            Title = fetchRandomItem.Item1;
            IsoCode = Initials[fetchRandomItem.Item2];
        }

        private static IReadOnlyList<string> Titles { get; }

        private static IReadOnlyList<string> Initials { get; }
        public string Title { get; }

        public string IsoCode { get; }
    }
}