using System;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator {
    internal static class Program {
        private static void Main(string[] args) {
            var nameFactory = new NameFunctionFactory(new RandomGenerator());
            var generateName = nameFactory.GenerateName(Country.Sweden, Gender.Female);
            for (int i = 0; i < 100; i++) {
                Console.WriteLine(generateName());
            }
        }
    }
}