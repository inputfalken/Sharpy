using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class TimeSpanRandomizerTests
    {
        private readonly IProviders.ITimeSpanProvider _timeSpanProvider = new TimeSpanRandomizer(new Random());
        private const int Iterations = 1000;

        [Test]
        public void No_Argument_All_Timespans_Greater_Or_Equal_To_Zero()
        {
            var timeSpans = new TimeSpan[Iterations];

            for (var i = 0; i < Iterations; i++)
                timeSpans[i] = _timeSpanProvider.TimeSpan();


            Assert.True(timeSpans.All(x => x >= TimeSpan.Zero), "timeSpans.All(x => x > TimeSpan.Zero)");
            timeSpans.AssertNotAllValuesAreTheSame();
            // Assert we just don't get zeroes
            Assert.True(timeSpans.Any(x => x >= TimeSpan.Zero), "timeSpans.Any(x => x >= TimeSpan.Zero)");
        }

        [Test]
        public void Max_All_TimeSpans_Greater_Or_Equal_To_Zero()
        {
            var timeSpans = new TimeSpan[Iterations];

            var max = TimeSpan.FromDays(20);
            for (var i = 0; i < Iterations; i++)
                timeSpans[i] = _timeSpanProvider.TimeSpan(max);


            Assert.True(timeSpans.All(x => x <= max), "timeSpans.All(x => x <= max)");

            timeSpans.AssertNotAllValuesAreTheSame();
            // Assert we just don't get zeroes
            Assert.True(timeSpans.Any(x => x >= TimeSpan.Zero), "timeSpans.Any(x => x >= TimeSpan.Zero)");
        }

        [Test]
        public void Max_Greater_Than_Min_Only_Gives_Values_Within_The_Range()
        {
            var timeSpans = new TimeSpan[Iterations];
            var min = TimeSpan.FromDays(10);
            var max = TimeSpan.FromDays(20);

            for (var i = 0; i < Iterations; i++)
                timeSpans[i] = _timeSpanProvider.TimeSpan(min, max);

            timeSpans.AssertNotAllValuesAreTheSame();
            Assert.True(timeSpans.All(x => x <= max), "timeSpans.All(x => x == max)");
        }

        [Test]
        public void Max_Equal_To_Min_Only_Gives_Same_Value()
        {
            var timeSpans = new TimeSpan[Iterations];

            var max = TimeSpan.Zero;
            for (var i = 0; i < Iterations; i++)
                timeSpans[i] = _timeSpanProvider.TimeSpan(TimeSpan.Zero, max);

            Assert.True(timeSpans.All(x => x == max), "timeSpans.All(x => x == max)");
        }
    }
}