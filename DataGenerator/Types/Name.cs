﻿using System;
using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Types
{
    internal class Name
    {
        private static readonly Random Random = new Random();

        /// <summary>
        ///     Will run once to populate lists which will contain random data reed from text files
        /// </summary>
        static Name() {
            FirstNames = ReadFromFile("Data/Types/Name/firstNames.txt");
            LastNames = ReadFromFile("Data/Types/Name/lastNames.txt");
        }

        /// <summary>
        ///     Will run on every instance to give data
        /// </summary>
        public Name() {
            lock (Random)
                FirstName = FirstNames[Random.Next(FirstNames.Count)];
            lock (Random)
                LastName = LastNames[Random.Next(LastNames.Count)];
        }

        private static IReadOnlyList<string> FirstNames { get; }
        private static IReadOnlyList<string> LastNames { get; }
        private string FirstName { get; }
        private string LastName { get; }


        private static IReadOnlyList<string> ReadFromFile(string filePath) {
            var list = new List<string>();
            using (var reader = new StreamReader(filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                    list.Add(line);

                return list;
            }
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}