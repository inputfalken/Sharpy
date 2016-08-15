using System;
using System.Collections.Generic;
using DataGen.Types.Mail;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class MailFactoryTest {
        [Test]
        public void MailFactory_NoArg_Mail_OneArg() {
            var mailFactory = new MailFactory();
            var result = mailFactory.Mail("bob");
            var expected = result == "bob@yahoo.com" || result == "bob@gmail.com" || result == "bob@hotmail.com";
            Assert.IsTrue(expected);
        }

        [Test]
        public void MailFactory_TwoArg_Mail_OneArg() {
            var mailFactory = new MailFactory("test.com", "cray.net");
            var result = mailFactory.Mail("bob");
            var expected = result == "bob@test.com" || result == "bob@cray.net";
            Assert.IsTrue(expected);
        }

        [Test]
        public void MailFactory_OneArg_Mail_OneArg() {
            var mailFactory = new MailFactory("test.com");
            var result = mailFactory.Mail("bob");
            Assert.IsTrue(result == "bob@test.com");
        }

        [Test]
        public void MailFactory_OneArg_Mail_TwoArg() {
            var mailFactory = new MailFactory("test.com");
            var result = mailFactory.Mail("bob", "Doe");
            var expected = result == "bob.doe@test.com" || result == "bob-doe@test.com" || result == "bob_doe@test.com";
            Assert.IsTrue(expected);
        }
    }
}