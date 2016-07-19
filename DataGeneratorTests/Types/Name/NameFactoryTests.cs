using System.Collections.Generic;
using System.Linq;
using DataGenerator.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types.Name {
    [TestClass]
    public class NameFactoryTests {
        [TestMethod]
        public void TestRussianNames() {
            // Names taken from data.json
            IEnumerable<string> russianMaleNames = new[] {
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
            IEnumerable<string> russianFemaleNames = new[] {
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
            IEnumerable<string> russianLastNames = new[] {
                "Smirnov",
                "Ivanov",
                "Kuznetsov",
                "Popov",
                "Sokolov",
                "Lebedev",
                "Kozlov",
                "Novikov",
                "Morozov",
                "Petrov",
                "Volkov",
                "Solovyov",
                "Vasilyev",
                "Zaytsev",
                "Pavlov",
                "Semyonov",
                "Golubev",
                "Vinogradov",
                "Bogdanov",
                "Vorobyov"
            };

            Assert.IsTrue(NameFactory.FirstNameCollection("russia", Gender.Male).SequenceEqual(russianMaleNames));
            Assert.IsTrue(NameFactory.FirstNameCollection("russia", Gender.Female).SequenceEqual(russianFemaleNames));
            Assert.IsTrue(NameFactory.LastNameCollection("russia").SequenceEqual(russianLastNames));
        }
    }
}