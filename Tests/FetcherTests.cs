using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataGen;
using DataGen.Types.CountryCode;
using DataGen.Types.Date;
using DataGen.Types.Enums;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using Newtonsoft.Json;
using NodaTime;
using NUnit.Framework;

//Todo set a seed and let all tests be ran from that seed so i can expect values...
namespace Tests {
    [TestFixture]
    public class FetcherTests {
        [Test]
        public void CreateGenerator_CorrectType() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass() );
            var testClass = generator.Generate();
            Assert.IsTrue(testClass.GetType() == typeof(TestClass));
        }

        [Test]
        public void CreateGenerator_Name() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.Name() }
                );

            //This test will check if the name given is from the common names collection
            Assert.IsTrue(CommonNames.Select(name => name.Data).Contains(generator.Generate().StringProp));
        }

        [Test]
        public void TestEveryCountryEnumForName() {
            //This code will iterate all country enums and try to generate from it if no data is fetched from the country an exception will be thrown...
            Assert.DoesNotThrow(() => {
                foreach (var enumName in typeof(Country).GetEnumNames()) {
                    var countryEnum = (Country) Enum.Parse(typeof(Country), enumName);
                    CommonNames.ByCountry(countryEnum);
                }
            });
        }

        [Test]
        public void TestEveryCountryEnumForCountryCode() {
            //This code will iterate all country enums and try to generate from it if no data is fetched from the country an exception will be thrown...
            Assert.DoesNotThrow(() => {
                foreach (var enumName in typeof(Country).GetEnumNames()) {
                    var phoneNumberGenerator = new PhoneNumberGenerator(enumName, "code");
                    if (!phoneNumberGenerator.IsParsed) {
                        throw new Exception();
                    }
                }
            });
        }

        [Test]
        public void CreateGenerator_NameByType() {
            var generator =
                Sharpy.CreateGenerator(
                    randomizer => new TestClass { StringProp = randomizer.Name(NameType.MaleFirstName) }
                    );
            var firstName = generator.Generate();
            //This test will check if the name given is from the common names collection
            Assert.IsTrue(
                CommonNames.ByType(NameType.MaleFirstName).Select(name => name.Data).Contains(firstName.StringProp));
            Assert.IsTrue(
                CommonNames.ByType(NameType.MixedFirstName).Select(name => name.Data).Contains(firstName.StringProp));
            Assert.IsFalse(
                CommonNames.ByType(NameType.FemaleFirstName).Select(name => name.Data).Contains(firstName.StringProp));
        }

        [Test]
        public void CreateGenerator_UserName() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.UserName() }
                );
            //This test will check that the random user name is contained in the list
            var list = Usernames.ToList();
            Assert.IsTrue(list.Contains(generator.Generate().StringProp));
        }

        [Test]
        public void CreateGenerator_Number_OneArg() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass { IntProp = randomizer.Number(10) }
                );

            Assert.IsTrue(generator.Generate().IntProp < 10);
        }

        [Test]
        public void CreateGenerator_Number_TwoArg() {
            var generator =
                Sharpy.CreateGenerator(randomizer => new TestClass { IntProp = randomizer.Number(10, 20) }
                    );

            var @class = generator.Generate();
            Assert.IsTrue(@class.IntProp < 19 && @class.IntProp >= 10);
        }

        [Test]
        public void CreateGenerator_Bool() {
            var generator = Sharpy.CreateGenerator(randomizer => new TestClass { BoolProp = randomizer.Bool() }
                );

            //This test will ask if the collection contains atleast one of false & true
            var testClasses = generator.Generate(10).ToList();
            Assert.IsTrue(testClasses.Any(c => c.BoolProp));
            Assert.IsTrue(testClasses.Any(c => !c.BoolProp));
        }

        [Test]
        public void CreateGenerator_LocalDateAge() {
            var generator =
                Sharpy.CreateGenerator(randomizer => new TestClass { LocalDateProp = randomizer.DateByAge(10) }
                    );
            Assert.IsTrue(generator.Generate().LocalDateProp.Year == DateGenerator.CurrentLocalDate.Minus(Period.FromYears(10)).Year);
        }


        [Test]
        public void CreateGenerator_LocalDateYear() {
            var generator = Sharpy.CreateGenerator(
                randomizer => new TestClass { LocalDateProp = randomizer.DateByYear(1920) } );
            Assert.IsTrue(generator.Generate().LocalDateProp.Year == 1920);
        }


        [Test]
        public void CreateGenerator_MailAddress_OneArg() {
            var generator =
                Sharpy.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.MailAdress("bobby") }
                    );
            var mailAddress = generator.Generate().StringProp;
            //This test makes sure that the number is from sweden and the length is correct
            Assert.IsTrue(mailAddress.Contains("@gmail.com"));
        }

        [Test]
        public void CreateGenerator_MailAddress_TwoArg() {
            var generator =
                Sharpy.CreateGenerator(
                    randomizer => new TestClass { StringProp = randomizer.MailAdress("bobby", null) } );
            var mailAddress = generator.Generate().StringProp;
            Assert.IsTrue(mailAddress.Contains("@gmail.com"));
        }

        #region With Config

        [Test]
        public void CreateGenerator_Config_MailAddress() {
            var generator =
                Sharpy.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.MailAdress("Bobby") });
            generator.Config.Mail(new[] { "gmail.com" }, true);
            var testClasses = generator.Generate(3).ToList();

            // Checks if there's no number
            Assert.IsTrue(testClasses[0].StringProp.All(c => !char.IsNumber(c)));
            //Should contain a number
            Assert.IsTrue(testClasses[1].StringProp.Any(char.IsNumber));
            //Should contain a number
            Assert.IsTrue(testClasses[2].StringProp.Any(char.IsNumber));
        }

        [Test]
        public void CreateGenerator_Config_Names() {
            var generator = Sharpy.CreateGenerator(randomizer => randomizer.Name(NameType.MixedFirstName)                );
            generator.Config.NameOrigin(Country.Sweden);
            Assert.IsTrue(
                generator.Generate(30)
                    .All(s => CommonNames.ByCountry(Country.Sweden).Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void CreateGenerator_Config_UserNames() {
            var generator = Sharpy.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.ByLength(5));

            Assert.IsTrue(generator.Generate(30).All(s => s.Length == 5));
        }



        #endregion

        private class TestClass {
            public string StringProp { get; set; }
            public bool BoolProp { get; set; }
            public LocalDate LocalDateProp { get; set; }
            public int IntProp { get; set; }
        }

        private static readonly NameFilter CommonNames =
            new NameFilter(new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(DataGen.Properties.Resources.NamesByOrigin))));

        private static readonly StringFilter Usernames =
            new StringFilter(DataGen.Properties.Resources.usernames.Split(Convert.ToChar("\n")));


    }
}