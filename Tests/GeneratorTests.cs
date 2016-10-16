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
        private const int Seed = 100;
        private Name[] _names;
        private const string MailUserName = "mailUserName";

        [SetUp]
        public void Setup() {
            var config = new Config();
            _names = config.Names.ToArray();
        }

        [Test]
        public void Seed_With_Bools() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the bools expected
            var generator = Factory.CreateNew(randomizer => randomizer.Bool(), new Config().Seed(Seed));
            var random = new Random(Seed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(2) != 0);
            var result = generator.GenerateEnumerable(1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }


        [Test]
        public void Seed_With_Number() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int limit = 100;
            var generator = Factory.CreateNew(randomizer => randomizer.Number(limit), new Config().Seed(Seed));
            var random = new Random(Seed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(limit));
            var result = generator.GenerateEnumerable(1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NumbersAreNotDefaultValue() {
            var generator = Factory.CreateNew(randomizer => randomizer.Number(100));
            Assert.IsFalse(generator.GenerateEnumerable(100).All(i => i == 0));
        }

        [Test]
        public void NamesAreNotNull() {
            var generator = Factory.CreateNew(randomizer => randomizer.String(StringType.AnyName));
            var strings = generator.GenerateEnumerable(20).ToArray();
            Assert.IsFalse(strings.All(string.IsNullOrEmpty));
            Assert.IsFalse(strings.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void UserNamesAreNotNull() {
            var generator = Factory.CreateNew(randomizer => randomizer.String(StringType.UserName));
            var strings = generator.GenerateEnumerable(20).ToArray();
            Assert.IsFalse(strings.All(string.IsNullOrEmpty));
            Assert.IsFalse(strings.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void MailsAreNotnull() {
            var generator = Factory.CreateNew(randomizer => randomizer.MailAdress(MailUserName));
            var strings = generator.GenerateEnumerable(20).ToArray();
            Assert.IsFalse(strings.All(string.IsNullOrEmpty));
            Assert.IsFalse(strings.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void PhoneNumberAreNotNullOrwhiteSpace() {
            var sharpyGenerator = Factory.CreateNew(randomizer => randomizer.PhoneNumber());
            var numbers = sharpyGenerator.GenerateEnumerable(100).ToArray();
            Assert.IsFalse(numbers.All(string.IsNullOrWhiteSpace));
            Assert.IsFalse(numbers.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void NamesAreFilteredByGender() {
            var femaleNameGenerator =
                Factory.CreateNew(randomizer => randomizer.String(StringType.FemaleFirstName));
            var femaleNames = _names.Where(name => name.Type == 1).Select(name => name.Data);
            var maleNameGenerator =
                Factory.CreateNew(randomizer => randomizer.String(StringType.MaleFirstName));
            var maleNames = _names.Where(name => name.Type == 2).Select(name => name.Data);
            var lastNameGenerator = Factory.CreateNew(randomizer => randomizer.String(StringType.LastName));
            var lastNames = _names.Where(name => name.Type == 3).Select(name => name.Data);
            var mixedFirstNameGenerator =
                Factory.CreateNew(randomizer => randomizer.String(StringType.MixedFirstName));
            var mixedNames = _names.Where(name => name.Type == 1 | name.Type == 2).Select(name => name.Data);
            Assert.IsTrue(femaleNameGenerator.GenerateEnumerable(100).All(femaleNames.Contains));
            Assert.IsTrue(maleNameGenerator.GenerateEnumerable(100).All(maleNames.Contains));
            Assert.IsTrue(lastNameGenerator.GenerateEnumerable(100).All(lastNames.Contains));
            Assert.IsTrue(mixedFirstNameGenerator.GenerateEnumerable(100).All(mixedNames.Contains));
        }

        [Test]
        public void IteratorWithEnumerable() {
            var iteration = 0;
            var generator = Factory.CreateNew<bool>((randomizer, i) => iteration++ == i);
            var result = generator.GenerateEnumerable(20).ToArray();
            Assert.IsTrue(result.All(b => b));
        }

        [Test]
        public void IteratorWithGenerate() {
            var iteration = 0;
            var generator = Factory.CreateNew<bool>((randomizer, i) => iteration++ == i);
            for (var i = 0; i < 10; i++)
                Assert.IsTrue(generator.Generate());
        }

        [Test]
        public void Mail() {
            var mailGenerator = Factory.CreateNew(randomizer => randomizer.MailAdress(MailUserName),
                new Config().MailGenerator(new List<string> {"gmail.com"}, true));
            // Should be true since mailgenerator has been configured to produce unique mails.
            Assert.IsTrue(mailGenerator.GenerateEnumerable(100).GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }
    }
}