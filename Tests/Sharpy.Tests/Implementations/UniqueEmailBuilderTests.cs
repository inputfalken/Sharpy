using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Implementation;

namespace Tests.Sharpy.Tests.Implementations {
    [TestFixture]
    public class UniqueEmailBuilderTests {
        private const string MailUserName = "test";
        private const int Amount = 1000;

        private static List<string> FindDuplicates(IEnumerable<string> enumerable) {
            return enumerable.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();
        }

        [Test]
        [Repeat(Amount)]
        public void Mail_No_Arg_Does_Not_start_With_AT_One_Domain() {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {"hotmail.com"}, new Random());
            var mail = uniqueEmailBuilder.Mail();
            Assert.IsTrue(mail.IndexOf('@') > 1);
        }

        [Test]
        public void Mail_With_Arg_Append_Number_If_Mail_Is_Duplicated_One_Domain() {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {"hotmail.com"}, new Random());
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            for (var i = 0; i < Amount; i++) Assert.IsTrue(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
        }

        [Test]
        public void Mail_With_Arg_Append_Number_If_Mail_Is_Duplicated_Three_Domain() {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {"hotmail.com", "google.com", "yahoo.com"}, new Random());
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            for (var i = 0; i < Amount; i++) Assert.IsTrue(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
        }

        [Test]
        public void Mail_With_Arg_Append_Number_If_Mail_Is_Duplicated_Two_Domain() {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {"hotmail.com", "google.com"}, new Random());
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            for (var i = 0; i < Amount; i++) Assert.IsTrue(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
        }

        [Test]
        [Repeat(Amount)]
        public void Mail_With_Arg_Does_Not_start_With_AT_One_Domain() {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {"hotmail.com"}, new Random());
            var mail = uniqueEmailBuilder.Mail(MailUserName);
            Assert.IsTrue(mail.IndexOf('@') > 1);
        }
    }
}