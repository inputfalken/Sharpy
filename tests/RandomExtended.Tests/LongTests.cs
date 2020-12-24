using System;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class LongTests
    {
        private static readonly Random Random = new();

        [Test]
        public void Is_Distributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Long(long.MinValue, long.MaxValue),
                x => Assert.IsTrue(x.Count > Assertion.Amount / 2, "x.Count > Assertion.Amount / 2")
            );
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new Random(i), x => x.Long(0, 50));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new Random(i), x => x.Long(0, 50));
        }


        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var longs = new long[Assertion.Amount];

            const long min = 100;
            const long max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                longs[i] = Random.Long(min, max);

            longs.AssertNotAllValuesAreTheSame();
            Assert.True(
                longs.All(x => x >= min && x < max),
                "longs.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var longs = new long[Assertion.Amount];

            const long arg = 100;
            for (var i = 0; i < Assertion.Amount; i++)
                longs[i] = Random.Long(arg, arg);

            Assert.True(
                longs.All(x => x == arg),
                "longs.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var longs = new long[Assertion.Amount];

            const long max = 100;
            const long min = max - 1;
            for (var i = 0; i < Assertion.Amount; i++)
                longs[i] = Random.Long(min, max);


            Assert.True(
                longs.All(x => x == min),
                "longs.All(x => x == min)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const long max = 100;
            const long min = max;

            Assertion.DoesNotThrow(() => Random.Long(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const long max = 100;
            const long min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Long(min, max));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Throw()
        {
            Assertion.DoesNotThrow(() => Random.Long(long.MinValue, long.MaxValue));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Produce_Same_Values()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Long(long.MinValue, long.MaxValue),
                _ => { }
            );
        }
    }
}