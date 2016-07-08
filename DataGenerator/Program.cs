using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DataGenerator.Types;
using Newtonsoft.Json;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var nameFactory = new NameFactory(new RandomGenerator());
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            var firstName = nameFactory.GetFirstName(CountryEnum.Sweden);
            for (int i = 0; i < 10; i++) {
                Console.WriteLine(firstName(Gender.Male)); 
            }
        }
    }

    enum CountryEnum
    {
        Sweden,
        Norway
    }

    internal class NameFactory
    {
        private IGenerator<string> Generator { get; }

        public NameFactory(IGenerator<string> generator) {
            Generator = generator;
            NameData = JsonConvert.DeserializeObject<NameData>(File.ReadAllText("Data/Types/Name/data.json"));
        }

        private NameData NameData { get; }

        //Create another to return many with same requirement
        public Func<Gender, string> GetFirstName(CountryEnum countryEnum) => gender => {
            switch (countryEnum) {
                case CountryEnum.Sweden:
                    var sweden =
                        NameData.Regions.First(region => region.Name == "europe")
                            .Countries.First(country => country.Name == "sweden");
                    return
                        Generator.Generate(gender == Gender.Female
                            ? sweden.CommonName.Female
                            : sweden.CommonName.Male);
                case CountryEnum.Norway:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException(nameof(countryEnum), countryEnum, null);
            }
        };


        public string GetLastName() {
            return "lastName";
        }
    }

    internal class NameData
    {
        public NameData(List<Region> regions) {
            Regions = regions;
        }

        public List<Region> Regions { get; }
    }

    internal class Region
    {
        public Region(List<Country> countries, string name) {
            Countries = countries;
            Name = name;
        }

        public string Name { get; }
        public List<Country> Countries { get; }
    }

    internal class Country
    {
        public Country(string name, CommonName commonName) {
            Name = name;
            CommonName = commonName;
        }

        public string Name { get; }
        public CommonName CommonName { get; }
    }

    internal class CommonName
    {
        public CommonName(List<string> female, List<string> male, List<string> lastName) {
            Female = female;
            Male = male;
            LastName = lastName;
        }

        public List<string> Female { get; }
        public List<string> Male { get; }
        public List<string> LastName { get; }
    }
}