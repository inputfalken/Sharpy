using System.Collections.Generic;
using System.Linq;

namespace DataGenerator.Types.Name.Regions
{
    internal class Europe : IRegion
    {
        private readonly EuropeanCountries _country;
        private List<Country> Countries { get; set; } = new List<Country>();

        public Europe(EuropeanCountries country) {
            _country = country;
        }

        public void SetCountries(NameData nameData)
            => Countries = nameData.Regions.First(region => region.Name == "europe").Countries;

        public Country GetCountry() {
            switch (_country) {
                case EuropeanCountries.Sweden:
                    return Countries.First(country => country.Name == "sweden");
                case EuropeanCountries.Norway:
                    return null;
                case EuropeanCountries.Denmark:
                    return null;
                default:
                    return null;
            }
        }
    }
}