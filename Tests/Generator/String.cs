using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Implementation.DataObjects;

namespace Tests.Generator {
    [TestFixture]
    public class String {
        [SetUp]
        public void Setup() {
            var generator = new Sharpy.Generator();
            _names = generator.Names.ToArray();
        }

        private Name[] _names;

        [Test]
        public void NamesAreFilteredByGender() {
            var femaleNameGenerator =
                new Sharpy.Generator();
            var femaleNames = _names.Where(name => name.Type == 1).Select(name => name.Data).ToArray();
            var maleNameGenerator =
                new Sharpy.Generator();
            var maleNames = _names.Where(name => name.Type == 2).Select(name => name.Data).ToArray();
            var lastNameGenerator = new Sharpy.Generator();
            var lastNames = _names.Where(name => name.Type == 3).Select(name => name.Data).ToArray();
            var mixedFirstNameGenerator =
                new Sharpy.Generator();
            var mixedNames =
                _names.Where(name => (name.Type == 1) | (name.Type == 2)).Select(name => name.Data).ToArray();
            //Many
            const int count = 100;
            Assert.IsTrue(
                femaleNameGenerator.GenerateMany(generatorr => generatorr.String(StringType.FemaleFirstName), count)
                    .All(femaleNames.Contains));
            Assert.IsTrue(
                maleNameGenerator.GenerateMany(generatorr => generatorr.String(StringType.MaleFirstName), count)
                    .All(maleNames.Contains));
            Assert.IsTrue(lastNameGenerator.GenerateMany(generatorr => generatorr.String(StringType.LastName), count)
                .All(lastNames.Contains));
            Assert.IsTrue(
                mixedFirstNameGenerator.GenerateMany(generatorr => generatorr.String(StringType.FirstName),
                    count).All(mixedNames.Contains));

            //Single
            Assert.IsTrue(
                femaleNames.Contains(
                    femaleNameGenerator.Generate(generatorr => generatorr.String(StringType.FemaleFirstName))));
            Assert.IsTrue(
                maleNames.Contains(maleNameGenerator.Generate(generatorr => generatorr.String(StringType.MaleFirstName))));
            Assert.IsTrue(
                lastNames.Contains(lastNameGenerator.Generate(generatorr => generatorr.String(StringType.LastName))));
            Assert.IsTrue(
                mixedNames.Contains(
                    mixedFirstNameGenerator.Generate(generatorr => generatorr.String(StringType.FirstName))));
        }

        [Test]
        public void NamesAreNotNull() {
            var generator = new Sharpy.Generator();
            //Many
            var names = generator.GenerateMany(generatorr => generatorr.String(StringType.AnyName), 20).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = generator.Generate(generatorr => generatorr.String(StringType.AnyName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void UserNamesAreNotNull() {
            var generator = new Sharpy.Generator();
            //Many
            var userNames = generator.GenerateMany(generatorr => generatorr.String(StringType.UserName), 20).ToArray();
            Assert.IsFalse(userNames.All(string.IsNullOrEmpty));
            Assert.IsFalse(userNames.All(string.IsNullOrWhiteSpace));

            //Single
            var userName = generator.Generate(generatorr => generatorr.String(StringType.UserName));
            Assert.IsFalse(string.IsNullOrEmpty(userName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(userName));
        }
    }
}