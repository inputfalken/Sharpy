using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Randomizer;
using Sharpy.Randomizer.DataObjects;

namespace Tests.Randomizer {
    [TestFixture]
    public class String {
        [SetUp]
        public void Setup() {
            var config = new Config();
            _names = config.Names.ToArray();
        }

        private Name[] _names;

        [Test]
        public void NamesAreFilteredByGender() {
            var femaleNameGenerator =
                RandomGenerator.Create();
            var femaleNames = _names.Where(name => name.Type == 1).Select(name => name.Data).ToArray();
            var maleNameGenerator =
                RandomGenerator.Create();
            var maleNames = _names.Where(name => name.Type == 2).Select(name => name.Data).ToArray();
            var lastNameGenerator = RandomGenerator.Create();
            var lastNames = _names.Where(name => name.Type == 3).Select(name => name.Data).ToArray();
            var mixedFirstNameGenerator =
                RandomGenerator.Create();
            var mixedNames = _names.Where(name => name.Type == 1 | name.Type == 2).Select(name => name.Data).ToArray();
            //Many
            const int count = 100;
            Assert.IsTrue(
                femaleNameGenerator.GenerateMany(randomizerr => randomizerr.String(StringType.FemaleFirstName), count)
                    .All(femaleNames.Contains));
            Assert.IsTrue(
                maleNameGenerator.GenerateMany(randomizerr => randomizerr.String(StringType.MaleFirstName), count)
                    .All(maleNames.Contains));
            Assert.IsTrue(lastNameGenerator.GenerateMany(randomizerr => randomizerr.String(StringType.LastName), count)
                .All(lastNames.Contains));
            Assert.IsTrue(
                mixedFirstNameGenerator.GenerateMany(randomizerr => randomizerr.String(StringType.FirstName),
                    count).All(mixedNames.Contains));

            //Single
            Assert.IsTrue(
                femaleNames.Contains(
                    femaleNameGenerator.Generate(randomizerr => randomizerr.String(StringType.FemaleFirstName))));
            Assert.IsTrue(
                maleNames.Contains(maleNameGenerator.Generate(randomizerr => randomizerr.String(StringType.MaleFirstName))));
            Assert.IsTrue(
                lastNames.Contains(lastNameGenerator.Generate(randomizerr => randomizerr.String(StringType.LastName))));
            Assert.IsTrue(
                mixedNames.Contains(
                    mixedFirstNameGenerator.Generate(randomizerr => randomizerr.String(StringType.FirstName))));
        }

        [Test]
        public void NamesAreNotNull() {
            var generator = RandomGenerator.Create();
            //Many
            var names = generator.GenerateMany(randomizerr => randomizerr.String(StringType.AnyName), 20).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = generator.Generate(randomizerr => randomizerr.String(StringType.AnyName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void UserNamesAreNotNull() {
            var generator = RandomGenerator.Create();
            //Many
            var userNames = generator.GenerateMany(randomizerr => randomizerr.String(StringType.UserName), 20).ToArray();
            Assert.IsFalse(userNames.All(string.IsNullOrEmpty));
            Assert.IsFalse(userNames.All(string.IsNullOrWhiteSpace));

            //Single
            var userName = generator.Generate(randomizerr => randomizerr.String(StringType.UserName));
            Assert.IsFalse(string.IsNullOrEmpty(userName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(userName));
        }
    }
}