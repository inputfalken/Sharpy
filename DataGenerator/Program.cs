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

            
            //var s = nameFactory.GetFirstName(RegionName.Europe)(CountryName.Norway)(Gender.Male);


            var firstNameWithInterface = nameFactory.GetFirstNameWithInterface(new Europe(Europe.Country.Sweden));
            var name = firstNameWithInterface(Gender.Male);
            Console.WriteLine(name);
            //for (var i = 0; i < 10; i++) {
            //    //Console.WriteLine(firstName(Gender.Male));
            //    Console.WriteLine(nameFactory.GetLastName(CountryName.Norway));
            //}
        }
    }
}