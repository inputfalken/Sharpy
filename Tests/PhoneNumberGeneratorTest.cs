using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Types.CountryCode;

namespace Tests {
    [TestFixture]
    public class PhoneNumberGeneratorTest {
        [Test]
        public void CreateRandomNumber_Args_Length5_PreNumber35() {
            var phoneGenerator = new PhoneNumberGenerator("Sweden", "+20", new Random());
            var randomNumber = phoneGenerator.RandomNumber(5, "35");
            const int exepectedLength = 10;
            Assert.AreEqual(exepectedLength, randomNumber.Length);
            Assert.IsTrue(randomNumber.Contains("+2035"));
        }

        [Test]
        public void CreateRandomNumber_Args_MinLength5_MaxLength10_PreNumber35() {
            var phoneGenerator = new PhoneNumberGenerator("Norway", "+20", new Random());
            var randomNumber = phoneGenerator.RandomNumber(5, 10, "35");
            IEnumerable<int> possibleLength = new[] { 10, 11, 12, 13, 14, 15 };
            Assert.IsTrue(possibleLength.Any(i => i == randomNumber.Length));
            Assert.IsTrue(randomNumber.Contains("+2035"));
        }
    }
}