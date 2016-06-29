using System.Collections.Generic;
using System.Linq;
using DataGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DataGeneratorTests.Types
{
    [TestClass]
    public class DataGenHelperClassTests
    {
        [TestMethod]
        public void ReadFromFileCountryTest() {
            const int expectedCount = 241;
            var dictionary = new Dictionary<int, string> {
                {207, "Sweden"},
                {43, "China"},
                {103, "Ireland"},
                {240, "Zimbabwe"}
            };
            var result = DataGenHelperClass.ReadFromFile("Country/country.txt");

            //Check that every country starts with uppercase
            foreach (var s in result)
                Assert.IsTrue(char.IsUpper(s[0]));

            //Check the order is as expected
            foreach (var keyValuePair in dictionary)
                Assert.IsTrue(result[keyValuePair.Key] == keyValuePair.Value);
            //Check that the size of the text hasn't changed
            Assert.IsTrue(result.Count == expectedCount);
        }

        [TestMethod()]
        public void ReadFromFileSeoCodeTest() {
            const int expectedCount = 241;
            var dictionary = new Dictionary<int, string> {
                {207, "SE"},
                {43, "CN"},
                {103, "IE"},
                {240, "ZW"}
            };
            var result = DataGenHelperClass.ReadFromFile("Country/seoCode.txt");
            foreach (var seocode in result) {
                //Make sure the seocode is not bigger than 2 chars
                Assert.IsTrue(seocode.Length == 2);
                //Make sure everything is uppercased
                Assert.IsTrue(seocode.Any(char.IsUpper));
            }

            //Check that the order is as expected
            foreach (var keyValuePair in dictionary)
                Assert.IsTrue(result[keyValuePair.Key] == keyValuePair.Value);
            Assert.IsTrue(result.Count == expectedCount);
        }
    }
}