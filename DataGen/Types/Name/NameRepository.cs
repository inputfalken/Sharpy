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
        private const string FilePath = "Data/Types/Name/data.json";

        public static IEnumerable<NameRepository> NameRepositories
            => JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(File.ReadAllText(FilePath));

        public NameRepository(IEnumerable<string> femaleFirstNames, IEnumerable<string> maleFirstNames,
            IEnumerable<string> lastNames, string country, string region) {
            FemaleFirstNames = femaleFirstNames;
            MaleFirstNames = maleFirstNames;
            LastNames = lastNames;
            Origin = new Origin(country, region);
        }

        public IEnumerable<string> FemaleFirstNames { get; }
        public IEnumerable<string> LastNames { get; }
        public IEnumerable<string> MaleFirstNames { get; }
        private Origin Origin { get; }

        public IEnumerable<string> MixedFirstNames
            => FemaleFirstNames.Concat(MaleFirstNames);

        public static IEnumerable<NameRepository> FilterByRegion(Region region) {
            switch (region) {
                case Region.Europe:
                    return NameRepositories.Where(repository => repository.Origin.Region == Europe);
                case Region.CentralAmerika:
                    return NameRepositories.Where(repository => repository.Origin.Region == CentralAmerica);
                case Region.NorthAmerica:
                    return NameRepositories.Where(repository => repository.Origin.Region == NorthAmerica);
                case Region.SouthAmerica:
                    return NameRepositories.Where(repository => repository.Origin.Region == SouthAmerica);
            }
            throw new ArgumentOutOfRangeException(nameof(region), region, null);
        }

        public static NameRepository FilterByCountry(string country)
            => NameRepositories.FirstOrDefault(repository => repository.Origin.Country == country);

        #region Regions

        private const string Europe = "Europe";
        private const string NorthAmerica = "North America";
        private const string SouthAmerica = "South America";
        private const string CentralAmerica = "Central America";

        #endregion
    }


    public class Origin {
        public readonly string Country;
        public readonly string Region;

        public Origin(string country, string region) {
            Country = country;
            Region = region;
        }
    }
}