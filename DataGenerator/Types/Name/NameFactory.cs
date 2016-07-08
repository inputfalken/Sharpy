using System;
using System.IO;
using System.Linq;
using DataGenerator.Types.Name.Regions;
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

        public Func<Gender, string> GetFirstNameWithInterface(IRegion iRegion) {
            iRegion.SetCountries(NameData);
            return SelectName(iRegion.GetCountry());
        }

        public Func<CountryName, Func<Gender, string>> GetFirstName(RegionName regionName) {
            switch (regionName) {
                case RegionName.CentralAmerica:
                    return SelectCountry(NameData.Regions.First(region => region.Name == "centralAmerica"));
                case RegionName.NorthAmerica:
                    return SelectCountry(NameData.Regions.First(region => region.Name == "northAmerica"));
                case RegionName.Europe:
                    return SelectCountry(NameData.Regions.First(region => region.Name == "europe"));
                case RegionName.SouthAmerica:
                    return SelectCountry(NameData.Regions.First(region => region.Name == "southAmerica"));
                default:
                    throw new ArgumentOutOfRangeException(nameof(regionName), regionName, null);
            }
        }

        //How do i throw exception if no country was found in a dry way without out mutating a variable?
        private Func<CountryName, Func<Gender, string>> SelectCountry(Region region) {
            return countryEnum => {
                switch (countryEnum) {
                    case CountryName.Sweden:
                        return SelectName(region.Countries.FirstOrDefault(country => country.Name == "sweden"));
                    case CountryName.Norway:
                        return SelectName(region.Countries.FirstOrDefault(country => country.Name == "norway"));
                    default:
                        throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
                }
            };
        }


        private Func<Gender, string> SelectName(Country country) {
            return
                gender =>
                    gender == Gender.Female
                        ? Generator.Generate(country.CommonName.Female)
                        : Generator.Generate(country.CommonName.Male);
        }


        private Country FindCountryByName(string countryName, string regionName)
            =>
                NameData.Regions.First(region => region.Name == regionName)
                    .Countries.First(country => country.Name == countryName);

        public string GetLastName(CountryName countryName) {
            switch (countryName) {
                case CountryName.Sweden:
                    return Generator.Generate(FindCountryByName("sweden", "europe").CommonName.LastName);

                case CountryName.Norway:
                    return Generator.Generate(FindCountryByName("norway", "europe").CommonName.LastName);
                default:
                    throw new ArgumentOutOfRangeException(nameof(countryName), countryName, null);
            }
        }
    }
}