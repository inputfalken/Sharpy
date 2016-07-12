using System;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator {
    internal static class Program {
        private static void Main(string[] args) {
            var nameFunctionFactory = new NameFactory(new RandomGenerator());
            var nameFunctionCreator = nameFunctionFactory.FirstNameInitialiser(Region.Europe);
            for (int i = 0; i < 100; i++) {
                Console.WriteLine(nameFunctionCreator());
            }
        }
    }
}