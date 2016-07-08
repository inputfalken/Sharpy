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

            var firstName = nameFactory.GetFirstName(CountryEnum.Sweden);
            for (var i = 0; i < 10; i++) {
                Console.WriteLine(firstName(Gender.Male));
            }
        }
    }
}