using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataGen;
using DataGen.Types.Name;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests {
    ///<summary>
    /// These tests purpose is to make sure that you obtain the correct data from a json file.
    /// </summary>
    [TestFixture]
    internal class CommonNamesReadFileTest {
        #region Common Items

        private static NameFilter CommonNames => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
            File.ReadAllText(TestHelper.GetTestsPath() + @"\Data\Types\Name\NamesByOrigin.json")));


        private static readonly string[] EuropeCountries = {
            "Albania", "Austria",
            "Azerbaijan", "Belgium", "Croatia", "Czech", "Denmark", "Estonia", "FaroeIslands", "Finland", "France",
            "Germany", "Greece", "Hungary", "Ireland", "Italy", "Latvia", "Luxembourg", "Macedonia", "Malta",
            "Moldova", "Netherlands", "Norway", "Poland", "Portugal", "Romania", "Russia", "Slovakia", "Slovenia",
            "Spain", "Sweden", "Switzerland", "Turkey", "Ukraine", "UnitedKingdom"
        };

        private static readonly string[] NorthAmericanCountries = { "Canada", "Mexico", "Cuba", "UnitedStates" };
        private static readonly string[] SouthAmericanCountries = { "Argentina", "Brazil", "Columbia", "Paraguay" };
        private static readonly string[] CentralAmericanCountries = { "CostaRica", "Guatemala", "ElSalvador" };

        #endregion

        [Test]
        public void NameFilter_ByRegion_CentralAmerica() {
            var result = CommonNames.ByRegion(Region.CentralAmerica);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == Region.CentralAmerica) && result.Any());
            //Makes sure that each country in CentralAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country.ToString()).All(CentralAmericanCountries.Contains));
        }


        [Test]
        public void NameFilter_ByRegion_SouthAmerica() {
            var result = CommonNames.ByRegion(Region.SouthAmerica);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region ==Region.SouthAmerica) && result.Any());
            //Makes sure that each country in SouthAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country.ToString()).All(SouthAmericanCountries.Contains));
        }

        [Test]
        public void NameCollection_ByRegion_Europe() {
            var result = CommonNames.ByRegion(Region.Europe);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == Region.Europe) && result.Any());
            //Makes sure that each country in EuropeCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country.ToString()).All(EuropeCountries.Contains));
        }

        [Test]
        public void NameCollection_ByRegion_NorthAmerica() {
            var result = CommonNames.ByRegion(Region.NorthAmerica);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == Region.NorthAmerica) && result.Any());
            //Makes sure that each country in NorthAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country.ToString()).All(NorthAmericanCountries.Contains));
        }

        #region Country

        [Test]
        public void NameFilter_Arg_Country_Denmark() {
            //Makes sure that each object country is equal to current country.
            var result = CommonNames.ByCountry(Country.Denmark);
            Assert.IsTrue(result.All(name => name.Country == Country.Denmark));
        }


        [Test]
        public void NameFilter_Arg_Country_Norway() {
            const string country = "norway";
            //Makes sure that each object country is equal to current country.
            var result = CommonNames.ByCountry(Country.Norway);
            Assert.IsTrue(result.All(name => name.Country == Country.Norway));
        }

        [Test]
        public void NameFilter_Arg_Country_Sweden() {
            const string country = "sweden";
            //Makes sure that each object country is equal to current country.
            var result = CommonNames.ByCountry(Country.Sweden);
            Assert.IsTrue(result.All(name => name.Country == Country.Sweden));
        }

        [Test]
        public void NameFilter_Arg_FemaleFirstNames() {
            string[] names = {
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia"
            };
            var result = CommonNames.ByType(NameType.FemaleFirst);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_FemaleFirstNames_With_WrongData() {
            string[] names = {
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia",
                "Michael", "Jack"
            };
            var result = CommonNames.ByType(NameType.FemaleFirst);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }


        [Test]
        public void NameFilter_Arg_LastNames() {
            string[] names = {
                "Green", "Wood", "Pavlov", "Bogdanov", "Volkov", "Rusu", "Ceban", "Nagy", "Salo", "Niemi", "Koppel",
                "Urbonas", "Torres", "Calvo", "Romero", "Johnson", "Salas", "Vargas"
            };
            var result = CommonNames.ByType(NameType.LastNames);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_LastNames_With_WrongData() {
            string[] names = {
                "Green", "Wood", "Pavlov", "Bogdanov", "Volkov", "Rusu", "Ceban", "Nagy", "Salo", "Niemi", "Koppel",
                "Urbonas", "Torres", "Calvo", "Romero", "Johnson", "Salas", "Vargas",
                "Jacob", "Erik",
                "Maria", "Olga"
            };
            var result = CommonNames.ByType(NameType.LastNames);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MaleFirstNames() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack"
            };
            var result = CommonNames.ByType(NameType.MaleFirst);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MaleFirstNames_With_WrongData() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack",
                "Maria", "Olga"
            };
            var result = CommonNames.ByType(NameType.LastNames);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MixedFirstNames() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack",
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia"
            };
            var result = CommonNames.ByType(NameType.MixedFirstNames);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        #endregion
    }
}