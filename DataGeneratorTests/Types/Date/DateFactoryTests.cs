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
        public void BirthYearTestNegativeValue() {
            var randomBirthDate = DateFactory.RandomBirthDate(-1, -1);
            Assert.IsTrue(randomBirthDate.Year >= DateTime.UtcNow.Year - -1 &&
                          randomBirthDate.Year <= DateTime.UtcNow.Year - -1);
        }

        [TestMethod]
        public void BirthYearTestTwentyToForty() {
            var randomBirthDate = DateFactory.RandomBirthDate(20, 40);
            Assert.IsTrue(randomBirthDate.Year >= DateTime.UtcNow.Year - 40 &&
                          randomBirthDate.Year <= DateTime.UtcNow.Year - 20);
        }


        [TestMethod]
        public void BirthYearTestTwentyToTwenty() {
            var randomBirthDate = DateFactory.RandomBirthDate(20, 20);
            Assert.IsTrue(randomBirthDate.Year >= DateTime.UtcNow.Year - 20 &&
                          randomBirthDate.Year <= DateTime.UtcNow.Year - 20);
        }

        [TestMethod]
        public void BirthYearTestTwoToEight() {
            var randomBirthDate = DateFactory.RandomBirthDate(2, 8);
            Assert.IsTrue(randomBirthDate.Year >= DateTime.UtcNow.Year - 8 &&
                          randomBirthDate.Year <= DateTime.UtcNow.Year - 2);
        }

        [TestMethod]
        public void BirthYearTestOneToThree() {
            var randomBirthDate = DateFactory.RandomBirthDate(1, 3);
            Assert.IsTrue(randomBirthDate.Year >= DateTime.UtcNow.Year - 3 &&
                          randomBirthDate.Year <= DateTime.UtcNow.Year - 1);
        }

        [TestMethod]
        public void BirthYearTestOneToOne() {
            var randomBirthDate = DateFactory.RandomBirthDate(1, 1);
            Assert.IsTrue(randomBirthDate.Year >= DateTime.UtcNow.Year - 1 &&
                          randomBirthDate.Year <= DateTime.UtcNow.Year - 1);
        }


        [TestMethod]
        public void BirthYearTestZeroToZero() {
            var randomBirthDate = DateFactory.RandomBirthDate(0, 0);
            Assert.IsTrue(randomBirthDate.Year >= DateTime.UtcNow.Year &&
                          randomBirthDate.Year <= DateTime.UtcNow.Year);
        }


        [TestMethod]
        public void BirthYearTestZeroToOne() {
            var randomBirthDate = DateFactory.RandomBirthDate(0, 1);
            Assert.IsTrue(randomBirthDate.Year >= DateTime.UtcNow.Year - 1 &&
                          randomBirthDate.Year <= DateTime.UtcNow.Year);
        }

        #endregion

        #region Specifik Birth Year

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BirthYearNegativeValue() {
            var randomBirthDate = DateFactory.RandomBirthDate(-1);
            Assert.IsTrue(randomBirthDate.Year == DateTime.UtcNow.Year - -1);
        }

        [TestMethod]
        public void BirthYearTwentyYear() {
            var randomBirthDate = DateFactory.RandomBirthDate(20);
            Assert.IsTrue(randomBirthDate.Year == DateTime.UtcNow.Year - 20);
        }

        [TestMethod]
        public void BirthYearTenYear() {
            var randomBirthDate = DateFactory.RandomBirthDate(10);
            Assert.IsTrue(randomBirthDate.Year == DateTime.UtcNow.Year - 10);
        }

        [TestMethod]
        public void BirthYearZeroYears() {
            var randomBirthDate = DateFactory.RandomBirthDate(0);
            Assert.IsTrue(randomBirthDate.Year == DateTime.UtcNow.Year);
        }

        #endregion

        #region Future Date

        [TestMethod]
        public void TwoYearsFromNow() {
            var randomFutureDate = DateFactory.RandomFutureDate(2);
            Assert.IsTrue(randomFutureDate.Year == DateTime.Now.Year + 2);
        }

        [TestMethod]
        public void ZeroFromNow() {
            var randomFutureDate = DateFactory.RandomFutureDate(0);
            Assert.IsTrue(randomFutureDate.Year == DateTime.Now.Year + 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeFutureDate() {
            DateFactory.RandomFutureDate(-20);
        }

        #endregion
    }
}