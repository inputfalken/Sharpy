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
            var result = DateFactory.Date(new LocalDate(day, month, year));
            Assert.AreEqual(currentLocalDate.Day - day, result.Day);
            Assert.AreEqual(currentLocalDate.Year - year, result.Year);
            Assert.AreEqual(currentLocalDate.Month - month, result.Month);
        }

        [Test]
        public void CreateDate_Arg_Subtract_False() {
            var currentLocalDate = DateFactory.CurrentLocalDate;
            const int day = 1;
            const int year = 1;
            const int month = 2;
            var result = DateFactory.Date(new LocalDate(day, month, year), false);
            Assert.AreEqual(currentLocalDate.Day + day, result.Day);
            Assert.AreEqual(currentLocalDate.Year + year, result.Year);
            Assert.AreEqual(currentLocalDate.Month + month, result.Month);
        }
    }
}