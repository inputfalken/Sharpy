﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class CustomCollection {
        [Test]
        public void Array() {
            var randomGenerator = Generator.Create(new Builder());

            var args = new[] {"hello", "there", "foo"};
            var generateMany = randomGenerator
                .Select(provider => provider.Element(args))
                .Take(10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }

        [Test]
        public void List() {
            var randomGenerator = Generator.Create(new Builder());
            var args = new List<string> {"hello", "there", "foo"};
            var generateMany = randomGenerator
                .Select(provider => provider.Element(args))
                .Take(10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}