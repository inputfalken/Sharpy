using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types.Name {
    [TestClass]
    public class NameFactoryTests {

        #region NameCollection

        [TestMethod]
        public void NameCollection_LastNames_Arg_CentralAmerica() {
            string[] countries = {
                "Costa Rica", "Guatemala",
                "El Salvador"
            };
            var lastNameResult = NameFactory.NameCollection(repository => repository.LastNames, Region.CentralAmerika);
            var lastNameExpected = NameFactory.NameCollection(repository => repository.LastNames, countries);
            var femaleFirstNameResult = NameFactory.NameCollection(repository => repository.FemaleFirstNames,
                Region.CentralAmerika);
            var femaleFirstNameExpected = NameFactory.NameCollection(repository => repository.FemaleFirstNames,
                countries);
            var maleFirstNameResult = NameFactory.NameCollection(repository => repository.MaleFirstNames,
                Region.CentralAmerika);
            var maleFirstNameExpected = NameFactory.NameCollection(repository => repository.MaleFirstNames, countries);

            Assert.IsTrue(lastNameResult.SequenceEqual(lastNameExpected));
            Assert.IsTrue(femaleFirstNameResult.SequenceEqual(femaleFirstNameExpected));
            Assert.IsTrue(maleFirstNameResult.SequenceEqual(maleFirstNameExpected));
        }

        [TestMethod]
        public void NameCollection_LastNames_Arg_SouthAmerica() {
            string[]
                countries = {
                    "Argentina", "Brazil",
                    "Columbia", "Paraguay"
                };
            var lastNameResult = NameFactory.NameCollection(repository => repository.LastNames, Region.SouthAmerica);
            var lastNameExpected = NameFactory.NameCollection(repository => repository.LastNames, countries);
            var femaleFirstNameResult = NameFactory.NameCollection(repository => repository.FemaleFirstNames,
                Region.SouthAmerica);
            var femaleFirstNameExpected = NameFactory.NameCollection(repository => repository.FemaleFirstNames,
                countries);
            var maleFirstNameResult = NameFactory.NameCollection(repository => repository.MaleFirstNames,
                Region.SouthAmerica);
            var maleFirstNameExpected = NameFactory.NameCollection(repository => repository.MaleFirstNames, countries);

            Assert.IsTrue(lastNameResult.SequenceEqual(lastNameExpected));
            Assert.IsTrue(femaleFirstNameResult.SequenceEqual(femaleFirstNameExpected));
            Assert.IsTrue(maleFirstNameResult.SequenceEqual(maleFirstNameExpected));
        }

        [TestMethod]
        public void NameCollection_Arg_NorthAmerica() {
            string[] countries = {
                "Canada", "Mexico", "Cuba",
                "United States"
            };
            var lastNameResult = NameFactory.NameCollection(repository => repository.LastNames, Region.NorthAmerica);
            var lastNameExpected = NameFactory.NameCollection(repository => repository.LastNames, countries);
            var femaleFirstNameResult = NameFactory.NameCollection(repository => repository.FemaleFirstNames,
                Region.NorthAmerica);
            var femaleFirstNameExpected = NameFactory.NameCollection(repository => repository.FemaleFirstNames,
                countries);
            var maleFirstNameResult = NameFactory.NameCollection(repository => repository.MaleFirstNames,
                Region.NorthAmerica);
            var maleFirstNameExpected = NameFactory.NameCollection(repository => repository.MaleFirstNames, countries);

            Assert.IsTrue(lastNameResult.SequenceEqual(lastNameExpected));
            Assert.IsTrue(femaleFirstNameResult.SequenceEqual(femaleFirstNameExpected));
            Assert.IsTrue(maleFirstNameResult.SequenceEqual(maleFirstNameExpected));
        }

        [TestMethod]
        public void NameCollection_Arg_Europe() {
            string[] countries = {
                "Albania", "Austria",
                "Azerbaijan", "Belgium", "Croatia", "Czech", "Denmark", "Estonia", "Faroe Islands", "Finland", "France",
                "Germany", "Greece", "Hungary", "Ireland", "Italy", "Latvia", "Luxembourg", "Macedonia", "Malta",
                "Moldova", "Netherlands", "Norway", "Poland", "Portugal", "Romania", "Russia", "Slovakia", "Slovenia",
                "Spain", "Sweden", "Switzerland", "Turkey", "Ukraine", "United Kingdom"
            };
            var lastNameResult = NameFactory.NameCollection(repository => repository.LastNames, Region.Europe);
            var lastNameExpected = NameFactory.NameCollection(repository => repository.LastNames, countries);
            var femaleFirstNameResult = NameFactory.NameCollection(repository => repository.FemaleFirstNames,
                Region.Europe);
            var femaleFirstNameExpected = NameFactory.NameCollection(repository => repository.FemaleFirstNames,
                countries);
            var maleFirstNameResult = NameFactory.NameCollection(repository => repository.MaleFirstNames, Region.Europe);
            var maleFirstNameExpected = NameFactory.NameCollection(repository => repository.MaleFirstNames, countries);

            Assert.IsTrue(lastNameResult.SequenceEqual(lastNameExpected));
            Assert.IsTrue(femaleFirstNameResult.SequenceEqual(femaleFirstNameExpected));
            Assert.IsTrue(maleFirstNameResult.SequenceEqual(maleFirstNameExpected));
        }

        [TestMethod]
        public void NameCollection_LastNames_Arg_None() {
            var result = NameFactory.NameCollection(repository => repository.LastNames);
            var expected = NameFactory.NameCollection(repository => repository.LastNames, Region.CentralAmerika,
                Region.NorthAmerica, Region.SouthAmerica, Region.Europe);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        #endregion

        #region Exception Handling

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LastNameCollection_ExceptionHandling() {
            const string countryQuery = "foobar";
            NameFactory.NameCollection(repository => repository.LastNames, countryQuery);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FirstNameCollection_ExceptionHandling() {
            const string stringQuery = "foobar";
            NameFactory.NameCollection(repository => repository.MixedFirstNames, stringQuery);
        }

        #endregion
    }

}