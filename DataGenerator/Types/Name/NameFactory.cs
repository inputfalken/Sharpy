using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name {
    public class NameFactory {
        private readonly IGenerator _generator;
        private readonly IEnumerable<NameRepository> _nameRepositories;
        private const string FilePath = "Data/Types/Name/data.json";

        public NameFactory(IGenerator generator) {
            _generator = generator;
            _nameRepositories = JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(File.ReadAllText(FilePath));
        }

        /// <summary>
        /// Filters repeated strings from argument
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
                .SelectMany(name => name.LastNames)
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
                .Where(name => name.Origin.Country == country)
                .SelectMany(name => name.LastNames)
                .ToList());

        /// <summary>
        ///     Initialises a function to generate firstnames whose data is not filtered at all.
        /// </summary>
        /// <returns>
        ///     Returns a function will will generate a random name without any filtering.
        /// </returns>
        public Func<string> FirstNameInitialiser()
            => GenerateName(_nameRepositories
                .SelectMany(name => name.MixedFirstNames)
                .ToList());


        //contains repeated data need to filter out
        public Func<string> FirstNameInitialiser(Region region)
            => GenerateName(GetNameRepositorysBasedOnRegion(region)
                .SelectMany(name => name.MixedFirstNames)
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
                ? _nameRepositories.SelectMany(name => name.FemaleFirstNames).ToList()
                : _nameRepositories.SelectMany(name => name.MaleFirstNames).ToList());


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

        private IEnumerable<NameRepository> GetNameRepositorysBasedOnRegion(Region region) {
            switch (region) {
                case Region.Europe:
                    return _nameRepositories.Where(name => name.Origin.Region == Europe);
                case Region.CentralAmerika:
                    return _nameRepositories.Where(name => name.Origin.Region == CentralAmerica);
                case Region.NorthAmerica:
                    return _nameRepositories.Where(name => name.Origin.Region == NorthAmerica);
                case Region.SouthAmerica:
                    return _nameRepositories.Where(name => name.Origin.Region == SouthAmerica);
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