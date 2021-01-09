using System;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class TimeSpanTests
    {
        private static readonly Random Random = new();

        [Test]
        public void Exclusive()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Random.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(2), Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Random.TimeSpan(TimeSpan.FromTicks(2), TimeSpan.FromTicks(3), Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Random.TimeSpan(TimeSpan.FromTicks(3), TimeSpan.FromTicks(4), Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Random.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(1), Rule.Exclusive));
            Assert.DoesNotThrow(() => Random.Int(1, 3, Rule.Exclusive));

            // The only viable tick to randomize is 2 with these values.
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(TimeSpan.FromTicks(2),
                    Random.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(3), Rule.Exclusive));
        }

        [Test]
        public void Inclusive()
        {
            Assert.AreEqual(
                TimeSpan.MaxValue,
                Random.TimeSpan(TimeSpan.MaxValue, TimeSpan.MaxValue, Rule.Inclusive),
                "Can return maxValue"
            );
            Assertion.IsDistributed(
                Random,
                x => x.TimeSpan(TimeSpan.MaxValue - TimeSpan.FromTicks(1), TimeSpan.MaxValue, Rule.Inclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(TimeSpan.FromTicks(1),
                    Random.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(1), Rule.Inclusive));
        }

        [Test]
        public void InclusiveExclusive()
        {
            Assert.AreEqual(
                TimeSpan.MaxValue,
                Random.TimeSpan(TimeSpan.MaxValue, TimeSpan.MaxValue, Rule.InclusiveExclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(TimeSpan.FromTicks(1),
                Random.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(1), Rule.InclusiveExclusive));
            Assert.AreEqual(TimeSpan.FromTicks(1),
                Random.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(2), Rule.InclusiveExclusive));
            Assert.AreEqual(TimeSpan.FromTicks(2),
                Random.TimeSpan(TimeSpan.FromTicks(2), TimeSpan.FromTicks(3), Rule.InclusiveExclusive));
            Assert.AreEqual(TimeSpan.FromTicks(3),
                Random.TimeSpan(TimeSpan.FromTicks(3), TimeSpan.FromTicks(4), Rule.InclusiveExclusive));
        }

        [Test]
        public void ExclusiveInclusive()
        {
            Assert.AreEqual(
                TimeSpan.MaxValue,
                Random.TimeSpan(TimeSpan.MaxValue, TimeSpan.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(
                TimeSpan.MaxValue,
                Random.TimeSpan(TimeSpan.MaxValue - TimeSpan.FromTicks(1), TimeSpan.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(TimeSpan.FromTicks(1), Random.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(1), Rule.ExclusiveInclusive));
            Assert.AreEqual(TimeSpan.FromTicks(2), Random.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(2), Rule.ExclusiveInclusive));
            Assert.AreEqual(TimeSpan.FromTicks(3), Random.TimeSpan(TimeSpan.FromTicks(2), TimeSpan.FromTicks(3), Rule.ExclusiveInclusive));
            Assert.AreEqual(TimeSpan.FromTicks(4), Random.TimeSpan(TimeSpan.FromTicks(3), TimeSpan.FromTicks(4), Rule.ExclusiveInclusive));


            Assertion.IsDistributed(
                Random,
                x => x.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(3), Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            Assertion.IsDistributed(
                Random,
                x => x.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(4), Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 3, "x.Count == 3")
            );

            Assertion.IsDistributed(
                Random,
                x => x.TimeSpan(TimeSpan.FromTicks(1), TimeSpan.FromTicks(5), Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 4, "x.Count == 4")
            );
        }

        [Test]
        public void Is_Distributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.TimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue),
                x => Assert.IsTrue(x.Count > Assertion.Amount / 2, "x.Count > Assertion.Amount / 2")
            );
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(
                i => new Random(i),
                x => x.TimeSpan(TimeSpan.MaxValue.Subtract(TimeSpan.FromDays(20)),
                    TimeSpan.MaxValue.Subtract(TimeSpan.FromDays(5)))
            );
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(
                i => new Random(i),
                x => x.TimeSpan(TimeSpan.MaxValue.Subtract(TimeSpan.FromDays(20)),
                    TimeSpan.MaxValue.Subtract(TimeSpan.FromDays(5)))
            );
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var timeSpans = new TimeSpan[Assertion.Amount];

            var min = TimeSpan.FromDays(1);
            var max = TimeSpan.FromDays(2);
            for (var i = 0; i < Assertion.Amount; i++)
                timeSpans[i] = Random.TimeSpan(min, max);

            timeSpans.AssertNotAllValuesAreTheSame();
            Assert.True(
                timeSpans.All(x => x >= min && x < max),
                "TimeSpans.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var timeSpans = new TimeSpan[Assertion.Amount];

            var arg = TimeSpan.FromDays(1);
            for (var i = 0; i < Assertion.Amount; i++)
                timeSpans[i] = Random.TimeSpan(arg, arg);

            Assert.True(
                timeSpans.All(x => x == arg),
                "TimeSpans.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var timeSpans = new TimeSpan[Assertion.Amount];

            var max = TimeSpan.FromDays(1);
            var min = max.Subtract(TimeSpan.FromTicks(1));
            for (var i = 0; i < Assertion.Amount; i++)
                timeSpans[i] = Random.TimeSpan(min, max);

            Assert.True(
                timeSpans.All(x => x == min),
                "TimeSpans.All(x => x == min)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            var max = TimeSpan.FromDays(1);
            var min = max;

            Assertion.DoesNotThrow(() => Random.TimeSpan(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            var max = TimeSpan.FromDays(1);
            var min = max.Add(TimeSpan.FromTicks(1));

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.TimeSpan(min, max));
        }

        [Test]
        public void MinValue_And_Max_Does_Not_Throw()
        {
            Assertion.DoesNotThrow(() => Random.TimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Produce_Same_Values()
        {
            Assertion.IsDistributed(
                Random,
                x => x.TimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue),
                _ => { }
            );
        }
    }
}