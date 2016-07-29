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

        public IEnumerable<NameRepository> FilterByRegion(params string[] regions) {
            return from region in regions
                where region == _origin.Region
                select this;
        }

        public IEnumerable<NameRepository> FilterByCountry(params string[] countrys) {
            return from country in countrys
                where country == _origin.Country
                select this;
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