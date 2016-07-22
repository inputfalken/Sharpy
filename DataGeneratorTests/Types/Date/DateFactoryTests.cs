using System;
using DataGenerator.Types.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace DataGeneratorTests.Types.Date {
    [TestClass]
    public class DateFactoryTests {
        #region Previous Random Date With Year Only

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BirthYearNegativeValue()
            => DateFactory.RandomPreviousDate(-1);

        [TestMethod]
        public void BirthYearTwentyYear() {
            var randomPreviousDate = DateFactory.RandomPreviousDate(20);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomPreviousDate.Year == currentYear - 20);
        }

        [TestMethod]
        public void BirthYearTenYear() {
            var randomPreviousDate = DateFactory.RandomPreviousDate(10);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomPreviousDate.Year == currentYear - 10);
        }

        [TestMethod]
        public void BirthYearZeroYears() {
            var randomPreviousDate = DateFactory.RandomPreviousDate(0);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomPreviousDate.Year == currentYear);
        }

        [TestMethod]
        public void PreviousDateWithinCurrentYear() {
            const int minYear = 0;
            var randomPreviousDate = DateFactory.RandomPreviousDate(minYear);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomPreviousDate < now);
        }

        #endregion

        #region Future Random Date with year only argument

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomFutureDateNegativeValue()
            => DateFactory.RandomFutureDate(-1);

        [TestMethod]
        public void RandomFutureDateTenYears() {
            const int years = 10;
            var randomFutureDate = DateFactory.RandomFutureDate(years);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomFutureDate.Year == currentYear + years);
        }

        [TestMethod]
        public void RandomFutureDateOneYears() {
            const int years = 1;
            var randomFutureDate = DateFactory.RandomFutureDate(years);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomFutureDate.Year == currentYear + years);
        }

        [TestMethod]
        public void RandomFutureDateHundredYears() {
            const int years = 100;
            var randomFutureDate = DateFactory.RandomFutureDate(years);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomFutureDate.Year == currentYear + years);
        }

        [TestMethod]
        public void RandomFutureDateWithinCurrentYear() {
            const int years = 0;
            var randomFutureDate = DateFactory.RandomFutureDate(years);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate > currentYear);
        }

        #endregion
    }
}