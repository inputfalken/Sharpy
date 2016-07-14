using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator {
    internal static class Program {
        private static void Main(string[] args) {
            var nameFunctionFactory = new NameFactory();
            Console.WriteLine(nameFunctionFactory.LastNameInitialiser(Region.Europe)());
        }
    }
}