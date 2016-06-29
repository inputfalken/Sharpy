using System;
using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Types
{
    internal class Name
    {
        /// <summary>
        ///     Will run once to populate lists which will contain random data reed from text files
        /// </summary>
        static Name() {
            FirstNames = DataGenHelperClass.ReadFromFile("Name/firstNames.txt");
            LastNames = DataGenHelperClass.ReadFromFile("Name/lastNames.txt");
        }

        /// <summary>
        ///     Will run on every instance to give data
        /// </summary>
        public Name() {
            FirstName = DataGenHelperClass.FetchRandomItem(FirstNames);
            LastName = DataGenHelperClass.FetchRandomItem(LastNames);
        }

        private static IReadOnlyList<string> FirstNames { get; }
        private static IReadOnlyList<string> LastNames { get; }
        private string FirstName { get; }
        private string LastName { get; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}