using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
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
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "test2.com", "test3.com", "test4.com"}, true);
            var mails = randomGenerator.GenerateMany(generator => generator.MailAddress("john", "doe"), 12);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void CheckMailCountUniqueTrue() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            var generate = randomGenerator.Generate(generator => generator.MailAddress("hello"));
            Assert.AreEqual(14, generate.Length);
        }

        [Test]
        public void OneDomain_null_CalledOneTime() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null)));
        }

        [Test]
        public void OneDomain_OneString_CalledOneTime_UniqueFalse() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = false;
            var generate = randomGenerator.Generate(generator => generator.MailAddress("hello"));
            Assert.AreEqual("hello@test.com", generate);
        }

        [Test]
        public void OneDomain_OneString_CalledOneTime() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;

            const string expected = "bob@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void OneDomain_OneString_CalledTwoTimes() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            //Should not contain any numbers
            Assert.IsTrue(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(c => !char.IsDigit(c)));
            //Should contain a number since all possible combinations have been used
            Assert.IsTrue(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(char.IsDigit));
        }

        [Test]
        public void OneDomain_TwoStrings_CalledOneTime() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            const string expected = "bob.cool@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob", "cool"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void OneDomain_TwoStrings_CalledThreeTimes() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            var generateMany = randomGenerator.GenerateMany(generator => generator.MailAddress("bob", "cool"), 3);
            var result = generateMany.Last();
            const string expected = "bob-cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void OneDomain_TwoStrings_CalledTwoTimes() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            var generate = randomGenerator.GenerateMany(generator => generator.MailAddress("bob", "cool"), 2);
            var result = generate.Last();
            const string expected = "bob_cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void OneDomain_TwoStrings_FirstNull() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null, "bob")));
        }

        [Test]
        public void OneDomain_TwoStrings_NoDuplicates() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            var generateMany = randomGenerator.GenerateMany(generator => generator.MailAddress("john", "doe"), 30);
            Assert.IsTrue(FindDuplicates(generateMany).Count == 0);
        }

        [Test]
        public void OneDomain_TwoStrings_SecondNull() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob", null));
            const string expected = "bob@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void OneDomain_UniqueFalse_CheckLowerCase() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            var mail = randomGenerator.Generate(generator => generator.MailAddress("Bob"));
            Assert.IsTrue(mail.All(c => !char.IsUpper(c)));
        }

        [Test]
        public void OneDomain_UniqueTrue_CheckLowerCase() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            var mail = randomGenerator.Generate(generator => generator.MailAddress("Bob"));
            Assert.IsTrue(mail.All(c => !char.IsUpper(c)));
        }

        [Test]
        public void ThreeDomain_TwoStrings_NoDuplicates() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "test2.com", "test3.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            var mails = randomGenerator.GenerateMany(generator => generator.MailAddress("john", "doe"), 9);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void TwoDomain_OneString_CalledOneTime() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "foo.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            const string expected = "bob@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TwoDomain_OneString_CalledThreeTimes() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "foo.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(char.IsDigit));
            // All possible combinations have been used now needs a number
            Assert.IsTrue(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(char.IsDigit));
        }

        [Test]
        public void TwoDomain_OneString_CalledTwoTimes() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "foo.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            const string expected = "bob@foo.com";
            var generateMany = randomGenerator.GenerateMany(generator => generator.MailAddress("bob"), 2);

            var result = generateMany.Last();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TwoDomain_TwoStrings_NoDuplicates() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com", "test2.com"});
            randomGenerator.Config.UniqueMailAddresses = true;
            var mails = randomGenerator.GenerateMany(generator => generator.MailAddress("john", "doe"), 6);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void TwoStrings_CalledFourTimes() {
            var randomGenerator = Sharpy.Generator.Create();
            randomGenerator.Config.MailGenerator(new[] {"test.com"});
            randomGenerator.Config.UniqueMailAddresses = true;


            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Any(char.IsDigit));
            // All combinations have been reached now needs a number
            Assert.IsTrue(randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Any(char.IsDigit));
        }


        [Test]
        public void MailAddressesAreUnique() {
            var mailGenerator = Sharpy.Generator.Create();
            mailGenerator.Config.MailGenerator(new List<string> {"gmail.com"});
            mailGenerator.Config.UniqueMailAddresses = true;

            // Should be true since mailgenerator has been configured to produce unique mails.
            Assert.IsTrue(
                mailGenerator.GenerateMany(generatorr => generatorr.MailAddress(MailUserName), 100)
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void MailsAreNotnull() {
            var generator = Sharpy.Generator.Create();
            //Many
            var mails = generator.GenerateMany(generatorr => generatorr.MailAddress(MailUserName), 20).ToArray();
            Assert.IsFalse(mails.All(string.IsNullOrEmpty));
            Assert.IsFalse(mails.All(string.IsNullOrWhiteSpace));

            //Single
            var masil = generator.Generate(generatorr => generatorr.MailAddress(MailUserName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(masil));
            Assert.IsFalse(string.IsNullOrEmpty(masil));
        }
    }
}