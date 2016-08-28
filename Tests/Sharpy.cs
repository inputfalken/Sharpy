using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataGen;
using DataGen.Types.CountryCode;
using DataGen.Types.Date;
using DataGen.Types.Name;
using DataGen.Types.String;
using Newtonsoft.Json;
using NodaTime;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class Sharpy {
        [Test]
        public void CreateGenerator_CorrectType() {
            var generator = DataGen.Sharpy.CreateGenerator<TestClass>((testClass, fetcher) => { }, Fetcher);
            Assert.IsTrue(generator().GetType() == typeof(TestClass));
        }

        [Test]
        public void CreateGenerator_Name() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.StringProp = fetcher.Name; }, Fetcher);
            //This test will check if the name given is from the common names collection
            Assert.IsTrue(CommonNames.Select(name => name.Data).Contains(generator().StringProp));
        }

        [Test]
        public void CreateGenerator_UserName() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.StringProp = fetcher.UserName; }, Fetcher);
            //This test will check that the random user name is contained in the list
            var list = Usernames.ToList();
            Assert.IsTrue(list.Contains(generator().StringProp));
        }

        [Test]
        public void CreateGenerator_Number_OneArg() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.IntProp = fetcher.Number(10); }, Fetcher);

            Assert.IsTrue(generator().IntProp < 10);
        }

        [Test]
        public void CreateGenerator_Number_TwoArg() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.IntProp = fetcher.Number(10, 20); }, Fetcher);

            var @class = generator();
            Assert.IsTrue(@class.IntProp < 20 && @class.IntProp > 10);
        }

        [Test]
        public void CreateGenerator_Bool() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.BoolProp = fetcher.Bool; }, Fetcher);

            //This test will ask if the collection contains atleast one of false & true
            var testClasses = Enumerable.Range(0, 10).Select(i => generator()).ToList();
            Assert.IsTrue(testClasses.Any(c => c.BoolProp));
            Assert.IsTrue(testClasses.Any(c => !c.BoolProp));
        }

        [Test]
        public void CreateGenerator_LocalDateAge() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.LocalDateProp = fetcher.DateByAge(10); }, Fetcher);
            Assert.IsTrue(generator().LocalDateProp.Year == DateGenerator.CurrentLocalDate.Year - 10);
        }


        [Test]
        public void CreateGenerator_LocalDateYear() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.LocalDateProp = fetcher.DateByYear(1920); }, Fetcher);
            Assert.IsTrue(generator().LocalDateProp.Year == 1920);
        }

        [Test]
        public void CreateGenerator_PhoneNumber() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.StringProp = fetcher.PhoneNumber(); }, Fetcher);
            var phoneNumber = generator().StringProp;
            //This test makes sure that the number is from sweden and the length is correct
            Assert.IsTrue(phoneNumber.Contains("+46") && phoneNumber.Length == 7);
        }

        [Test]
        public void CreateGenerator_MailAddress() {
            var generator =
                DataGen.Sharpy.CreateGenerator<TestClass>(
                    (testClass, fetcher) => { testClass.StringProp = fetcher.MailAdress(fetcher.UserName); }, Fetcher);
            var mailAddress = generator().StringProp;
            //This test makes sure that the number is from sweden and the length is correct
            Assert.IsTrue(mailAddress.Contains("@gmail.com"));
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

        private static readonly Fetcher Fetcher = new Fetcher(CommonNames, Usernames,
            phoneNumberGenerator: CountryCodeFilter.First(generator => generator.Name == "sweden"));
    }
}