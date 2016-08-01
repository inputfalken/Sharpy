using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types;
using DataGen.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types.Name {
    [TestClass]
    public class NameFactoryTests {
        #region Name Collection

        #region Filtered by Country

        #endregion

        #region Filtered by Region

        [TestMethod]
        public void NameCollection_Arg_CentralAmerica() {
            string[] countries = {
                "Costa Rica", "Guatemala",
                "El Salvador"
            };

            var nameFilter = Factory.Filter(new NameFilter(new List<NameRepository>()));
            var numberfilter = Factory.Filter(new Numberfilter(new List<int>()));
        }


        //[TestMethod]
        //public void NameCollection_Arg_SouthAmerica() {
        //    string[]
        //        countries = {
        //            "Argentina", "Brazil",
        //            "Columbia", "Paraguay"
        //        };
        //    var lastNameResult = Name.Collection(repository => repository.LastNames, Region.SouthAmerica);
        //    var lastNameExpected = Name.Collection(repository => repository.LastNames, countries);
        //    var femaleFirstNameResult = Name.Collection(repository => repository.FemaleFirstNames,
        //        Region.SouthAmerica);
        //    var femaleFirstNameExpected = Name.Collection(repository => repository.FemaleFirstNames,
        //        countries);
        //    var maleFirstNameResult = Name.Collection(repository => repository.MaleFirstNames,
        //        Region.SouthAmerica);
        //    var maleFirstNameExpected = Name.Collection(repository => repository.MaleFirstNames, countries);

        //    Assert.IsTrue(lastNameResult.SequenceEqual(lastNameExpected));
        //    Assert.IsTrue(femaleFirstNameResult.SequenceEqual(femaleFirstNameExpected));
        //    Assert.IsTrue(maleFirstNameResult.SequenceEqual(maleFirstNameExpected));
        //}

        //[TestMethod]
        //public void NameCollection_Arg_NorthAmerica() {
        //    string[] countries = {
        //        "Canada", "Mexico", "Cuba",
        //        "United States"
        //    };
        //    var lastNameResult = Name.Collection(repository => repository.LastNames, Region.NorthAmerica);
        //    var lastNameExpected = Name.Collection(repository => repository.LastNames, countries);
        //    var femaleFirstNameResult = Name.Collection(repository => repository.FemaleFirstNames,
        //        Region.NorthAmerica);
        //    var femaleFirstNameExpected = Name.Collection(repository => repository.FemaleFirstNames,
        //        countries);
        //    var maleFirstNameResult = Name.Collection(repository => repository.MaleFirstNames,
        //        Region.NorthAmerica);
        //    var maleFirstNameExpected = Name.Collection(repository => repository.MaleFirstNames, countries);

        //    Assert.IsTrue(lastNameResult.SequenceEqual(lastNameExpected));
        //    Assert.IsTrue(femaleFirstNameResult.SequenceEqual(femaleFirstNameExpected));
        //    Assert.IsTrue(maleFirstNameResult.SequenceEqual(maleFirstNameExpected));
        //}

        //[TestMethod]
        //public void NameCollection_Arg_Europe() {
        //    string[] countries = {
        //        "Albania", "Austria",
        //        "Azerbaijan", "Belgium", "Croatia", "Czech", "Denmark", "Estonia", "Faroe Islands", "Finland", "France",
        //        "Germany", "Greece", "Hungary", "Ireland", "Italy", "Latvia", "Luxembourg", "Macedonia", "Malta",
        //        "Moldova", "Netherlands", "Norway", "Poland", "Portugal", "Romania", "Russia", "Slovakia", "Slovenia",
        //        "Spain", "Sweden", "Switzerland", "Turkey", "Ukraine", "United Kingdom"
        //    };
        //    var lastNameResult = Name.Collection(repository => repository.LastNames, Region.Europe);
        //    var lastNameExpected = Name.Collection(repository => repository.LastNames, countries);
        //    var femaleFirstNameResult = Name.Collection(repository => repository.FemaleFirstNames,
        //        Region.Europe);
        //    var femaleFirstNameExpected = Name.Collection(repository => repository.FemaleFirstNames,
        //        countries);
        //    var maleFirstNameResult = Name.Collection(repository => repository.MaleFirstNames, Region.Europe);
        //    var maleFirstNameExpected = Name.Collection(repository => repository.MaleFirstNames, countries);

        //    Assert.IsTrue(lastNameResult.SequenceEqual(lastNameExpected));
        //    Assert.IsTrue(femaleFirstNameResult.SequenceEqual(femaleFirstNameExpected));
        //    Assert.IsTrue(maleFirstNameResult.SequenceEqual(maleFirstNameExpected));
        //}

        [TestMethod]
        public void NameCollection_Arg_None() {
            ////var result = nameFactory.Collection(repository => repository.LastNames);
            ////var expected = Name.Collection(repository => repository.LastNames, Region.CentralAmerika,
            ////    Region.NorthAmerica, Region.SouthAmerica, Region.Europe);
            //Assert.IsTrue(result.SequenceEqual(expected));
        }

        //Cool syntax

        #endregion

        #endregion
    }

    public class StringFilter: Filter<string> {
        public StringFilter(IEnumerable<string> result) : base(result) {
        }
    }

    public class Numberfilter : Filter<int> {
        public Numberfilter(IEnumerable<int> result) : base(result) {
        }
        public void Test() { }
    }
}