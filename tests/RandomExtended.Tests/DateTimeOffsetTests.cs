using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class DateTimeOffsetTests
    {
        private static readonly Random Random = new();

        private static readonly DateTimeOffset BaseTime = DateTime.SpecifyKind(
            new DateTime(2020, 10, 20, 22, 50, 30, 20),
            DateTimeKind.Utc
        );

        [Test]
        public void Is_Distributed()
        {
            var max = BaseTime.AddYears(1);
            Assertion.IsDistributed(
                Random,
                x => x.DateTimeOffset(BaseTime, max),
                x => Assert.IsTrue(x.Count > Assertion.Amount / 2, "x.Count > Assertion.Amount / 2")
            );
        }

        private static DateTimeOffset Factory(long ticks)
        {
            var nowOffset = DateTimeOffset.Now.Offset;
            return new DateTimeOffset(nowOffset.Ticks + ticks, nowOffset);
        }

        [Test]
        public void Exclusive()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Random.DateTimeOffset(Factory(1), Factory(2), Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Random.DateTimeOffset(Factory(2), Factory(3), Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Random.DateTimeOffset(Factory(3), Factory(4), Rule.Exclusive));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Random.DateTimeOffset(Factory(1), Factory(1), Rule.Exclusive));
            Assert.DoesNotThrow(() => Random.DateTime(new DateTime(1), new DateTime(3), Rule.Exclusive));

            // The only viable tick to randomize is 2 with these values.
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(Factory(2), Random.DateTimeOffset(Factory(1), Factory(3), Rule.Exclusive));
        }

        [Test]
        public void Inclusive()
        {
            Assert.AreEqual(
                DateTimeOffset.MaxValue,
                Random.DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue, Rule.Inclusive),
                "Can return maxValue"
            );
            Assertion.IsDistributed(
                Random,
                x => x.DateTimeOffset(DateTimeOffset.MaxValue.AddTicks(-1), DateTimeOffset.MaxValue, Rule.Inclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(Factory(1),
                    Random.DateTimeOffset(Factory(1), Factory(1), Rule.Inclusive));
        }

        [Test]
        public void InclusiveExclusive()
        {
            Assert.AreEqual(
                DateTimeOffset.MaxValue,
                Random.DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue, Rule.InclusiveExclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(Factory(1),
                Random.DateTimeOffset(Factory(1), Factory(1), Rule.InclusiveExclusive));
            Assert.AreEqual(Factory(1),
                Random.DateTimeOffset(Factory(1), Factory(2), Rule.InclusiveExclusive));
            Assert.AreEqual(Factory(2),
                Random.DateTimeOffset(Factory(2), Factory(3), Rule.InclusiveExclusive));
            Assert.AreEqual(Factory(3),
                Random.DateTimeOffset(Factory(3), Factory(4), Rule.InclusiveExclusive));
        }

        [Test]
        public void ExclusiveInclusive()
        {
            Assert.AreEqual(
                DateTimeOffset.MaxValue,
                Random.DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(
                DateTimeOffset.MaxValue,
                Random.DateTimeOffset(DateTimeOffset.MaxValue.AddTicks(-1), DateTimeOffset.MaxValue, Rule.ExclusiveInclusive),
                "Can return maxValue"
            );

            Assert.AreEqual(Factory(1),
                Random.DateTimeOffset(Factory(1), Factory(1), Rule.ExclusiveInclusive));
            Assert.AreEqual(Factory(2),
                Random.DateTimeOffset(Factory(1), Factory(2), Rule.ExclusiveInclusive));
            Assert.AreEqual(Factory(3),
                Random.DateTimeOffset(Factory(2), Factory(3), Rule.ExclusiveInclusive));
            Assert.AreEqual(Factory(4),
                Random.DateTimeOffset(Factory(3), Factory(4), Rule.ExclusiveInclusive));


            Assertion.IsDistributed(
                Random,
                x => x.DateTimeOffset(Factory(1), Factory(3), Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 2, "x.Count == 2")
            );

            Assertion.IsDistributed(
                Random,
                x => x.DateTimeOffset(Factory(1), Factory(4), Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 3, "x.Count == 3")
            );

            Assertion.IsDistributed(
                Random,
                x => x.DateTimeOffset(Factory(1), Factory(5), Rule.ExclusiveInclusive),
                x => Assert.True(x.Count == 4, "x.Count == 4")
            );
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            var max = BaseTime.AddYears(1);
            Assertion.IsDeterministic(
                i => new Random(i),
                x => x.DateTimeOffset(BaseTime, max)
            );
        }

        [Test]
        public void Min_Max__Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            var max = BaseTime.AddYears(1);
            Assertion.IsNotDeterministic(
                i => new Random(i),
                x => x.DateTimeOffset(BaseTime, max)
            );
        }

        [Test]
        public void Does_Not_Throw_When_OffSet_Is_Out_Of_Range()
        {
            // MinValue
            Assertion.DoesNotThrow(() =>
                Random.DateTimeOffset(
                    DateTimeOffset.MinValue,
                    DateTimeOffset.MinValue.AddMinutes(1)
                )
            );
            Assertion.DoesNotThrow(() =>
                Random.DateTimeOffset(
                    DateTimeOffset.MinValue,
                    DateTimeOffset.MinValue.AddSeconds(1)
                )
            );
            Assertion.DoesNotThrow(() =>
                Random.DateTimeOffset(
                    DateTimeOffset.MinValue,
                    DateTimeOffset.MinValue.AddMilliseconds(1)
                )
            );

            // MaxValue
            Assertion.DoesNotThrow(() =>
                Random.DateTimeOffset(
                    DateTimeOffset.MaxValue.AddMinutes(-1),
                    DateTimeOffset.MaxValue
                )
            );
            Assertion.DoesNotThrow(() =>
                Random.DateTimeOffset(
                    DateTimeOffset.MaxValue.AddSeconds(-1),
                    DateTimeOffset.MaxValue
                )
            );
            Assertion.DoesNotThrow(() =>
                Random.DateTimeOffset(
                    DateTimeOffset.MaxValue.AddMilliseconds(-1),
                    DateTimeOffset.MaxValue
                )
            );
        }

        [Test]
        public void DateTimeOffset_MinDateTimeOffset_MaxDateTimeOffset__Adding_Years()
        {
            var list = new List<DateTimeOffset>();
            var baseTimeTwoYearsLater = BaseTime.AddYears(2);
            for (var i = 0; i < Assertion.Amount; i++)
                list.Add(Random.DateTimeOffset(BaseTime, baseTimeTwoYearsLater));

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
                list.Add(Random.DateTimeOffset(baseTimeTwoYearsAgo, BaseTime));

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
                list.Add(Random.DateTimeOffset(BaseTime, baseTimeTwoMonthsLater));

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
                list.Add(Random.DateTimeOffset(baseTimeTwoMonthsAgo, BaseTime));

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
                list.Add(Random.DateTimeOffset(BaseTime, baseTimeTwoDaysLater));

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
                list.Add(Random.DateTimeOffset(baseTimeTwoDaysAgo, BaseTime));

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
                list.Add(Random.DateTimeOffset(BaseTime, baseTimeTwoMinutesLater));

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
                list.Add(Random.DateTimeOffset(baseTimeTwoMinutesBefore, BaseTime));

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
                list.Add(Random.DateTimeOffset(BaseTime, baseTimeTwoSecondsLater));

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
                list.Add(Random.DateTimeOffset(baseTimeTwoSecondsAgo, BaseTime));

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
                list.Add(Random.DateTimeOffset(BaseTime, baseTimeTwoMillisecondsLater));

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
                list.Add(Random.DateTimeOffset(baseTimeTwoMiliSecondsBefore, BaseTime));

            Assert.IsTrue(list.All(time => time < BaseTime));
            Assert.IsTrue(
                list.All(time => time.Second == BaseTime.Second),
                "list.All(time => time.Second == BaseTime.Second)"
            );
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            var max = DateTimeOffset.Now;
            var min = max.AddMilliseconds(1);

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.DateTimeOffset(min, max));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Throw()
        {
            Assertion.DoesNotThrow(() =>
                Random.DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Produce_Same_Values()
        {
            Assertion.IsDistributed(
                Random,
                x => x.DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue),
                _ => { }
            );
        }
    }
}