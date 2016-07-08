using System.Collections.Generic;
using System.Linq;

namespace DataGenerator.Types.Name.Regions
{
    internal class Europe : IRegion
    {
        private readonly Country _country;
        //TODO find way to make this get initated in constructor so the data can be inmutable
        private IEnumerable<Name.Country> CountryList { get; set; } = new List<Name.Country>();

        public Europe(Country country) {
            _country = country;
        }

        public void SetCountries(IEnumerable<Region> regions)
            => CountryList = regions.First(region => region.Name == "europe").Countries;

        public Name.Country GetCountry() {
            switch (_country) {
                case Country.Sweden:
                    return CountryList.First(country => country.Name == "sweden");
                case Country.Norway:
                    return null;
                case Country.Denmark:
                    return null;
                default:
                    return null;
            }
        }

        internal enum Country
        {
            Sweden,
            Norway,
            Denmark
        }
    }
}