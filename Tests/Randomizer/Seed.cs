using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Randomizer {
    [TestFixture]
    public class Seed {
        private const int TestSeed = 100;

        [Test]
        public void Bools() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the bools expected
            var generator = SharpyGenerator.Create();
            generator.Config.Seed(TestSeed);
            var random = new Random(TestSeed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(2) != 0);
            var result = generator.GenerateMany(randomizerr => randomizerr.Bool(), 1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void IntegerDoubleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            const int min = 20;
            var generator = SharpyGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = SharpyGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizerr => randomizerr.Integer(min, max), count);
            var result = generator.GenerateMany(randomizerr => randomizerr.Integer(min, max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void IntegerNoArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = SharpyGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = SharpyGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizerr => randomizerr.Integer(), count);
            var result = generator.GenerateMany(randomizerr => randomizerr.Integer(), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void IntegerSingleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            var generator = SharpyGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = SharpyGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizerr => randomizerr.Integer(max), count);
            var result = generator.GenerateMany(randomizerr => randomizerr.Integer(max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void LongDoubleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = SharpyGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = SharpyGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = generator2.GenerateMany(randomizerr => randomizerr.Long(min, max), count);
            var result = generator.GenerateMany(randomizerr => randomizerr.Long(min, max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void LongNoArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = SharpyGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = SharpyGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizerr => randomizerr.Long(), count);
            var result = generator.GenerateMany(randomizerr => randomizerr.Long(), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void LongSingleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = SharpyGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = SharpyGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            const long max = long.MaxValue - 3923329;
            var expected = generator2.GenerateMany(randomizerr => randomizerr.Long(max), count);
            var result = generator.GenerateMany(randomizerr => randomizerr.Long(max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void SecurityNumber() {
            const int count = 100;
            var generatorA = SharpyGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = SharpyGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            const int age = 20;
            var generateManyA =
                generatorA.GenerateMany(randomizerr => randomizerr.SocialSecurityNumber(randomizerr.DateByAge(age)), count);
            var generateManyB =
                generatorB.GenerateMany(randomizerr => randomizerr.SocialSecurityNumber(randomizerr.DateByAge(age)), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringAnyName() {
            const int count = 100;
            var generatorA = SharpyGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = SharpyGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizerr => randomizerr.String(StringType.AnyName), count);
            var generateManyB = generatorB.GenerateMany(randomizerr => randomizerr.String(StringType.AnyName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringFemaleFirstName() {
            const int count = 100;
            var generatorA = SharpyGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = SharpyGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizerr => randomizerr.String(StringType.FemaleFirstName),
                count);
            var generateManyB = generatorB.GenerateMany(randomizerr => randomizerr.String(StringType.FemaleFirstName),
                count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringFirstName() {
            const int count = 100;
            var generatorA = SharpyGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = SharpyGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizerr => randomizerr.String(StringType.FirstName), count);
            var generateManyB = generatorB.GenerateMany(randomizerr => randomizerr.String(StringType.FirstName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringLastName() {
            const int count = 100;
            var generatorA = SharpyGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = SharpyGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizerr => randomizerr.String(StringType.LastName), count);
            var generateManyB = generatorB.GenerateMany(randomizerr => randomizerr.String(StringType.LastName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringMaleFirstName() {
            const int count = 100;
            var generatorA = SharpyGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = SharpyGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizerr => randomizerr.String(StringType.MaleFirstName), count);
            var generateManyB = generatorB.GenerateMany(randomizerr => randomizerr.String(StringType.MaleFirstName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringUserName() {
            const int count = 100;
            var generatorA = SharpyGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = SharpyGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizerr => randomizerr.String(StringType.UserName), count);
            var generateManyB = generatorB.GenerateMany(randomizerr => randomizerr.String(StringType.UserName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }
    }
}