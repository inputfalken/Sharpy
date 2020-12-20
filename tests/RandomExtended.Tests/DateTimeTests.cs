using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class DateTimeTests
    {
        private static readonly Random Random = new();
        private static readonly DateTime BaseTime = new(2020, 10, 20, 22, 50, 30, 20);

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            var max = BaseTime.AddYears(1);
            Assertion.IsDeterministic(
                i => new Random(i),
                x => x.DateTime(BaseTime, max)
            );
        }

        [Test]
        public void Min_Max__Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            var max = BaseTime.AddYears(1);
            Assertion.IsNotDeterministic(
                i => new Random(i),
                x => x.DateTime(BaseTime, max)
            );
        }


        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Adding_Years()
        {
            var list = new List<DateTime>();
            var baseTimeTwoYearsLater = BaseTime.AddYears(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(BaseTime, baseTimeTwoYearsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoYearsLater),
                "list.All(time => time < baseTimeTwoYearsLater)"
            );
        }


        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Subtracting_Years()
        {
            var list = new List<DateTime>();
            var baseTimeTwoYearsAgo = BaseTime.AddYears(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(baseTimeTwoYearsAgo, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime),
                "list.All(time => time < BaseTime)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Adding_Months()
        {
            var list = new List<DateTime>();
            var baseTimeTwoMonthsLater = BaseTime.AddMonths(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(BaseTime, baseTimeTwoMonthsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMonthsLater),
                "list.All(time => time < baseTimeTwoMonthsLater)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Subtracting_Months()
        {
            var list = new List<DateTime>();
            var baseTimeTwoMonthsAgo = BaseTime.AddMonths(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(baseTimeTwoMonthsAgo, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime),
                "list.All(time => time < BaseTime)"
            );
            Assert.IsTrue(
                list.All(time => time.Year == BaseTime.Year),
                "list.All(time => time.Year == BaseTime.Year)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Adding_Days()
        {
            var list = new List<DateTime>();
            var baseTimeTwoDaysLater = BaseTime.AddDays(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(BaseTime, baseTimeTwoDaysLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoDaysLater),
                "list.All(time => time < baseTimeTwoDaysLater)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Subtracting_Days()
        {
            var list = new List<DateTime>();
            var baseTimeTwoDaysAgo = BaseTime.AddDays(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(baseTimeTwoDaysAgo, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime), "list.All(time => time < BaseTime)"
            );
            Assert.IsTrue(
                list.All(time => time.Month == BaseTime.Month),
                "list.All(time => time.Month == BaseTime.Month)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Adding_Minutes()
        {
            var list = new List<DateTime>();
            var baseTimeTwoMinutesLater = BaseTime.AddMinutes(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(BaseTime, baseTimeTwoMinutesLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMinutesLater),
                "list.All(time => time < baseTimeTwoMinutesLater)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Subtracting_Minutes()
        {
            var list = new List<DateTime>();
            var baseTimeTwoMinutesBefore = BaseTime.AddMinutes(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(baseTimeTwoMinutesBefore, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime),
                "list.All(time => time < BaseTime)"
            );

            Assert.IsTrue(
                list.All(time => time.Hour == BaseTime.Hour),
                "list.All(time => time.Hour == BaseTime.Hour)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Adding_Seconds()
        {
            var list = new List<DateTime>();
            var baseTimeTwoSecondsLater = BaseTime.AddSeconds(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(BaseTime, baseTimeTwoSecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoSecondsLater),
                "list.All(time => time < baseTimeTwoSecondsLater)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Subtracting_Seconds()
        {
            var list = new List<DateTime>();
            var baseTimeTwoSecondsAgo = BaseTime.AddSeconds(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(baseTimeTwoSecondsAgo, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime),
                "list.All(time => time < BaseTime)"
            );
            Assert.IsTrue(
                list.All(time => time.Minute == BaseTime.Minute),
                "list.All(time => time.Minute == BaseTime.Minute)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Adding_MilliSeconds()
        {
            var list = new List<DateTime>();
            var baseTimeTwoMillisecondsLater = BaseTime.AddMilliseconds(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(BaseTime, baseTimeTwoMillisecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMillisecondsLater),
                "list.All(time => time < baseTimeTwoMillisecondsLater)"
            );
        }

        [Test]
        public void DateTime_MinDateTime_MaxDateTime__Subtracting_Milliseconds()
        {
            var list = new List<DateTime>();
            var baseTimeTwoMiliSecondsBefore = BaseTime.AddMilliseconds(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTime(baseTimeTwoMiliSecondsBefore, BaseTime));

            Assert.IsTrue(list.All(time => time < BaseTime));
            Assert.IsTrue(
                list.All(time => time.Second == BaseTime.Second),
                "list.All(time => time.Second == BaseTime.Second)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            var max = DateTime.Now;
            var min = max;


            Assertion.DoesNotThrow(() => Random.DateTime(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            var max = DateTime.Now;
            var min = max.AddMilliseconds(1);

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.DateTime(min, max));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Throw()
        {
            Assertion.DoesNotThrow(() => Random.DateTime(DateTime.MinValue, DateTime.MaxValue));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Produce_Same_Values()
        {
            Assertion.IsDistributed(
                Random,
                x => x.DateTime(DateTime.MinValue, DateTime.MaxValue),
                _ => { }
            );
        }
    }
}