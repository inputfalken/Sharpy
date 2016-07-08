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

        public Func<CountryEnum, Func<Gender, string>> SelectRegion(WorldRegion worldRegion) {
            switch (worldRegion) {
                case WorldRegion.CentralAmerica:
                    var region = NameData.Regions.First(region1 => region1.Name == "centralAmerica");
                    return countryEnum => {
                        Country selectedCountry;
                        switch (countryEnum) {
                            case CountryEnum.Sweden:
                                selectedCountry = region.Countries.First(country => country.Name == "sweden");
                                break;
                            case CountryEnum.Norway:
                                selectedCountry = region.Countries.First(country => country.Name == "norway");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
                        }
                        return
                            gender => gender == Gender.Female
                                ? Generator.Generate(selectedCountry.CommonName.Female)
                                : Generator.Generate(selectedCountry.CommonName.Male);
                    };
                case WorldRegion.NorthAmerica:
                    return GetFirstName;
                case WorldRegion.Europe:
                    return GetFirstName;
                case WorldRegion.SouthAmerica:
                    return GetFirstName;
                default:
                    throw new ArgumentOutOfRangeException(nameof(worldRegion), worldRegion, null);
            }
        }

        //Create another to return a list with many names with same requirement
        public Func<Gender, string> GetFirstName(CountryEnum countryEnum) => gender => {
            switch (countryEnum) {
                case CountryEnum.Sweden:
                    var sweden = FindCountryByName("sweden", "europe");
                    return
                        Generator.Generate(gender == Gender.Female ? sweden.CommonName.Female : sweden.CommonName.Male);
                case CountryEnum.Norway:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
            }
        };

        private Country FindCountryByName(string countryName, string regionName)
            =>
                NameData.Regions.First(region => region.Name == regionName)
                    .Countries.First(country => country.Name == countryName);

        public string GetLastName(CountryEnum countryEnum) {
            switch (countryEnum) {
                case CountryEnum.Sweden:
                    return Generator.Generate(FindCountryByName("sweden", "europe").CommonName.LastName);

                case CountryEnum.Norway:
                    return Generator.Generate(FindCountryByName("norway", "europe").CommonName.LastName);
                default:
                    throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
            }
        }
    }
}