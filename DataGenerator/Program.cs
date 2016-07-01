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

            for (var i = 0; i < 10000; i++)
                names.Add(Factory.GetFirstName(Gender.Female, new RandomFetcher()));
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}