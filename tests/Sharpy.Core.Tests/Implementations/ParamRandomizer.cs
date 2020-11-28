using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Sharpy.Core.Linq;

namespace Sharpy.Core.Tests.Implementations
{
    [TestFixture]
    internal class ParameterRandomizer
    {
        [Test]
        public void Randomizes_As_Expected_With_Seed()
        {
            var items = new List<string> {"Foo", "Bar", "Doe"};
            var result = Generator
                .ArgumentRandomizer(new Random(20), "Foo", "Bar", "Doe")
                .ToList(100);
            // So the seed can change
            var expected = Generator
                .Create(new Random(20))
                .Select(rnd => items[rnd.Next(items.Count)])
                .ToList(100);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Randomizes_Different_Values_If_No_Seed_Is_Provided()
        {
            var items = new List<string> {"Foo", "Bar", "Doe"};
            var result = Generator
                .ArgumentRandomizer("Foo", "Bar", "Doe")
                .ToList(100);
            // So the seed can change
            Thread.Sleep(100);
            var expected = Generator
                .Create(new Random())
                .Select(rnd => items[rnd.Next(items.Count)])
                .ToList(100);
            Assert.AreNotEqual(expected, result);
        }
    }
}