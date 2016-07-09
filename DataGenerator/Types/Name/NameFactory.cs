using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name
{
    internal class NameFactory
    {
        public NameFactory(IGenerator<string> generator) {
            Generator = generator;
            Names =
                JsonConvert.DeserializeObject<IEnumerable<CommonName>>(
                    File.ReadAllText("Data/Types/Name/data.json"));
        }

        private IEnumerable<CommonName> Names { get; }
        private IGenerator<string> Generator { get; }

        public string GenerateName(Country country) {
            switch (country) {
                case Country.Sweden:
                    return Generator.Generate(Names.First(name => name.Country == "sweden").Male);
                case Country.Norway:
                    return Generator.Generate(Names.First(name => name.Country == "norway").Male);
                case Country.Denmark:
                    return Generator.Generate(Names.First(name => name.Country == "denmark").Male);
                default:
                    throw new ArgumentOutOfRangeException(nameof(country), country, null);
            }
        }

        /// <summary>
        /// Generates a random Name without any filtering
        /// </summary>
        /// <returns>string</returns>
        public string GenerateName() {
            var commonName = Generator.Generate(Names.ToList());
            var enumerable = commonName.Female.Concat(commonName.Male).ToList();
            return Generator.Generate(enumerable);
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        // Is generated to json
        private class CommonName
        {
            public readonly string Country;
            public readonly List<string> Female;
            public readonly List<string> LastName;
            public readonly List<string> Male;
            public readonly string Region;

            public CommonName(List<string> female, List<string> male, List<string> lastName, string country,
                string region) {
                Female = female;
                Male = male;
                LastName = lastName;
                Country = country;
                Region = region;
            }
        }
    }

    internal enum NameType
    {
        Firstname,
        LastName
    }

    internal enum Country
    {
        Sweden,
        Norway,
        Denmark
    }
}