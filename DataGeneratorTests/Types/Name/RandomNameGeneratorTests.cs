using System.Linq;
using DataGenerator.Types;
using DataGenerator.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types.Name {
    [TestClass]
    public class RandomNameGeneratorTests {
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
            var russianLastNames = new[] {
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

            var russianMaleFirstNamesCollection = NameFactory.FirstNameCollection("russia", Gender.Male);
            var russianFemaleFirstNamesCollection = NameFactory.FirstNameCollection("russia", Gender.Female);
            var russianLastNamesCollection = NameFactory.LastNameCollection("russia");
            for (var i = 0; i < 1000; i++) {
                Assert.IsTrue(russianMaleFirstNamesCollection.SequenceEqual(russianMaleNames));
                Assert.IsTrue(russianFemaleFirstNamesCollection.SequenceEqual(russianFemaleNames));
                Assert.IsTrue(russianLastNamesCollection.SequenceEqual(russianLastNames));
            }
        }
    }
}