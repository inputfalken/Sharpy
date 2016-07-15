using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name {
    public class NameFactory {
        private const string FilePath = "Data/Types/Name/data.json";
        private readonly IGenerator _generator;

        public NameFactory(IGenerator generator) {
            _generator = generator;
        }

        private static IEnumerable<NameRepository> NameRepositories
            => JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(File.ReadAllText(FilePath));


        /// <summary>
        ///     Initialises a function that generates last names whose data is not filtered at all.
        /// </summary>
        /// <returns>
        ///     Returns a function which will generate a random last name without any filtering
        /// </returns>
        public Func<string> LastNameGenerator()
            => GenerateName(LastNameCollection());

        /// <summary>
        ///     Returns a collection of unique last names whose data is not filtered
        /// </summary>
        /// <returns></returns>
        public static ImmutableList<string> LastNameCollection()
            => Filter.RepeatedData(NameRepositories.SelectMany(repository => repository.LastNames));

        /// <summary>
        ///     Initialises a function that generates last names whose data is filtered by Country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns a function which will generate last names filtered by Country
        /// </returns>
        public Func<string> LastNameGenerator(string country)
            => GenerateName(LastNameCollection(country));

        /// <summary>
        ///     Returns a collection of unique last name whos data is filtered by country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public static ImmutableList<string> LastNameCollection(string country)
            => ImmutableList.CreateRange(NameRepositories
                .Where(repository => repository.Origin.Country == country)
                .SelectMany(repository => repository.LastNames));


        /// <summary>
        ///     Returns a collection of unique lastname whose data is filtered by region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static ImmutableList<string> LastNameCollection(Region region)
            => Filter.RepeatedData(FilterByRegion(region)
                .SelectMany(repository => repository.LastNames));

        /// <summary>
        ///     Initialises a function to generate firstnames whose data is not filtered at all.
        /// </summary>
        /// <returns>
        ///     Returns a function will will generate a random name without any filtering.
        /// </returns>
        public Func<string> FirstNameGenerator()
            => GenerateName(FirstNameCollection());

        /// <summary>
        ///     Returns a collection of unique first names whose data is not filtered
        /// </summary>
        /// <returns></returns>
        public static ImmutableList<string> FirstNameCollection()
            => Filter.RepeatedData(ImmutableList.CreateRange(NameRepositories
                .SelectMany(repository => repository.MixedFirstNames)));

        //contains repeated data need to filter out
        /// <summary>
        ///     Initalizes a function to generate first names whose data is filtered by region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public Func<string> FirstNameGenerator(Region region)
            => GenerateName(FirstNameCollection(region));

        /// <summary>
        ///     Returns a collecion of unique first names whose data is filtered by region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static ImmutableList<string> FirstNameCollection(Region region)
            => Filter.RepeatedData(ImmutableList.CreateRange(FilterByRegion(region)
                .SelectMany(repository => repository.MixedFirstNames)));


        /// <summary>
        ///     Creates a function whose data is filtered by Country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns a function that returns names based on Country.
        /// </returns>
        public Func<string> FirstNameGenerator(string country)
            => GenerateName(FirstNameCollection(country));

        /// <summary>
        ///     Returns a collecion of unique first names whose data is filtered by country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public static ImmutableList<string> FirstNameCollection(string country)
            => NameRepositories
                .Single(repository => repository.Origin.Country == country).MixedFirstNames;

        /// <summary>
        ///     Initialises a function to generate firstnames whose data is filtered by Gender.
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>
        ///     Returns a function which will generate names filtered by Gender
        /// </returns>
        public Func<string> FirstNameGenerator(Gender gender)
            => GenerateName(FirstNameCollection(gender));


        /// <summary>
        ///     Returns a collecion of unique first names whose data is filtered by gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static ImmutableList<string> FirstNameCollection(Gender gender)
            => gender == Gender.Female
                ? Filter.RepeatedData(
                    ImmutableList.CreateRange(NameRepositories.SelectMany(repository => repository.FemaleFirstNames)))
                : Filter.RepeatedData(
                    ImmutableList.CreateRange(NameRepositories.SelectMany(repository => repository.MaleFirstNames)));


        /// <summary>
        ///     Initialises a function to generate firstnames whose data is filtered by Gender & Country.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="gender"></param>
        /// <returns>
        ///     Returns a function which will generate unique first names filtered by Gender & Country.
        /// </returns>
        public Func<string> FirstNameGenerator(string country, Gender gender)
            => GenerateName(FirstNameCollection(country, gender));

        /// <summary>
        ///     Returns a collection of unique first names filtered by country & gender
        /// </summary>
        /// <param name="country"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static ImmutableList<string> FirstNameCollection(string country, Gender gender)
            => gender == Gender.Female
                ? NameRepositories.Single(repository => repository.Origin.Country == country).FemaleFirstNames
                : NameRepositories.Single(repository => repository.Origin.Country == country).MaleFirstNames;

        /// <summary>
        ///     Generates a name from from list
        /// </summary>
        /// <param name="names"></param>
        /// <returns>
        ///     Returns the Generator
        /// </returns>
        private Func<string> GenerateName(ImmutableList<string> names)
            => () => _generator.Generate(names);

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
            //TODO configure json file so i can have plural names for the collections
            public NameRepository(ImmutableList<string> female, ImmutableList<string> male,
                ImmutableList<string> lastName,
                string country, string region) {
                FemaleFirstNames = female;
                MaleFirstNames = male;
                LastNames = lastName;
                Origin = new Origin(country, region);
            }

            public ImmutableList<string> FemaleFirstNames { get; }
            public ImmutableList<string> LastNames { get; }
            public ImmutableList<string> MaleFirstNames { get; }
            public Origin Origin { get; }

            public ImmutableList<string> MixedFirstNames
                => ImmutableList.CreateRange(FemaleFirstNames.Concat(MaleFirstNames));
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