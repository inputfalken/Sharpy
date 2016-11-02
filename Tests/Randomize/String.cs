using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Randomize;
using Sharpy.Randomize.DataObjects;

namespace Tests.Randomize {
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
                femaleNameGenerator.GenerateMany(randomizer => randomizer.String(StringType.FemaleFirstName), count)
                    .All(femaleNames.Contains));
            Assert.IsTrue(
                maleNameGenerator.GenerateMany(randomizer => randomizer.String(StringType.MaleFirstName), count)
                    .All(maleNames.Contains));
            Assert.IsTrue(lastNameGenerator.GenerateMany(randomizer => randomizer.String(StringType.LastName), count)
                .All(lastNames.Contains));
            Assert.IsTrue(
                mixedFirstNameGenerator.GenerateMany(randomizer => randomizer.String(StringType.FirstName),
                    count).All(mixedNames.Contains));

            //Single
            Assert.IsTrue(
                femaleNames.Contains(
                    femaleNameGenerator.Generate(randomizer => randomizer.String(StringType.FemaleFirstName))));
            Assert.IsTrue(
                maleNames.Contains(maleNameGenerator.Generate(randomizer => randomizer.String(StringType.MaleFirstName))));
            Assert.IsTrue(
                lastNames.Contains(lastNameGenerator.Generate(randomizer => randomizer.String(StringType.LastName))));
            Assert.IsTrue(
                mixedNames.Contains(
                    mixedFirstNameGenerator.Generate(randomizer => randomizer.String(StringType.FirstName))));
        }

        [Test]
        public void NamesAreNotNull() {
            var generator = RandomGenerator.Create();
            //Many
            var names = generator.GenerateMany(randomizer => randomizer.String(StringType.AnyName), 20).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = generator.Generate(randomizer => randomizer.String(StringType.AnyName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void UserNamesAreNotNull() {
            var generator = RandomGenerator.Create();
            //Many
            var userNames = generator.GenerateMany(randomizer => randomizer.String(StringType.UserName), 20).ToArray();
            Assert.IsFalse(userNames.All(string.IsNullOrEmpty));
            Assert.IsFalse(userNames.All(string.IsNullOrWhiteSpace));

            //Single
            var userName = generator.Generate(randomizer => randomizer.String(StringType.UserName));
            Assert.IsFalse(string.IsNullOrEmpty(userName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(userName));
        }
    }
}