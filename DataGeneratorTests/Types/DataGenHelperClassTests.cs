using System.Collections.Generic;
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
            var result = DataGenHelperClass.ReadFromFile("Country/Country.txt");
            foreach (var keyValuePair in dictionary)
                Assert.IsTrue(result[keyValuePair.Key] == keyValuePair.Value);
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
            var result = DataGenHelperClass.ReadFromFile("Country/inital.txt");
            foreach (var keyValuePair in dictionary)
                Assert.IsTrue(result[keyValuePair.Key] == keyValuePair.Value);
            Assert.IsTrue(result.Count == expectedCount);
        }
    }
}