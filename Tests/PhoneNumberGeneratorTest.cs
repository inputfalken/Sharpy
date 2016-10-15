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
            const int expectedLength = 7;
            Assert.AreEqual(expectedLength, randomNumber.Length);
            Assert.IsTrue(randomNumber.Contains("35"));
        }

        [Test]
        public void CreateRandomNumber_Args_Length6_PreNumber35() {
            var phoneGenerator = new PhoneNumberGenerator(new Random(), 6, "35");
            var randomNumber = phoneGenerator.RandomNumber();
            const int expectedLength = 8;
            Assert.AreEqual(expectedLength, randomNumber.Length);
            Assert.IsTrue(randomNumber.Contains("35"));
        }

        [Test]
        public void CreateRandomNumber_Args_Length5_PreNumber37() {
            var phoneGenerator = new PhoneNumberGenerator(new Random(), 5, "37");
            var randomNumber = phoneGenerator.RandomNumber();
            const int expectedLength = 7;
            Assert.AreEqual(expectedLength, randomNumber.Length);
            Assert.IsTrue(randomNumber.Contains("37"));
        }

        [Test]
        public void CreateRandomNumber_Args_Length5_PreNumber30_UniqueTrue() {
            var phoneNumberGenerator = new PhoneNumberGenerator(new Random(), 4, "30", true);
            //Runs the randomnumber 1000 times and checks if any number is ever repeated
            Assert.IsTrue(Enumerable.Range(0, 1000)
                    .Select(i => phoneNumberGenerator.RandomNumber())
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1)
            );
        }

        [Test]
        public void CreateRandomNumber_Args_Length5_PreNumber30_UniqueFalse() {
            var phoneNumberGenerator = new PhoneNumberGenerator(new Random(), 4, "30", false);
            //Runs the randomnumber 1000 times and checks if any number is ever repeated
            Assert.IsFalse(Enumerable.Range(0, 1000)
                    .Select(i => phoneNumberGenerator.RandomNumber())
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1)
            );
        }
    }
}