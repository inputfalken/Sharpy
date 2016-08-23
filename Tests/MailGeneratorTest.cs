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
            var mailGenerator = new MailGenerator("test.com");
            Assert.Throws<NullReferenceException>(() => mailGenerator.Mail(null));
        }

        [Test]
        public void Mail_OneDomain_OneString_CalledOneTime() {
            var mailGenerator = new MailGenerator("test.com");
            const string expected = "bob@test.com";
            var result = mailGenerator.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_OneString_CalledTwoTimes() {
            var mailGenerator = new MailGenerator("test.com");
            mailGenerator.Mail("bob");
            Assert.Throws<Exception>(() => mailGenerator.Mail("bob"));
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledOneTime() {
            var mailGenerator = new MailGenerator("test.com", "foo.com");
            const string expected = "bob@test.com";
            var result = mailGenerator.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledTwoTimes() {
            var mailGenerator = new MailGenerator("test.com", "foo.com");
            const string expected = "bob@foo.com";
            mailGenerator.Mail("bob");
            var result = mailGenerator.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_TwoDomain_OneString_CalledThreeTimes() {
            var mailGenerator = new MailGenerator("test.com", "foo.com");
            mailGenerator.Mail("bob");
            mailGenerator.Mail("bob");
            Assert.Throws<Exception>(() => mailGenerator.Mail("bob"));
        }

        #endregion

        #region Mail With Two Strings

        [Test]
        public void Mail_OneDomain_TwoStrings_SecondNull() {
            var mailGenerator = new MailGenerator("test.com");
            var result = mailGenerator.Mail("bob", null);
            const string expected = "bob@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_FirstNull() {
            var mailGenerator = new MailGenerator("test.com");
            Assert.Throws<NullReferenceException>(() => mailGenerator.Mail(null, "bob"));
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledOneTime() {
            var mailGenerator = new MailGenerator("test.com");
            const string expected = "bob.cool@test.com";
            var result = mailGenerator.Mail("bob", "cool");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledTwoTimes() {
            var mailGenerator = new MailGenerator("test.com");
            mailGenerator.Mail("bob", "cool");
            var result = mailGenerator.Mail("bob", "cool");
            const string expected = "bob_cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_OneDomain_TwoStrings_CalledThreeTimes() {
            var mailGenerator = new MailGenerator("test.com");
            mailGenerator.Mail("bob", "cool");
            mailGenerator.Mail("bob", "cool");
            var result = mailGenerator.Mail("bob", "cool");
            const string expected = "bob-cool@test.com";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Mail_TwoStrings_CalledFourTimes() {
            var mailGenerator = new MailGenerator("test.com");
            mailGenerator.Mail("bob", "cool");
            mailGenerator.Mail("bob", "cool");
            mailGenerator.Mail("bob", "cool");
            Assert.Throws<Exception>(() => mailGenerator.Mail("bob", "cool"));
        }

        #endregion

        #region Find Duplicates

        [Test]
        public void Mail_OneDomain_TwoStrings_NoDuplicates() {
            var mailGenerator = new MailGenerator("test.com");
            var mails = Enumerable.Range(1, 3).Select(i => mailGenerator.Mail("john", "doe"));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_TwoDomain_TwoStrings_NoDuplicates() {
            var mailGenerator = new MailGenerator("test.com", "test2.com");
            var mails = Enumerable.Range(1, 6).Select(i => mailGenerator.Mail("john", "doe"));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_ThreeDomain_TwoStrings_NoDuplicates() {
            var mailGenerator = new MailGenerator("test.com", "test2.com", "test3.com");
            var mails = Enumerable.Range(1, 9).Select(i => mailGenerator.Mail("john", "doe"));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_FourDomain_TwoStrings_NoDuplicates() {
            var mailGenerator = new MailGenerator("test.com", "test2.com", "test3.com", "test4.com");
            var mails = Enumerable.Range(1, 12).Select(i => mailGenerator.Mail("john", "doe"));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        #endregion

        private static List<string> FindDuplicates(IEnumerable<string> enumerable) {
            return enumerable.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();
        }
    }
}