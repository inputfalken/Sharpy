using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name
{
    internal class NameFactory
    {
        public NameFactory(IGenerator<string> generator) {
            Generator = generator;
            NameData = JsonConvert.DeserializeObject<NameData>(File.ReadAllText("Data/Types/Name/data.json"));
        }

        private IGenerator<string> Generator { get; }

        private NameData NameData { get; }

        //Create another to return a list with many names with same requirement
        public Func<Gender, string> GetFirstName(CountryEnum countryEnum) => gender => {
            switch (countryEnum) {
                case CountryEnum.Sweden:
                    var sweden = FindCountryByName("sweden", "europe");
                    return
                        Generator.Generate(gender == Gender.Female
                            ? sweden.CommonName.Female
                            : sweden.CommonName.Male);
                case CountryEnum.Norway:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
            }
        };

        private Country FindCountryByName(string countryName, string regionName)
            => NameData.Regions.First(region => region.Name == regionName)
                .Countries.First(country => country.Name == countryName);

        public string GetLastName(CountryEnum countryEnum) {
            switch (countryEnum) {
                case CountryEnum.Sweden:
                    return
                        Generator.Generate(
                            FindCountryByName("sweden", "europe").CommonName.LastName);

                case CountryEnum.Norway:
                    return Generator.Generate(
                        FindCountryByName("norway", "europe")
                            .CommonName.LastName);
                default:
                    throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
            }
        }
    }
}