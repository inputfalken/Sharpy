using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Types
{
    public class Country
    {
        private static IReadOnlyList<string> Titles { get; }

        private static IReadOnlyList<string> Initials { get; }
        public string Title { get; }

        public string IsoCode { get; }

        static Country() {
            Titles = DataGenHelperClass.ReadFromFile("Country/country.txt");
            Initials = DataGenHelperClass.ReadFromFile("Country/seoCode.txt");
        }

        public Country() {
            var fetchRandomItem = DataGenHelperClass.FetchRandomBundledWithIndex(Titles);
            Title = fetchRandomItem.Item1;
            IsoCode = Initials[fetchRandomItem.Item2];
        }
    }
}