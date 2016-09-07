using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataGen;
using DataGen.Types.Enums;
using DataGen.Types.Name;
using Newtonsoft.Json;
using NUnit.Framework;
using static DataGen.Types.Enums.Country;

namespace Tests {
    ///<summary>
    /// These tests purpose is to make sure that you obtain the correct data from a json file.
    /// </summary>
    [TestFixture]
    internal class CommonNamesReadFileTest {
        #region Common Items

        private static NameFilter CommonNames => new NameFilter(new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(DataGen.Properties.Resources.NamesByOrigin))));

        private static readonly Country[] EuropeCountries = {
            Albania, Austria, Azerbaijan, Belgium, Croatia, Czech, Denmark,
            Estonia, FaroeIslands, Finland, France, Germany, Greece, Hungary,
            Ireland, Italy, Latvia, Luxembourg, Macedonia, Malta,  Moldova,
            Netherlands, Norway, Poland, Portugal, Romania, Russia, Slovakia,
            Slovenia, Spain, Sweden, Switzerland, Turkey, Ukraine, UnitedKingdom
        };

        private static readonly Country[] NorthAmericanCountries = { Canada, Mexico, Cuba, UnitedStates };
        private static readonly Country[] SouthAmericanCountries = { Argentina, Brazil, Columbia, Paraguay };
        private static readonly Country[] CentralAmericanCountries = { CostaRica, Guatemala, ElSalvador };

        #endregion

        [Test]
        public void NameFilter_ByRegion_CentralAmerica() {
            var result = CommonNames.ByRegion(Region.CentralAmerica);
            //Makes sure that each object region is equal to current region.and that it contains data
            Assert.IsTrue(result.All(name => name.Region == Region.CentralAmerica) && result.Any());
            //Makes sure that each country in CentralAmericanCountries is contained from the result
            Assert.IsTrue(result.All(name => CentralAmericanCountries.Contains(name.Country)));
        }


        [Test]
        public void NameFilter_ByRegion_SouthAmerica() {
            var result = CommonNames.ByRegion(Region.SouthAmerica);
            //Makes sure that each object region is equal to current region.and that it contains data
            Assert.IsTrue(result.All(name => name.Region == Region.SouthAmerica) && result.Any());
            //Makes sure that each country in SouthAmericanCountries is contained from the result
            Assert.IsTrue(result.All(name => SouthAmericanCountries.Contains(name.Country)));
        }

        [Test]
        public void NameCollection_ByRegion_Europe() {
            var result = CommonNames.ByRegion(Region.Europe);
            //Makes sure that each object region is equal to current region.and that it contains data
            Assert.IsTrue(result.All(name => name.Region == Region.Europe) && result.Any());
            //Makes sure that each country in EuropeCountries is contained from the result
            Assert.IsTrue(result.All(name => EuropeCountries.Contains(name.Country)));
        }

        [Test]
        public void NameCollection_ByRegion_NorthAmerica() {
            var result = CommonNames.ByRegion(Region.NorthAmerica);
            //Makes sure that each object region is equal to current region.and that it contains data
            Assert.IsTrue(result.All(name => name.Region == Region.NorthAmerica) && result.Any());
            //Makes sure that each country in NorthAmericanCountries is contained from the result
            Assert.IsTrue(result.All(name => NorthAmericanCountries.Contains(name.Country)));
        }

        #region Country

        [Test]
        public void NameFilter_Arg_Country_Denmark() {
            //Makes sure that each object country is equal to current country.
            var result = CommonNames.ByCountry(Denmark);
            Assert.IsTrue(result.All(name => name.Country == Denmark));
        }


        [Test]
        public void NameFilter_Arg_Country_Norway() {
            //Makes sure that each object country is equal to current country.
            var result = CommonNames.ByCountry(Norway);
            Assert.IsTrue(result.All(name => name.Country == Norway));
        }

        [Test]
        public void NameFilter_Arg_Country_Sweden() {
            //Makes sure that each object country is equal to current country.
            var result = CommonNames.ByCountry(Sweden);
            Assert.IsTrue(result.All(name => name.Country == Sweden));
        }

        [Test]
        public void NameFilter_Arg_FemaleFirstNames() {
            string[] names = {
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia"
            };
            var result = CommonNames.ByType(NameType.FemaleFirstName);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_FemaleFirstNames_With_WrongData() {
            string[] names = {
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia",
                "Michael", "Jack"
            };
            var result = CommonNames.ByType(NameType.FemaleFirstName);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }


        [Test]
        public void NameFilter_Arg_LastNames() {
            string[] names = {
                "Green", "Wood", "Pavlov", "Bogdanov", "Volkov", "Rusu", "Ceban", "Nagy", "Salo", "Niemi", "Koppel",
                "Urbonas", "Torres", "Calvo", "Romero", "Johnson", "Salas", "Vargas"
            };
            var result = CommonNames.ByType(NameType.LastName);
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
            var result = CommonNames.ByType(NameType.LastName);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MaleFirstNames() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack"
            };
            var result = CommonNames.ByType(NameType.MaleFirstName);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MaleFirstNames_With_WrongData() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack",
                "Maria", "Olga"
            };
            var result = CommonNames.ByType(NameType.LastName);
            Assert.IsFalse(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void NameFilter_Arg_MixedFirstNames() {
            string[] names = {
                "Jacob", "Erik", "Simon", "Alexander", "Afonso", "Adam", "Michael", "Jack",
                "Maria", "Olga", "Jessica", "Linda", "Sophie", "Vanessa", "Sophie", "Julia"
            };
            var result = CommonNames.ByType(NameType.MixedFirstName);
            Assert.IsTrue(names.All(s => result.Select(name => name.Data).Contains(s)));
        }

        #endregion
    }
}