using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator.Types.Name {
    public class NameFactory {
        private readonly IGenerator _generator;
        private readonly IEnumerable<NameRepository> _names;

        public NameFactory(IGenerator generator) {
            _generator = generator;
            _names = JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(File.ReadAllText(FilePath));
        }

        /// <summary>
        ///     Initialises a function that generates last names whose data is not filtered at all.
        /// </summary>
        /// <returns>
        ///     Returns a function which will generate a random last name without any filtering
        /// </returns>
        public Func<string> LastNameInitialiser()
            => GenerateName(_names
                .SelectMany(name => name.LastNames)
                .ToList());

        /// <summary>
        ///     Initialises a function that generates last names whose data is filtered by Country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns a function which will generate last names filtered by Country
        /// </returns>
        public Func<string> LastNameInitialiser(Country country)
            => GenerateName(_names
                .Where(name => name == GetNameBasedOnCountry(country))
                .SelectMany(name => name.LastNames)
                .ToList());


        /// <summary>
        ///     Initialises a function to generate firstnames whose data is not filtered at all.
        /// </summary>
        /// <returns>
        ///     Returns a function will will generate a random name without any filtering.
        /// </returns>
        public Func<string> FirstNameInitialiser()
            => GenerateName(_names
                .SelectMany(name => name.MixedFirstNames)
                .ToList());


        //contains repeated data need to filter out
        public Func<string> FirstNameInitialiser(Region region)
            => GenerateName(GetNamesBasedOnRegion(region)
                .SelectMany(name => name.MixedFirstNames)
                .ToList());

        /// <summary>
        ///     Creates a function whose data is filtered by Country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns a function that returns names based on Country.
        /// </returns>
        public Func<string> FirstNameInitialiser(Country country)
            => GenerateName(GetNameBasedOnCountry(country).MixedFirstNames);

        /// <summary>
        ///     Initialises a function to generate firstnames whose data is filtered by Gender.
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>
        ///     Returns a function which will generate names filtered by Gender
        /// </returns>
        public Func<string> FirstNameInitialiser(Gender gender)
            => GenerateName(gender == Gender.Female
                ? _names.SelectMany(name => name.FemaleFirstNames).ToList()
                : _names.SelectMany(name => name.MaleFirstNames).ToList());


        /// <summary>
        ///     Initialises a function to generate firstnames whose data is filtered by Gender & Country.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="gender"></param>
        /// <returns>
        ///     Returns a function which will generate names filtered by Gender & Country.
        /// </returns>
        public Func<string> FirstNameInitialiser(Country country, Gender gender)
            => GenerateName(gender == Gender.Female
                ? GetNameBasedOnCountry(country).FemaleFirstNames
                : GetNameBasedOnCountry(country).MaleFirstNames);


        /// <summary>
        ///     Generates NameRepository from list
        /// </summary>
        /// <param name="names"></param>
        /// <returns>
        ///     Returns the Generator
        /// </returns>
        private Func<string> GenerateName(List<string> names)
            =>
                () => _generator.Generate(names);

        private IEnumerable<NameRepository> GetNamesBasedOnRegion(Region region) {
            switch (region) {
                case Region.Europe:
                    return _names.Where(name => name.Origin.Region == Europe);
                case Region.CentralAmerika:
                    return _names.Where(name => name.Origin.Region == CentralAmerica);
                case Region.NorthAmerica:
                    return _names.Where(name => name.Origin.Region == NorthAmerica);
                case Region.SouthAmerica:
                    return _names.Where(name => name.Origin.Region == SouthAmerica);
                default:
                    throw new ArgumentOutOfRangeException(nameof(region), region, null);
            }
        }

        /// <summary>
        ///     Finds the correct Object based on country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>
        ///     Returns the correct object pointing at the correct Country.
        /// </returns>
        private NameRepository GetNameBasedOnCountry(Country country) {
            switch (country) {
                case Country.Sweden:
                    return _names.Single(name => name.Origin.Country == Sweden);
                case Country.Norway:
                    return _names.Single(name => name.Origin.Country == Norway);
                case Country.Denmark:
                    return _names.Single(name => name.Origin.Country == Denmark);
                case Country.Russia:
                    return _names.Single(name => name.Origin.Country == Russia);
                case Country.Finland:
                    return _names.Single(name => name.Origin.Country == Finland);
                case Country.Spain:
                    return _names.Single(name => name.Origin.Country == Spain);
                default:
                    throw new ArgumentOutOfRangeException(nameof(country), country, null);
            }
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        // Is generated from json
        private class NameRepository {
            public readonly List<string> FemaleFirstNames;
            public readonly List<string> LastNames;
            public readonly List<string> MaleFirstNames;
            public readonly Origin Origin;

            public NameRepository(List<string> femaleFirstNames, List<string> maleFirstNames, List<string> lastNames,
                string country, string region) {
                FemaleFirstNames = femaleFirstNames;
                MaleFirstNames = maleFirstNames;
                LastNames = lastNames;
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

        #region Countries

        private const string FilePath = "Data/Types/Name/data.json";
        private const string Sweden = "sweden";
        private const string Norway = "norway";
        private const string Denmark = "denmark";
        private const string Russia = "russia";
        private const string Finland = "finland";
        private const string Spain = "spain";

        #endregion

        #region Regions

        private const string Europe = "europe";
        private const string NorthAmerica = "northAmerica";
        private const string SouthAmerica = "southAmerica";
        private const string CentralAmerica = "centralAmerica";

        #endregion
    }
}