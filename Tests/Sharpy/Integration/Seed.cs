using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Sharpy;
using Sharpy.Core;
using Sharpy.Core.Linq;
using Sharpy.Enums;

namespace Tests.Sharpy.Integration {
    /// <summary>
    ///     <para>These tests check that the result will be the same when setting a seed.</para>
    ///     <para>Thread.Sleep is for making sure a new seed would be given if the Seed given does not work.</para>
    /// </summary>
    [TestFixture]
    public class Seed {
        /// <summary>
        ///     <para>The seed given to all Provider instance created in the tests.</para>
        /// </summary>
        private const int TestSeed = 100;

        /// <summary>
        ///     <para>The amount generated from GenrateMany Tests.</para>
        /// </summary>
        private const int Count = 100;

        /// <summary>
        ///     <para>The duration of Thread.Sleep() on all tests.</para>
        /// </summary>
        private const int SleepDuration = 20;

        [Test]
        public void Select_Seed_Bool() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.Bool()).Generate();
            var result = g2.Select(g => g.Bool()).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_CustomCollection() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.TakeElement(list)).Generate();
            var result = g2.Select(g => g.TakeElement(list)).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_DateByAge() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.DateByAge(20)).Generate();
            var result = g2.Select(g => g.DateByAge(20)).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_DateByYear() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.DateByAge(2000)).Generate();
            var result = g2.Select(g => g.DateByAge(2000)).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_Double_No_Arg() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Double()).Generate();
            var result = g1.Select(g => g.Double()).Generate();
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Select_Seed_Double_One_Arg() {
            const double max = 3.3;
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Double(max)).Generate();
            var result = g1.Select(g => g.Double(max)).Generate();
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Select_Seed_Double_Two_Args() {
            const double max = 3.3;
            const double min = 1.3;
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.Double(min, max)).Generate();
            var result = g2.Select(g => g.Double(min, max)).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_Integer_No_Arg() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Integer()).Generate();
            var result = g1.Select(g => g.Integer()).Generate();
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Select_Seed_Integer_One_Arg() {
            const int max = 100;
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Integer(max)).Generate();
            var result = g1.Select(g => g.Integer(max)).Generate();
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Select_Seed_Integer_Two_Args() {
            const int max = 100;
            const int min = 20;
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Integer(min, max)).Generate();
            var result = g1.Select(g => g.Integer(min, max)).Generate();
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Select_Seed_Long_No_Arg() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Long()).Generate();
            var result = g1.Select(g => g.Long()).Generate();
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Select_Seed_Long_One_Arg() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            const long max = long.MaxValue - 3923329;
            var expected = g2.Select(g => g.Long(max)).Generate();
            var result = g1.Select(g => g.Long(max)).Generate();
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Select_Seed_Long_Two_Args() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.Select(g => g.Long(min, max)).Generate();
            var result = g1.Select(g => g.Long(min, max)).Generate();
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Select_Seed_MailAddress() {
            const string name = "bob";
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(generator => generator.Mail(name)).Generate();
            var result = g2.Select(generator => generator.Mail(name)).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_Name_FemaleFirstName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var expected = g1.Select(g => g.FirstName(Gender.Female)).Generate();
            var result = g2.Select(g => g.FirstName(Gender.Female)).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_Name_FirstName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var expected = g1.Select(g => g.FirstName()).Generate();
            var result = g2.Select(g => g.FirstName()).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_Name_LastName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var expected = g1.Select(g => g.LastName()).Generate();
            var result = g2.Select(g => g.LastName()).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_Name_MaleFirstName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var generateManyA = g1.Select(g => g.FirstName(Gender.Male)).Generate();
            var generateManyB = g2.Select(g => g.FirstName(Gender.Male)).Generate();
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void Select_Seed_Name_UserName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var generateManyA = g1.Select(g => g.UserName()).Take(Count);
            var generateManyB = g2.Select(g => g.UserName()).Take(Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void Select_Seed_Params() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.TakeArgument("Foo", "Bar", "John", "Doe")).Generate();
            var result = g2.Select(g => g.TakeArgument("Foo", "Bar", "John", "Doe")).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_PhoneNumber() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.NumberByLength(10)).Generate();
            var result = g2.Select(g => g.NumberByLength(10)).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_SecurityNumber_Formated_False() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            const int age = 20;
            var expected = g1.Select(g => g.SocialSecurityNumber(g.DateByAge(age), false)).Generate();
            var result = g2.Select(g => g.SocialSecurityNumber(g.DateByAge(age), false)).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Select_Seed_SecurityNumber_Formated_True() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            const int age = 20;
            var expected = g1.Select(g => g.SocialSecurityNumber(g.DateByAge(age))).Generate();
            var result = g2.Select(g => g.SocialSecurityNumber(g.DateByAge(age))).Generate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_Bool() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(g => g.Bool()).Take(Count);
            var result = g2.Select(g => g.Bool()).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_CustomCollection() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(g => g.TakeElement(list)).Take(Count);
            var result = g2.Select(g => g.TakeElement(list)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_DateByAge() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(generator => generator.DateByAge(20)).Take(Count);
            var result = g2.Select(generator => generator.DateByAge(20)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_DateByYear() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(generator => generator.DateByAge(2000)).Take(Count);
            var result = g2.Select(generator => generator.DateByAge(2000)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_Double_No_Arg() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g2.Select(g => g.Double()).Take(Count);
            var result = g1.Select(g => g.Double()).Take(Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void SelectMany_No_Seed_Double_One_Arg() {
            const double max = 3.3;
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g2.Select(g => g.Double(max)).Take(Count);
            var result = g1.Select(g => g.Double(max)).Take(Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void SelectMany_No_Seed_Double_Two_Args() {
            const double max = 3.3;
            const double min = 1.3;
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(g => g.Double(min, max)).Take(Count);
            var result = g2.Select(g => g.Double(min, max)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_Integer_No_Arg() {
            var generator = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var generator2 = Generator.Create(new Builder());
            var expected = generator2.Select(g => g.Integer()).Take(Count);
            var result = generator.Select(g => g.Integer()).Take(Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void SelectMany_No_Seed_Integer_One_Arg() {
            const int max = 100;
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g2.Select(g => g.Integer(max)).Take(Count);
            var result = g1.Select(g => g.Integer(max)).Take(Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void SelectMany_No_Seed_Integer_Two_Args() {
            const int max = 100;
            const int min = 20;
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g2.Select(g => g.Integer(min, max)).Take(Count);
            var result = g1.Select(g => g.Integer(min, max)).Take(Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void SelectMany_No_Seed_Long_No_Arg() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g2.Select(g => g.Long()).Take(Count);
            var result = g1.Select(g => g.Long()).Take(Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void SelectMany_No_Seed_Long_One_Arg() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            const long max = long.MaxValue - 3923329;
            var expected = g2.Select(g => g.Long(max)).Take(Count);
            var result = g1.Select(g => g.Long(max)).Take(Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void SelectMany_No_Seed_Long_Two_Args() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.Select(g => g.Long(min, max)).Take(Count);
            var result = g1.Select(g => g.Long(min, max)).Take(Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void SelectMany_No_Seed_MailAddress() {
            const string name = "bob";
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(generator => generator.Mail(name)).Take(Count);
            var result = g2.Select(generator => generator.Mail(name)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_Name_FemaleFirstName() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());

            var expected = g1.Select(g => g.FirstName(Gender.Female)).Take(Count);
            var result = g2.Select(g => g.FirstName(Gender.Male)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_Name_FirstName() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());

            var expected = g1.Select(g => g.FirstName()).Take(Count);
            var result = g2.Select(g => g.FirstName()).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_Name_LastName() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(g => g.LastName()).Take(Count);
            var result = g2.Select(g => g.LastName()).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_Name_MaleFirstName() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());

            var generateManyA = g1.Select(g => g.FirstName(Gender.Male)).Take(Count);
            var generateManyB = g2.Select(g => g.FirstName(Gender.Male)).Take(Count);
            Assert.AreNotEqual(generateManyA, generateManyB);
        }

        [Test]
        public void SelectMany_No_Seed_Name_UserName() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());

            var generateManyA = g1.Select(g => g.UserName()).Take(Count);
            var generateManyB = g2.Select(g => g.UserName()).Take(Count);
            Assert.AreNotEqual(generateManyA, generateManyB);
        }

        [Test]
        public void SelectMany_No_Seed_Params() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(g => g.TakeElement(list)).Take(Count);
            var result = g2.Select(g => g.TakeElement(list)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_PhoneNumber() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());
            var expected = g1.Select(g => g.NumberByLength(10)).Take(Count);
            var result = g2.Select(g => g.NumberByLength(10)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_SecurityNumber_Formated_False() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());

            const int age = 20;
            var expected = g1.Select(g => g.SocialSecurityNumber(g.DateByAge(age), false)).Take(Count);
            var result = g2.Select(g => g.SocialSecurityNumber(g.DateByAge(age), false)).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_No_Seed_SecurityNumber_Formated_True() {
            var g1 = Generator.Create(new Builder());
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder());

            const int age = 20;
            var expected = g1.Select(g => g.SocialSecurityNumber(g.DateByAge(age))).Take(Count);
            var result = g2.Select(g => g.SocialSecurityNumber(g.DateByAge(age))).Take(Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_Bool() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.Bool()).Take(Count);
            var result = g2.Select(g => g.Bool()).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_CustomCollection() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.TakeElement(list)).Take(Count);
            var result = g2.Select(g => g.TakeElement(list)).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_DateByAge() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(generator => generator.DateByAge(20)).Take(Count);
            var result = g2.Select(generator => generator.DateByAge(20)).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_DateByYear() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(generator => generator.DateByAge(2000)).Take(Count);
            var result = g2.Select(generator => generator.DateByAge(2000)).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_Double_No_Arg() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Double()).Take(Count);
            var result = g1.Select(g => g.Double()).Take(Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SelectMany_Seed_Double_One_Arg() {
            const double max = 3.3;
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Double(max)).Take(Count);
            var result = g1.Select(g => g.Double(max)).Take(Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SelectMany_Seed_Double_Two_Args() {
            const double max = 3.3;
            const double min = 1.3;
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.Double(min, max)).Take(Count);
            var result = g2.Select(g => g.Double(min, max)).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_Integer_No_Arg() {
            var generator = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var generator2 = Generator.Create(new Builder(TestSeed));
            var expected = generator2.Select(g => g.Integer()).Take(Count);
            var result = generator.Select(g => g.Integer()).Take(Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SelectMany_Seed_Integer_One_Arg() {
            const int max = 100;
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Integer(max)).Take(Count);
            var result = g1.Select(g => g.Integer(max)).Take(Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SelectMany_Seed_Integer_Two_Args() {
            const int max = 100;
            const int min = 20;
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Integer(min, max)).Take(Count);
            var result = g1.Select(g => g.Integer(min, max)).Take(Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SelectMany_Seed_Long_No_Arg() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g2.Select(g => g.Long()).Take(Count);
            var result = g1.Select(g => g.Long()).Take(Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SelectMany_Seed_Long_One_Arg() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            const long max = long.MaxValue - 3923329;
            var expected = g2.Select(g => g.Long(max)).Take(Count);
            var result = g1.Select(g => g.Long(max)).Take(Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SelectMany_Seed_Long_Two_Args() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.Select(g => g.Long(min, max)).Take(Count);
            var result = g1.Select(g => g.Long(min, max)).Take(Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SelectMany_Seed_MailAddress() {
            const string name = "bob";
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(generator => generator.Mail(name)).Take(Count);
            var result = g2.Select(generator => generator.Mail(name)).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_Name_FemaleFirstName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var expected = g1.Select(g => g.FirstName(Gender.Female)).Take(Count);
            var result = g2.Select(g => g.FirstName(Gender.Female)).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_Name_FirstName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var expected = g1.Select(g => g.FirstName()).Take(Count);
            var result = g2.Select(g => g.FirstName()).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_Name_LastName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.LastName()).Take(Count);
            var result = g2.Select(g => g.LastName()).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_Name_MaleFirstName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var generateManyA = g1.Select(g => g.FirstName(Gender.Male)).Take(Count);
            var generateManyB = g2.Select(g => g.FirstName(Gender.Male)).Take(Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void SelectMany_Seed_Name_UserName() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            var generateManyA = g1.Select(g => g.UserName()).Take(Count);
            var generateManyB = g2.Select(g => g.UserName()).Take(Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void SelectMany_Seed_Params() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.TakeArgument("Foo", "Bar", "John", "Doe")).Take(Count);
            var result = g2.Select(g => g.TakeArgument("Foo", "Bar", "John", "Doe")).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_PhoneNumber() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));
            var expected = g1.Select(g => g.NumberByLength(10)).Take(Count);
            var result = g2.Select(g => g.NumberByLength(10)).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_SecurityNumber_Formated_False() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            const int age = 20;
            var expected = g1.Select(g => g.SocialSecurityNumber(g.DateByAge(age), false)).Take(Count);
            var result = g2.Select(g => g.SocialSecurityNumber(g.DateByAge(age), false)).Take(Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SelectMany_Seed_SecurityNumber_Formated_True() {
            var g1 = Generator.Create(new Builder(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = Generator.Create(new Builder(TestSeed));

            const int age = 20;
            var expected = g1.Select(g => g.SocialSecurityNumber(g.DateByAge(age))).Take(Count);
            var result = g2.Select(g => g.SocialSecurityNumber(g.DateByAge(age))).Take(Count);
            Assert.AreEqual(expected, result);
        }
    }
}