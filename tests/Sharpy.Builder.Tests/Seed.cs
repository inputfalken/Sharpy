using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Sharpy.Builder.Enums;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests {
    /// <summary>
    ///     <para>These tests check that the result will be the same when setting a seed.</para>
    ///     <para>Thread.Sleep is for making sure a new seed would be given if the Seed given does not work.</para>
    /// </summary>
    [TestFixture]
    public class Seed {
        private static DateTime TrimMilliseconds(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
        }

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
        public void No_Seed_Bool() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.Bool());
            var result = Enumerable.Range(0, Count).Select(i => g2.Bool());
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_CustomCollection() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.FromList(list));
            var result = Enumerable.Range(0, Count).Select(i => g2.FromList(list));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_DateByAge() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.DateTimeByAge(20));
            var result = Enumerable.Range(0, Count).Select(i => g2.DateTimeByAge(20));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_DateByYear() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.DateTimeByAge(2000));
            var result = Enumerable.Range(0, Count).Select(i => g2.DateTimeByAge(2000));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_Double_No_Arg() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g2.Double());
            var result = Enumerable.Range(0, Count).Select(i => g1.Double());
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void No_Seed_Double_One_Arg() {
            const double max = 3.3;
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g2.Double(max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Double(max));
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void No_Seed_Double_Two_Args() {
            const double max = 3.3;
            const double min = 1.3;
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.Double(min, max));
            var result = Enumerable.Range(0, Count).Select(i => g2.Double(min, max));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_Integer_No_Arg() {
            var generator = new Builder();
            Thread.Sleep(SleepDuration);
            var generator2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => generator2.Integer());
            var result = Enumerable.Range(0, Count).Select(i => generator.Integer());
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void No_Seed_Integer_One_Arg() {
            const int max = 100;
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g2.Integer(max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Integer(max));
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void No_Seed_Integer_Two_Args() {
            const int max = 100;
            const int min = 20;
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g2.Integer(min, max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Integer(min, max));
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void No_Seed_Long_No_Arg() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g2.Long());
            var result = Enumerable.Range(0, Count).Select(i => g1.Long());
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void No_Seed_Long_One_Arg() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            const long max = long.MaxValue - 3923329;
            var expected = Enumerable.Range(0, Count).Select(i => g2.Long(max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Long(max));
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void No_Seed_Long_Two_Args() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = Enumerable.Range(0, Count).Select(i => g2.Long(min, max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Long(min, max));
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void No_Seed_MailAddress() {
            const string name = "bob";
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.Mail(name));
            var result = Enumerable.Range(0, Count).Select(i => g2.Mail(name));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_Name_FemaleFirstName() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();

            var expected = Enumerable.Range(0, Count).Select(i => g1.FirstName(Gender.Female));
            var result = Enumerable.Range(0, Count).Select(i => g2.FirstName(Gender.Male));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_Name_FirstName() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();

            var expected = Enumerable.Range(0, Count).Select(i => g1.FirstName());
            var result = Enumerable.Range(0, Count).Select(i => g2.FirstName());
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_Name_LastName() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.LastName());
            var result = Enumerable.Range(0, Count).Select(i => g2.LastName());
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_Name_MaleFirstName() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();

            var generateManyA = Enumerable.Range(0, Count).Select(i => g1.FirstName(Gender.Male));
            var generateManyB = Enumerable.Range(0, Count).Select(i => g2.FirstName(Gender.Male));
            Assert.AreNotEqual(generateManyA, generateManyB);
        }

        [Test]
        public void No_Seed_Name_UserName() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();

            var generateManyA = Enumerable.Range(0, Count).Select(i => g1.UserName());
            var generateManyB = Enumerable.Range(0, Count).Select(i => g2.UserName());
            Assert.AreNotEqual(generateManyA, generateManyB);
        }

        [Test]
        public void No_Seed_Params() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.FromList(list));
            var result = Enumerable.Range(0, Count).Select(i => g2.FromList(list));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_PhoneNumber() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.PhoneNumber(10));
            var result = Enumerable.Range(0, Count).Select(i => g2.PhoneNumber(10));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_SecurityNumber_Formated_False() {
            var c1 = new Configurement();
            c1.SecurityNumberProvider = new UniqueSecurityBuilder(c1.Random);
            var g1 = new Builder(c1);
            Thread.Sleep(SleepDuration);
            var c2 = new Configurement();
            c2.SecurityNumberProvider = new UniqueSecurityBuilder(c2.Random);
            var g2 = new Builder(c2);

            const int age = 20;
            var expected = Enumerable.Range(0, Count).Select(i => g1.DateTimeByAge(age));
            var result = Enumerable.Range(0, Count).Select(i => g2.DateTimeByAge(age));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void No_Seed_SecurityNumber_Formated_True() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();

            const int age = 20;
            var expected = Enumerable.Range(0, Count).Select(i => g1.DateTimeByAge(age));
            var result = Enumerable.Range(0, Count).Select(i => g2.DateTimeByAge(age));
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void Seed_Bool() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.Bool());
            var result = Enumerable.Range(0, Count).Select(i => g2.Bool());
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_CustomCollection() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.FromList(list));
            var result = Enumerable.Range(0, Count).Select(i => g2.FromList(list));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_DateByAge() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.DateTimeByAge(20)).Select(TrimMilliseconds);
            var result = Enumerable.Range(0, Count).Select(i => g2.DateTimeByAge(20)).Select(TrimMilliseconds);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_DateByYear() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.DateTimeByAge(2000)).Select(TrimMilliseconds);
            var result = Enumerable.Range(0, Count).Select(i => g2.DateTimeByAge(2000)).Select(TrimMilliseconds);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_Double_No_Arg() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g2.Double());
            var result = Enumerable.Range(0, Count).Select(i => g1.Double());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Seed_Double_One_Arg() {
            const double max = 3.3;
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g2.Double(max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Double(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Seed_Double_Two_Args() {
            const double max = 3.3;
            const double min = 1.3;
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.Double(min, max));
            var result = Enumerable.Range(0, Count).Select(i => g2.Double(min, max));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_Integer_No_Arg() {
            var generator = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var generator2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => generator2.Integer());
            var result = Enumerable.Range(0, Count).Select(i => generator.Integer());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Seed_Integer_One_Arg() {
            const int max = 100;
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g2.Integer(max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Integer(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Seed_Integer_Two_Args() {
            const int max = 100;
            const int min = 20;
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g2.Integer(min, max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Integer(min, max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Seed_Long_No_Arg() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g2.Long());
            var result = Enumerable.Range(0, Count).Select(i => g1.Long());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Seed_Long_One_Arg() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            const long max = long.MaxValue - 3923329;
            var expected = Enumerable.Range(0, Count).Select(i => g2.Long(max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Long(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Seed_Long_Two_Args() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = Enumerable.Range(0, Count).Select(i => g2.Long(min, max));
            var result = Enumerable.Range(0, Count).Select(i => g1.Long(min, max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Seed_MailAddress() {
            const string name = "bob";
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.Mail(name));
            var result = Enumerable.Range(0, Count).Select(i => g2.Mail(name));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_Name_FemaleFirstName() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);

            var expected = Enumerable.Range(0, Count).Select(i => g1.FirstName(Gender.Female));
            var result = Enumerable.Range(0, Count).Select(i => g2.FirstName(Gender.Female));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_Name_FirstName() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);

            var expected = Enumerable.Range(0, Count).Select(i => g1.FirstName());
            var result = Enumerable.Range(0, Count).Select(i => g2.FirstName());
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_Name_LastName() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.LastName());
            var result = Enumerable.Range(0, Count).Select(i => g2.LastName());
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_Name_MaleFirstName() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);

            var generateManyA = Enumerable.Range(0, Count).Select(i => g1.FirstName(Gender.Male));
            var generateManyB = Enumerable.Range(0, Count).Select(i => g2.FirstName(Gender.Male));
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void Seed_Name_UserName() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);

            var generateManyA = Enumerable.Range(0, Count).Select(i => g1.UserName());
            var generateManyB = Enumerable.Range(0, Count).Select(i => g2.UserName());
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void Seed_Params() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.Argument("Foo", "Bar", "John", "Doe"));
            var result = Enumerable.Range(0, Count).Select(i => g2.Argument("Foo", "Bar", "John", "Doe"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_PhoneNumber() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.PhoneNumber(10));
            var result = Enumerable.Range(0, Count).Select(i => g2.PhoneNumber(10));
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void No_Seed_DateTime() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.TimeSpan(TimeSpan.Zero, TimeSpan.MaxValue));
            var result = Enumerable.Range(0, Count).Select(i => g2.TimeSpan(TimeSpan.Zero, TimeSpan.MaxValue));
            Assert.AreNotEqual(expected, result);
        }
        [Test]
        public void Seed_DateTime() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.DateTime(DateTime.Now, DateTime.MaxValue));
            var result = Enumerable.Range(0, Count).Select(i => g2.DateTime(DateTime.Now, DateTime.MaxValue));
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void No_Seed_TimeSpan() {
            var g1 = new Builder();
            Thread.Sleep(SleepDuration);
            var g2 = new Builder();
            var expected = Enumerable.Range(0, Count).Select(i => g1.TimeSpan(TimeSpan.Zero, TimeSpan.MaxValue));
            var result = Enumerable.Range(0, Count).Select(i => g2.TimeSpan(TimeSpan.Zero, TimeSpan.MaxValue));
            Assert.AreNotEqual(expected, result);
        }
        [Test]
        public void Seed_TimeSpan() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);
            var expected = Enumerable.Range(0, Count).Select(i => g1.TimeSpan(TimeSpan.Zero, TimeSpan.MaxValue));
            var result = Enumerable.Range(0, Count).Select(i => g2.TimeSpan(TimeSpan.Zero, TimeSpan.MaxValue));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_SecurityNumber_Formated_False() {
            var c1 = new Configurement(TestSeed);
            c1.SecurityNumberProvider = new UniqueSecurityBuilder(c1.Random);
            var g1 = new Builder(c1);
            Thread.Sleep(SleepDuration);
            var c2 = new Configurement(TestSeed);
            c2.SecurityNumberProvider = new UniqueSecurityBuilder(c2.Random);
            var g2 = new Builder(c2);

            const int age = 20;
            var expected = Enumerable.Range(0, Count).Select(i => TrimMilliseconds(g1.DateTimeByAge(age)));
            var result = Enumerable.Range(0, Count).Select(i => TrimMilliseconds(g2.DateTimeByAge(age)));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Seed_SecurityNumber_Formated_True() {
            var g1 = new Builder(TestSeed);
            Thread.Sleep(SleepDuration);
            var g2 = new Builder(TestSeed);

            const int age = 20;
            var expected = Enumerable.Range(0, Count).Select(i => TrimMilliseconds(g1.DateTimeByAge(age)));
            var result = Enumerable.Range(0, Count).Select(i => TrimMilliseconds(g2.DateTimeByAge(age)));
            Assert.AreEqual(expected, result);
        }
    }
}