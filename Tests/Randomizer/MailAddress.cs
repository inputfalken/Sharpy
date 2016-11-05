﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.source {
    [TestFixture]
    public class MailAddress {
        private const string MailUserName = "mailUserName";

        private static List<string> FindDuplicates(IEnumerable<string> enumerable) {
            return enumerable.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();
        }

        [Test]
        public void FourDomain_TwoArgs_NoDuplicates() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "test2.com", "test3.com", "test4.com"}, true);
            var mails = randomGenerator.GenerateMany(source => source.MailAddress("john", "doe"), 12);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_OneDomain_null_CalledOneTime() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(source => source.MailAddress(null)));
        }

        [Test]
        public void Mail_OneDomain_OneString_CalledOneTime() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);

            const string expected = "bob@test.com";
            var result = randomGenerator.Generate(source => source.MailAddress("bob"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_OneString_CalledTwoTimes() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            //Should not contain any numbers
            Assert.IsTrue(randomGenerator.Generate(source => source.MailAddress("bob")).Any(c => !char.IsDigit(c)));
            //Should contain a number since all possible combinations have been used
            Assert.IsTrue(randomGenerator.Generate(source => source.MailAddress("bob")).Any(char.IsDigit));
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledOneTime() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            const string expected = "bob.cool@test.com";
            var result = randomGenerator.Generate(source => source.MailAddress("bob", "cool"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledThreeTimes() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            var generateMany = randomGenerator.GenerateMany(source => source.MailAddress("bob", "cool"), 3);
            var result = generateMany.Last();
            const string expected = "bob-cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledTwoTimes() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            var generate = randomGenerator.GenerateMany(source => source.MailAddress("bob", "cool"), 2);
            var result = generate.Last();
            const string expected = "bob_cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_FirstNull() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(source => source.MailAddress(null, "bob")));
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_NoDuplicates() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            var generateMany = randomGenerator.GenerateMany(source => source.MailAddress("john", "doe"));
            Assert.IsTrue(FindDuplicates(generateMany).Count == 0);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_SecondNull() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            var result = randomGenerator.Generate(source => source.MailAddress("bob", null));
            const string expected = "bob@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_UniqueFalse_CheckLowerCase() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            var mail = randomGenerator.Generate(source => source.MailAddress("bob"));
            Assert.IsTrue(mail.All(c => !char.IsUpper(c)));
        }

        [Test]
        public void Mail_OneDomain_UniqueTrue_CheckLowerCase() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);
            var mail = randomGenerator.Generate(source => source.MailAddress("bob"));
            Assert.IsTrue(mail.All(c => !char.IsUpper(c)));
        }

        [Test]
        public void Mail_ThreeDomain_TwoStrings_NoDuplicates() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "test2.com", "test3.com"}, true);
            var mails = randomGenerator.GenerateMany(source => source.MailAddress("john", "doe"), 9);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledOneTime() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "foo.com"}, true);
            const string expected = "bob@test.com";
            var result = randomGenerator.Generate(source => source.MailAddress("bob"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledThreeTimes() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "foo.com"}, true);
            Assert.IsFalse(randomGenerator.Generate(source => source.MailAddress("bob")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(source => source.MailAddress("bob")).Any(char.IsDigit));
            // All possible combinations have been used now needs a number
            Assert.IsTrue(randomGenerator.Generate(source => source.MailAddress("bob")).Any(char.IsDigit));
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledTwoTimes() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "foo.com"}, true);
            const string expected = "bob@foo.com";
            var generateMany = randomGenerator.GenerateMany(source => source.MailAddress("bob"), 2);

            var result = generateMany.Last();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_TwoDomain_TwoStrings_NoDuplicates() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "test2.com"}, true);
            var mails = randomGenerator.GenerateMany(source => source.MailAddress("john", "doe"), 6);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_TwoStrings_CalledFourTimes() {
            var randomGenerator = SharpyGenerator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"}, true);


            Assert.IsFalse(randomGenerator.Generate(source => source.MailAddress("bob", "cool")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(source => source.MailAddress("bob", "cool")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(source => source.MailAddress("bob", "cool")).Any(char.IsDigit));
            // All combinations have been reached now needs a number
            Assert.IsTrue(randomGenerator.Generate(source => source.MailAddress("bob", "cool")).Any(char.IsDigit));
        }


        [Test]
        public void MailAddressesAreUnique() {
            var mailGenerator = SharpyGenerator.Create();
            mailGenerator.Config.MailGenerator(new List<string> {"gmail.com"}, true);

            // Should be true since mailgenerator has been configured to produce unique mails.
            Assert.IsTrue(
                mailGenerator.GenerateMany(sourcer => sourcer.MailAddress(MailUserName), 100)
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void MailsAreNotnull() {
            var generator = SharpyGenerator.Create();
            //Many
            var mails = generator.GenerateMany(sourcer => sourcer.MailAddress(MailUserName), 20).ToArray();
            Assert.IsFalse(mails.All(string.IsNullOrEmpty));
            Assert.IsFalse(mails.All(string.IsNullOrWhiteSpace));

            //Single
            var masil = generator.Generate(sourcer => sourcer.MailAddress(MailUserName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(masil));
            Assert.IsFalse(string.IsNullOrEmpty(masil));
        }
    }
}