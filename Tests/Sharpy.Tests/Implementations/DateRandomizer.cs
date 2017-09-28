using System;
using NUnit.Framework;
using Sharpy.Implementation;

namespace Tests.Sharpy.Tests.Integration {
    [TestFixture]
    public class DateRandomizerTests {
        [Test]
        public void Date_By_Age_Arg_MinusOne() {
            var dateRandomizer = new DateRandomizer(new Random());
            Assert.Throws<ArgumentException>(() => dateRandomizer.DateByAge(-1));
        }

        [Test]
        public void Date_By_Age_Arg_Twenty() {
            const int age = 20;
            var dateRandomizer = new DateRandomizer(new Random());
            var dateByAge = dateRandomizer.DateByAge(age);
            Assert.AreEqual(DateTime.Now.Year - age, dateByAge.Year);
        }

        [Test]
        [Repeat(100)]
        public void Date_By_Age_Arg_Zero() {
            const int age = 0;
            var dateRandomizer = new DateRandomizer(new Random());
            var dateByAge = dateRandomizer.DateByAge(age);

            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateRandomizer.CurrentLocalDate > dateByAge);
        }

        [Test]
        [Repeat(100)]
        public void Date_By_Year_Arg_Current_Year() {
            var year = DateTime.Now.Year;
            var dateRandomizer = new DateRandomizer(new Random());
            var dateByAge = dateRandomizer.DateByYear(year);

            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateRandomizer.CurrentLocalDate > dateByAge);
        }

        [Test]
        public void Date_By_Year_Arg_MinusOne() {
            var dateRandomizer = new DateRandomizer(new Random());
            Assert.Throws<ArgumentException>(() => dateRandomizer.DateByYear(-1));
        }

        [Test]
        public void Date_By_Year_Arg_TwoThousand() {
            const int year = 2000;
            var dateRandomizer = new DateRandomizer(new Random());
            var dateByYear = dateRandomizer.DateByYear(year);
            Assert.AreEqual(year, dateByYear.Year);
        }

        [Test]
        public void Date_By_Year_Arg_TwoThousandTen() {
            const int year = 2010;
            var dateRandomizer = new DateRandomizer(new Random());
            var dateByYear = dateRandomizer.DateByYear(year);
            Assert.AreEqual(year, dateByYear.Year);
        }
    }
}