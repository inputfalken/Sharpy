using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var stopwatch = new Stopwatch();
            var names = new List<Data<string>>();
            stopwatch.Start();
            var stringGenerator = Factory.StringGenerator(new RandomGenerator());
            for (var i = 0; i < 10000; i++)
                names.Add(stringGenerator.FirstName(Gender.Female));
            stopwatch.Stop();
            //TODO OVERRIDE TOSTRING
            Console.WriteLine(names[0].ToString());
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}