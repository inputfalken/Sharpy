using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Randomize {
    [TestFixture]
    public class MailAddress {
        private const string MailUserName = "mailUserName";

        [Test]
        [Repeat(10)]
        public void MailAddressesAreUnique() {
            var mailGenerator = RandomGenerator.Create();
            mailGenerator.Config.MailGenerator(new List<string> {"gmail.com"}, true);

            // Should be true since mailgenerator has been configured to produce unique mails.
            Assert.IsTrue(
                mailGenerator.GenerateMany(randomizer => randomizer.MailAddress(MailUserName), 100)
                    .GroupBy(s => s)
                    .All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void MailsAreNotnull() {
            var generator = RandomGenerator.Create();
            //Many
            var mails = generator.GenerateMany(randomizer => randomizer.MailAddress(MailUserName), 20).ToArray();
            Assert.IsFalse(mails.All(string.IsNullOrEmpty));
            Assert.IsFalse(mails.All(string.IsNullOrWhiteSpace));

            //Single
            var masil = generator.Generate(randomizer => randomizer.MailAddress(MailUserName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(masil));
            Assert.IsFalse(string.IsNullOrEmpty(masil));
        }
    }
}