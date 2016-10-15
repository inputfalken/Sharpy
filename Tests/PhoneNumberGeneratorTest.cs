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
            var phoneGenerator = new PhoneNumberGenerator(new Random(), 5, "35");
            var randomNumber = phoneGenerator.RandomNumber();
            const int exepectedLength = 10;
            Assert.AreEqual(exepectedLength, randomNumber.Length);
            Assert.IsTrue(randomNumber.Contains("35"));
        }
    }
}