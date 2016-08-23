using System.Collections.Generic;
using System.Linq;
using DataGen.Types.CountryCode;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class CountryCodeTest {
        [Test]
        public void CreateRandomNumber_Args_Length5_PreNumber35() {
            var countryCode = new CountryCode("foo", "+20");
            var randomNumber = countryCode.CreateRandomNumber(5, "35");
            const int exepectedLength = 10;
            Assert.AreEqual(exepectedLength, randomNumber.Length);
            Assert.IsTrue(randomNumber.Contains("+2035"));
        }

        [Test]
        public void CreateRandomNumber_Args_MinLength5_MaxLength10_PreNumber35() {
            var countryCode = new CountryCode("foo", "+20");
            var randomNumber = countryCode.CreateRandomNumber(5, 10, "35");
            IEnumerable<int> possibleLength = new[] { 10, 11, 12, 13, 14, 15 };
            Assert.IsTrue(possibleLength.Any(i => i == randomNumber.Length));
            Assert.IsTrue(randomNumber.Contains("+2035"));
        }
    }
}