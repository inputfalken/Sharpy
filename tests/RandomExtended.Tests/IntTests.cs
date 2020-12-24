using System;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class IntTests
    {
        private static readonly Random Random = new();

        [Test]
        public void Is_Distributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Int(int.MinValue, int.MaxValue),
                x => Assert.IsTrue(x.Count > Assertion.Amount / 2, "x.Count > Assertion.Amount / 2")
            );
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new Random(i), x => x.Int(0, int.MaxValue));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new Random(i), x => x.Int(0, int.MaxValue));
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var ints = new int[Assertion.Amount];

            const int arg = 100;
            for (var i = 0; i < Assertion.Amount; i++)
                ints[i] = Random.Int(arg, arg);

            Assert.True(
                ints.All(x => x == arg),
                "ints.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var ints = new int[Assertion.Amount];

            const int max = 100;
            const int min = max - 1;
            for (var i = 0; i < Assertion.Amount; i++)
                ints[i] = Random.Int(min, max);


            Assert.True(
                ints.All(x => x == min),
                "ints.All(x => x == min)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const int max = 100;
            const int min = max;

            Assertion.DoesNotThrow(() => Random.Int(min, max));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Throw()
        {
            const int max = 100;
            const int min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Int(min, max));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Produce_Same_Values()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Int(int.MinValue, int.MaxValue),
                _ => { }
            );
        }
    }
}