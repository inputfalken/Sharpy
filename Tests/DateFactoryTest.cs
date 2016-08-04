using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGen.Types.Date;
using NodaTime;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class DateFactoryTest {
        public class DateFactoryTests {
            #region Subtraction No UInt args

            [Test]
            public void SubtractNoArgs() {
                var result = DateFactory.Subtraction();
                var expected = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubtractSpecifikDate() {
                var result = DateFactory.Subtraction(new LocalDate(2000, 1, 1));
                var expected = new LocalDate(2000, 1, 1);
                Assert.AreEqual(result, expected);
            }

            #endregion

            #region Subtraction Years

            [Test]
            public void SubtractOneYear() {
                const int years = 1;
                var result = DateFactory.Subtraction(years: years);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Minus(Period.FromYears(1));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubtractSpecifkDateOneYear() {
                const int years = 1;
                var result = DateFactory.Subtraction(years: years, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(1999, 1, 1);
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubtractHundredYear() {
                const int years = 100;
                var result = DateFactory.Subtraction(years: years);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Minus(Period.FromYears(100));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubtractSpecifkDateHundredYear() {
                const int years = 100;
                var result = DateFactory.Subtraction(years: years, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(1900, 1, 1);
                Assert.AreEqual(result, expected);
            }

            #endregion

            #region Subtraction Months

            [Test]
            public void SubtractionOneMonth() {
                const int months = 1;
                var result = DateFactory.Subtraction(months: months);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Minus(Period.FromMonths(months));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubractionSpecifikDateOnemonth() {
                const int months = 1;
                var result = DateFactory.Subtraction(months: months, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(1999, 12, 1);
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubtractionHundredMonth() {
                const int months = 100;
                var result = DateFactory.Subtraction(months: months);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Minus(Period.FromMonths(months));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubractionSpecifikDateHundredMonth() {
                const int months = 100;
                var result = DateFactory.Subtraction(months: months, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(2000, 1, 1).Minus(Period.FromMonths(months));
                Assert.AreEqual(result, expected);
            }

            #endregion

            #region Subtraction Days

            [Test]
            public void SubtractinOneDay() {
                const int days = 1;
                var result = DateFactory.Subtraction(days: days);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Minus(Period.FromDays(days));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubtractionHundredDays() {
                const int days = 100;
                var result = DateFactory.Subtraction(days: days);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Minus(Period.FromDays(days));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubtractionSpecifikDateHundredDays() {
                const int days = 100;
                var result = DateFactory.Subtraction(days: days, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(2000, 1, 1).Minus(Period.FromDays(days));

                Assert.AreEqual(result, expected);
            }

            [Test]
            public void SubtractionSpecifikDateOneDay() {
                const int days = 1;
                var result = DateFactory.Subtraction(days: days, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(2000, 1, 1).Minus(Period.FromDays(days));

                Assert.AreEqual(result, expected);
            }

            #endregion

            #region Addition No UInt args

            [Test]
            public void AddNoArgs() {
                var expected = SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;
                var result = DateFactory.Addition();
                Assert.IsTrue(result == expected);
            }

            [Test]
            public void AddDateArg() {
                var expected = new LocalDate(2000, 1, 1);
                var result = DateFactory.Addition(new LocalDate(2000, 1, 1));
                Assert.IsTrue(expected == result);
            }

            #endregion

            #region Addition Years

            [Test]
            public void AddOneYear() {
                const int years = 1;
                var result = DateFactory.Addition(years: years);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Plus(Period.FromYears(years));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AddOneYearSpecifikYear() {
                const int years = 1;
                var result = DateFactory.Addition(years: years, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(2001, 1, 1);
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AddHundredYears() {
                const int years = 100;
                var result = DateFactory.Addition(years: years);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Plus(Period.FromYears(years));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AddHundredYearsSpecifikYear() {
                const int years = 100;
                var result = DateFactory.Addition(years: years, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(2100, 1, 1);
                Assert.AreEqual(result, expected);
            }

            #endregion

            #region Addition Months

            [Test]
            public void AddtionOneMonth() {
                const int months = 1;
                var result = DateFactory.Addition(month: months);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.PlusMonths(months);
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AdditionHundredMonths() {
                const int months = 100;
                var result = DateFactory.Addition(month: months);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.PlusMonths(months);
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AdditionSpecifikDateOneMonth() {
                const int month = 1;
                var result = DateFactory.Addition(month: month, localDate: new LocalDate(2000, month, month));
                var expected = new LocalDate(2000, 2, month);
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AdditionSpecifkDateHundredMonths() {
                var result = DateFactory.Addition(month: 100, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(2000, 1, 1).PlusMonths(100);
                Assert.AreEqual(result, expected);
            }

            #endregion

            #region Addtion Days

            [Test]
            public void AdditionOneDay() {
                const int days = 1;
                var result = DateFactory.Addition(days: days);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Plus(Period.FromDays(days));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AdditionSpecifkDateHundredDays() {
                const int days = 100;
                var result = DateFactory.Addition(days: days, localDate: new LocalDate(2000, 1, 1));
                var expected = new LocalDate(2000, 1, 1).Plus(Period.FromDays(days));
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AdditionSpecifkDate1Day() {
                const int month = 1;
                var result = DateFactory.Addition(days: month, localDate: new LocalDate(2000, month, month));
                var expected = new LocalDate(2000, month, 2);
                Assert.AreEqual(result, expected);
            }

            [Test]
            public void AdditionHundredDays() {
                const int days = 100;
                var result = DateFactory.Addition(days: days);
                var expected =
                    SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                        .Date.Plus(Period.FromDays(days));
                Assert.AreEqual(result, expected);
            }

            #endregion
        }
    }
}