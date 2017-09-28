using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Implementation;

namespace Tests.Sharpy.Tests.Integration {
    [TestFixture]
    public class BoolRandomizerTests {
        [Test]
        public void Returns_Various_Results() {
            var boolRandomizer = new BoolRandomizer(new Random());
            var list = new List<bool>(100);
            for (var i = 0; i < 100; i++) list.Add(boolRandomizer.Bool());
            Assert.IsFalse(list.All(b => b));
        }
    }
}