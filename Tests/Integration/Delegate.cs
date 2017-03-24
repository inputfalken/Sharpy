using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    public class Delegate {
        private class TestClas {
            public string Text { get; set; }
        }

        [Test]
        public void Bind_Flattens() {
            var generator = new Generator()
                .ToDelegate(g => g.ToDelegate(g2 => g2.Integer()))
                .SelectMany(g => g);
            Assert.IsInstanceOf<Generator<int>>(generator);
        }

        [Test]
        public void Bind_Flattens_And_Combines() {
            var doubledGeneration = new Generator(100)
                .ToDelegate(x => x.Integer(20))
                .GenerateSequence(20)
                .Select(x => x * 2);
            var gen = new Generator(100)
                .ToDelegate(x => x.Integer(20));
            var gen2 = new Generator(100)
                .ToDelegate(x => x.Integer(20));
            var combinedGenerators = gen.SelectMany(i => gen2, (i, i1) => i + i1);

            Assert.IsInstanceOf<Generator<int>>(combinedGenerators);
            Assert.IsTrue(combinedGenerators
                .GenerateSequence(20)
                .SequenceEqual(doubledGeneration)
            );
        }

        [Test]
        public void Do_Set_String() {
            var gen = new Generator()
                .ToDelegate(g => new TestClas {Text = g.FirstName()})
                .Do(clas => clas.Text = "World")
                .Generate();
            Assert.AreEqual("World", gen.Text);
        }

        [Test]
        public void Filter_Custom_Threshold_Reached() {
            var badPredicate = new Generator()
                .ToDelegate(g => g.Bool())
                .Where(x => false, 1000);
            Assert.Throws<ArgumentException>(() => badPredicate());
        }

        [Test]
        public void Filter_Integers_Are_Dividable_By_Two() {
            var numbers = new Generator()
                .ToDelegate(g => g.Integer(20))
                .Where(x => x % 2 == 0)
                .GenerateSequence(20);
            Assert.IsTrue(numbers.All(x => x % 2 == 0));
        }

        [Test]
        public void Filter_Integers_Are_Dividable_By_Two_CustomThreshold() {
            var numbers = new Generator()
                .ToDelegate(g => g.Integer(20))
                .Where(x => x % 2 == 0, 1000)
                .GenerateSequence(20);
            Assert.IsTrue(numbers.All(x => x % 2 == 0));
        }

        [Test]
        public void Filter_String_Contains_Mail() {
            var mails = new Generator(new Configurement {MailDomains = new[] {"gmail.com", "foobar.com"}})
                .ToDelegate(x => x.MailAddress("bob", "doe"))
                .Where(x => x.Contains("mail"))
                .GenerateSequence(20);

            Assert.IsTrue(mails.All(s => s.Contains("mail")));
        }

        [Test]
        public void Filter_String_Contains_Mail_Custom_ThreshHold() {
            var mails = new Generator(new Configurement {MailDomains = new[] {"gmail.com", "foobar.com"}})
                .ToDelegate(x => x.MailAddress("bob", "doe"))
                .Where(x => x.Contains("mail"), 1000)
                .GenerateSequence(20);

            Assert.IsTrue(mails.All(s => s.Contains("mail")));
        }

        [Test]
        public void Filter_Threshold_Reached() {
            var badPredicate = new Generator()
                .ToDelegate(g => g.Bool())
                .Where(x => false);
            Assert.Throws<ArgumentException>(() => badPredicate());
        }

        [Test]
        public void Map_String_Length() {
            var names = new Generator(20)
                .ToDelegate(x => x.FirstName())
                .GenerateSequence(20);
            var nameLenghts = new Generator(20)
                .ToDelegate(x => x.FirstName())
                .Select(x => x.Length)
                .GenerateSequence(20);
            Assert.IsTrue(names.Select(x => x.Length).SequenceEqual(nameLenghts));
        }
    }
}