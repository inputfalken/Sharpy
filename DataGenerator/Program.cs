﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator {
    internal static class Program {
        private static void Main(string[] args) {
            var nameFactory = new NameFactory(new RandomGenerator());
            Console.WriteLine(nameFactory.LastNameGenerator(Region.Europe)());
        }
    }
}