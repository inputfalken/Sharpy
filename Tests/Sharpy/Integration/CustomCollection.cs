﻿using System.Collections.Generic;
using System.Linq;
using GeneratorAPI;
using NUnit.Framework;
using Sharpy;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class CustomCollection {
        [Test]
        public void Array() {
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider());

            var args = new[] {"hello", "there", "foo"};
            var generateMany = randomGenerator
                .Generate(provider => provider.Params(args))
                .Take(10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }

        [Test]
        public void List() {
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider());
            var args = new List<string> {"hello", "there", "foo"};
            var generateMany = randomGenerator
                .Generate(provider => provider.CustomCollection(args))
                .Take(10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}