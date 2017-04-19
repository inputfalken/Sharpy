using System;
using System.Collections.Generic;
using System.Linq;
using GeneratorAPI;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    public class MailAddress {
        private const string MailUserName = "mailUserName";

        private static List<string> FindDuplicates(IEnumerable<string> enumerable) {
            return enumerable.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();
        }


        [Test]
        public void Check_Mail_Count_Unique_True() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var generate = randomGenerator.Generate(generator => generator.MailAddress("hello")).Take();
            Assert.AreEqual(14, generate.Length);
        }


        [Test]
        public void Four__Domain_Two_Args_Unique_True() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com", "test2.com", "test3.com", "test4.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var mails = randomGenerator.Generate(generator => generator.MailAddress("john", "doe"))
                .Take(12);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }


        [Test]
        public void MailsAreNotnull() {
            var generator = Generator.Factory.SharpyGenerator(new Provider());
            //Many
            var mails = generator
                .Generate(g => g.MailAddress(MailUserName))
                .Take(20)
                .ToArray();
            Assert.IsFalse(mails.All(string.IsNullOrEmpty));
            Assert.IsFalse(mails.All(string.IsNullOrWhiteSpace));

            //Single
            var mail = generator.Generate(g => g.MailAddress(MailUserName)).Take();
            Assert.IsFalse(string.IsNullOrWhiteSpace(mail));
            Assert.IsFalse(string.IsNullOrEmpty(mail));
        }

        [Test]
        public void One__Domain_One_Arg_UniqueMails_True_Called_One_Time() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var mails = randomGenerator
                .Generate(generator => generator.MailAddress("john"))
                .Take(12);
            Assert.IsTrue(mails.SelectMany(s => s).Any(char.IsNumber));
        }

        [Test]
        public void One__Domain_One_Arg_UniqueMails_True_Called_Two_Times() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            //Should not contain any numbers
            Assert.IsTrue(
                randomGenerator.Generate(generator => generator.MailAddress("bob")).Take().Any(c => !char.IsDigit(c)));
            //Should contain a number since all possible combinations have been used
            Assert.IsTrue(
                randomGenerator.Generate(generator => generator.MailAddress("bob")).Take().Any(char.IsDigit));
        }

        [Test]
        public void One__Domain_Two_Args_SecondNull_UniqueMails_True() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob")).Take();
            const string expected = "bob@test.com";
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void One__Domain_Unique_True_Check_All_Is_LowerCase() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var mail = randomGenerator.Generate(generator => generator.MailAddress("Bob")).Take();
            Assert.IsFalse(mail.Any(char.IsUpper));
        }


        [Test]
        public void One_Domain_First_Arg_Null_Second_String_UniqueMails_True_Called_One_Time() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null, "hello")).Take());
        }


        [Test]
        public void One_Domain_First_Arg_Null_UniqueMails_True_Called_One_Time() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null)).Take());
        }


        [Test]
        public void One_Domain_One_Arg_UniqueMails_True_Called_One_Time() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            const string expected = "bob@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob")).Take();
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void One_Domain_Two_Args_UniqueMails_True() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var generateMany =
                randomGenerator.Generate(generator => generator.MailAddress("john", "doe"))
                    .Take(30);
            Assert.IsTrue(FindDuplicates(generateMany).Count == 0);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_True_Called_One_Time() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            const string expected = "bob.cool@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob", "cool")).Take();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_True_Called_Two_Times() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var generate = randomGenerator
                .Generate(generator => generator.MailAddress("bob", "cool"))
                .Take(2);
            var result = generate.Last();
            const string expected = "bob_cool@test.com";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void One_Domain_Two_Args_UniqueMails_True_FirstNull() {
            var configurement = new Configurement {MailDomains = new[] {"test.com"}};
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            Assert.Throws<NullReferenceException>(
                () => randomGenerator.Generate(generator => generator.MailAddress(null, "bob")).Take());
        }


        [Test]
        public void One_Domain_Two_UniqueMails_True_Args_Called_Three_Times() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var generateMany = randomGenerator
                .Generate(generator => generator.MailAddress("bob", "cool"))
                .Take(3);
            var result = generateMany.Last();
            const string expected = "bob-cool@test.com";
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Single_Argurment_Does_Not_Contain_Seperator() {
            var generator =
                Generator.Factory.SharpyGenerator(new Provider(new Configurement {MailDomains = new[] {"test.com"}}))
                    .Generate(g => g.MailAddress("Bob"))
                    .Take(2)
                    .ToArray();
            var first = generator.First();
            var last = generator.Last();

            Assert.AreEqual("bob@test.com", first);
            Assert.IsFalse(last.Any(char.IsSeparator));
        }

        [Test]
        public void Three__Domain_Two_Strings_UniqueMails_True_Called_Nine_Times() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com", "test2.com", "test3.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var mails = randomGenerator
                .Generate(generator => generator.MailAddress("john", "doe"))
                .Take(9);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Two__Domain_One_Arg_Called_One_Time() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com", "foo.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            const string expected = "bob@test.com";
            var result = randomGenerator.Generate(generator => generator.MailAddress("bob")).Take();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Two__Domain_One_String_UniqueMails_True_Called_Three_Times() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com", "foo.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob"))
                .Take()
                .Any(char.IsDigit));
            Assert.IsFalse(randomGenerator.Generate(generator => generator.MailAddress("bob"))
                .Take()
                .Any(char.IsDigit));
            // All possible combinations have been used now needs a number
            Assert.IsTrue(
                randomGenerator.Generate(generator => generator.MailAddress("bob")).Take().Any(char.IsDigit));
        }

        [Test]
        public void Two__Domain_One_String_UniqueMails_True_Called_Two_Times() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com", "foo.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            const string expected = "bob@foo.com";
            var generateMany = randomGenerator
                .Generate(generator => generator.MailAddress("bob"))
                .Take(2);

            var result = generateMany.Last();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Two__Domain_Two_Strings_UniqueMails_Called_Six_Times() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com", "test2.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));
            var mails = randomGenerator
                .Generate(generator => generator.MailAddress("john", "doe"))
                .Take(6);
            Assert.IsTrue(FindDuplicates(mails).Count == 0);
        }

        [Test]
        public void Two_Strings_Called_Four_Times() {
            var configurement = new Configurement {
                MailDomains = new[] {"test.com"}
            };
            var randomGenerator = Generator.Factory.SharpyGenerator(new Provider(configurement));

            Assert.IsFalse(
                randomGenerator.Generate(generator => generator.MailAddress("bob", "cool"))
                    .Take()
                    .Any(char.IsDigit));
            Assert.IsFalse(
                randomGenerator.Generate(generator => generator.MailAddress("bob", "cool"))
                    .Take()
                    .Any(char.IsDigit));
            Assert.IsFalse(
                randomGenerator.Generate(generator => generator.MailAddress("bob", "cool"))
                    .Take()
                    .Any(char.IsDigit));
            // All combinations have been reached now needs a number
            Assert.IsTrue(
                randomGenerator.Generate(generator => generator.MailAddress("bob", "cool"))
                    .Take()
                    .Any(char.IsDigit));
        }
    }
}