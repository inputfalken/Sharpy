using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGenerator
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var deserializeObject = JsonConvert.DeserializeObject<NameData>(File.ReadAllText("Data/Types/Name/data.json"));

            var count = deserializeObject.Regions.Count;
            Console.WriteLine(count);
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