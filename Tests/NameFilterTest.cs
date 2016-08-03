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

        #endregion

        #region Filtered by Region

        [Test]
        public void NameFilter_Arg_CentralAmerica() {
            const string region = "centralAmerica";
            var result = NameFilter.FilterBy(FilterArg.Region, region).Result.ToList();

            Assert.IsTrue(result.TrueForAll(name => name.Region == region) && result.Any());
        }


        [Test]
        public void NameFilter_Arg_SouthAmerica() {
            const string region = "southAmerica";
            var result = NameFilter.FilterBy(FilterArg.Region, region).Result.ToList();
            Assert.IsTrue(result.TrueForAll(name => name.Region == region) && result.Any());
        }

        [Test]
        public void NameCollection_Arg_NorthAmerica() {
            const string region = "northAmerica";
            var result = NameFilter.FilterBy(FilterArg.Region, region).Result.ToList();
            Assert.IsTrue(result.TrueForAll(name => name.Region == region) && result.Any());
        }

        [Test]
        public void NameCollection_Arg_Europe() {
            string[] countries = {
                "Albania", "Austria",
                "Azerbaijan", "Belgium", "Croatia", "Czech", "Denmark", "Estonia", "Faroe Islands", "Finland", "France",
                "Germany", "Greece", "Hungary", "Ireland", "Italy", "Latvia", "Luxembourg", "Macedonia", "Malta",
                "Moldova", "Netherlands", "Norway", "Poland", "Portugal", "Romania", "Russia", "Slovakia", "Slovenia",
                "Spain", "Sweden", "Switzerland", "Turkey", "Ukraine", "United Kingdom"
            };

            const string region = "europe";
            var result = NameFilter.FilterBy(FilterArg.Region, region).Result.ToList();
            Assert.IsTrue(result.TrueForAll(name => name.Region == region) && result.Any());
        }

        #endregion

        #endregion

        private static NameFilter NameFilter => Factory.Filter(enumerable => new NameFilter(enumerable),
            JsonConvert.DeserializeObject<IEnumerable<Name>>(
                File.ReadAllText(TestHelper.GetTestsPath() + @"\Data\Types\Name\newData.json")));
    }
}