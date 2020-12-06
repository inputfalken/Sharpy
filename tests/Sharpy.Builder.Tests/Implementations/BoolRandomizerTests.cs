using System;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class BoolRandomizerTests
    {
        [Test]
        public void Is_Deterministic_With_Seed()
        {
            var expected = new BoolRandomizer(new Random(Assertion.MainSeed));
            var result = new BoolRandomizer(new Random(Assertion.MainSeed));

            Assertion.AreEqual(expected, result, x => x.Bool());
        }

        [Test]
        public void Is_Not_Deterministic_With_Different_Seed()
        {
            var expected = new BoolRandomizer(new Random(Assertion.MainSeed));
            var result = new BoolRandomizer(new Random(Assertion.SecondarySeed));

            Assertion.AreNotEqual(expected, result, x => x.Bool());
        }

        [Test]
        public void Values_Are_Distributed()
        {
            var result = new BoolRandomizer(new Random(Assertion.MainSeed));
            Assertion.IsDistributed(result, x => x.Bool(), grouping => Assert.AreEqual(2, grouping.Count));
        }
    }
}