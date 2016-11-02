﻿using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Generator {
    [TestFixture]
    public class Seed {
        private const int TestSeed = 100;

        [Test]
        public void Bools() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the bools expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(TestSeed);
            var random = new Random(TestSeed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(2) != 0);
            var result = generator.GenerateMany(randomizer => randomizer.Bool(), 1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void IntegerDoubleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            const int min = 20;
            var generator = RandomGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizer => randomizer.Integer(min, max), count);
            var result = generator.GenerateMany(randomizer => randomizer.Integer(min, max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void IntegerNoArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizer => randomizer.Integer(), count);
            var result = generator.GenerateMany(randomizer => randomizer.Integer(), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void IntegerSingleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            var generator = RandomGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizer => randomizer.Integer(max), count);
            var result = generator.GenerateMany(randomizer => randomizer.Integer(max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void LongDoubleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = generator2.GenerateMany(randomizer => randomizer.Long(min, max), count);
            var result = generator.GenerateMany(randomizer => randomizer.Long(min, max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void LongNoArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            var expected = generator2.GenerateMany(randomizer => randomizer.Long(), count);
            var result = generator.GenerateMany(randomizer => randomizer.Long(), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void LongSingleArgument() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = RandomGenerator.Create();
            generator.Config.Seed(TestSeed);
            var generator2 = RandomGenerator.Create();
            generator2.Config.Seed(TestSeed);
            const int count = 1000;
            const long max = long.MaxValue - 3923329;
            var expected = generator2.GenerateMany(randomizer => randomizer.Long(max), count);
            var result = generator.GenerateMany(randomizer => randomizer.Long(max), count);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void SecurityNumber() {
            const int count = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            const int age = 20;
            var generateManyA =
                generatorA.GenerateMany(randomizer => randomizer.SocialSecurityNumber(randomizer.DateByAge(age)), count);
            var generateManyB =
                generatorB.GenerateMany(randomizer => randomizer.SocialSecurityNumber(randomizer.DateByAge(age)), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringAnyName() {
            const int count = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.AnyName), count);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.AnyName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringFemaleFirstName() {
            const int count = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.FemaleFirstName),
                count);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.FemaleFirstName),
                count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringFirstName() {
            const int count = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.FirstName), count);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.FirstName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringLastName() {
            const int count = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.LastName), count);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.LastName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringMaleFirstName() {
            const int count = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.MaleFirstName), count);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.MaleFirstName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }

        [Test]
        public void StringUserName() {
            const int count = 100;
            var generatorA = RandomGenerator.Create();
            generatorA.Config.Seed(TestSeed);
            var generatorB = RandomGenerator.Create();
            generatorB.Config.Seed(TestSeed);

            var generateManyA = generatorA.GenerateMany(randomizer => randomizer.String(StringType.UserName), count);
            var generateManyB = generatorB.GenerateMany(randomizer => randomizer.String(StringType.UserName), count);
            Assert.IsTrue(generateManyA.SequenceEqual(generateManyB));
        }
    }
}