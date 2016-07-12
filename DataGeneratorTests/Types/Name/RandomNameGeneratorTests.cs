﻿using System.Linq;
using DataGenerator.Types;
using DataGenerator.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types.Name {
    [TestClass]
    public class RandomNameGeneratorTests {
        private readonly NameFunctionFactory _nameFunctionFactory = new NameFunctionFactory(new RandomGenerator());

        [TestMethod]
        public void TestRussianNames() {
            // Names taken from data.json
            var russianMaleNames = new[] {
                "Alexander",
                "Sergei",
                "Dmitry",
                "Andrei",
                "Alexey",
                "Maxim",
                "Evgeny",
                "Ivan",
                "Mikhail",
                "Artyom"
            };
            var russianFemaleNames = new[] {
                "Anastasia",
                "Yelena",
                "Olga",
                "Natalia",
                "Yekaterina",
                "Anna",
                "Tatiana",
                "Maria",
                "Irina",
                "Yulia"
            };
            var russianMaleGenerator = _nameFunctionFactory.NameFunctionCreator(Country.Russia, Gender.Male);
            var russianFemaleGenerator = _nameFunctionFactory.NameFunctionCreator(Country.Russia, Gender.Female);
            for (var i = 0; i < 1000; i++) {
                Assert.IsTrue(russianMaleNames.Contains(russianMaleGenerator()));
                Assert.IsTrue(russianFemaleNames.Contains(russianFemaleGenerator()));
            }
        }
    }
}