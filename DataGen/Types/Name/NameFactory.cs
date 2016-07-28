using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

//Todo Rework json again to either camelCase or PascalCase was better...
//Todo make this generic

namespace DataGen.Types.Name {
    public static class NameFactory {
        private const string FilePath = "Data/Types/Name/data.json";

        private static IEnumerable<NameRepository> NameRepositories
            => JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(File.ReadAllText(FilePath));


        /// <summary>
        /// Takes a function which will collect from specified countries/country. The function's argument is a NameRepository containing IEnumerable of different name types.
        /// </summary>
        /// <param name="func">A function who's argument will be a NameRepository for the selected countries</param>
        /// <param name="countries">Lets you decide which name type you are interested in</param>
        /// <returns></returns>
        public static IEnumerable<string> NameCollection(Func<NameRepository, IEnumerable<string>> func,
            params string[] countries) {
            var list = new List<string>();
            foreach (var country in countries) {
                var firstOrDefault = NameRepositories.FirstOrDefault(repository => repository.Origin.Country == country);
                if (firstOrDefault == null)
                    throw new NullReferenceException($"Country: {country} was not found");
                list.AddRange(func(firstOrDefault));
            }
            return Filter.RepeatedData(list);
        }

        /// <summary>
        /// Takes a function which will collect from specified regions/region. The function's argument is a NameRepository containing IEnumerable of different name types.
        /// </summary>
        /// <param name="func">A function who's argument will be a NameRepository for the selected countries</param>
        /// <param name="regions">Lets you decide which name type you are interested in</param>
        /// <returns></returns>
        public static IEnumerable<string> NameCollection(Func<NameRepository, IEnumerable<string>> func,
            params Region[] regions) {
            var list = new List<string>();
            foreach (var region in regions)
                foreach (var nameRepository in FilterByRegion(region))
                    list.AddRange(func(nameRepository));
            return Filter.RepeatedData(list);
        }

        /// <summary>
        /// Takes a function which will collect all avaiable data. The function's argument is a NameRepository containing IEnumerable of different name types.
        /// </summary>
        /// <param name="func">Lets you decide which name type you are interested in</param>
        /// <returns></returns>
        public static IEnumerable<string> NameCollection(Func<NameRepository, IEnumerable<string>> func) {
            var list = new List<string>();
            foreach (var nameRepository in NameRepositories)
                list.AddRange(func(nameRepository));
            return Filter.RepeatedData(list);
        }


        private static IEnumerable<NameRepository> FilterByRegion(Region region) {
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

        // Is generated from json
        // ReSharper disable once ClassNeverInstantiated.Global
        public class NameRepository {
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
            public Origin Origin { get; }

            public IEnumerable<string> MixedFirstNames
                => FemaleFirstNames.Concat(MaleFirstNames);
        }

        public class Origin {
            public readonly string Country;
            public readonly string Region;

            public Origin(string country, string region) {
                Country = country;
                Region = region;
            }
        }

        #region Regions

        private const string Europe = "Europe";
        private const string NorthAmerica = "North America";
        private const string SouthAmerica = "South America";
        private const string CentralAmerica = "Central America";

        #endregion
    }
}