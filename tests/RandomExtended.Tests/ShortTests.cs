using System;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class ShortTests
    {
        private static readonly Random Random = new();

        [Test]
        public void Is_Distributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Short(short.MinValue, short.MaxValue),
                x => Assert.IsTrue(x.Count > Assertion.Amount / 2, "x.Count > Assertion.Amount / 2")
            );
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new Random(i), x => x.Short(0, short.MaxValue));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new Random(i), x => x.Short(0, short.MaxValue));
        }

        [Test]
        public void Exclusive()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Short(1, 2, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Short(2, 3, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Short(3, 4, Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Short(1, 1, Rule.Exclusive));
            Assert.DoesNotThrow(() => Random.Short(1, 3, Rule.Exclusive));

            // The only viable number to randomize is 2 with these numbers.
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(2, Random.Short(1, 3, Rule.Exclusive));
        }

        [Test]
        public void Inclusive()
        {
            Assert.AreEqual(
                short.MaxValue,
                Random.Short(short.MaxValue, short.MaxValue, Rule.Inclusive),
                "Can return maxValue"
            );
            
            Assertion.IsDistributed(
                Random,
                x => x.Short(short.MaxValue - 1, short.MaxValue, Rule.Inclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(1, Random.Short(1, 1, Rule.Inclusive));
        }

        [Test]
        public void InclusiveExclusive()
        {
            Assert.AreEqual(
                short.MaxValue,
                Random.Short(short.MaxValue, short.MaxValue, Rule.InclusiveExclusive),
                "Can return maxValue"
            );
            
            Assert.AreEqual(1, Random.Short(1, 1, Rule.InclusiveExclusive));
            Assert.AreEqual(1, Random.Short(1, 2, Rule.InclusiveExclusive));
            Assert.AreEqual(2, Random.Short(2, 3, Rule.InclusiveExclusive));
            Assert.AreEqual(3, Random.Short(3, 4, Rule.InclusiveExclusive));
        }

        [Test]
        public void ExclusiveInclusive()
        {
            Assert.AreEqual(
                short.MaxValue,
                Random.Short(short.MaxValue, short.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(
                short.MaxValue,
                Random.Short(short.MaxValue - 1, short.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(1, Random.Short(1, 1, Rule.ExclusiveInclusive));
            Assert.AreEqual(2, Random.Short(1, 2, Rule.ExclusiveInclusive));
            Assert.AreEqual(3, Random.Short(2, 3, Rule.ExclusiveInclusive));
            Assert.AreEqual(4, Random.Short(3, 4, Rule.ExclusiveInclusive));


            Assertion.IsDistributed(
                Random,
                x => x.Short(1, 3, Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            Assertion.IsDistributed(
                Random,
                x => x.Short(1, 4, Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 3, "x.Count == 3")
            );

            Assertion.IsDistributed(
                Random,
                x => x.Short(1, 5, Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 4, "x.Count == 4")
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var shorts = new short[Assertion.Amount];

            const short arg = 100;
            for (var i = 0; i < Assertion.Amount; i++)
                shorts[i] = Random.Short(arg, arg);

            Assert.True(
                shorts.All(x => x == arg),
                "shorts.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var shorts = new short[Assertion.Amount];

            const short max = 100;
            const short min = max - 1;
            for (var i = 0; i < Assertion.Amount; i++)
                shorts[i] = Random.Short(min, max);


            Assert.True(
                shorts.All(x => x == min),
                "shorts.All(x => x == min)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const short max = 100;
            const short min = max;

            Assertion.DoesNotThrow(() => Random.Short(min, max));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Throw()
        {
            const short max = 100;
            const short min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Short(min, max));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Produce_Same_Values()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Short(short.MinValue, short.MaxValue),
                _ => { }
            );
        }
    }
}