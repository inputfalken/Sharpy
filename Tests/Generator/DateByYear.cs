using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class DateByYear {
        [Test]
        public void DateByYearTwoThousand() {
            var randomGenerator = RandomGenerator.Create();
            var localDate = randomGenerator.Generate(randomize => randomize.DateByYear(2000));
            Assert.AreEqual(2000, localDate.Year);
        }
    }
}