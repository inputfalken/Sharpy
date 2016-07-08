using System;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;
using DataGenerator.Types.Name.Regions;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var nameFactory = new NameFactory(new RandomGenerator());

            
            var s = nameFactory.GetFirstName(RegionName.Europe)(CountryName.Norway)(Gender.Male);


            nameFactory.GetFirstNameWithInterface(new Europe(Europe.Country.Denmark));
            //for (var i = 0; i < 10; i++) {
            //    //Console.WriteLine(firstName(Gender.Male));
            //    Console.WriteLine(nameFactory.GetLastName(CountryName.Norway));
            //}
        }
    }
}