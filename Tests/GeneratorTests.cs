﻿using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using NUnit.Framework;
using Sharpy;
using Sharpy.Configurement;
using Sharpy.Enums;
using Sharpy.Types;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Date;
using Sharpy.Types.String;

//Todo set a seed and let all tests be ran from that seed so i can expect values...

namespace Tests {
    /// <summary>
    ///    These tests are all used with a seed so the result are always the same.
    /// </summary>
    [TestFixture]
    public class GeneratorTests {
        private const int Seed = 100;


        [Test]
        public void Seed_With_Number() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int limit = 100;
            var generator = Factory.CreateGenerator(randomizer => randomizer.Number(limit));
            generator.Config.Seed(Seed);

            var random = new Random(Seed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(limit));

            var result = generator.Generate(1000);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void Seed_With_Bools() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the bools expected
            var generator = Factory.CreateGenerator(randomizer => randomizer.Bool());
            generator.Config.Seed(Seed);

            var random = new Random(Seed);
            var expected = Enumerable.Range(0, 1000).Select(i => random.Next(2) != 0);

            var result = generator.Generate(1000);

            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}