using System;
using System.Linq;
using DataGen.Types.Date;
using NodaTime;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class DateFactoryTest {
        #region CreateDate

        [Test]
        public void CreateDate_Arg_Subtract_True() {
            var currentLocalDate = DateFactory.CurrentLocalDate;
            const int day = 1;
            const int year = 1;
            const int month = 2;
            var result = DateFactory.CreateDate(day, month, year);
            Assert.AreEqual(currentLocalDate.Day - day, result.Day);
            Assert.AreEqual(currentLocalDate.Year - year, result.Year);
            Assert.AreEqual(currentLocalDate.Month - month, result.Month);
        }

        [Test]
        public void CreateDate_Arg_Subtract_True_ZeroYearsMontsDays() {
            var currentLocalDate = DateFactory.CurrentLocalDate;
            const int day = 0;
            const int year = 0;
            const int month = 0;
            var result = DateFactory.CreateDate(year, month, day);
            Assert.AreEqual(currentLocalDate, result);
        }

        [Test]
        public void CreateDate_Arg_Subtract_NegativeArgs() {
            Assert.Throws<ArgumentException>(() => DateFactory.CreateDate(-1, 1, 1));
            Assert.Throws<ArgumentException>(() => DateFactory.CreateDate(0, -1, 1));
            Assert.Throws<ArgumentException>(() => DateFactory.CreateDate(0, 0, -1));
        }


        [Test]
        public void CreateDate_Arg_Subtract_False() {
            var currentLocalDate = DateFactory.CurrentLocalDate;
            const int day = 1;
            const int year = 1;
            const int month = 2;
            var result = DateFactory.CreateDate(year, month, year, false);
            Assert.AreEqual(currentLocalDate.Day + day, result.Day);
            Assert.AreEqual(currentLocalDate.Year + year, result.Year);
            Assert.AreEqual(currentLocalDate.Month + month, result.Month);
        }

        [Test]
        public void CreateDate_Arg_Subtract_False_ZeroYearsMontsDays() {
            var currentLocalDate = DateFactory.CurrentLocalDate;
            const int day = 0;
            const int year = 0;
            const int month = 0;
            var result = DateFactory.CreateDate(year, month, day, false);
            Assert.AreEqual(currentLocalDate, result);
        }

        #endregion

        #region Sequence

        [Test]
        public void CreateSequence_Arg_DefaultDate() {
            var result = DateFactory.CreateSequence(3, new LocalDate(1, 2, 3)).ToList();
            var expected1 =
                DateFactory.CurrentLocalDate
                    .Plus(Period.FromYears(1))
                    .Plus(Period.FromMonths(2))
                    .Plus(Period.FromDays(3));
            var expected2 =
                DateFactory.CurrentLocalDate
                    .Plus(Period.FromYears(2))
                    .Plus(Period.FromMonths(4))
                    .Plus(Period.FromDays(6));
            var expected3 =
                DateFactory.CurrentLocalDate
                    .Plus(Period.FromYears(3))
                    .Plus(Period.FromMonths(6))
                    .Plus(Period.FromDays(9));
            Assert.AreEqual(result[0], expected1);
            Assert.AreEqual(result[1], expected2);
            Assert.AreEqual(result[2], expected3);
        }

        [Test]
        public void CreateSequence_Arg_CustomDate() {
            var startDate = new LocalDate(1992, 1, 1);
            var result = DateFactory.CreateSequence(3, new LocalDate(1, 2, 3), startDate).ToList();
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

        #endregion
    }
}