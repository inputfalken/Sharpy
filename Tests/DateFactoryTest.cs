using System;
using DataGen.Types.Date;
using NodaTime;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class DateFactoryTest {
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
    }
}