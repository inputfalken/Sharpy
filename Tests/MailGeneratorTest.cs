using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.Mail;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests {
    [TestFixture]
    public class MailGeneratorTest {
        #region Mail With One String

        [Test]
        public void Mail_OneDomain_null_CalledOneTime() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            Assert.Throws<NullReferenceException>(() => mailGenerator.Mail(null));
        }

        [Test]
        public void Mail_OneDomain_UniqueFalse_CheckLowerCase() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, false);
            var mail = mailGenerator.Mail("bob");
            Assert.IsTrue(mail.All(c => !char.IsUpper(c)));
        }

        [Test]
        public void Mail_OneDomain_UniqueTrue_CheckLowerCase() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            var mail = mailGenerator.Mail("bob");
            Assert.IsTrue(mail.All(c => !char.IsUpper(c)));
        }

        [Test]
        public void Mail_OneDomain_OneString_CalledOneTime() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            const string expected = "bob@test.com";
            var result = mailGenerator.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_OneString_CalledTwoTimes() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            //Should not contain any numbers
            Assert.IsTrue(mailGenerator.Mail("bob").Any(c => !char.IsDigit(c)));
            //Should contain a number since all possible combinations have been used
            Assert.IsTrue(mailGenerator.Mail("bob").Any(char.IsDigit));
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledOneTime() {
            var mailGenerator = new MailGenerator(new[] { "test.com", "foo.com" }, Random, true);
            const string expected = "bob@test.com";
            var result = mailGenerator.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledTwoTimes() {
            var mailGenerator = new MailGenerator(new[] { "test.com", "foo.com" }, Random, true);
            const string expected = "bob@foo.com";
            mailGenerator.Mail("bob");
            var result = mailGenerator.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledThreeTimes() {
            var mailGenerator = new MailGenerator(new[] { "test.com", "foo.com" }, Random, true);
            Assert.IsTrue(mailGenerator.Mail("bob").Any(c => !char.IsDigit(c)));
            Assert.IsTrue(mailGenerator.Mail("bob").Any(c => !char.IsDigit(c)));
            // All possible combinations have been used now needs a number
            Assert.IsTrue(mailGenerator.Mail("bob").Any(char.IsDigit));
        }

        #endregion

        #region Mail With Two Strings

        [Test]
        public void Mail_OneDomain_TwoStrings_SecondNull() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            var result = mailGenerator.Mail("bob", null);
            const string expected = "bob@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_FirstNull() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            Assert.Throws<NullReferenceException>(() => mailGenerator.Mail(null, "bob"));
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledOneTime() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            const string expected = "bob.cool@test.com";
            var result = mailGenerator.Mail("bob", "cool");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledTwoTimes() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            mailGenerator.Mail("bob", "cool");
            var result = mailGenerator.Mail("bob", "cool");
            const string expected = "bob_cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledThreeTimes() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            mailGenerator.Mail("bob", "cool");
            mailGenerator.Mail("bob", "cool");
            var result = mailGenerator.Mail("bob", "cool");
            const string expected = "bob-cool@test.com";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_TwoStrings_CalledFourTimes() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            Assert.IsTrue(mailGenerator.Mail("bob", "cool").Any(c => !char.IsDigit(c)));
            Assert.IsTrue(mailGenerator.Mail("bob", "cool").Any(c => !char.IsDigit(c)));
            Assert.IsTrue(mailGenerator.Mail("bob", "cool").Any(c => !char.IsDigit(c)));
            // All combinations have been reached now needs a number
            Assert.IsTrue(mailGenerator.Mail("bob", "cool").Any(char.IsDigit));
        }

        #endregion

        #region Find Duplicates

        [Test]
        public void Mail_OneDomain_TwoStrings_NoDuplicates() {
            var mailGenerator = new MailGenerator(new[] { "test.com" }, Random, true);
            var mails = Enumerable.Range(1, 3).Select(i => mailGenerator.Mail("john", "doe"));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_TwoDomain_TwoStrings_NoDuplicates() {
            var mailGenerator = new MailGenerator(new[] { "test.com", "test2.com" }, Random, true);
            var mails = Enumerable.Range(1, 6).Select(i => mailGenerator.Mail("john", "doe"));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_ThreeDomain_TwoStrings_NoDuplicates() {
            var mailGenerator = new MailGenerator(new[] { "test.com", "test2.com", "test3.com" }, Random, true);
            var mails = Enumerable.Range(1, 9).Select(i => mailGenerator.Mail("john", "doe"));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_FourDomain_TwoStrings_NoDuplicates() {
            var mailGenerator = new MailGenerator(new[] { "test.com", "test2.com", "test3.com", "test4.com" }, Random,
                true);
            var mails = Enumerable.Range(1, 12).Select(i => mailGenerator.Mail("john", "doe"));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        #endregion

        private static readonly Random Random = new Random();

        private static List<string> FindDuplicates(IEnumerable<string> enumerable) {
            return enumerable.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();
        }
    }
}