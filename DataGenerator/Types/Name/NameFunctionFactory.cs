using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name {
    public class NameFunctionFactory {
        private const string FilePath = "Data/Types/Name/data.json";
        private const string Sweden = "sweden";
        private const string Norway = "norway";
        private const string Denmark = "denmark";
        private const string Russia = "russia";
        private const string Finland = "finland";
        private const string Spain = "spain";

        public NameFunctionFactory(IGenerator<string> generator) {
            Generator = generator;
            Names = JsonConvert.DeserializeObject<IEnumerable<Name>>(File.ReadAllText(FilePath));
        }

        private IEnumerable<Name> Names { get; }
        private IGenerator<string> Generator { get; }

        /// <summary>
        ///     Creates a function whose data is filtered by Country 
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns a function that returns names based on country
        /// </returns>
        public Func<string> NameFunctionCreator(Country country) {
            var commonName = GetCountry(country);
            return GenerateName(commonName.Female.Concat(commonName.Male).ToList());
        }


        /// <summary>
        ///     Creates a function whose data is not filtered at all
        /// </summary>
        /// <returns>
        ///     Returns a function will will generate a random name without any filtering
        /// </returns>
        public Func<string> NameFunctionCreator()
            => GenerateName(Names.SelectMany(name => name.Female
                .Concat(name.Male))
                .ToList());

        /// <summary>
        ///     Creates a function whose data is filtered by gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>
        ///     Returns a function which will generate names filtered by gender
        /// </returns>
        public Func<string> NameFunctionCreator(Gender gender)
            => GenerateName(gender == Gender.Female
                ? Names.SelectMany(name => name.Female).ToList()
                : Names.SelectMany(name => name.Male).ToList());

        /// <summary>
        ///     Creates a funcion whose data is filtered by Country & Gender
        /// </summary>
        /// <param name="country"></param>
        /// <param name="gender"></param>
        /// <returns>
        ///     Returns a function which will generate names filtered by Gender & Country
        /// </returns>
        public Func<string> NameFunctionCreator(Country country, Gender gender)
            => GenerateName(gender == Gender.Female
                ? GetCountry(country).Female
                : GetCountry(country).Male);

        /// <summary>
        ///     Generates Name from list
        /// </summary>
        /// <param name="names"></param>
        /// <returns>
        ///     Returns the Generator
        /// </returns>
        private Func<string> GenerateName(List<string> names)
            => () => Generator.Generate(names);

        /// <summary>
        /// Finds the correct Object based on country
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns the correct object pointing at the correct country
        /// </returns>
        private Name GetCountry(Country country) {
            switch (country) {
                case Country.Sweden:
                    return Names.First(name => name.Country == Sweden);
                case Country.Norway:
                    return Names.First(name => name.Country == Norway);
                case Country.Denmark:
                    return Names.First(name => name.Country == Denmark);
                case Country.Russia:
                    return Names.First(name => name.Country == Russia);
                case Country.Finland:
                    return Names.First(name => name.Country == Finland);
                case Country.Spain:
                    return Names.First(name => name.Country == Spain);
                default:
                    throw new ArgumentOutOfRangeException(nameof(country), country, null);
            }
        }

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

    /// <summary>
    ///     Used as argument in case if you want a name generated from a specifik country
    /// </summary>
    public enum Country {
        Sweden,
        Norway,
        Denmark,
        Russia,
        Spain,
        Finland
    }
}