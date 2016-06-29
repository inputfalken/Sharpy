using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Types
{
    internal class Country
    {
        private static List<string> Titles { get; set; }

        private static List<string> Initials { get; set; }
        public string Title { get; private set; }

        public string Initial { get; private set; }

        static Country() {
            var readFromFile = DataGenHelperClass.ReadFromFile("Country/country.txt");
            //foreach (var line in readFromFile) {
            //    var strings = line.Split('|');
            //    Initials.Add(strings[0]);
            //    Titles.Add(strings[1]);
            //}
            var strings = readFromFile[0].Split('|');
            //PROGRAMS DIES HERE
            Titles.Add(strings[1]);
            Initials.Add(strings[0]);
        }

        public Country() {
            Title = DataGenHelperClass.FetchRandomItem(Titles);
            Initial = DataGenHelperClass.FetchRandomItem(Initials);
        }
    }
}