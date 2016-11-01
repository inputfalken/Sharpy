using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Randomizer;
using Sharpy.Randomizer.DataObjects;

namespace Tests {
    /// <summary>
    ///     These tests are all used with a seed so the result are always the same.
    /// </summary>
    [TestFixture]
    public class RandomGeneratorTests {
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
                mailGenerator.GenerateMany(randomizer => randomizer.MailAddress(MailUserName), 100)
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void MailsAreNotnull() {
            var generator = RandomGenerator.Create();
            //Many
            var mails = generator.GenerateMany(randomizer => randomizer.MailAddress(MailUserName), 20).ToArray();
            Assert.IsFalse(mails.All(string.IsNullOrEmpty));
            Assert.IsFalse(mails.All(string.IsNullOrWhiteSpace));

            //Single
            var masil = generator.Generate(randomizer => randomizer.MailAddress(MailUserName));
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
        public void Seed_With_LongNoArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(Seed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(Seed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizer => randomizer.Long(), count);
            var result = generator.GenerateMany(randomizer => randomizer.Long(), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void Seed_With_LongSingleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(Seed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(Seed);
            const int count = 1000;
            const long max = long.MaxValue - 3923329;
            var expected = generator2.GenerateMany(randomizer => randomizer.Long(max), count);
            var result = generator.GenerateMany(randomizer => randomizer.Long(max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void Seed_With_LongDoubleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(Seed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(Seed);
            const int count = 1000;
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = generator2.GenerateMany(randomizer => randomizer.Long(min, max), count);
            var result = generator.GenerateMany(randomizer => randomizer.Long(min, max), count);
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
        public void Seed_With_SecurityNumber() {
            const int limit = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(Seed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(Seed);

            var generateManyA =
                generatorA.GenerateMany(randomizer => randomizer.SocialSecurityNumber(randomizer.DateByAge(20)), limit);
            var generateManyB =
                generatorB.GenerateMany(randomizer => randomizer.SocialSecurityNumber(randomizer.DateByAge(20)), limit);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void Seed_With_StringAnyName() {
            const int limit = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(Seed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(Seed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.AnyName), limit);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.AnyName), limit);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void Seed_With_StringFemaleFirstName() {
            const int limit = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(Seed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(Seed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.FemaleFirstName),
                limit);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.FemaleFirstName),
                limit);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void Seed_With_StringFirstName() {
            const int limit = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(Seed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(Seed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.FirstName), limit);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.FirstName), limit);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void Seed_With_StringLastName() {
            const int limit = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(Seed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(Seed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.LastName), limit);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.LastName), limit);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void Seed_With_StringMaleFirstName() {
            const int limit = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(Seed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(Seed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.MaleFirstName), limit);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.MaleFirstName), limit);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void Seed_With_StringUserName() {
            const int limit = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(Seed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(Seed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.UserName), limit);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.UserName), limit);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void SocialSecurityNumberAllContainsDashAtSameIndex() {
            var generator = RandomGenerator.Create();

            var generateMany = generator.GenerateMany(randomizer =>
                    randomizer.SocialSecurityNumber(randomizer.DateByAge(randomizer.Integer(19, 20))), 10000).ToArray();

            Assert.IsTrue(generateMany.All(s => s[6] == '-'));
        }

        [Test]
        public void SocialSecurityNumberAllSameLength() {
            var generator = RandomGenerator.Create();

            var generateMany = generator.GenerateMany(randomizer =>
                    randomizer.SocialSecurityNumber(randomizer.DateByAge(randomizer.Integer(19, 20))), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 11));
        }

        [Test]
        public void SocialSecurityNumberAllUnique() {
            var generator = RandomGenerator.Create();

            var generateMany = generator.GenerateMany(randomizer =>
                    randomizer.SocialSecurityNumber(randomizer.DateByAge(randomizer.Integer(19, 20))), 10000);
            // Will look for repeats and expected behaviour is that it should only contain 1 repeat per grouping.
            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void SocialSecurityOnlyContainsNumberWithNoFormating() {
            var generator = RandomGenerator.Create();

            var generateMany = generator.GenerateMany(randomizer =>
                        randomizer.SocialSecurityNumber(randomizer.DateByAge(randomizer.Integer(19, 20)), false), 10000)
                .ToArray();

            Assert.IsTrue(generateMany.All(s => s.All(char.IsNumber)));
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