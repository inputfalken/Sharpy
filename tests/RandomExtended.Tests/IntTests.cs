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
        public void Exclusive()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Int(1, 2, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Int(2, 3, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Int(3, 4, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Int(1, 1, Rule.Exclusive));
            Assert.DoesNotThrow(() => Random.Int(1, 3, Rule.Exclusive));

            // The only viable number to randomize is 2 with these numbers.
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(2, Random.Int(1, 3, Rule.Exclusive));
        }

        [Test]
        public void Inclusive()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Int(int.MaxValue - 1, int.MaxValue, Rule.Inclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(1, Random.Int(1, 1, Rule.Inclusive));
        }

        [Test]
        public void InclusiveExclusive()
        {
            Assert.AreEqual(
                int.MaxValue,
                Random.Int(int.MaxValue, int.MaxValue, Rule.InclusiveExclusive),
                "Can return maxValue"
            );
            
            Assert.AreEqual(1, Random.Int(1, 1, Rule.InclusiveExclusive));
            Assert.AreEqual(1, Random.Int(1, 2, Rule.InclusiveExclusive));
            Assert.AreEqual(2, Random.Int(2, 3, Rule.InclusiveExclusive));
            Assert.AreEqual(3, Random.Int(3, 4, Rule.InclusiveExclusive));
        }

        [Test]
        public void ExclusiveInclusive()
        {
            Assert.AreEqual(
                int.MaxValue,
                Random.Int(int.MaxValue, int.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(
                int.MaxValue,
                Random.Int(int.MaxValue - 1, int.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(1, Random.Int(1, 1, Rule.ExclusiveInclusive));
            Assert.AreEqual(2, Random.Int(1, 2, Rule.ExclusiveInclusive));
            Assert.AreEqual(3, Random.Int(2, 3, Rule.ExclusiveInclusive));
            Assert.AreEqual(4, Random.Int(3, 4, Rule.ExclusiveInclusive));


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