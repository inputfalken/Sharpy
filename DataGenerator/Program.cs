using System;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var nameFactory = new NameFactory(new RandomGenerator());

            
            var s = nameFactory.GetFirstName(RegionName.Europe)(CountryName.Norway)(Gender.Male);


            //for (var i = 0; i < 10; i++) {
            //    //Console.WriteLine(firstName(Gender.Male));
            //    Console.WriteLine(nameFactory.GetLastName(CountryName.Norway));
            //}
        }
    }
}