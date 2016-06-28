using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var names = new List<Name>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++) {
               names.Add(new Name()); 
            }
            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
