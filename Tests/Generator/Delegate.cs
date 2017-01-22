﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Delegate {
        [Test]
        public void Filter_Integers_Is_Dividable_By_Two() {
            var numbers = new Sharpy.Generator()
                .ToDelegate(g => g.Integer(20))
                .Filter(x => x % 2 == 0)
                .GenerateSequence(20);
            Assert.IsTrue(numbers.All(x => x % 2 == 0));
        }

        [Test]
        public void Filter_String_Contains_Mail() {
            var mails = new Sharpy.Generator(new Configurement {MailDomains = new[] {"gmail.com", "foobar.com"}})
                .ToDelegate(x => x.MailAddress("bob", "doe"))
                .Filter(x => x.Contains("mail"))
                .GenerateSequence(20);

            Assert.IsTrue(mails.All(s => s.Contains("mail")));
        }

        [Test]
        public void Filter_Threshold_Reached() {
            var badPredicate = new Sharpy.Generator().ToDelegate(g => g.Bool()).Filter(x => false);
            Assert.Throws<ArgumentException>(() => badPredicate());
        }

        [Test]
        public void Map_String_Length() {
            var names = new Sharpy.Generator(20)
                .ToDelegate(x => x.FirstName())
                .GenerateSequence(20);
            var nameLenghts = new Sharpy.Generator(20)
                .ToDelegate(x => x.FirstName())
                .Map(x => x.Length)
                .GenerateSequence(20);
            Assert.IsTrue(names.Select(x => x.Length).SequenceEqual(nameLenghts));
        }

        [Test]
        public void Do_Set_String() {
            var gen = new Sharpy.Generator()
                .ToDelegate(g => new TestClas {Text = g.FirstName()})
                .Do(clas => clas.Text = "World")
                .Generate();
            Assert.AreEqual("World", gen.Text);
        }

        [Test]
        public void Bind_Number_Generators() {
            var doubledGeneration = new Sharpy.Generator(100)
                .ToDelegate(x => x.Integer(20))
                .GenerateSequence(20)
                .Select(x => x * 2);
            var gen = new Sharpy.Generator(100)
                .ToDelegate(x => x.Integer(20));
            var gen2 = new Sharpy.Generator(100)
                .ToDelegate(x => x.Integer(20));
            var addedGenerations = gen.Bind(i => gen2, (i, i1) => i + i1).GenerateSequence(20);

            Assert.IsTrue(addedGenerations.SequenceEqual(doubledGeneration));
        }

        private class TestClas {
            public string Text { get; set; }
        }
    }
}