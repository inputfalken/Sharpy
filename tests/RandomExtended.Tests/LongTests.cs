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
        public void Exclusive()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Long(1, 2, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Long(2, 3, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Long(3, 4, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Long(1, 1, Rule.Exclusive));
            Assert.DoesNotThrow(() => Random.Long(1, 3, Rule.Exclusive));

            // The only viable number to randomize is 2 with these numbers.
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(2, Random.Long(1, 3, Rule.Exclusive));
        }

        [Test]
        public void Inclusive()
        {
            Assert.AreEqual(
                long.MaxValue,
                Random.Long(long.MaxValue, long.MaxValue, Rule.Inclusive),
                "Can return maxValue"
            );
            
            Assertion.IsDistributed(
                Random,
                x => x.Int(int.MaxValue - 1, int.MaxValue, Rule.Inclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(1, Random.Long(1, 1, Rule.Inclusive));
        }

        [Test]
        public void InclusiveExclusive()
        {
            Assert.AreEqual(
                int.MaxValue,
                Random.Long(int.MaxValue, int.MaxValue, Rule.InclusiveExclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(1, Random.Long(1, 1, Rule.InclusiveExclusive));
            Assert.AreEqual(1, Random.Long(1, 2, Rule.InclusiveExclusive));
            Assert.AreEqual(2, Random.Long(2, 3, Rule.InclusiveExclusive));
            Assert.AreEqual(3, Random.Long(3, 4, Rule.InclusiveExclusive));
        }

        [Test]
        public void ExclusiveInclusive()
        {
            Assert.AreEqual(
                int.MaxValue,
                Random.Long(int.MaxValue, int.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(
                int.MaxValue,
                Random.Long(int.MaxValue - 1, int.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(1, Random.Long(1, 1, Rule.ExclusiveInclusive));
            Assert.AreEqual(2, Random.Long(1, 2, Rule.ExclusiveInclusive));
            Assert.AreEqual(3, Random.Long(2, 3, Rule.ExclusiveInclusive));
            Assert.AreEqual(4, Random.Long(3, 4, Rule.ExclusiveInclusive));


            Assertion.IsDistributed(
                Random,
                x => x.Int(1, 3, Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            Assertion.IsDistributed(
                Random,
                x => x.Int(1, 4, Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 3, "x.Count == 3")
            );

            Assertion.IsDistributed(
                Random,
                x => x.Int(1, 5, Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 4, "x.Count == 4")
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