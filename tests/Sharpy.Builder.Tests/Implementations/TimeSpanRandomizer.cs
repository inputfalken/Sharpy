using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class TimeSpanRandomizerTests
    {
        private const int Amount = 10000000;

        private static readonly ITimeSpanProvider TimeSpanProvider = new TimeSpanRandomizer(new Random());

        [Test]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var timeSpans = new TimeSpan[Amount];

            for (var i = 0; i < Amount; i++)
                timeSpans[i] = TimeSpanProvider.TimeSpan();

            timeSpans.AssertNotAllValuesAreTheSame();
            Assert.True(
                timeSpans.All(x => x > TimeSpan.Zero && x < TimeSpan.MaxValue),
                "TimeSpans.All(x => x > 0 && x < TimeSpan.MaxValue)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var timeSpans = new TimeSpan[Amount];

            var max = TimeSpan.FromDays(1);

            for (var i = 0; i < Amount; i++)
                timeSpans[i] = TimeSpanProvider.TimeSpan(max);

            timeSpans.AssertNotAllValuesAreTheSame();
            Assert.True(
                timeSpans.All(x => x >= TimeSpan.Zero && x < max),
                "TimeSpans.All(x => x > 0 && x < max)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var timeSpans = new TimeSpan[Amount];

            var min = TimeSpan.FromDays(1);
            var max = TimeSpan.FromDays(2);
            for (var i = 0; i < Amount; i++)
                timeSpans[i] = TimeSpanProvider.TimeSpan(min, max);

            timeSpans.AssertNotAllValuesAreTheSame();
            Assert.True(
                timeSpans.All(x => x >= min && x < max),
                "TimeSpans.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var timeSpans = new TimeSpan[Amount];

            var arg = TimeSpan.FromDays(1);
            for (var i = 0; i < Amount; i++)
                timeSpans[i] = TimeSpanProvider.TimeSpan(arg, arg);

            Assert.True(
                timeSpans.All(x => x == arg),
                "TimeSpans.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var timeSpans = new TimeSpan[Amount];

            var max = TimeSpan.FromDays(1);
            var min = max.Subtract(TimeSpan.FromTicks(1));
            for (var i = 0; i < Amount; i++)
                timeSpans[i] = TimeSpanProvider.TimeSpan(min, max);

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

            Assert.DoesNotThrow(() => TimeSpanProvider.TimeSpan(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            var max = TimeSpan.FromDays(1);
            var min = max.Add(TimeSpan.FromTicks(1));

            Assert.Throws<ArgumentOutOfRangeException>(() => TimeSpanProvider.TimeSpan(min, max));
        }
    }
}