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
            //Many
            var mails = generator.GenerateMany(randomizer => randomizer.MailAdress(MailUserName), 20).ToArray();
            Assert.IsFalse(mails.All(string.IsNullOrEmpty));
            Assert.IsFalse(mails.All(string.IsNullOrWhiteSpace));

            //Single
            var masil = generator.Generate(randomizer => randomizer.MailAdress(MailUserName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(masil));
            Assert.IsFalse(string.IsNullOrEmpty(masil));
        }

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
        public void NumbersAreNotDefaultValue() {
            var generator = RandomGenerator.Create();
            //many
            Assert.IsFalse(generator.GenerateMany(randomizer => randomizer.Integer(1, 100), 100).All(i => i == 0));

            //Single
            Assert.IsFalse(generator.Generate(randomizer => randomizer.Integer(1, 100)) == 0);
        }

        [Test]
        public void PhoneNumberAreNotNullOrwhiteSpace() {
            var sharpyGenerator = RandomGenerator.Create();
            //Many
            var numbers =
                sharpyGenerator.GenerateMany(randomizer => randomizer.String(StringType.Number), 100)
                    .ToArray();
            Assert.IsFalse(numbers.All(string.IsNullOrWhiteSpace));
            Assert.IsFalse(numbers.All(string.IsNullOrWhiteSpace));

            //Single
            var number = sharpyGenerator.Generate(randomizer => randomizer.String(StringType.Number));
            Assert.IsFalse(string.IsNullOrWhiteSpace(number));
            Assert.IsFalse(string.IsNullOrWhiteSpace(number));
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