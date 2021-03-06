using System;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class BoolTests
    {
        [TestFixture]
        public class BoolRandomizerTests
        {
            [Test]
            public void Is_Deterministic_With_Seed()
            {
                Assertion.IsDeterministic(
                    i => new Random(i),
                    x => x.Bool()
                );
            }

            [Test]
            public void Is_Not_Deterministic_With_Different_Seed()
            {
                Assertion.IsNotDeterministic(
                    i => new Random(i),
                    x => x.Bool()
                );
            }

            [Test]
            public void Values_Are_Distributed()
            {
                Assertion.IsDistributed(
                    new Random(Assertion.MainSeed),
                    x => x.Bool(),
                    grouping => Assert.AreEqual(2, grouping.Count)
                );
            }
        }
    }
}