using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class DateRandomizerTests
    {
        private const int Amount = 10000;

        [Test]
        public void Date_By_Age_Arg_MinusOne_Throws()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            Assert.Throws<ArgumentException>(() => dateRandomizer.DateByAge(-1));
        }

        [Test]
        public void Date_By_Age_Arg_Twenty_Is_Not_More_Than_Twenty_And_Less_Than_Twenty_One()
        {
            const int age = 20;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByAge(age));
            var dateTime = DateTime.Now;
            var twentyYearsAgo = dateTime.AddYears(-age);
            var twentyOneYearsAgo = dateTime.AddYears(-(age + 1));
            // Checks that The date will be at least 20 years. 
            Assert.IsTrue(list.All(time => twentyYearsAgo > time && twentyOneYearsAgo < time));
        }

        [Test]
        public void Date_By_Age_Arg_Zero()
        {
            const int age = 0;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByAge(age));
            // Checks that date will be more than now.
            Assert.IsTrue(list.All(time => DateTime.Now > time));
        }

        [Test]
        public void Date_By_Age_Time_Is_Not_Zero()
        {
            const int age = 20;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByAge(age));
            // Checks if any time is not zero.
            Assert.IsTrue(list.Any(time => time.Minute != 0 || time.Second != 0 || time.Hour != 0));
        }

        [Test]
        public void Date_By_Year_Arg_Current_Year()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByYear(DateTime.Now.Year));
            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(list.All(time => time.Year == DateTime.Now.Year));
        }

        [Test]
        public void Date_By_Year_Arg_MinusOne_Throws()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            Assert.Throws<ArgumentException>(() => dateRandomizer.DateByYear(-1));
        }

        [Test]
        public void Date_By_Year_Arg_TwoThousand()
        {
            const int year = 2000;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByYear(year));
            Assert.IsTrue(list.All(time => time.Year == year));
        }

        [Test]
        public void Date_By_Year_Arg_TwoThousandTen()
        {
            const int year = 2010;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByYear(year));
            Assert.IsTrue(list.All(time => time.Year == year));
        }

        private static readonly DateTime BaseTime = new DateTime(2020, 10, 20, 22, 50, 30, 20);

        [Test]
        public void Date_MaxDateTime__Adding_Years()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoYearsLater = DateTime.Now.AddYears(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoYearsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoYearsLater),
                "list.All(time => time < baseTimeTwoYearsLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Subtracting_Years()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoYearsBefore = DateTime.Now.AddYears(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoYearsBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoYearsBefore),
                "list.All(time => time < baseTimeTwoYearsLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Adding_Months()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMonthsLater = DateTime.Now.AddMonths(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMonthsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMonthsLater),
                "list.All(time => time < baseTimeTwoMonthsLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Subtracting_Months()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMonthsBefore = DateTime.Now.AddMonths(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMonthsBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMonthsBefore),
                "list.All(time => time < baseTimeTwoMonthsLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Adding_Days()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoDaysLater = DateTime.Now.AddDays(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoDaysLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoDaysLater),
                "list.All(time => time < baseTimeTwoDaysLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Subtracting_Days()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoDaysBefore = DateTime.Now.AddDays(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoDaysBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoDaysBefore),
                "list.All(time => time < baseTimeTwoDaysLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Adding_Hours()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoHoursLater = DateTime.Now.AddHours(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoHoursLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoHoursLater),
                "list.All(time => time < baseTimeTwoHoursLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Subtracting_Hours()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoHoursBefore = DateTime.Now.AddHours(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoHoursBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoHoursBefore),
                "list.All(time => time < baseTimeTwoHoursLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Adding_Minutes()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMinutesLater = DateTime.Now.AddMinutes(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMinutesLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMinutesLater),
                "list.All(time => time < baseTimeTwoMinutesLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Subtracting_Minutes()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMinutesBefore = DateTime.Now.AddMinutes(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMinutesBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMinutesBefore),
                "list.All(time => time < baseTimeTwoMinutesLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Adding_Seconds()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoSecondsLater = DateTime.Now.AddSeconds(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoSecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoSecondsLater),
                "list.All(time => time < baseTimeTwoSecondsLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Subtracting_Seconds()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoSecondsBefore = DateTime.Now.AddSeconds(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoSecondsBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoSecondsBefore),
                "list.All(time => time < baseTimeTwoSecondsLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Adding_MilliSeconds()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMilliSecondsLater = DateTime.Now.AddMilliseconds(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMilliSecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMilliSecondsLater),
                "list.All(time => time < baseTimeTwoMilliSecondsLater)"
            );
        }

        [Test]
        public void Date_MaxDateTime__Subtracting_MilliSeconds()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMilliSecondsBefore = DateTime.Now.AddMilliseconds(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMilliSecondsBefore));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMilliSecondsBefore),
                "list.All(time => time < baseTimeTwoMilliSecondsLater)"
            );
        }

        [Test]
        public void Date_MinDateTime_MaxDateTime__Adding_Years()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoYearsLater = DateTime.Now.AddYears(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(DateTime.Now, baseTimeTwoYearsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoYearsLater),
                "list.All(time => time < baseTimeTwoYearsLater)"
            );
        }


        [Test]
        public void Date_MinDateTime_MaxDateTime__Subtracting_Years()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoYearsAgo = BaseTime.AddYears(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoYearsAgo, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime),
                "list.All(time => time < BaseTime)"
            );
        }

        [Test]
        public void Date_MinDateTime_MaxDateTime__Adding_Months()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMonthsLater = BaseTime.AddMonths(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(DateTime.Now, baseTimeTwoMonthsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMonthsLater),
                "list.All(time => time < baseTimeTwoMonthsLater)"
            );
        }

        [Test]
        public void Date_MinDateTime_MaxDateTime__Subtracting_Months()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMonthsAgo = BaseTime.AddMonths(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMonthsAgo, BaseTime));

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
        public void Date_MinDateTime_MaxDateTime__Adding_Days()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoDaysLater = BaseTime.AddDays(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(DateTime.Now, baseTimeTwoDaysLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoDaysLater),
                "list.All(time => time < baseTimeTwoDaysLater)"
            );
        }

        [Test]
        public void Date_MinDateTime_MaxDateTime__Subtracting_Days()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoDaysAgo = BaseTime.AddDays(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoDaysAgo, BaseTime));

            Assert.IsTrue(
                list.All(time => time < BaseTime), "list.All(time => time < BaseTime)"
            );
            Assert.IsTrue(
                list.All(time => time.Month == BaseTime.Month),
                "list.All(time => time.Month == BaseTime.Month)"
            );
        }

        [Test]
        public void Date_MinDateTime_MaxDateTime__Adding_Minutes()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMinutesLater = BaseTime.AddMinutes(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(DateTime.Now, baseTimeTwoMinutesLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMinutesLater),
                "list.All(time => time < baseTimeTwoMinutesLater)"
            );
        }

        [Test]
        public void Date_MinDateTime_MaxDateTime__Subtracting_Minutes()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMinutesBefore = BaseTime.AddMinutes(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMinutesBefore, BaseTime));

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
        public void Date_MinDateTime_MaxDateTime__Adding_Seconds()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoSecondsLater = BaseTime.AddSeconds(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(DateTime.Now, baseTimeTwoSecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoSecondsLater),
                "list.All(time => time < baseTimeTwoSecondsLater)"
            );
        }

        [Test]
        public void Date_MinDateTime_MaxDateTime__Subtracting_Seconds()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoSecondsAgo = BaseTime.AddSeconds(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoSecondsAgo, BaseTime));

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
        public void Date_MinDateTime_MaxDateTime__Adding_MilliSeconds()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMillisecondsLater = BaseTime.AddMilliseconds(2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(DateTime.Now, baseTimeTwoMillisecondsLater));

            Assert.IsTrue(
                list.All(time => time < baseTimeTwoMillisecondsLater),
                "list.All(time => time < baseTimeTwoMillisecondsLater)"
            );
        }

        [Test]
        public void Date_MinDateTime_MaxDateTime__Subtracting_Milliseconds()
        {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            var baseTimeTwoMiliSecondsBefore = BaseTime.AddMilliseconds(-2);
            for (var i = 0; i < Amount; i++)
                list.Add(dateRandomizer.Date(baseTimeTwoMiliSecondsBefore, BaseTime));

            Assert.IsTrue(list.All(time => time < BaseTime));
            Assert.IsTrue(
                list.All(time => time.Second == BaseTime.Second),
                "list.All(time => time.Second == BaseTime.Second)"
            );
        }
    }
}