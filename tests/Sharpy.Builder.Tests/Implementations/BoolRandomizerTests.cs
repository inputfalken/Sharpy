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
            Assertion.IsDeterministic(
                i => new BoolRandomizer(new Random(i)),
                x => x.Bool()
            );
        }

        [Test]
        public void Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(
                i => new BoolRandomizer(new Random(i)),
                x => x.Bool()
            );
        }

        [Test]
        public void Values_Are_Distributed()
        {
            Assertion.IsDistributed(
                new BoolRandomizer(new Random(Assertion.MainSeed)),
                x => x.Bool(),
                grouping => Assert.AreEqual(2, grouping.Count)
            );
        }
    }
}