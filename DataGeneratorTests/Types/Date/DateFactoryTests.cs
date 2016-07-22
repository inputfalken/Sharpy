using System;
using DataGen.Types.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace DataGeneratorTests.Types.Date {
    [TestClass]
    public class DateFactoryTests {
        #region Subtraction No Args

        [TestMethod]
        public void SubtractNoArgs() {
            var randomPreviousDate = DateFactory.Subtraction();
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomPreviousDate == now);
        }

        #endregion

        #region Subtraction Years

        [TestMethod]
        public void SubtractOneYear() {
            const int years = 1;
            var randomPreviousDate = DateFactory.Subtraction(years: years);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomPreviousDate == now.Minus(Period.FromYears(1)));
        }

        [TestMethod]
        public void SubtractHundredYear() {
            const int years = 100;
            var randomPreviousDate = DateFactory.Subtraction(years: years);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomPreviousDate == now.Minus(Period.FromYears(100)));
        }

        #endregion

        #region Subtraction Months

        [TestMethod]
        public void SubtractionDateTestOneMonth() {
            var randomFutureDate = DateFactory.Subtraction(months: 1);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.Minus(Period.FromMonths(1)));
        }

        [TestMethod]
        public void SubtractionDateTestHundredMonth() {
            var randomFutureDate = DateFactory.Subtraction(months: 100);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.Minus(Period.FromMonths(100)));
        }

        #endregion

        #region Subtraction Days

        [TestMethod]
        public void SubtractionDateTestOneDay() {
            var randomFutureDate = DateFactory.Subtraction(days: 1);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.Minus(Period.FromDays(1)));
        }

        [TestMethod]
        public void SubtractionDateTestHundredDays() {
            var randomFutureDate = DateFactory.Subtraction(days: 100);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.Minus(Period.FromDays(100)));
        }

        #endregion

        #region Addition No Args

        [TestMethod]
        public void AddNoArgs() {
            var expected = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            var result = DateFactory.Addition();
            Assert.IsTrue(result == expected);
        }

        #endregion

        #region Addition Years

        [TestMethod]
        public void AddOneYear() {
            const int years = 1;
            var randomFutureDate = DateFactory.Addition(years);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomFutureDate.Year == currentYear + years);
        }

        [TestMethod]
        public void AddHundredYears() {
            const int years = 100;
            var randomFutureDate = DateFactory.Addition(years);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomFutureDate.Year == currentYear + years);
        }

        #endregion

        #region Addition Months

        [TestMethod]
        public void FutureDateTestOneMonth() {
            var randomFutureDate = DateFactory.Addition(month: 1);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.PlusMonths(1));
        }

        [TestMethod]
        public void FutureDateTestHundredMonth() {
            var randomFutureDate = DateFactory.Addition(month: 100);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.PlusMonths(100));
        }

        #endregion

        #region Addtion Days

        public void FutureDateTestOneDay() {
            var randomFutureDate = DateFactory.Addition(days: 1);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.PlusDays(1));
        }

        [TestMethod]
        public void FutureDateTestHundredDays() {
            var randomFutureDate = DateFactory.Addition(days: 100);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.PlusDays(100));
        }

        #endregion

    }
}