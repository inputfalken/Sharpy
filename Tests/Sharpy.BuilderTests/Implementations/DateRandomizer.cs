using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Tests.Sharpy.BuilderTests.Implementations {
    [TestFixture]
    public class DateRandomizerTests {
        private const int Amount = 10000;

        [Test]
        public void Date_By_Age_Arg_MinusOne_Throws() {
            var dateRandomizer = new DateRandomizer(new Random());
            Assert.Throws<ArgumentException>(() => dateRandomizer.DateByAge(-1));
        }

        [Test]
        public void Date_By_Age_Arg_Twenty_Is_Not_More_Than_Twenty_And_Less_Than_Twenty_One() {
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
        public void Date_By_Age_Arg_Zero() {
            const int age = 0;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByAge(age));
            // Checks that date will be more than now.
            Assert.IsTrue(list.All(time => DateTime.Now > time));
        }

        [Test]
        public void Date_By_Age_Time_Is_Not_Zero() {
            const int age = 20;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByAge(age));
            // Checks if any time is not zero.
            Assert.IsTrue(list.Any(time => time.Minute != 0 || time.Second != 0 || time.Hour != 0));
        }

        [Test]
        public void Date_By_Year_Arg_Current_Year() {
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByYear(DateTime.Now.Year));
            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(list.All(time => time.Year == DateTime.Now.Year));
        }

        [Test]
        public void Date_By_Year_Arg_MinusOne_Throws() {
            var dateRandomizer = new DateRandomizer(new Random());
            Assert.Throws<ArgumentException>(() => dateRandomizer.DateByYear(-1));
        }

        [Test]
        public void Date_By_Year_Arg_TwoThousand() {
            const int year = 2000;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByYear(year));
            Assert.IsTrue(list.All(time => time.Year == year));
        }

        [Test]
        public void Date_By_Year_Arg_TwoThousandTen() {
            const int year = 2010;
            var dateRandomizer = new DateRandomizer(new Random());
            var list = new List<DateTime>();
            for (var i = 0; i < Amount; i++) list.Add(dateRandomizer.DateByYear(year));
            Assert.IsTrue(list.All(time => time.Year == year));
        }
    }
}