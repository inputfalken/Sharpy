using System;
using DataGen.Types.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace DataGeneratorTests.Types.Date {
    [TestClass]
    public class DateFactoryTests {
        #region Date Subtraction Exception handling

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SubtractNegativeValue()
            => DateFactory.SubtractionToCurrentDate(-1);

        #endregion

        #region Date Subtraction no args

        [TestMethod]
        public void SubtractNoArgs() {
            var randomPreviousDate = DateFactory.SubtractionToCurrentDate();
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomPreviousDate == now);
        }

        #endregion

        #region Date Subtraction with Year arg

        [TestMethod]
        public void SubtractOneYear() {
            const int years = 1;
            var randomPreviousDate = DateFactory.SubtractionToCurrentDate(years: years);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomPreviousDate == now.Minus(Period.FromYears(1)));
        }

        [TestMethod]
        public void SubtractHundredYear() {
            const int years = 100;
            var randomPreviousDate = DateFactory.SubtractionToCurrentDate(years: years);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomPreviousDate == now.Minus(Period.FromYears(100)));
        }

        #endregion

        #region Subtraction With Month arg

        [TestMethod]
        public void SubtractionDateTestOneMonth() {
            var randomFutureDate = DateFactory.SubtractionToCurrentDate(month: 1);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.Minus(Period.FromMonths(1)));
        }

        [TestMethod]
        public void SubtractionDateTestHundredMonth() {
            var randomFutureDate = DateFactory.SubtractionToCurrentDate(month: 100);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.Minus(Period.FromMonths(100)));
        }

        #endregion

        #region Subtraction With Days arg

        [TestMethod]
        public void SubtractionDateTestOneDay() {
            var randomFutureDate = DateFactory.SubtractionToCurrentDate(days: 1);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.Minus(Period.FromDays(1)));
        }

        [TestMethod]
        public void SubtractionDateTestHundredDays() {
            var randomFutureDate = DateFactory.SubtractionToCurrentDate(days: 100);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.Minus(Period.FromDays(100)));
        }

        #endregion

        #region Date Addition Exception handling 

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomFutureDateNegativeYear()
            => DateFactory.AdditionToCurrentDate(years: -1);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomFutureDateNegativeMonth()
            => DateFactory.AdditionToCurrentDate(month: -1);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomFutureDateNegativedate()
            => DateFactory.AdditionToCurrentDate(days: -1);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomFutureDateAllArgsNegative()
            => DateFactory.AdditionToCurrentDate(years: -1, month: -1, days: -1);

        #endregion

        #region Date Addition no args

        [TestMethod]
        public void AddNoArgs() {
            var expected = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            var result = DateFactory.AdditionToCurrentDate();
            Assert.IsTrue(result == expected);
        }

        #endregion

        #region Date Addition with year arg

        [TestMethod]
        public void AddOneYear() {
            const int years = 1;
            var randomFutureDate = DateFactory.AdditionToCurrentDate(years);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomFutureDate.Year == currentYear + years);
        }

        [TestMethod]
        public void AddHundredYears() {
            const int years = 100;
            var randomFutureDate = DateFactory.AdditionToCurrentDate(years);
            var currentYear = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date.Year;
            Assert.IsTrue(randomFutureDate.Year == currentYear + years);
        }

        #endregion

        #region Future Random Date With Month Argument

        [TestMethod]
        public void FutureDateTestOneMonth() {
            var randomFutureDate = DateFactory.AdditionToCurrentDate(month: 1);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.PlusMonths(1));
        }

        [TestMethod]
        public void FutureDateTestHundredMonth() {
            var randomFutureDate = DateFactory.AdditionToCurrentDate(month: 100);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.PlusMonths(100));
        }

        #endregion

        #region Date Additon with days arg

        public void FutureDateTestOneDay() {
            var randomFutureDate = DateFactory.AdditionToCurrentDate(days: 1);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.PlusDays(1));
        }

        [TestMethod]
        public void FutureDateTestHundredDays() {
            var randomFutureDate = DateFactory.AdditionToCurrentDate(days: 100);
            var now = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
            Assert.IsTrue(randomFutureDate == now.PlusDays(100));
        }

        #endregion

    }
}