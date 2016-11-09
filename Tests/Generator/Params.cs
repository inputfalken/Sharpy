﻿using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Params {
        [Test]
        public void WithString() {
            var randomGenerator = Sharpy.Generator.Create();
            var args = new[] {"hello", "there", "foo"};
            var generateMany =
                GeneratorExtensions.GenerateMany(randomGenerator, generator => generator.Params("hello", "there", "foo"), 10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}