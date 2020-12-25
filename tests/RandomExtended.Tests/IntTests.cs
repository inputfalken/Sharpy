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
        public void Clusivity_Exclusive_Min_Exclusive_Max()
        {
            Assert.Throws<ArgumentException>(() => Random.Int(RandomExtension.Exclusive(1), RandomExtension.Exclusive(2)));
            Assert.Throws<ArgumentException>(() => Random.Int(RandomExtension.Exclusive(1), RandomExtension.Exclusive(1)));
            Assert.DoesNotThrow(() => Random.Int(RandomExtension.Exclusive(1), RandomExtension.Exclusive(3)));

            // The only viable number to randomize is 2 with these numbers.
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(2, Random.Int(RandomExtension.Exclusive(1), RandomExtension.Exclusive(3)));
        }

        [Test]
        public void Clusivity_Inclusive_Min_Inclusive_Max()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Int(RandomExtension.Inclusive(int.MaxValue - 1), RandomExtension.Inclusive(int.MaxValue)),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(1, Random.Int(RandomExtension.Inclusive(1), RandomExtension.Inclusive(1)));
        }

        [Test]
        public void Clusivity_Exclusive_Min_Inclusive_Max()
        {
            Assert.AreEqual(
                int.MaxValue,
                Random.Int(RandomExtension.Exclusive(int.MaxValue), RandomExtension.Inclusive(int.MaxValue)),
                "Can return maxValue"
            );

            Assert.AreEqual(
                int.MaxValue,
                Random.Int(RandomExtension.Exclusive(int.MaxValue - 1), RandomExtension.Inclusive(int.MaxValue)),
                "Can return maxValue"
            );
            
            Assert.AreEqual(1, Random.Int(RandomExtension.Exclusive(1), RandomExtension.Inclusive(1)));
            Assert.AreEqual(2, Random.Int(RandomExtension.Exclusive(1), RandomExtension.Inclusive(2)));
            Assert.AreEqual(3, Random.Int(RandomExtension.Exclusive(2), RandomExtension.Inclusive(3)));
            Assert.AreEqual(4, Random.Int(RandomExtension.Exclusive(3), RandomExtension.Inclusive(4)));


            Assertion.IsDistributed(
                Random,
                x => x.Int(RandomExtension.Exclusive(1), RandomExtension.Inclusive(3)),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            Assertion.IsDistributed(
                Random,
                x => x.Int(RandomExtension.Exclusive(1), RandomExtension.Inclusive(4)),
                x => Assert.True(x.Count == 3, "x.Count == 3")
            );

            Assertion.IsDistributed(
                Random,
                x => x.Int(RandomExtension.Exclusive(1), RandomExtension.Inclusive(5)),
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