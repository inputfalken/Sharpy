using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name {
    public class NameFactory {
        private const string FilePath = "Data/Types/Name/data.json";
        private readonly IGenerator _generator;
        private readonly IEnumerable<NameRepository> _nameRepositories;

        public NameFactory(IGenerator generator = null) {
            _generator = generator ?? new RandomGenerator();
            _nameRepositories = JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(File.ReadAllText(FilePath));
        }

        /// <summary>
        ///     Filters repeated strings from argument
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<string> RemoveDuplicatedData(IEnumerable<string> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);

        /// <summary>
        ///     Initialises a function that generates last names whose data is not filtered at all.
        /// </summary>
        /// <returns>
        ///     Returns a function which will generate a random last name without any filtering
        /// </returns>
        public Func<string> LastNameInitialiser()
            => GenerateName(_nameRepositories
                .SelectMany(repository => repository.LastNames)
                .ToList());

        /// <summary>
        ///     Initialises a function that generates last names whose data is filtered by Country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns a function which will generate last names filtered by Country
        /// </returns>
        public Func<string> LastNameInitialiser(string country)
            => GenerateName(_nameRepositories
                .Where(repository => repository.Origin.Country == country)
                .SelectMany(repository => repository.LastNames)
                .ToList());

        /// <summary>
        /// Initalises a function that generates last names whose data is filtered by region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public Func<string> LastNameInitialiser(Region region)
            => GenerateName(GetRepositorysByRegion(region)
                .SelectMany(repository => repository.LastNames)
                .ToList());

        /// <summary>
        ///     Initialises a function to generate firstnames whose data is not filtered at all.
        /// </summary>
        /// <returns>
        ///     Returns a function will will generate a random name without any filtering.
        /// </returns>
        public Func<string> FirstNameInitialiser()
            => GenerateName(_nameRepositories
                .SelectMany(repository => repository.MixedFirstNames)
                .ToList());


        //contains repeated data need to filter out
        /// <summary>
        ///     Initalizes a function to generate first names whose data is filtered by region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public Func<string> FirstNameInitialiser(Region region)
            => GenerateName(GetRepositorysByRegion(region)
                .SelectMany(repository => repository.MixedFirstNames)
                .ToList());

        /// <summary>
        ///     Creates a function whose data is filtered by Country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns a function that returns names based on Country.
        /// </returns>
        public Func<string> FirstNameInitialiser(string country)
            => GenerateName(_nameRepositories
                .Single(repository => repository.Origin.Country == country).MixedFirstNames);

        /// <summary>
        ///     Initialises a function to generate firstnames whose data is filtered by Gender.
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>
        ///     Returns a function which will generate names filtered by Gender
        /// </returns>
        public Func<string> FirstNameInitialiser(Gender gender)
            => GenerateName(gender == Gender.Female
                ? _nameRepositories.SelectMany(repository => repository.FemaleFirstNames).ToList()
                : _nameRepositories.SelectMany(repository => repository.MaleFirstNames).ToList());


        /// <summary>
        ///     Initialises a function to generate firstnames whose data is filtered by Gender & Country.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="gender"></param>
        /// <returns>
        ///     Returns a function which will generate names filtered by Gender & Country.
        /// </returns>
        public Func<string> FirstNameInitialiser(string country, Gender gender)
            => GenerateName(gender == Gender.Female
                ? _nameRepositories.Single(repository => repository.Origin.Country == country).FemaleFirstNames
                : _nameRepositories.Single(repository => repository.Origin.Country == country).MaleFirstNames);


        /// <summary>
        ///     Generates a name from from list
        /// </summary>
        /// <param name="names"></param>
        /// <returns>
        ///     Returns the Generator
        /// </returns>
        private Func<string> GenerateName(List<string> names)
            => () => _generator.Generate(names);

        private IEnumerable<NameRepository> GetRepositorysByRegion(Region region) {
            switch (region) {
                case Region.Europe:
                    return _nameRepositories.Where(repository => repository.Origin.Region == Europe);
                case Region.CentralAmerika:
                    return _nameRepositories.Where(repository => repository.Origin.Region == CentralAmerica);
                case Region.NorthAmerica:
                    return _nameRepositories.Where(repository => repository.Origin.Region == NorthAmerica);
                case Region.SouthAmerica:
                    return _nameRepositories.Where(repository => repository.Origin.Region == SouthAmerica);
                default:
                    throw new ArgumentOutOfRangeException(nameof(region), region, null);
            }
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        // Is generated from json
        private class NameRepository {
            public readonly List<string> FemaleFirstNames;
            public readonly List<string> LastNames;
            public readonly List<string> MaleFirstNames;
            public readonly Origin Origin;

            //TODO configure json file so i can have plural names for the collections
            public NameRepository(List<string> female, List<string> male, List<string> lastName,
                string country, string region) {
                FemaleFirstNames = female;
                MaleFirstNames = male;
                LastNames = lastName;
                Origin = new Origin(country, region);
            }

            public List<string> MixedFirstNames => FemaleFirstNames.Concat(MaleFirstNames).ToList();
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