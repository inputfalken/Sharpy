using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NodaTime;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Types;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Date;
using Sharpy.Types.Name;
using Sharpy.Types.String;

//Todo set a seed and let all tests be ran from that seed so i can expect values...

namespace Tests {
    [TestFixture]
    public class RandomizerTest {
        [Test]
        public void CreateGenerator_CorrectType() {
            var generator = Factory.CreateGenerator(randomizer => new TestClass());
            var testClass = generator.Generate();
            Assert.IsTrue(testClass.GetType() == typeof(TestClass));
        }


        [Test]
        public void TestEveryCountryEnumForCountryCode() {
            //This code will iterate all country enums and try to generate from it if no data is fetched from the country an exception will be thrown...
            Assert.DoesNotThrow(() => {
                foreach (var enumName in typeof(Country).GetEnumNames()) {
                    var phoneNumberGenerator = new CountryCode(enumName, "code");
                    if (!phoneNumberGenerator.IsParsed) {
                        throw new Exception();
                    }
                }
            });
        }

        [Test]
        public void CreateGenerator_WithSuppliedList() {
            var list = new List<int> { 1, 2, 3, 4, 5, 5, 6, 7 };
            var generator = Factory.CreateGenerator(randomizer => randomizer.CustomCollection(list));
            Assert.IsTrue(generator.Generate(10).All(list.Contains));
        }

        [Test]
        public void CreateGenerator_WithSuppliedArray() {
            var items = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var generator = Factory.CreateGenerator(randomizer => randomizer.CustomCollection(items));
            Assert.IsTrue(generator.Generate(10).All(items.Contains));
        }


        [Test]
        public void CreateGenerator_UserName() {
            var generator =
                Factory.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.UserName() }
                );
            //This test will check that the random user name is contained in the list
            var list = Usernames.ToList();
            Assert.IsTrue(list.Contains(generator.Generate().StringProp));
        }

        [Test]
        public void CreateGenerator_Number_OneArg() {
            var generator = Factory.CreateGenerator(randomizer => new TestClass { IntProp = randomizer.Number(10) }
            );

            Assert.IsTrue(generator.Generate().IntProp < 10);
        }

        [Test]
        public void CreateGenerator_Number_TwoArg() {
            var generator =
                Factory.CreateGenerator(randomizer => new TestClass { IntProp = randomizer.Number(10, 20) }
                );

            var @class = generator.Generate();
            Assert.IsTrue(@class.IntProp <= 19 && @class.IntProp >= 10);
        }

        [Test]
        public void CreateGenerator_Bool() {
            var generator = Factory.CreateGenerator(randomizer => new TestClass { BoolProp = randomizer.Bool() }
            );

            //This test will ask if the collection contains atleast one of false & true
            var testClasses = generator.Generate(10).ToList();
            Assert.IsTrue(testClasses.Any(c => c.BoolProp));
            Assert.IsTrue(testClasses.Any(c => !c.BoolProp));
        }

        [Test]
        public void CreateGenerator_LocalDateAge() {
            var generator =
                Factory.CreateGenerator(randomizer => new TestClass { LocalDateProp = randomizer.DateByAge(10) }
                );
            Assert.IsTrue(generator.Generate().LocalDateProp.Year ==
                          DateGenerator.CurrentLocalDate.Minus(Period.FromYears(10)).Year);
        }


        [Test]
        public void CreateGenerator_LocalDateYear() {
            var generator = Factory.CreateGenerator(
                randomizer => new TestClass { LocalDateProp = randomizer.DateByYear(1920) });
            Assert.IsTrue(generator.Generate().LocalDateProp.Year == 1920);
        }


        [Test]
        public void CreateGenerator_MailAddress_OneArg() {
            var generator =
                Factory.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.MailAdress("joby") }
                );
            generator.Config.Mail(new[] { "gmail.com" });
            var mailAddress = generator.Generate().StringProp;

            Assert.IsTrue(mailAddress.Contains("@gmail.com"));
        }

        [Test]
        public void CreateGenerator_MailAddress_TwoArg() {
            var generator =
                Factory.CreateGenerator(
                    randomizer => new TestClass { StringProp = randomizer.MailAdress("joby", null) });
            generator.Config.Mail(new[] { "gmail.com" });
            var mailAddress = generator.Generate().StringProp;
            Assert.IsTrue(mailAddress.Contains("@gmail.com"));
        }

        [Test]
        public void CreateGenerator_PhoneNumber_Default() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.PhoneNumber());
            Assert.IsTrue(generator.Generate(2).All(s => s.Contains("+1") && s.Length == 6));
        }

        [Test]
        public void CreateGenerator_PhoneNumber_Args_Length() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.PhoneNumber(length: 5));
            Assert.IsTrue(generator.Generate(2).All(s => s.Contains("+1") && s.Length == 7));
        }


        [Test]
        public void CreateGenerator_PhoneNumber_Args_Prenumber() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.PhoneNumber("10"));
            Assert.IsTrue(generator.Generate(2).All(s => s.Contains("+110") && s.Length == 8));
        }

        [Test]
        public void CreateGenerator_PhoneNumber_Args_PreNumber_length() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.PhoneNumber("11", 5));
            Assert.IsTrue(generator.Generate(2).All(s => s.Contains("+111") && s.Length == 9));
        }

        #region With Config

        [Test]
        public void CreateGenerator_Config_MailAddressWithOutNumberAppending() {
            var generator =
                Factory.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.MailAdress("joby") });
            generator.Config.Mail(new[] { "gmail.com" });
            var testClasses = generator.Generate(3).ToList();

            // Checks if there's no number
            Assert.IsTrue(testClasses[0].StringProp.All(c => !char.IsNumber(c)));
            //Should not contain a number
            Assert.IsFalse(testClasses[1].StringProp.Any(char.IsNumber));
            //Should not contain a number
            Assert.IsFalse(testClasses[2].StringProp.Any(char.IsNumber));
        }

        [Test]
        public void CreateGenerator_Config_MailAddressWithNumberAppending() {
            var generator =
                Factory.CreateGenerator(randomizer => new TestClass { StringProp = randomizer.MailAdress("joby") });
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
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name(NameType.MixedFirstName));
            generator.Config.Name.ByOrigin(Country.Sweden);
            Assert.IsTrue(
                generator.Generate(30)
                    .All(s => CommonNames.ByOrigin(Country.Sweden).Filter.Select(name => name.Data).Contains(s)));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_ByLength5() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.ByLength(5));
            Assert.IsTrue(generator.Generate(30).All(s => s.Length == 5));
        }

        [Test]
        public void CreateGenerator_Config_Names_ByLength5() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.ByLength(5);
            Assert.IsTrue(generator.Generate(30).All(s => s.Length == 5));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_Contains_S() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.Contains("S"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("S", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_Contains_Sot() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.Contains("Sot"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("Sot", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }


        [Test]
        public void CreateGenerator_Config_UserNames_Contains_MultipleArgs_S_Y() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.Contains("S", "Y"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("S", StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        s.IndexOf("Y", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_Contains_MultipleArgs_Sot_Yor() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.Contains("Sot", "Yor"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("Sot", StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        s.IndexOf("Yor", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_Names_Contains_S() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.Contains("S");
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("S", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_Names_Contains_Sot() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.Contains("Sot");
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("Sot", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_Names_Contains_MultipleArgs_S_Y() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.Contains("S", "Y");
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("S", StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        s.IndexOf("Y", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_Names_Contains_MultipleArgs_Sot_Yor() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.Contains("Sot", "Yor");
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("Sot", StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        s.IndexOf("Yor", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_DoesNotContains_S() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.DoesNotContain("S"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("S", StringComparison.CurrentCultureIgnoreCase) < 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_DoesNotContains_Sot() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.DoesNotContain("Sot"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("Sot", StringComparison.CurrentCultureIgnoreCase) < 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_Names_DoesNotContains_S() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.DoesNotContain("S");
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("S", StringComparison.CurrentCultureIgnoreCase) < 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_Names_DoesNotContains_Sot() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.DoesNotContain("Sot");
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("Sot", StringComparison.CurrentCultureIgnoreCase) < 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_StartsWith_j() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.StartsWith("j"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("j", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_StartsWith_jo() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.StartsWith("jo"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                var b = s.IndexOf("jo", StringComparison.CurrentCultureIgnoreCase) >= 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_StartsWith_MultipleArgs_j_p() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.StartsWith("j", "p"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                //^ means XOR
                var b = s.IndexOf("j", StringComparison.CurrentCultureIgnoreCase) == 0 ^
                        s.IndexOf("p", StringComparison.CurrentCultureIgnoreCase) == 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_StartsWith_MultipleArgs_jo_pol() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.StartsWith("jo", "pol"));
            Assert.IsTrue(generator.Generate(30).All(s => {
                //^ means XOR
                var b = s.IndexOf("jo", StringComparison.CurrentCultureIgnoreCase) == 0 ^
                        s.IndexOf("pol", StringComparison.CurrentCultureIgnoreCase) == 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_Names_StartsWith_j() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.StartsWith("j");
            Assert.IsTrue(generator.Generate(30)
                .All(s => s.IndexOf("j", StringComparison.CurrentCultureIgnoreCase) == 0));
        }

        [Test]
        public void CreateGenerator_Config_Names_StartsWith_jo() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.StartsWith("jo");
            Assert.IsTrue(generator.Generate(30)
                .All(s => s.IndexOf("jo", StringComparison.CurrentCultureIgnoreCase) == 0));
        }

        [Test]
        public void CreateGenerator_Config_Names_StartsWith_MultipleArgs_j_p() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.StartsWith("j", "p");
            Assert.IsTrue(generator.Generate(30).All(s => {
                //^ means XOR
                var b = s.IndexOf("j", StringComparison.CurrentCultureIgnoreCase) == 0 ^
                        s.IndexOf("p", StringComparison.CurrentCultureIgnoreCase) == 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_Names_StartsWith_MultipleArgs_jo_pol() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.StartsWith("jo", "pol");
            Assert.IsTrue(generator.Generate(30).All(s => {
                //^ means XOR
                var b = s.IndexOf("jo", StringComparison.CurrentCultureIgnoreCase) == 0 ^
                        s.IndexOf("pol", StringComparison.CurrentCultureIgnoreCase) == 0;
                return b;
            }));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_DoesNotStartsWith_J() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.DoesNotStartWith("j"));
            Assert.IsTrue(generator.Generate(30)
                .All(s => s.IndexOf("j", StringComparison.CurrentCultureIgnoreCase) != 0));
        }

        [Test]
        public void CreateGenerator_Config_UserNames_DoesNotStartsWith_jo() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.UserName());
            generator.Config.UserName(filter => filter.DoesNotStartWith("jo"));
            Assert.IsTrue(generator.Generate(30)
                .All(s => s.IndexOf("jo", StringComparison.CurrentCultureIgnoreCase) != 0));
        }

        [Test]
        public void CreateGenerator_Config_Names_DoesNotStartsWith_J() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.DoesNotStartWith("J");
            Assert.IsTrue(generator.Generate(30)
                .All(s => s.IndexOf("j", StringComparison.CurrentCultureIgnoreCase) != 0));
        }

        [Test]
        public void CreateGenerator_Config_Names_DoesNotStartsWith_jo() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.Name());
            generator.Config.Name.DoesNotStartWith("jo");
            Assert.IsTrue(generator.Generate(30)
                .All(s => s.IndexOf("jo", StringComparison.CurrentCultureIgnoreCase) != 0));
        }

        [Test]
        public void CreateGenerator_PhoneNumber_Config_Args_Default() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.PhoneNumber());
            generator.Config.CountryCode(Country.Sweden);
            Assert.IsTrue(generator.Generate(2).All(s => s.Contains("+46") && s.Length == 7));
        }

        [Test]
        public void CreateGenerator_PhoneNumber_Config_Args_Length() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.PhoneNumber(length: 5));
            generator.Config.CountryCode(Country.Norway);
            Assert.IsTrue(generator.Generate(2).All(s => s.Contains("+47") && s.Length == 8));
        }

        [Test]
        public void CreateGenerator_PhoneNumber_Config_Args_PreNumber() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.PhoneNumber("11"));
            generator.Config.CountryCode(Country.Norway);
            Assert.IsTrue(generator.Generate(2).All(s => s.Contains("+4711") && s.Length == 9));
        }

        [Test]
        public void CreateGenerator_PhoneNumber_Config_Args_PreNumber_length() {
            var generator = Factory.CreateGenerator(randomizer => randomizer.PhoneNumber("11", 5));
            generator.Config.CountryCode(Country.Norway);
            Assert.IsTrue(generator.Generate(2).All(s => s.Contains("+4711") && s.Length == 10));
        }

        #endregion

        private class TestClass {
            public string StringProp { get; set; }
            public bool BoolProp { get; set; }
            public LocalDate LocalDateProp { get; set; }
            public int IntProp { get; set; }
        }

        private static readonly NameConfig CommonNames = new Config().Name;
            

        private static readonly StringFilter Usernames =
            new StringFilter(Sharpy.Properties.Resources.usernames.Split(Convert.ToChar("\n")));
    }
}