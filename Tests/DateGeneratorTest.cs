using System;
using System.Linq;
using NodaTime;
using NUnit.Framework;
using Sharpy.Randomizer.Generators;

namespace Tests {
    [TestFixture]
    public class DateGeneratorTest {
        [Test]
        public void CreateSequence_Arg_CustomDate() {
            var startDate = new LocalDate(1992, 1, 1);
            var result = DateGenerator.Sequence(3, new LocalDate(1, 2, 3), startDate).ToList();
            var expected1 =
                startDate
                    .Plus(Period.FromYears(1))
                    .Plus(Period.FromMonths(2))
                    .Plus(Period.FromDays(3));
            var expected2 =
                startDate
                    .Plus(Period.FromYears(2))
                    .Plus(Period.FromMonths(4))
                    .Plus(Period.FromDays(6));
            var expected3 =
                startDate
                    .Plus(Period.FromYears(3))
                    .Plus(Period.FromMonths(6))
                    .Plus(Period.FromDays(9));
            Assert.AreEqual(result[0], expected1);
            Assert.AreEqual(result[1], expected2);
            Assert.AreEqual(result[2], expected3);
        }

        [Test]
        public void CreateSequence_Arg_DefaultDate() {
            var result = DateGenerator.Sequence(3, new LocalDate(1, 2, 3)).ToList();
            var expected1 =
                DateGenerator.CurrentLocalDate
                    .Plus(Period.FromYears(1))
                    .Plus(Period.FromMonths(2))
                    .Plus(Period.FromDays(3));
            var expected2 =
                DateGenerator.CurrentLocalDate
                    .Plus(Period.FromYears(2))
                    .Plus(Period.FromMonths(4))
                    .Plus(Period.FromDays(6));
            var expected3 =
                DateGenerator.CurrentLocalDate
                    .Plus(Period.FromYears(3))
                    .Plus(Period.FromMonths(6))
                    .Plus(Period.FromDays(9));
            Assert.AreEqual(result[0], expected1);
            Assert.AreEqual(result[1], expected2);
            Assert.AreEqual(result[2], expected3);
        }


        [Test]
        public void Date_Arg_Subtract_False() {
            var currentLocalDate = DateGenerator.CurrentLocalDate;
            const int day = 1;
            const int year = 1;
            const int month = 2;
            var result = DateGenerator.Date(year, month, year, false);
            Assert.AreEqual(currentLocalDate.Day + day, result.Day);
            Assert.AreEqual(currentLocalDate.Year + year, result.Year);
            Assert.AreEqual(currentLocalDate.Month + month, result.Month);
        }

        [Test]
        public void Date_Arg_Subtract_False_ZeroYearsMontsDays() {
            var currentLocalDate = DateGenerator.CurrentLocalDate;
            const int day = 0;
            const int year = 0;
            const int month = 0;
            var result = DateGenerator.Date(year, month, day, false);
            Assert.AreEqual(currentLocalDate, result);
        }

        [Test]
        public void Date_Arg_Subtract_NegativeArgs() {
            Assert.Throws<ArgumentException>(() => DateGenerator.Date(-1, 1, 1));
            Assert.Throws<ArgumentException>(() => DateGenerator.Date(0, -1, 1));
            Assert.Throws<ArgumentException>(() => DateGenerator.Date(0, 0, -1));
        }

        [Test]
        public void Date_Arg_Subtract_True() {
            var currentLocalDate = DateGenerator.CurrentLocalDate;
            const int day = 1;
            const int year = 1;
            const int month = 2;
            var result = DateGenerator.Date(day, month, year);
            Assert.AreEqual(currentLocalDate.Day - day, result.Day);
            Assert.AreEqual(currentLocalDate.Year - year, result.Year);
            Assert.AreEqual(currentLocalDate.Month - month, result.Month);
        }

        [Test]
        public void Date_Arg_Subtract_True_ZeroYearsMontsDays() {
            var currentLocalDate = DateGenerator.CurrentLocalDate;
            const int day = 0;
            const int year = 0;
            const int month = 0;
            var result = DateGenerator.Date(year, month, day);
            Assert.AreEqual(currentLocalDate, result);
        }

        [Test]
        public void RandomDateByAgeTwentyMinusOne() {
            //Will throw exception if argument is less than 0
            var dateGenerator = new DateGenerator(new Random());
            Assert.Throws<ArgumentException>(() => dateGenerator.RandomDateByAge(-1));
        }

        [Test]
        public void RandomDateByAgeTwentyYears() {
            var dateGenerator = new DateGenerator(new Random());
            var result = dateGenerator.RandomDateByAge(20);
            Assert.AreEqual(result.Year, DateGenerator.CurrentLocalDate.Year - 20);
        }

        [Test]
        public void RandomDateByAgeZeroYears() {
            var dateGenerator = new DateGenerator(new Random());
            var result = dateGenerator.RandomDateByAge(0);
            //will make sure that the date created is earlier than today this year
            Assert.IsTrue(DateGenerator.CurrentLocalDate > result);
        }
    }
}