using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Types.Name;

namespace Tests {
    /// <summary>
    ///     These tests are all used with a seed so the result are always the same.
    /// </summary>
    [TestFixture]
    public class GeneratorTests {
        [SetUp]
        public void Setup() {
            var config = new Config();
            _names = config.Names.ToArray();
        }

        private const int Seed = 100;
        private Name[] _names;
        private const string MailUserName = "mailUserName";

        [Test]
        public void IteratorWithEnumerable() {
            var iteration = 0;
            var generator = RandomGenerator.Create();
            var result = generator.GenerateMany((randomizer, i) => iteration++ == i, 20).ToArray();
            Assert.IsTrue(result.All(b => b));
        }


        [Test]
        public void Mail() {
            var mailGenerator = RandomGenerator.Create();
            mailGenerator.Config.MailGenerator(new List<string> {"gmail.com"}, true);

            // Should be true since mailgenerator has been configured to produce unique mails.
            Assert.IsTrue(
                mailGenerator.GenerateMany(randomizer => randomizer.MailAdress(MailUserName), 100)
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void MailsAreNotnull() {
            var generator = RandomGenerator.Create();
            var strings = generator.GenerateMany(randomizer => randomizer.MailAdress(MailUserName), 20).ToArray();
            Assert.IsFalse(strings.All(string.IsNullOrEmpty));
            Assert.IsFalse(strings.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void NamesAreFilteredByGender() {
            var femaleNameGenerator =
                RandomGenerator.Create();
            var femaleNames = _names.Where(name => name.Type == 1).Select(name => name.Data);
            var maleNameGenerator =
                RandomGenerator.Create();
            var maleNames = _names.Where(name => name.Type == 2).Select(name => name.Data);
            var lastNameGenerator = RandomGenerator.Create();
            var lastNames = _names.Where(name => name.Type == 3).Select(name => name.Data);
            var mixedFirstNameGenerator =
                RandomGenerator.Create();
            var mixedNames = _names.Where(name => name.Type == 1 | name.Type == 2).Select(name => name.Data);
            Assert.IsTrue(
                femaleNameGenerator.GenerateMany(randomizer => randomizer.String(StringType.FemaleFirstName), 100)
                    .All(femaleNames.Contains));
            Assert.IsTrue(
                maleNameGenerator.GenerateMany(randomizer => randomizer.String(StringType.MaleFirstName), 100)
                    .All(maleNames.Contains));
            Assert.IsTrue(lastNameGenerator.GenerateMany(randomizer => randomizer.String(StringType.LastName), 100)
                .All(lastNames.Contains));
            Assert.IsTrue(
                mixedFirstNameGenerator.GenerateMany(randomizer => randomizer.String(StringType.FirstName),
                    100).All(mixedNames.Contains));
        }

        [Test]
        public void NamesAreNotNull() {
            var generator = RandomGenerator.Create();
            var strings =
                generator.GenerateMany(randomizer => randomizer.String(StringType.AnyName), 20).ToArray();
            Assert.IsFalse(strings.All(string.IsNullOrEmpty));
            Assert.IsFalse(strings.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void NumbersAreNotDefaultValue() {
            var generator = RandomGenerator.Create();
            Assert.IsFalse(generator.GenerateMany(randomizer => randomizer.Integer(100), 100).All(i => i == 0));
        }

        [Test]
        public void PhoneNumberAreNotNullOrwhiteSpace() {
            var sharpyGenerator = RandomGenerator.Create();
            var numbers =
                sharpyGenerator.GenerateMany(randomizer => randomizer.String(StringType.Number), 100)
                    .ToArray();
            Assert.IsFalse(numbers.All(string.IsNullOrWhiteSpace));
            Assert.IsFalse(numbers.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void Seed_With_Bools() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the bools expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(Seed);
            var random = new Random(Seed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(2) != 0);
            var result = generator.GenerateMany(randomizer => randomizer.Bool(), 1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }


        [Test]
        public void Seed_With_Number() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int limit = 100;
            var generator = RandomGenerator.Create();
            generator.Config.Seed(Seed);
            var random = new Random(Seed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(limit));
            var result = generator.GenerateMany(randomizer => randomizer.Integer(limit), 1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void UserNamesAreNotNull() {
            var generator = RandomGenerator.Create();
            var strings =
                generator.GenerateMany(randomizer => randomizer.String(StringType.UserName), 20).ToArray();
            Assert.IsFalse(strings.All(string.IsNullOrEmpty));
            Assert.IsFalse(strings.All(string.IsNullOrWhiteSpace));
        }
    }
}