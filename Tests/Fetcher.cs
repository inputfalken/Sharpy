using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataGen;
using DataGen.Types.CountryCode;
using DataGen.Types.Date;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using Newtonsoft.Json;
using NodaTime;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class Fetcher {
        [Test]
        public void CreateGenerator_CorrectType() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass(), TestRandomizer);
            var testClass = generator.Generate();
            Assert.IsTrue(testClass.GetType() == typeof(TestClass));
        }

        [Test]
        public void CreateGenerator_Name() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.Name() },
                TestRandomizer);

            //This test will check if the name given is from the common names collection
            Assert.IsTrue(CommonNames.Select(name => name.Data).Contains(generator.Generate().StringProp));
        }

        [Test]
        public void CreateGenerator_NameByType() {
            var generator =
                Sharpy.CreateGenerator(
                    randomizer => new TestClass { StringProp = randomizer.NameByType(NameTypes.MaleFirst) },
                    TestRandomizer);
            var firstName = generator.Generate();
            //This test will check if the name given is from the common names collection
            Assert.IsTrue(
                CommonNames.ByType(NameTypes.MaleFirst).Select(name => name.Data).Contains(firstName.StringProp));
            Assert.IsTrue(
                CommonNames.ByType(NameTypes.MixedFirstNames).Select(name => name.Data).Contains(firstName.StringProp));
            Assert.IsFalse(
                CommonNames.ByType(NameTypes.FemaleFirst).Select(name => name.Data).Contains(firstName.StringProp));
        }

        [Test]
        public void CreateGenerator_UserName() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.UserName() },
                TestRandomizer);
            //This test will check that the random user name is contained in the list
            var list = Usernames.ToList();
            Assert.IsTrue(list.Contains(generator.Generate().StringProp));
        }

        [Test]
        public void CreateGenerator_Number_OneArg() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass { IntProp = randomizer.Number(10) },
                TestRandomizer);

            Assert.IsTrue(generator.Generate().IntProp < 10);
        }

        [Test]
        public void CreateGenerator_Number_TwoArg() {
            var generator =
                Sharpy.CreateGenerator(randomizer => new TestClass { IntProp = randomizer.Number(10, 20) },
                    TestRandomizer);

            var @class = generator.Generate();
            Assert.IsTrue(@class.IntProp < 20 && @class.IntProp > 10);
        }

        [Test]
        public void CreateGenerator_Bool() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass { BoolProp = randomizer.Bool() },
                TestRandomizer);

            //This test will ask if the collection contains atleast one of false & true
            var testClasses = generator.Generate(10).ToList();
            Assert.IsTrue(testClasses.Any(c => c.BoolProp));
            Assert.IsTrue(testClasses.Any(c => !c.BoolProp));
        }

        [Test]
        public void CreateGenerator_LocalDateAge() {
            var generator =
                Sharpy.CreateGenerator(randomizer => new TestClass { LocalDateProp = randomizer.DateByAge(10) },
                    TestRandomizer);
            Assert.IsTrue(generator.Generate().LocalDateProp.Year == DateGenerator.CurrentLocalDate.Year - 10);
        }


        [Test]
        public void CreateGenerator_LocalDateYear() {
            var generator = Sharpy.CreateGenerator(
                randomizer => new TestClass { LocalDateProp = randomizer.DateByYear(1920) }, TestRandomizer);
            Assert.IsTrue(generator.Generate().LocalDateProp.Year == 1920);
        }

        [Test]
        public void CreateGenerator_PhoneNumber() {
            var generator = Sharpy.CreateGenerator(
                randomizer => new TestClass { StringProp = randomizer.PhoneNumber() },
                TestRandomizer);
            var phoneNumber = generator.Generate().StringProp;
            //This test makes sure that the number is from sweden and the length is correct
            Assert.IsTrue(phoneNumber.Contains("+46") && phoneNumber.Length == 7);
        }

        [Test]
        public void CreateGenerator_MailAddress_OneArg() {
            var generator =
                Sharpy.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.MailAdress("bobby") },
                    TestRandomizer);
            var mailAddress = generator.Generate().StringProp;
            //This test makes sure that the number is from sweden and the length is correct
            Assert.IsTrue(mailAddress.Contains("@gmail.com"));
        }

        [Test]
        public void CreateGenerator_MailAddress_TwoArg() {
            var generator =
                Sharpy.CreateGenerator(
                    randomizer => new TestClass { StringProp = randomizer.MailAdress("bobby", null) }, TestRandomizer);
            var mailAddress = generator.Generate().StringProp;
            Assert.IsTrue(mailAddress.Contains("@hotmail.com"));
        }

        private class TestClass {
            public string StringProp { get; set; }
            public bool BoolProp { get; set; }
            public LocalDate LocalDateProp { get; set; }
            public int IntProp { get; set; }
        }

        private static readonly NameFilter CommonNames =
            new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                File.ReadAllText(TestHelper.GetTestsPath() + @"\Data\Types\Name\NamesByOrigin.json")));

        private static readonly StringFilter Usernames =
            new StringFilter(File.ReadAllLines(TestHelper.GetTestsPath() + @"\Data\Types\Name\usernames.txt"));

        private static readonly CountryCodeFilter CountryCodeFilter = new CountryCodeFilter(
            JsonConvert.DeserializeObject<IEnumerable<PhoneNumberGenerator>>(
                File.ReadAllText(TestHelper.GetTestsPath() + @"\Data\Types\CountryCodes\CountryCodes.json"))
        );

        private static readonly DataGen.Randomizer TestRandomizer =
            new Randomizer(new Config(CommonNames, Usernames,
                phoneNumberGenerator: CountryCodeFilter.First(generator => generator.Name == "sweden"),
                mailGenerator: new MailGenerator(new[] { "gmail.com", "hotmail.com", "yahoo.com" }, true
                )));
    }
}