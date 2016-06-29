using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Types
{
    //TODO CREATE HELPER CLASS
    internal class Country
    {
        private static IReadOnlyList<string> ReadOnlyList { get; set; }
        static Country() {
            ReadOnlyList = DataGenHelperClass.ReadFromFile("Country/country.txt");
        }
    }
}