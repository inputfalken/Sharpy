using System;
using DataGenerator.Types.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace DataGeneratorTests.Types.Date {
    [TestClass]
    public class DateFactoryTests {
        #region Min And Max Birth Year

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BirthYearTestNegativeValue()
            => DateFactory.RandomPreviousDate(-1, -1);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BirthYearTestMinAgeLargerThanMax()
            => DateFactory.RandomPreviousDate(40, 20);

        [TestMethod]
        public void BirthYearTestTwentyToForty() {
            const int minYear = 20;
            const int maxYear = 40;
            var randomPreviousDate = DateFactory.RandomPreviousDate(minYear, maxYear).Year;
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomPreviousDate >= currentYear - maxYear && randomPreviousDate <= currentYear - minYear);
        }


        [TestMethod]
        public void BirthYearTestTwentyToTwenty() {
            const int maxYear = 20;
            const int minYear = 20;
            var randomPreviousDate = DateFactory.RandomPreviousDate(minYear, maxYear);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomPreviousDate.Year >= currentYear - maxYear &&
                          randomPreviousDate.Year <= currentYear - minYear);
        }

        [TestMethod]
        public void BirthYearTestTwoToEight() {
            const int minYear = 2;
            const int maxYear = 8;
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            var randomPreviousDate = DateFactory.RandomPreviousDate(minYear, maxYear);
            Assert.IsTrue(randomPreviousDate.Year >= currentYear - maxYear &&
                          randomPreviousDate.Year <= currentYear - minYear);
        }

        [TestMethod]
        public void BirthYearTestOneToThree() {
            const int minYear = 1;
            const int maxYear = 3;
            var randomPreviousDate = DateFactory.RandomPreviousDate(minYear, maxYear);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomPreviousDate.Year >= currentYear - maxYear &&
                          randomPreviousDate.Year <= currentYear - minYear);
        }

        [TestMethod]
        public void BirthYearTestOneToOne() {
            const int minYear = 1;
            const int maxYear = 1;
            var randomPreviousDate = DateFactory.RandomPreviousDate(minYear, maxYear);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomPreviousDate.Year >= currentYear - maxYear &&
                          randomPreviousDate.Year <= currentYear - minYear);
        }


        [TestMethod]
        public void BirtYearTestTenToHundred() {
            const int minYear = 10;
            const int maxYear = 100;
            var randomPreviousDate = DateFactory.RandomPreviousDate(minYear, maxYear);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomPreviousDate.Year >= currentYear - maxYear &&
                          randomPreviousDate.Year <= currentYear - minYear);
        }

        [TestMethod]
        public void BirthYearTestZeroToZero() {
            var randomPreviousDate = DateFactory.RandomPreviousDate(0, 0);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomPreviousDate < now);
        }


        [TestMethod]
        public void BirtYearTestZeroToOne() {
            var currentDate = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            var randomPreviousDate = DateFactory.RandomPreviousDate(0, 1);
            Assert.IsTrue(randomPreviousDate < currentDate);
        }

        [TestMethod]
        public void BirthYearTestZeroToOne() {
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            var randomPreviousDate = DateFactory.RandomPreviousDate(0, 1);
            Assert.IsTrue(randomPreviousDate.Year >= currentYear - 1 &&
                          randomPreviousDate.Year <= currentYear);
        }

        #endregion

        #region Specifik Birth Year

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

        #region Future Date with year only argument

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