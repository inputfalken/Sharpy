﻿using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Iteration {
        [Test]
        public void IteratorWithEnumerable() {
            var iteration = 0;
            var gen = Sharpy.Generator.Create();
            var result = gen.GenerateSequence((generator, i) => iteration++ == i, 20).ToArray();
            Assert.IsTrue(result.All(b => b));
        }
    }
}