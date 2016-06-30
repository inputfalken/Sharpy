using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var stopwatch = new Stopwatch();
            var names = new List<string>();

            stopwatch.Start();
            for (var i = 0; i < 10000; i++) {
                names.Add(Factory.GetLastName());
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}