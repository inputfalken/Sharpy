using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class UniqueEmailBuilderTests
    {
        private const string MailUserName = "test";
        private const string GMail = "gmail.com";
        private const string HotMail = "hotmail.com";
        private const string Yahoo = "yahoo.com";

        private static List<string> FindDuplicates(IEnumerable<string> enumerable)
        {
            return enumerable.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();
        }

        [Test]
        public void One_Arg_One_Domain_Produces_Valid_Emails()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.DoesNotThrow(() =>
                {
                    var res = new MailAddress(uniqueEmailBuilder.Mail(MailUserName));
                    Assert.AreEqual(GMail, res.Host);
                });
        }

        [Test]
        public void Two_Arg_One_Domain_Produces_Valid_Emails()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.DoesNotThrow(() =>
                {
                    var res = new MailAddress(uniqueEmailBuilder.Mail(MailUserName, "foo"));
                    Assert.AreEqual(GMail, res.Host);
                });
        }

        [Test]
        public void Three_Arg_One_Domain_Produces_Valid_Emails()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.DoesNotThrow(() =>
                {
                    var res = new MailAddress(uniqueEmailBuilder.Mail(MailUserName, "foo", "bar"));
                    Assert.AreEqual(GMail, res.Host);
                });
        }

        [Test]
        public void Four_Arg_One_Domain_Produces_Valid_Emails()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.DoesNotThrow(() =>
                {
                    var res = new MailAddress(uniqueEmailBuilder.Mail(MailUserName, "foo", "bar", "john"));
                    Assert.AreEqual(GMail, res.Host);
                });
        }

        [Test]
        public void Five_Arg_One_Domain_Produces_Valid_Emails()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.DoesNotThrow(() =>
                {
                    var res = new MailAddress(uniqueEmailBuilder.Mail(MailUserName, "foo", "bar", "john", "doe"));
                    Assert.AreEqual(GMail, res.Host);
                });
        }

        [Test]
        public void Six_Arg_One_Domain_Produces_Valid_Emails()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.DoesNotThrow(() =>
                {
                    var res = new MailAddress(
                        uniqueEmailBuilder.Mail(MailUserName, "foo", "bar", "john", "doe", "test"));
                    Assert.AreEqual(GMail, res.Host);
                });
        }

        [Test]
        public void Seven_Arg_One_Domain_Produces_Valid_Emails()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.DoesNotThrow(() =>
                {
                    var res = new MailAddress(uniqueEmailBuilder.Mail(MailUserName, "foo", "bar", "john", "doe", "test",
                        "testi"));
                    Assert.AreEqual(GMail, res.Host);
                });
        }

        [Test]
        public void One_Arg_Acts_Same_As_Array_OverLoad()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            var uniqueEmailBuilder2 = new UniqueEmailBuilder(new[] {GMail});

            var first = MailUserName + 1;
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(
                    uniqueEmailBuilder.Mail(first),
                    uniqueEmailBuilder2.Mail(new[] {first})
                );
        }

        [Test]
        public void Two_Arg_Acts_Same_As_Array_OverLoad()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            var uniqueEmailBuilder2 = new UniqueEmailBuilder(new[] {GMail});

            var first = MailUserName + 1;
            var second = MailUserName + 2;
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(
                    uniqueEmailBuilder.Mail(first, second),
                    uniqueEmailBuilder2.Mail(new[] {first, second})
                );
        }

        [Test]
        public void Three_Arg_Acts_Same_As_Array_OverLoad()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            var uniqueEmailBuilder2 = new UniqueEmailBuilder(new[] {GMail});

            var first = MailUserName + 1;
            var second = MailUserName + 2;
            var third = MailUserName + 3;
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(
                    uniqueEmailBuilder.Mail(first, second, third),
                    uniqueEmailBuilder2.Mail(new[] {first, second, third})
                );
        }

        [Test]
        public void Four_Arg_Acts_Same_As_Array_OverLoad()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            var uniqueEmailBuilder2 = new UniqueEmailBuilder(new[] {GMail});

            var first = MailUserName + 1;
            var second = MailUserName + 2;
            var third = MailUserName + 3;
            var fourth = MailUserName + 4;
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(
                    uniqueEmailBuilder.Mail(first, second, third, fourth),
                    uniqueEmailBuilder2.Mail(new[] {first, second, third, fourth})
                );
        }

        [Test]
        public void Five_Arg_Acts_Same_As_Array_OverLoad()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            var uniqueEmailBuilder2 = new UniqueEmailBuilder(new[] {GMail});

            var first = MailUserName + 1;
            var second = MailUserName + 2;
            var third = MailUserName + 3;
            var fourth = MailUserName + 4;
            var fifth = MailUserName + 5;
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(
                    uniqueEmailBuilder.Mail(first, second, third, fourth, fifth),
                    uniqueEmailBuilder2.Mail(new[] {first, second, third, fourth, fifth})
                );
        }

        [Test]
        public void Empty_Array_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new UniqueEmailBuilder(new[] {GMail}).Mail(Array.Empty<string>())
            );
        }

        [Test]
        public void Six_Arg_Acts_Same_As_Array_OverLoad()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            var uniqueEmailBuilder2 = new UniqueEmailBuilder(new[] {GMail});

            var first = MailUserName + 1;
            var second = MailUserName + 2;
            var third = MailUserName + 3;
            var fourth = MailUserName + 4;
            var fifth = MailUserName + 5;
            var sixth = MailUserName + 6;
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(
                    uniqueEmailBuilder.Mail(first, second, third, fourth, fifth, sixth),
                    uniqueEmailBuilder2.Mail(new[] {first, second, third, fourth, fifth, sixth})
                );
        }

        [Test]
        public void Seven_Arg_Acts_Same_As_Array_OverLoad()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new[] {GMail});
            var uniqueEmailBuilder2 = new UniqueEmailBuilder(new[] {GMail});

            var first = MailUserName + 1;
            var second = MailUserName + 2;
            var third = MailUserName + 3;
            var fourth = MailUserName + 4;
            var fifth = MailUserName + 5;
            var sixth = MailUserName + 6;
            var seventh = MailUserName + 7;
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.AreEqual(
                    uniqueEmailBuilder.Mail(first, second, third, fourth, fifth, sixth, seventh),
                    uniqueEmailBuilder2.Mail(new[] {first, second, third, fourth, fifth, sixth, seventh})
                );
        }

        [Test]
        public void Mail_With_Arg_Append_Number_If_Mail_Is_Duplicated_One_Domain()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {HotMail});
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.IsTrue(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
        }

        [Test]
        public void Mail_With_Arg_Append_Number_If_Mail_Is_Duplicated_Three_Domain()
        {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {HotMail, GMail, Yahoo});
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.IsTrue(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
        }

        [Test]
        public void Mail_With_Arg_Append_Number_If_Mail_Is_Duplicated_Two_Domain()
        {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {HotMail, GMail});
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            Assert.IsFalse(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
            for (var i = 0; i < Assertion.Amount; i++)
                Assert.IsTrue(uniqueEmailBuilder.Mail(MailUserName).Any(char.IsDigit));
        }

        [Test]
        public void Mail_With_Arg_Does_Not_start_With_AT_One_Domain()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {HotMail});
            var mails = new List<string>();
            for (var i = 0; i < Assertion.Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(mails.All(s => s.IndexOf('@') > 1));
        }

        [Test]
        public void Mail_With_Arg_Is_Unique_One_Domain()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {HotMail});
            var mails = new List<string>();
            for (var i = 0; i < Assertion.Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_With_Arg_Is_Unique_Three_Domain()
        {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {HotMail, GMail, "foo.com"});
            var mails = new List<string>();
            for (var i = 0; i < Assertion.Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_With_Arg_Is_Unique_Two_Domain()
        {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {HotMail, GMail});
            var mails = new List<string>();
            for (var i = 0; i < Assertion.Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Mail_With_Arg_Use_Argument_One_Domain()
        {
            var uniqueEmailBuilder = new UniqueEmailBuilder(new List<string> {HotMail});
            var mails = new List<string>();
            for (var i = 0; i < Assertion.Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(mails.All(s => s.Split('@')[0].StartsWith(MailUserName)));
        }

        [Test]
        public void Mail_With_Arg_Use_Argument_Three_Domain()
        {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {HotMail, GMail, "foo.com"});
            var mails = new List<string>();
            for (var i = 0; i < Assertion.Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(mails.All(s => s.Split('@')[0].StartsWith(MailUserName)));
        }

        [Test]
        public void Mail_With_Arg_Use_Argument_Two_Domain()
        {
            var uniqueEmailBuilder =
                new UniqueEmailBuilder(new List<string> {HotMail, GMail});
            var mails = new List<string>();
            for (var i = 0; i < Assertion.Amount; i++) mails.Add(uniqueEmailBuilder.Mail(MailUserName));
            Assert.IsTrue(mails.All(s => s.Split('@')[0].StartsWith(MailUserName)));
        }
    }
}