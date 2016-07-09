using System;
using System.Diagnostics;
using System.Linq;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var nameFactory = new NameFactory(new RandomGenerator());
            var generateName = nameFactory.GenerateName();
            Console.WriteLine(generateName);
        }
    }
}