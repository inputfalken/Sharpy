using System.Linq;
using DataGenerator.Types;
using DataGenerator.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types.Name {
    [TestClass]
    public class RandomNameGeneratorTests {
        private readonly NameFactory _nameFactory = new NameFactory(new RandomGenerator());

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

            var russianMaleGenerator = _nameFactory.FirstNameInitialiser("russia", Gender.Male);
            var russianFemaleGenerator = _nameFactory.FirstNameInitialiser("russia", Gender.Female);
            var russianLastNameGenerator = _nameFactory.LastNameInitialiser("russia");
            for (var i = 0; i < 1000; i++) {
                Assert.IsTrue(russianLastNames.Contains(russianLastNameGenerator()));
                Assert.IsTrue(russianMaleNames.Contains(russianMaleGenerator()));
                Assert.IsTrue(russianFemaleNames.Contains(russianFemaleGenerator()));
            }
        }
    }
}