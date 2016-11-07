﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests.Generator {
    [TestFixture]
    public class CustomCollection {
        [Test]
        public void Array() {
            var randomGenerator = Sharpy.Generator.Create();
            var args = new[] {"hello", "there", "foo"};
            var generateMany = randomGenerator.GenerateMany(generator => generator.Params(args),10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }

        [Test]
        public void List() {
            var randomGenerator = Sharpy.Generator.Create();
            var args = new List<string> {"hello", "there", "foo"};
            var generateMany = randomGenerator.GenerateMany(generator => generator.CustomCollection(args), 10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}