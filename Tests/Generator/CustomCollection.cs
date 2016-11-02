﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class CustomCollection {
        [Test]
        public void CustomCollectionArray() {
            var randomGenerator = RandomGenerator.Create();
            var args = new[] {"hello", "there", "foo"};
            var generateMany = randomGenerator.GenerateMany(randomize => randomize.Params(args));
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }

        [Test]
        public void CustomCollectionList() {
            var randomGenerator = RandomGenerator.Create();
            var args = new List<string> {"hello", "there", "foo"};
            var generateMany = randomGenerator.GenerateMany(randomize => randomize.CustomCollection(args));
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}