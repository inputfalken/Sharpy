using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Tests.Sharpy.BuilderTests.Implementations {
    [TestFixture]
    public class UniqueEmailBuilderTests {
        private const string MailUserName = "test";
        private const int Amount = 100000;

        private static List<string> FindDuplicates(IEnumerable<string> enumerable) {
            return enumerable.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();
        }

        [Test]
        public void Mail_No_Arg_Does_Not_start_With_AT_One_Domain() {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {"hotmail.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail());
            Assert.IsTrue(mails.All(s => s.IndexOf('@') > 1));
        }

        [Test]
        public void Mail_No_Arg_Is_Unique_One_Domain() {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {"hotmail.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail());
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_No_Arg_Is_Unique_Three_Domain() {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {"hotmail.com", "gmail.com", "foo.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail());
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_No_Arg_Is_Unique_Two_Domain() {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {"hotmail.com", "gmail.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail());
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
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
        public void Mail_With_Arg_Does_Not_start_With_AT_One_Domain() {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {"hotmail.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(mails.All(s => s.IndexOf('@') > 1));
        }

        [Test]
        public void Mail_With_Arg_Is_Unique_One_Domain() {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {"hotmail.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_With_Arg_Is_Unique_Three_Domain() {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {"hotmail.com", "gmail.com", "foo.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_With_Arg_Is_Unique_Two_Domain() {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {"hotmail.com", "gmail.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_With_Arg_Use_Argument_One_Domain() {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {"hotmail.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(mails.All(s => s.Split('@')[0].StartsWith(MailUserName)));
        }

        [Test]
        public void Mail_With_Arg_Use_Argument_Three_Domain() {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {"hotmail.com", "gmail.com", "foo.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(mails.All(s => s.Split('@')[0].StartsWith(MailUserName)));
        }

        [Test]
        public void Mail_With_Arg_Use_Argument_Two_Domain() {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {"hotmail.com", "gmail.com"}, new Random());
            var mails = new List<string>();
            for (var i = 0; i < Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(mails.All(s => s.Split('@')[0].StartsWith(MailUserName)));
        }
    }
}