using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name {
    internal class NameFunctionFactory {
        private const string FilePath = "Data/Types/Name/data.json";
        private const string Sweden = "sweden";
        private const string Norway = "norway";
        private const string Denmark = "denmark";

        public NameFunctionFactory(IGenerator<string> generator) {
            Generator = generator;
            Names = JsonConvert.DeserializeObject<IEnumerable<Name>>(File.ReadAllText(FilePath));
        }

        private IEnumerable<Name> Names { get; }
        private IGenerator<string> Generator { get; }

        //TODO Make methods return named methods which can be overloaded with aditional filters 

        /// <summary>
        ///     Gives a function that returns names based on country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public Func<string> NameFunctionCreator(Country country) {
            var commonName = GetCountry(country);
            return GenerateName(commonName.Female.Concat(commonName.Male).ToList());
        }

        private Name GetCountry(Country country) {
            switch (country) {
                case Country.Sweden:
                    return Names.First(name => name.Country == Sweden);
                case Country.Norway:
                    return Names.First(name => name.Country == Norway);
                case Country.Denmark:
                    return Names.First(name => name.Country == Denmark);
                default:
                    throw new ArgumentOutOfRangeException(nameof(country), country, null);
            }
        }

        /// <summary>
        ///     Gives a functions that returns names from a huge collection of names
        ///     TODO Find a way to make this function not pick up repeated names
        /// </summary>
        /// <returns>string</returns>
        public Func<string> NameFunctionCreator()
            => GenerateName(Names.SelectMany(name => name.Female
                .Concat(name.Male))
                .ToList());

        /// <summary>
        /// gives a function which randoms a name filtered by gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public Func<string> NameFunctionCreator(Gender gender)
            => GenerateName(gender == Gender.Female
                ? Names.SelectMany(name => name.Female).ToList()
                : Names.SelectMany(name => name.Male).ToList());

        public Func<string> NameFunctionCreator(Country country, Gender gender)
            => GenerateName(gender == Gender.Female
                ? GetCountry(country).Female
                : GetCountry(country).Male);

        private Func<string> GenerateName(List<string> names) => () => Generator.Generate(names);

        // ReSharper disable once ClassNeverInstantiated.Local
        // Is generated from json
        private class Name {
            public readonly string Country;
            public readonly List<string> Female;
            public readonly List<string> LastName;
            public readonly List<string> Male;
            public readonly string Region;

            public Name(List<string> female, List<string> male, List<string> lastName, string country,
                string region) {
                Female = female;
                Male = male;
                LastName = lastName;
                Country = country;
                Region = region;
            }
        }
    }

    internal enum Country {
        Sweden,
        Norway,
        Denmark
    }
}