using System;
using DataGen.Types.Date;
using NodaTime;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class DateFactoryTest {
        [Test]
        //Todo Test with sero day, month, year and create tests for mail factory as well as sequence in the date factory
        public void CreateDate_Arg_Subtract_True() {
            var currentLocalDate = DateFactory.CurrentLocalDate;
            const int day = 1;
            const int year = 1;
            const int month = 2;
            var result = DateFactory.Date(day, month, year);
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
            var result = DateFactory.Date(year, month, day);
            Assert.AreEqual(currentLocalDate, result);
        }

        [Test]
        public void CreateDate_Arg_Subtract_NegativeArgs() {
            Assert.Throws<ArgumentException>(() => DateFactory.Date(-1, 1, 1));
            Assert.Throws<ArgumentException>(() => DateFactory.Date(0, -1, 1));
            Assert.Throws<ArgumentException>(() => DateFactory.Date(0, 0, -1));
        }


        [Test]
        public void CreateDate_Arg_Subtract_False() {
            var currentLocalDate = DateFactory.CurrentLocalDate;
            const int day = 1;
            const int year = 1;
            const int month = 2;
            var result = DateFactory.Date(year, month, year, false);
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
            var result = DateFactory.Date(year, month, day, false);
            Assert.AreEqual(currentLocalDate, result);
        }
    }
}