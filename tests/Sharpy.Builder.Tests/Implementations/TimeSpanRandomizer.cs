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

            // Assert we just don't get zeroes
            Assert.True(timeSpans.Any(x => x >= TimeSpan.Zero), "timeSpans.Any(x => x >= TimeSpan.Zero)");
        }
        [Test]
        public void All_Timespans_Greater_Or_Equal_To_Zero_And_Less_Than_Argument()
        {
            var timeSpans = new TimeSpan[Iterations];

            var max = TimeSpan.MaxValue.Subtract(TimeSpan.FromDays(20));
            for (var i = 0; i < Iterations; i++)
                timeSpans[i] = _timeSpanProvider.TimeSpan(max);


            Assert.True(timeSpans.All(x => x <= max), "timeSpans.All(x => x <= max)");

            // Assert we just don't get zeroes
            Assert.True(timeSpans.Any(x => x >= TimeSpan.Zero), "timeSpans.Any(x => x >= TimeSpan.Zero)");
        }
        
        [Test]
        public void Passing_TimeSpan_Equal_To_Min_Only_Gives_Min_Value()
        {
            var timeSpans = new TimeSpan[Iterations];

            var max = TimeSpan.Zero;
            for (var i = 0; i < Iterations; i++)
                timeSpans[i] = _timeSpanProvider.TimeSpan(max);


            Assert.True(timeSpans.All(x => x == max), "timeSpans.All(x => x == max)");

        }
    }
}