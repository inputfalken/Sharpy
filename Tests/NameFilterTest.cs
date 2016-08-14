using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataGen.Types.Name;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    internal class NameFilterTest {
        #region Common Items

        private static NameFilter Names => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
            File.ReadAllText(TestHelper.GetTestsPath() + @"\Data\Types\Name\NamesByOrigin.json")));


        private static readonly string[] EuropeCountries = {
            "albania", "austria",
            "azerbaijan", "belgium", "croatia", "czech", "denmark", "estonia", "faroeIslands", "finland", "france",
            "germany", "greece", "hungary", "ireland", "italy", "latvia", "luxembourg", "macedonia", "malta",
            "moldova", "netherlands", "norway", "poland", "portugal", "romania", "russia", "slovakia", "slovenia",
            "spain", "sweden", "switzerland", "turkey", "ukraine", "unitedKingdom"
        };

        private static readonly string[] NorthAmericanCountries = { "canada", "mexico", "cuba", "unitedStates" };
        private static readonly string[] SouthAmericanCountries = { "argentina", "brazil", "columbia", "paraguay" };
        private static readonly string[] CentralAmericanCountries = { "costaRica", "guatemala", "elSalvador" };

        #endregion

        #region Region

        [Test]
        public void NameFilter_Arg_Region_Foobar()
            => Assert.Throws<ArgumentException>(() => Names.ByRegion( "foobar"));

        [Test]
        public void NameFilter_Arg_Region_CentralAmerica() {
            const string region = "centralAmerica";
            var result = Names.ByRegion(region);

            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == region) && result.Any());
            //Makes sure that each country in CentralAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country).All(CentralAmericanCountries.Contains));
        }


        [Test]
        public void NameFilter_Arg_Region_SouthAmerica() {
            const string region = "southAmerica";
            var result = Names.ByRegion(region);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == region) && result.Any());
            //Makes sure that each country in SouthAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country).All(SouthAmericanCountries.Contains));
        }

        [Test]
        public void NameCollection_Arg_Region_Europe() {
            const string region = "europe";
            var result = Names.ByRegion(region);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == region) && result.Any());
            //Makes sure that each country in EuropeCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country).All(EuropeCountries.Contains));
        }

        [Test]
        public void NameCollection_Arg_Region_NorthAmerica() {
            const string region = "northAmerica";
            var result = Names.ByRegion(region);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == region) && result.Any());
            //Makes sure that each country in NorthAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country).All(NorthAmericanCountries.Contains));
        }

        #endregion

        #region Country

        [Test]
        public void NameFilter_Arg_Country_Foobar()
            => Assert.Throws<ArgumentException>(() => Names.ByCountry("foobar"));

        [Test]
        public void NameFilter_Arg_Country_Denmark() {
            const string country = "denmark";
            //Makes sure that each object country is equal to current country.
            var result = Names.ByCountry(country);
            Assert.IsTrue(result.All(name => name.Country == country));
        }


        [Test]
        public void NameFilter_Arg_Country_Norway() {
            const string country = "norway";
            //Makes sure that each object country is equal to current country.
            var result = Names.ByCountry(country);
            Assert.IsTrue(result.All(name => name.Country == country));
        }

        [Test]
        public void NameFilter_Arg_Country_Sweden() {
            const string country = "sweden";
            //Makes sure that each object country is equal to current country.
            var result = Names.ByCountry(country);
            Assert.IsTrue(result.All(name => name.Country == country));
        }

        [Test]
        public void NameFilter_Arg_FemaleFirstNames() {
            string[] names = {
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia"
            };
            var result = Names.FemaleFirstNames;
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_FemaleFirstNames_With_WrongData() {
            string[] names = {
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia",
                "Michael", "Jack"
            };
            var result = Names.FemaleFirstNames;
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }


        [Test]
        public void NameFilter_Arg_LastNames() {
            string[] names = {
                "Green", "Wood", "Pavlov", "Bogdanov", "Volkov", "Rusu", "Ceban", "Nagy", "Salo", "Niemi", "Koppel",
                "Urbonas", "Torres", "Calvo", "Romero", "Johnson", "Salas", "Vargas"
            };
            var result = Names.LastNames;
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
            var result = Names.LastNames;
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MaleFirstNames() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack"
            };
            var result = Names.MaleFirstNames;
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MaleFirstNames_With_WrongData() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack",
                "Maria", "Olga"
            };
            var result = Names.MaleFirstNames;
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MixedFirstNames() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack",
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia"
            };
            var result = Names.MixedFirstNames;
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        #endregion
    }
}