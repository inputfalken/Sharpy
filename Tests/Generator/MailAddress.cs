﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class MailAddress {
        private const string MailUserName = "mailUserName";

        private static List<string> FindDuplicates(IEnumerable<string> enumerable)
        {
            return enumerable.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();
        }

        [Test]
        public void Check_Mail_Count_Unique_False()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            var generate = randomGenerator.Generate(generator => generator.MailAddress("hello"));
            Assert.AreEqual(14, generate.Length);
        }

        [Test]
        public void Check_Mail_Count_Unique_True()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            var generate = randomGenerator.Generate(generator => generator.MailAddress("hello"));
            Assert.AreEqual(14, generate.Length);
        }

        [Test]
        public void Four__Domain_Two_Args_Unique_False()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com", "test2.com", "test3.com", "test4.com"},
                UniqueMails = false
            }.Create();
            var mails = randomGenerator.GenerateSequence(generator => generator.MailAddress("john", "doe"), 12);
            Assert.IsFalse(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Four__Domain_Two_Args_Unique_True()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com", "test2.com", "test3.com", "test4.com"},
                UniqueMails = true
            }.Create();
            var mails = randomGenerator.GenerateSequence(generator => generator.MailAddress("john", "doe"), 12);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }


        [Test]
        public void MailsAreNotnull()
        {
            var generator = Sharpy.Generator.Create();
            //Many
            var mails = generator.GenerateSequence(g => g.MailAddress(MailUserName), 20).ToArray();
            Assert.IsFalse(mails.All(string.IsNullOrEmpty));
            Assert.IsFalse(mails.All(string.IsNullOrWhiteSpace));

            //Single
            var masil = generator.Generate(g => g.MailAddress(MailUserName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(masil));
            Assert.IsFalse(string.IsNullOrEmpty(masil));
        }

        [Test]
        public void One__Domain_One_Arg_UniqueMails_True_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            var mails = randomGenerator.GenerateSequence(generator => generator.MailAddress("john"), 12);
            Assert.IsTrue(mails.SelectMany(s => s).Any(char.IsNumber));
        }

        [Test]
        public void One__Domain_One_Arg_UniqueMails_True_Called_Two_Times()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            //Should not contain any numbers
            Assert.IsTrue(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(c => !char.IsDigit(c)));
            //Should contain a number since all possible combinations have been used
            Assert.IsTrue(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(char.IsDigit));
        }

        [Test]
        public void One__Domain_Two_Args_SecondNull_UniqueMails_True()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob", null));
            const string expected = "bob@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One__Domain_Unique_False_Check_All_Is_LowerCase()
        {
            var randomGenerator =
                new CustomGenerator(new Random()) {
                    MailProviders = new[] {"test.com"},
                    UniqueMails = false
                }.Create();
            var mail = randomGenerator.Generate(generator => generator.MailAddress("Bob"));
            Assert.IsFalse(mail.Any(char.IsUpper));
        }

        [Test]
        public void One__Domain_Unique_True_Check_All_Is_LowerCase()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            var mail = randomGenerator.Generate(generator => generator.MailAddress("Bob"));
            Assert.IsFalse(mail.Any(char.IsUpper));
        }

        [Test]
        public void One_Domain_First_Arg_Null_Second_String_UniqueMails_False_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null, "hello")));
        }

        [Test]
        public void One_Domain_First_Arg_Null_Second_String_UniqueMails_True_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null, "hello")));
        }

        [Test]
        public void One_Domain_First_Arg_Null_UniqueMails_False_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null)));
        }

        [Test]
        public void One_Domain_First_Arg_Null_UniqueMails_True_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null)));
        }


        [Test]
        public void One_Domain_One_Arg_UniqueMails_False()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            var mails = randomGenerator.GenerateSequence(generator => generator.MailAddress("john"), 12);
            Assert.IsFalse(mails.SelectMany(s => s).Any(char.IsNumber));
        }

        [Test]
        public void One_Domain_One_Arg_UniqueMails_False_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            var generate = randomGenerator.Generate(generator => generator.MailAddress("hello"));
            Assert.AreEqual("hello@test.com", generate);
        }

        [Test]
        public void One_Domain_One_Arg_UniqueMails_True_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            const string expected = "bob@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_false()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            var generateMany = randomGenerator.GenerateSequence(generator => generator.MailAddress("john", "doe"), 30);
            Assert.IsFalse(FindDuplicates(generateMany).Count == 0);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_false_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random(10)) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            const string expected = "bob-cool@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob", "cool"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_false_Called_Two_Times()
        {
            var randomGenerator = new CustomGenerator(new Random(10)) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            var generate = randomGenerator.GenerateSequence(generator => generator.MailAddress("bob", "cool"), 2);
            var result = generate.Last();
            const string expected = "bob-cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_false_FirstNull()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null, "bob")));
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_True()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            var generateMany = randomGenerator.GenerateSequence(generator => generator.MailAddress("john", "doe"), 30);
            Assert.IsTrue(FindDuplicates(generateMany).Count == 0);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_True_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            const string expected = "bob.cool@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob", "cool"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_True_Called_Two_Times()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            var generate = randomGenerator.GenerateSequence(generator => generator.MailAddress("bob", "cool"), 2);
            var result = generate.Last();
            const string expected = "bob_cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_True_FirstNull()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null, "bob")));
        }

        [Test]
        public void One_Domain_Two_UniqueMails_false_Args_Called_Three_Times()
        {
            var randomGenerator = new CustomGenerator(new Random(10)) {
                MailProviders = new[] {"test.com"},
                UniqueMails = false
            }.Create();
            var generateMany = randomGenerator.GenerateSequence(generator => generator.MailAddress("bob", "cool"), 3);
            var result = generateMany.Last();
            const string expected = "bob-cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One_Domain_Two_UniqueMails_True_Args_Called_Three_Times()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();
            var generateMany = randomGenerator.GenerateSequence(generator => generator.MailAddress("bob", "cool"), 3);
            var result = generateMany.Last();
            const string expected = "bob-cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Three__Domain_Two_Strings_UniqueMails_True_Called_Nine_Times()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com", "test2.com", "test3.com"},
                UniqueMails = true
            }.Create();
            var mails = randomGenerator.GenerateSequence(generator => generator.MailAddress("john", "doe"), 9);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Two__Domain_One_Arg_Called_One_Time()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com", "foo.com"},
                UniqueMails = true
            }.Create();
            const string expected = "bob@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Two__Domain_One_String_UniqueMails_True_Called_Three_Times()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com", "foo.com"},
                UniqueMails = true
            }.Create();
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(char.IsDigit));
            // All possible combinations have been used now needs a number
            Assert.IsTrue(randomGenerator.Generate(generator => generator.MailAddress("bob")).Any(char.IsDigit));
        }

        [Test]
        public void Two__Domain_One_String_UniqueMails_True_Called_Two_Times()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com", "foo.com"},
                UniqueMails = true
            }.Create();
            const string expected = "bob@foo.com";
            var generateMany = randomGenerator.GenerateSequence(generator => generator.MailAddress("bob"), 2);

            var result = generateMany.Last();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Two__Domain_Two_Strings_UniqueMails_Called_Six_Times()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com", "test2.com"},
                UniqueMails = true
            }.Create();
            var mails = randomGenerator.GenerateSequence(generator => generator.MailAddress("john", "doe"), 6);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Two_Strings_Called_Four_Times()
        {
            var randomGenerator = new CustomGenerator(new Random()) {
                MailProviders = new[] {"test.com"},
                UniqueMails = true
            }.Create();


            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Any(char.IsDigit));
            // All combinations have been reached now needs a number
            Assert.IsTrue(randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Any(char.IsDigit));
        }
    }
}