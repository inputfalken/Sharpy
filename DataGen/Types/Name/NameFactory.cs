using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

//Todo Rework json again to either camelCase or PascalCase was better...

namespace DataGen.Types.Name {
    public static class NameFactory {
        private const string FilePath = "Data/Types/Name/data.json";

        private static IEnumerable<NameRepository> NameRepositories
            => JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(File.ReadAllText(FilePath));


        /// <summary>
        ///     Returns a iterator of unique last names whose data is not filtered
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> LastNameCollection()
            => Filter.RepeatedData(NameRepositories.SelectMany(repository => repository.LastNames));


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

        public static IEnumerable<string> NameCollection(Func<NameRepository, IEnumerable<string>> func, Region region) {
            var list = new List<string>();
            var enumer = FilterByRegion(region);
            foreach (var nameRepository in enumer) {
                list.AddRange(func(nameRepository));
            }
            return Filter.RepeatedData(list);
        }


        /// <summary>
        ///     Returns a iterator of unique lastname whose data is filtered by region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IEnumerable<string> LastNameCollection(Region region)
            => Filter.RepeatedData(FilterByRegion(region)
                .SelectMany(repository => repository.LastNames));


        /// <summary>
        ///     Returns a iterator of unique first names whose data is not filtered
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> FirstNameCollection()
            => Filter.RepeatedData(NameRepositories
                .SelectMany(repository => repository.MixedFirstNames));

        /// <summary>
        ///     Returns a iterator of unique first names whose data is filtered by region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IEnumerable<string> FirstNameCollection(Region region)
            => Filter.RepeatedData(FilterByRegion(region)
                .SelectMany(repository => repository.MixedFirstNames));


        /// <summary>
        ///     Returns a iterator of unique first names whose data is filtered by gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static IEnumerable<string> FirstNameCollection(Gender gender)
            => gender == Gender.Female
                ? Filter.RepeatedData(NameRepositories.SelectMany(repository => repository.FemaleFirstNames))
                : Filter.RepeatedData(NameRepositories.SelectMany(repository => repository.MaleFirstNames));


        /// <summary>
        ///     Returns a iterator of unique first names filtered by country & gender
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="countries"></param>
        /// <returns></returns>
        public static IEnumerable<string> FirstNameCollection(Gender gender, params string[] countries) {
            var list = new List<string>();
            foreach (var country in countries) {
                var singleOrDefault = NameRepositories
                    .SingleOrDefault(repository => repository.Origin.Country == country);
                if (singleOrDefault == null)
                    throw new NullReferenceException("Country Not Found");
                list.AddRange(gender == Gender.Female
                    ? singleOrDefault.FemaleFirstNames
                    : singleOrDefault.MaleFirstNames);
            }
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(region), region, null);
            }
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