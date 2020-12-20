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