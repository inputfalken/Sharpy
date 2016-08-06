using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGen.Types;
using DataGen.Types.NameCollection;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests {
    [TestFixture]
    internal class NameFilterTest {
        #region Name Collection

        #region Filtered by Country

        [Test]
        public void NameFilter_Arg_Sweden() {
            const string country = "sweden";
            //Makes sure that each object country is equal to current country.
            var result = NameFilter.FilterBy(FilterArg.Country, country);
            Assert.IsTrue(result.All(name => name.Country == country));
        }

        [Test]
        public void NameFilter_Arg_Norway() {
            const string country = "norway";
            //Makes sure that each object country is equal to current country.
            var result = NameFilter.FilterBy(FilterArg.Country, country);
            Assert.IsTrue(result.All(name => name.Country == country));
        }

        [Test]
        public void NameFilter_Arg_Denmark() {
            const string country = "denmark";
            //Makes sure that each object country is equal to current country.
            var result = NameFilter.FilterBy(FilterArg.Country, country);
            Assert.IsTrue(result.All(name => name.Country == country));
        }

        #endregion

        #region Filtered by Region

        [Test]
        public void NameFilter_Arg_CentralAmerica() {
            const string region = "centralAmerica";
            var result = NameFilter.FilterBy(FilterArg.Region, region);

            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == region) && result.Any());
            //Makes sure that each country in CentralAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country).All(CentralAmericanCountries.Contains));
        }


        [Test]
        public void NameFilter_Arg_SouthAmerica() {
            const string region = "southAmerica";
            var result = NameFilter.FilterBy(FilterArg.Region, region);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == region) && result.Any());
            //Makes sure that each country in SouthAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country).All(SouthAmericanCountries.Contains));
        }

        [Test]
        public void NameCollection_Arg_NorthAmerica() {
            const string region = "northAmerica";
            var result = NameFilter.FilterBy(FilterArg.Region, region);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == region) && result.Any());
            //Makes sure that each country in NorthAmericanCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country).All(NorthAmericanCountries.Contains));
        }

        [Test]
        public void NameCollection_Arg_Europe() {
            const string region = "europe";
            var result = NameFilter.FilterBy(FilterArg.Region, region);
            //Makes sure that each object region is equal to current region.
            Assert.IsTrue(result.All(name => name.Region == region) && result.Any());
            //Makes sure that each country in EuropeCountries is contained from the result
            Assert.IsTrue(result.Select(name => name.Country).All(EuropeCountries.Contains));
        }

        #endregion

        #region Filtered by Name Types

        [Test]
        public void NameFilter_arg_FemaleFirstNames() {
            string[] names = {
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia"
            };
            var result = NameFilter.FilterBy(FilterArg.FemaleFirstName);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_FemaleFirstNames_With_WrongData() {
            string[] names = {
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia",
                "Michael", "Jack",
            };
            var result = NameFilter.FilterBy(FilterArg.FemaleFirstName);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MaleFirstNames() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack"
            };
            var result = NameFilter.FilterBy(FilterArg.MaleFirstName);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MaleFirstNames_With_WrongData() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack",
                "Maria", "Olga"
            };
            var result = NameFilter.FilterBy(FilterArg.MaleFirstName);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MixedFirstNames() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack",
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia"
            };
            var result = NameFilter.FilterBy(FilterArg.MixedFirstNames);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }


        [Test]
        public void NameFilter_Arg_LastNames() {
            string[] names = {
                "Green", "Wood", "Pavlov", "Bogdanov", "Volkov", "Rusu", "Ceban", "Nagy", "Salo", "Niemi", "Koppel",
                "Urbonas", "Torres", "Calvo", "Romero", "Johnson", "Salas", "Vargas"
            };
            var result = NameFilter.FilterBy(FilterArg.Lastname);
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
            var result = NameFilter.FilterBy(FilterArg.Lastname);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        #endregion

        #endregion

        private static NameFilter NameFilter => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
            File.ReadAllText(TestHelper.GetTestsPath() + @"\Data\Types\Name\newData.json")));


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
    }
}