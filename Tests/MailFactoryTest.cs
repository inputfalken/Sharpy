using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.Mail;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests {
    [TestFixture]
    public class MailFactoryTest {
        [Test]
        public void MailFactory_OneDomain_Mail_NoString_CalledOneTime() {
            Assert.Throws<ArgumentException>(() => new MailFactory("test.com").Mail());
        }

        [Test]
        public void MailFactory_OneDomain_Mail_EmptyString_CalledOneTime() {
            Assert.Throws<ArgumentException>(() => new MailFactory("").Mail());
        }

        [Test]
        public void MailFactory_OneDomain_Mail_Null_CalledOneTime() {
            Assert.Throws<ArgumentNullException>(() => new MailFactory(null).Mail());
        }

        [Test]
        public void MailFactory_OneDomain_Mail_OneStrings_CalledOneTime() {
            var mailFactory = new MailFactory("test.com");
            const string expected = "bob@test.com";
            var result = mailFactory.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MailFactory_OneDomain_Mail_OneStrings_CalledTwoTime() {
            var mailFactory = new MailFactory("test.com");
            mailFactory.Mail("bob");
            Assert.Throws<Exception>(() => mailFactory.Mail("bob"));
        }

        [Test]
        public void MailFactory_OneDomain_Mail_TwoStrings_CalledOneTime() {
            var mailFactory = new MailFactory("test.com");
            const string expected = "bob.cool@test.com";
            var result = mailFactory.Mail("bob", "cool");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MailFactory_OneDomain_Mail_TwoStrings_CalledTwoTime() {
            var mailFactory = new MailFactory("test.com");
            mailFactory.Mail("bob", "cool");
            var result = mailFactory.Mail("bob", "cool");
            const string expected = "bob_cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MailFactory_OneDomain_Mail_TwoStrings_CalledThreeTime() {
            var mailFactory = new MailFactory("test.com");
            mailFactory.Mail("bob", "cool");
            mailFactory.Mail("bob", "cool");
            var result = mailFactory.Mail("bob", "cool");
            const string expected = "bob-cool@test.com";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MailFactoryOneDomain_Mail_TwoStrings_CalledFourTime() {
            var mailFactory = new MailFactory("test.com");
            mailFactory.Mail("bob", "cool");
            mailFactory.Mail("bob", "cool");
            mailFactory.Mail("bob", "cool");
            Assert.Throws<Exception>(() => mailFactory.Mail("bob", "cool"));
        }

        [Test]
        public void MailFactory_TwoDomain_Mail_OneString_CalledOneTime() {
            var mailFactory = new MailFactory("test.com", "foo.com");
            const string expected = "bob@test.com";
            var result = mailFactory.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MailFactory_TwoDomain_Mail_OneString_CalledTwoTime() {
            var mailFactory = new MailFactory("test.com", "foo.com");
            const string expected = "bob@foo.com";
            mailFactory.Mail("bob");
            var result = mailFactory.Mail("bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MailFactory_TwoDomain_Mail_OneString_CalledThreeTime() {
            var mailFactory = new MailFactory("test.com", "foo.com");
            mailFactory.Mail("bob");
            mailFactory.Mail("bob");
            Assert.Throws<Exception>(() => mailFactory.Mail("bob"));
        }
    }
}