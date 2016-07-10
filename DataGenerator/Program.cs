using System;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator {
    internal static class Program {
        private static void Main(string[] args) {
            var nameFunctionFactory = new NameFunctionFactory(new RandomGenerator());

            var nameFunctionCreator = nameFunctionFactory.NameFunctionCreator();
            for (int i = 0; i < 100; i++) {
                Console.WriteLine(nameFunctionCreator());
            }
        }
    }
}