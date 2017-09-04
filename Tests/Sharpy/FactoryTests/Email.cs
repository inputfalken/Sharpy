using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.FactoryTests {
    [TestFixture]
    internal class Email {
        [Test]
        public void No_Args_Randomizes_Various_Domains() {
            var mails = Factory
                .Email()
                .Select(s => s.Split('@'))
                .Take(50)
                .Select(strings => strings.Last())
                .ToArray();

            Assert.IsFalse(mails.Skip(1).Zip(mails, (s, s1) => s == s1).All(b => b));
        }

        [Test]
        public void No_Args_Randomizes_Various_Names() {
            var mails = Factory
                .Email()
                .Select(s => s.Split('@'))
                .Take(50)
                .Select(strings => strings.First())
                .ToArray();

            Assert.IsFalse(mails.Skip(1).Zip(mails, (s, s1) => s == s1).All(b => b));
        }

        [Test]
        public void Supplied_Randomizer_With_Same_Seed_Are_Equal() {
            const int seed = 20;
            const int count = 200;
            var mails1 = Factory.Email(random: new Random(seed)).Take(count);
            var mails2 = Factory.Email(random: new Random(seed)).Take(count);
            Assert.AreEqual(mails1, mails2);
        }

        [Test]
        public void Supplied_Randomizer_With_Different_Seed_Are_Equal() {
            const int count = 200;
            var mails1 = Factory.Email(random: new Random(20)).Take(count);
            var mails2 = Factory.Email(random: new Random(30)).Take(count);
            Assert.AreNotEqual(mails1, mails2);
        }

        [Test]
        public void Supplied_Domains_Is_Only_Used() {
            const string gmailCom = "gmail.com";
            var res = Factory.Email(new[] {gmailCom})
                .Select(s => s.Split('@')[1])
                .Take(200)
                .All(s => s == gmailCom);
            Assert.IsTrue(res);
        }
        [Test]
        public void No_Supplied_Domains_Use_Random_Domain() {
            const string gmailCom = "gmail.com";
            var res = Factory.Email()
                .Select(s => s.Split('@')[1])
                .Take(200)
                .All(s => s == gmailCom);
            Assert.IsFalse(res);
        }
    }
}