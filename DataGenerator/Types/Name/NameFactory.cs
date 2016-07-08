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

        public Func<CountryName, Func<Gender, string>> GetFirstName(RegionName regionName) {
            Region selectedRegion;
            switch (regionName) {
                case RegionName.CentralAmerica:
                    selectedRegion = NameData.Regions.First(region => region.Name == "centralAmerica");
                    break;
                case RegionName.NorthAmerica:
                    selectedRegion = NameData.Regions.First(region => region.Name == "europe");
                    break;
                case RegionName.Europe:
                    selectedRegion = NameData.Regions.First(region => region.Name == "europe");
                    break;
                case RegionName.SouthAmerica:
                //return GetFirstName;
                default:
                    throw new ArgumentOutOfRangeException(nameof(regionName), regionName, null);
            }
            return SelectCountry(selectedRegion);
        }

        private Func<CountryName, Func<Gender, string>> SelectCountry(Region region) {
            Country selectedCountry;
            return countryEnum => {
                switch (countryEnum) {
                    case CountryName.Sweden:
                        selectedCountry = region.Countries.FirstOrDefault(country => country.Name == "sweden");
                        break;
                    case CountryName.Norway:
                        selectedCountry = region.Countries.FirstOrDefault(country => country.Name == "norway");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
                }
                if (selectedCountry == null)
                    throw new Exception("selectedCountry does not exist in selected region");
                return SelectName(selectedCountry);
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