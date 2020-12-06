using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class DateTimeOffsetOffsetRandomizerTests
    {
        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider = new DateTimeOffsetRandomizer(new Random());


        private static readonly DateTimeOffset BaseTime = DateTime.SpecifyKind(
            new DateTime(2020, 10, 20, 22, 50, 30, 20),
            DateTimeKind.Utc
        );

        [Test]
        public void No_Arg_Is_Deterministic_With_Seed()
        {
            var expected = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            var result = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            Assertion.AreEqual(expected, result, x => x.DateTimeOffset());
        }

        [Test]
        public void No_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            var expected = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            var result = new DateTimeOffsetRandomizer(new Random(Assertion.SecondarySeed));

            Assertion.AreNotEqual(expected, result, x => x.DateTimeOffset());
        }

        [Test]
        public void Max_Arg_Is_Deterministic_With_Seed()
        {
            var expected = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            var result = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            var max = BaseTime;
            Assertion.AreEqual(expected, result, x => x.DateTimeOffset(max));
        }

        [Test]
        public void Max__Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            var expected = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            var result = new DateTimeOffsetRandomizer(new Random(Assertion.SecondarySeed));
            var max = BaseTime;

            Assertion.AreNotEqual(expected, result, x => x.DateTimeOffset(max));
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            var expected = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            var result = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            var min = BaseTime;
            var max = min.AddYears(1);
            Assertion.AreEqual(expected, result, x => x.DateTimeOffset(min, max));
        }

        [Test]
        public void Min_Max__Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            var expected = new DateTimeOffsetRandomizer(new Random(Assertion.MainSeed));
            var result = new DateTimeOffsetRandomizer(new Random(Assertion.SecondarySeed));
            var min = BaseTime;
            var max = min.AddYears(1);

            Assertion.AreNotEqual(expected, result, x => x.DateTimeOffset(min, max));
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Adding_Years()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoYearsLater = BaseTime.AddYears(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoYearsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoYearsLater),
                "list.All(time => time < baseTimeTwoYearsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Subtracting_Years()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoYearsBefore = BaseTime.AddYears(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoYearsBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoYearsBefore),
                "list.All(time => time < baseTimeTwoYearsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Adding_Months()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMonthsLater = BaseTime.AddMonths(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMonthsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMonthsLater),
                "list.All(time => time < baseTimeTwoMonthsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Subtracting_Months()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMonthsBefore = BaseTime.AddMonths(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMonthsBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMonthsBefore),
                "list.All(time => time < baseTimeTwoMonthsLater)"
            );
        }

        [Test]
        public void Does_Not_Throw_When_OffSet_Is_Out_Of_Range()
        {
            // MinValue
            Assert.DoesNotThrow(() =>
                _dateTimeOffsetProvider.DateTimeOffset(
                    DateTimeOffset.MinValue,
                    DateTimeOffset.MinValue.AddMinutes(1)
                )
            );
            Assert.DoesNotThrow(() =>
                _dateTimeOffsetProvider.DateTimeOffset(
                    DateTimeOffset.MinValue,
                    DateTimeOffset.MinValue.AddSeconds(1)
                )
            );
            Assert.DoesNotThrow(() =>
                _dateTimeOffsetProvider.DateTimeOffset(
                    DateTimeOffset.MinValue,
                    DateTimeOffset.MinValue.AddMilliseconds(1)
                )
            );

            // MaxValue
            Assert.DoesNotThrow(() =>
                _dateTimeOffsetProvider.DateTimeOffset(
                    DateTimeOffset.MaxValue.AddMinutes(-1),
                    DateTimeOffset.MaxValue
                )
            );
            Assert.DoesNotThrow(() =>
                _dateTimeOffsetProvider.DateTimeOffset(
                    DateTimeOffset.MaxValue.AddSeconds(-1),
                    DateTimeOffset.MaxValue
                )
            );
            Assert.DoesNotThrow(() =>
                _dateTimeOffsetProvider.DateTimeOffset(
                    DateTimeOffset.MaxValue.AddMilliseconds(-1),
                    DateTimeOffset.MaxValue
                )
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Adding_Days()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoDaysLater = BaseTime.AddDays(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoDaysLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoDaysLater),
                "list.All(time => time < baseTimeTwoDaysLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Subtracting_Days()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoDaysBefore = BaseTime.AddDays(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoDaysBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoDaysBefore),
                "list.All(time => time < baseTimeTwoDaysLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Adding_Hours()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoHoursLater = BaseTime.AddHours(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoHoursLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoHoursLater),
                "list.All(time => time < baseTimeTwoHoursLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Subtracting_Hours()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoHoursBefore = BaseTime.AddHours(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoHoursBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoHoursBefore),
                "list.All(time => time < baseTimeTwoHoursLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Adding_Minutes()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMinutesLater = BaseTime.AddMinutes(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMinutesLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMinutesLater),
                "list.All(time => time < baseTimeTwoMinutesLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Subtracting_Minutes()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMinutesBefore = BaseTime.AddMinutes(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMinutesBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMinutesBefore),
                "list.All(time => time < baseTimeTwoMinutesLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Adding_Seconds()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoSecondsLater = BaseTime.AddSeconds(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoSecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoSecondsLater),
                "list.All(time => time < baseTimeTwoSecondsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Subtracting_Seconds()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoSecondsBefore = BaseTime.AddSeconds(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoSecondsBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoSecondsBefore),
                "list.All(time => time < baseTimeTwoSecondsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Adding_MilliSeconds()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMilliSecondsLater = BaseTime.AddMilliseconds(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMilliSecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMilliSecondsLater),
                "list.All(time => time < baseTimeTwoMilliSecondsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MaxDateTimeOffset__Subtracting_MilliSeconds()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMilliSecondsBefore = BaseTime.AddMilliseconds(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMilliSecondsBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMilliSecondsBefore),
                "list.All(time => time < baseTimeTwoMilliSecondsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Adding_Years()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoYearsLater = BaseTime.AddYears(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(BaseTime, baseTimeTwoYearsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoYearsLater),
                "list.All(time => time < baseTimeTwoYearsLater)"
            );
        }


        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Subtracting_Years()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoYearsAgo = BaseTime.AddYears(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoYearsAgo, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime),
                "list.All(time => time < BaseTime)"
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Adding_Months()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMonthsLater = BaseTime.AddMonths(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(BaseTime, baseTimeTwoMonthsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMonthsLater),
                "list.All(time => time < baseTimeTwoMonthsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Subtracting_Months()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMonthsAgo = BaseTime.AddMonths(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMonthsAgo, BaseTime));

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
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Adding_Days()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoDaysLater = BaseTime.AddDays(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(BaseTime, baseTimeTwoDaysLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoDaysLater),
                "list.All(time => time < baseTimeTwoDaysLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Subtracting_Days()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoDaysAgo = BaseTime.AddDays(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoDaysAgo, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime), "list.All(time => time < BaseTime)"
            );
            Assert.IsTrue(
                list.All(time => time.Month == BaseTime.Month),
                "list.All(time => time.Month == BaseTime.Month)"
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Adding_Minutes()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMinutesLater = BaseTime.AddMinutes(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(BaseTime, baseTimeTwoMinutesLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMinutesLater),
                "list.All(time => time < baseTimeTwoMinutesLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Subtracting_Minutes()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMinutesBefore = BaseTime.AddMinutes(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMinutesBefore, BaseTime));

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
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Adding_Seconds()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoSecondsLater = BaseTime.AddSeconds(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(BaseTime, baseTimeTwoSecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoSecondsLater),
                "list.All(time => time < baseTimeTwoSecondsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Subtracting_Seconds()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoSecondsAgo = BaseTime.AddSeconds(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoSecondsAgo, BaseTime));

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
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Adding_MilliSeconds()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMillisecondsLater = BaseTime.AddMilliseconds(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(BaseTime, baseTimeTwoMillisecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMillisecondsLater),
                "list.All(time => time < baseTimeTwoMillisecondsLater)"
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Subtracting_Milliseconds()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoMiliSecondsBefore = BaseTime.AddMilliseconds(-2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(_dateTimeOffsetProvider.DateTimeOffset(baseTimeTwoMiliSecondsBefore, BaseTime));

            Assert.IsTrue(list.All(time => time < BaseTime));
            Assert.IsTrue(
                list.All(time => time.Second == BaseTime.Second),
                "list.All(time => time.Second == BaseTime.Second)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            var max = DateTimeOffset.Now;
            var min = max;

            var dateRandomizer = new DateTimeOffsetRandomizer(new Random());

            Assert.DoesNotThrow(() => dateRandomizer.DateTimeOffset(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            var max = DateTimeOffset.Now;
            var min = max.AddMilliseconds(1);

            Assert.Throws<ArgumentOutOfRangeException>(() => _dateTimeOffsetProvider.DateTimeOffset(min, max));
        }
    }
}