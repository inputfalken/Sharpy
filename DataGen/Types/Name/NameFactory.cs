using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

//Todo find a way for the user to decide if he only wants string of names or objects

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


        /// <summary>
        ///     Returns a iterator of unique last name whos data is filtered by country
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public static IEnumerable<string> LastNameCollection(params string[] countries) {
            var list = new List<string>();
            foreach (var country in countries) {
                var firstOrDefault = NameRepositories.FirstOrDefault(repository => repository.Origin.Country == country);
                if (firstOrDefault == null)
                    throw new NullReferenceException($"Country: {country} was not found");
                list.AddRange(firstOrDefault.LastNames);
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
        ///     Returns a iterator of unique first names whose data is filtered by country
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public static IEnumerable<string> FirstNameCollection(params string[] countries) {
            var list = new List<string>();
            foreach (var country in countries) {
                var singleOrDefault = NameRepositories
                    .SingleOrDefault(repository => repository.Origin.Country == country);
                if (singleOrDefault == null)
                    throw new NullReferenceException("Country Not Found");
                list.AddRange(singleOrDefault.MixedFirstNames);
            }
            return Filter.RepeatedData(list);
        }

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
        /// <param name="country"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static IEnumerable<string> FirstNameCollection(string country, Gender gender)
            => gender == Gender.Female
                ? NameRepositories.Single(repository => repository.Origin.Country == country).FemaleFirstNames
                : NameRepositories.Single(repository => repository.Origin.Country == country).MaleFirstNames;


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

        // ReSharper disable once ClassNeverInstantiated.Local
        // Is generated from json
        private class NameRepository {
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

        private class Origin {
            public readonly string Country;
            public readonly string Region;

            public Origin(string country, string region) {
                Country = country;
                Region = region;
            }
        }

        #region Regions

        private const string Europe = "europe";
        private const string NorthAmerica = "northAmerica";
        private const string SouthAmerica = "southAmerica";
        private const string CentralAmerica = "centralAmerica";

        #endregion
    }
}