﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class BoolRandomizerTests
    {
        private const int MainSeed = 100;
        private const int SecondarySeed = MainSeed + 1;


        [Test]
        public void Is_Deterministic_With_Seed()
        {
            var expected = new BoolRandomizer(new Random(MainSeed));
            var result = new BoolRandomizer(new Random(MainSeed));

            Assertion.AreEqual(expected, result, x => x.Bool());
        }

        [Test]
        public void Is_Not_Deterministic_With_Different_Seed()
        {
            var expected = new BoolRandomizer(new Random(MainSeed));
            var result = new BoolRandomizer(new Random(SecondarySeed));

            Assertion.AreNotEqual(expected, result, x => x.Bool());
        }

        [Test]
        public void Values_Are_Distributed()
        {
            var result = new BoolRandomizer(new Random(MainSeed));
            Assertion.IsDistributed(result, x => x.Bool(), grouping => Assert.AreEqual(2, grouping.Count));
        }
    }
}