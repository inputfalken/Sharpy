using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataGen.Types.Name {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NameRepository {
        public NameRepository(IEnumerable<string> femaleFirstNames, IEnumerable<string> maleFirstNames,
            IEnumerable<string> lastNames, string country, string region) {
            FemaleFirstNames = femaleFirstNames;
            MaleFirstNames = maleFirstNames;
            LastNames = lastNames;
            _origin = new Origin(country, region);
        }

        public IEnumerable<string> FemaleFirstNames { get; }
        public IEnumerable<string> LastNames { get; }
        public IEnumerable<string> MaleFirstNames { get; }

        public IEnumerable<string> MixedFirstNames
            => FemaleFirstNames.Concat(MaleFirstNames);

        private readonly Origin _origin;

        public NameRepository FilterByRegion(Region region) {
            switch (region) {
                case Region.Europe:
                    return _origin.Region == Europe ? this : null;
                case Region.CentralAmerika:
                    return _origin.Region == CentralAmerica ? this : null;
                case Region.NorthAmerica:
                    return _origin.Region == NorthAmerica ? this : null;
                case Region.SouthAmerica:
                    return _origin.Region == SouthAmerica ? this : null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(region), region, null);
            }
        }

        public NameRepository FilterByCountry(params string[] countrys) {
            foreach (var country in countrys) {
                if (country == _origin.Country) {
                    return this;
                }
            }
            return null;
        }

        #region Regions

        private const string Europe = "Europe";
        private const string NorthAmerica = "North America";
        private const string SouthAmerica = "South America";
        private const string CentralAmerica = "Central America";

        #endregion

        private class Origin {
            public readonly string Country;
            public readonly string Region;

            public Origin(string country, string region) {
                Country = country;
                Region = region;
            }
        }
    }
}