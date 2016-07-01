using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var stopwatch = new Stopwatch();
            var names = new List<Name>();
            stopwatch.Start();

            for (var i = 0; i < 10000; i++)
                names.Add(Factory.GetFirstName(Gender.Female));
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}