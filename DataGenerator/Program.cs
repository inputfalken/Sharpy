using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator {
    internal static class Program {
        private static void Main(string[] args) {
            var nameFunctionFactory = new NameFactory(new RandomGenerator());
            Console.WriteLine(nameFunctionFactory.LastNameInitialiser("unitedKingdom")());
        }
    }
}