using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Generator {
    /// <summary>
    ///     <para>These tests check that the result will be the same when setting a seed.</para>
    ///     <para>Thread.Sleep is for making sure a new seed would be given if the Seed given does not work.</para>
    /// </summary>
    [TestFixture]
    public class Seed {
        /// <summary>
        ///     <para>The seed given to all Generator instance created in the tests.</para>
        /// </summary>
        private const int TestSeed = 100;

        /// <summary>
        ///     <para>The ammount generated from GenrateMany Tests.</para>
        /// </summary>
        private const int Count = 100;

        /// <summary>
        ///     <para>The duration of Thread.Sleep() on all tests.</para>
        /// </summary>
        private const int SleepDuration = 20;

        [Test]
        public void Compare_Generator_Seed_Default_Default() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            Assert.AreEqual(g1.Seed, g2.Seed);
        }

        [Test]
        public void Compare_Generator_Seed_Default_Set() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            Assert.AreNotEqual(g1.Seed, g2.Seed);
        }

        [Test]
        public void Compare_Generator_Seed_Set_Set() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            Assert.AreEqual(g1.Seed, g2.Seed);
        }


        [Test]
        public void Generate_Seed_Bool() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.Generate(g => g.Bool());
            var result = g2.Generate(g => g.Bool());
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Generate_Seed_CustomCollection() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.Generate(g => g.CustomCollection(list));
            var result = g2.Generate(g => g.CustomCollection(list));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_DateByAge() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.Generate(g => g.DateByAge(20));
            var result = g2.Generate(g => g.DateByAge(20));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_DateByYear() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.Generate(g => g.DateByAge(2000));
            var result = g2.Generate(g => g.DateByAge(2000));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_Double_No_Arg() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.Generate(g => g.Double());
            var result = g1.Generate(g => g.Double());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Seed_Double_One_Arg() {
            const double max = 3.3;
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.Generate(g => g.Double(max));
            var result = g1.Generate(g => g.Double(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Seed_Double_Two_Args() {
            const double max = 3.3;
            const double min = 1.3;
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.Generate(g => g.Double(min, max));
            var result = g2.Generate(g => g.Double(min, max));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_Integer_No_Arg() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.Generate(g => g.Integer());
            var result = g1.Generate(g => g.Integer());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Seed_Integer_One_Arg() {
            const int max = 100;
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.Generate(g => g.Integer(max));
            var result = g1.Generate(g => g.Integer(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Seed_Integer_Two_Args() {
            const int max = 100;
            const int min = 20;
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.Generate(g => g.Integer(min, max));
            var result = g1.Generate(g => g.Integer(min, max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Seed_Long_No_Arg() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.Generate(g => g.Long());
            var result = g1.Generate(g => g.Long());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Seed_Long_One_Arg() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            const long max = long.MaxValue - 3923329;
            var expected = g2.Generate(g => g.Long(max));
            var result = g1.Generate(g => g.Long(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Seed_Long_Two_Args() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.Generate(g => g.Long(min, max));
            var result = g1.Generate(g => g.Long(min, max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Seed_MailAddress() {
            const string name = "bob";
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.Generate(generator => generator.MailAddress(name));
            var result = g2.Generate(generator => generator.MailAddress(name));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_Name_AnyName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var expected = g1.Generate(g => g.Name(NameType.Any));
            var result = g2.Generate(g => g.Name(NameType.Any));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_Name_FemaleFirstName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var expected = g1.Generate(g => g.Name(NameType.FemaleFirstName));
            var result = g2.Generate(g => g.Name(NameType.FemaleFirstName));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_Name_FirstName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var expected = g1.Generate(g => g.Name(NameType.FirstName));
            var result = g2.Generate(g => g.Name(NameType.FirstName));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_Name_LastName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var expected = g1.Generate(g => g.Name(NameType.LastName));
            var result = g2.Generate(g => g.Name(NameType.LastName));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_Name_MaleFirstName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var generateManyA = g1.Generate(g => g.Name(NameType.MaleFirstName));
            var generateManyB = g2.Generate(g => g.Name(NameType.MaleFirstName));
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void Generate_Seed_Name_UserName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var generateManyA = g1.GenerateSequence(g => g.String(), Count);
            var generateManyB = g2.GenerateSequence(g => g.String(), Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void Generate_Seed_Params() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.Generate(g => g.Params("Foo", "Bar", "John", "Doe"));
            var result = g2.Generate(g => g.Params("Foo", "Bar", "John", "Doe"));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_PhoneNumber() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.Generate(g => g.NumberByLength(10));
            var result = g2.Generate(g => g.NumberByLength(10));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_SecurityNumber_Formated_False() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            const int age = 20;
            var expected = g1.Generate(g => g.SocialSecurityNumber(g.DateByAge(age), false));
            var result = g2.Generate(g => g.SocialSecurityNumber(g.DateByAge(age), false));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Seed_SecurityNumber_Formated_True() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            const int age = 20;
            var expected = g1.Generate(g => g.SocialSecurityNumber(g.DateByAge(age)));
            var result = g2.Generate(g => g.SocialSecurityNumber(g.DateByAge(age)));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_Bool() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(g => g.Bool(), Count);
            var result = g2.GenerateSequence(g => g.Bool(), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_CustomCollection() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(g => g.CustomCollection(list), Count);
            var result = g2.GenerateSequence(g => g.CustomCollection(list), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_DateByAge() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(generator => generator.DateByAge(20), Count);
            var result = g2.GenerateSequence(generator => generator.DateByAge(20), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_DateByYear() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(generator => generator.DateByAge(2000), Count);
            var result = g2.GenerateSequence(generator => generator.DateByAge(2000), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_Double_No_Arg() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g2.GenerateSequence(g => g.Double(), Count);
            var result = g1.GenerateSequence(g => g.Double(), Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void GenerateMany_No_Seed_Double_One_Arg() {
            const double max = 3.3;
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g2.GenerateSequence(g => g.Double(max), Count);
            var result = g1.GenerateSequence(g => g.Double(max), Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void GenerateMany_No_Seed_Double_Two_Args() {
            const double max = 3.3;
            const double min = 1.3;
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(g => g.Double(min, max), Count);
            var result = g2.GenerateSequence(g => g.Double(min, max), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_Integer_No_Arg() {
            var generator = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var generator2 = new Sharpy.Generator();
            var expected = generator2.GenerateSequence(g => g.Integer(), Count);
            var result = generator.GenerateSequence(g => g.Integer(), Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void GenerateMany_No_Seed_Integer_One_Arg() {
            const int max = 100;
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g2.GenerateSequence(g => g.Integer(max), Count);
            var result = g1.GenerateSequence(g => g.Integer(max), Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void GenerateMany_No_Seed_Integer_Two_Args() {
            const int max = 100;
            const int min = 20;
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g2.GenerateSequence(g => g.Integer(min, max), Count);
            var result = g1.GenerateSequence(g => g.Integer(min, max), Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void GenerateMany_No_Seed_Long_No_Arg() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g2.GenerateSequence(g => g.Long(), Count);
            var result = g1.GenerateSequence(g => g.Long(), Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void GenerateMany_No_Seed_Long_One_Arg() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            const long max = long.MaxValue - 3923329;
            var expected = g2.GenerateSequence(g => g.Long(max), Count);
            var result = g1.GenerateSequence(g => g.Long(max), Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void GenerateMany_No_Seed_Long_Two_Args() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.GenerateSequence(g => g.Long(min, max), Count);
            var result = g1.GenerateSequence(g => g.Long(min, max), Count);
            Assert.AreNotEqual(result, expected);
        }

        [Test]
        public void GenerateMany_No_Seed_MailAddress() {
            const string name = "bob";
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(generator => generator.MailAddress(name), Count);
            var result = g2.GenerateSequence(generator => generator.MailAddress(name), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_Name_AnyName() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();

            var expected = g1.GenerateSequence(g => g.Name(NameType.Any), Count);
            var result = g2.GenerateSequence(g => g.Name(NameType.Any), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_Name_FemaleFirstName() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();

            var expected = g1.GenerateSequence(g => g.Name(NameType.FemaleFirstName), Count);
            var result = g2.GenerateSequence(g => g.Name(NameType.FemaleFirstName), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_Name_FirstName() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();

            var expected = g1.GenerateSequence(g => g.Name(NameType.FirstName), Count);
            var result = g2.GenerateSequence(g => g.Name(NameType.FirstName), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_Name_LastName() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(g => g.Name(NameType.LastName), Count);
            var result = g2.GenerateSequence(g => g.Name(NameType.LastName), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_Name_MaleFirstName() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();

            var generateManyA = g1.GenerateSequence(g => g.Name(NameType.MaleFirstName), Count);
            var generateManyB = g2.GenerateSequence(g => g.Name(NameType.MaleFirstName), Count);
            Assert.AreNotEqual(generateManyA, generateManyB);
        }

        [Test]
        public void GenerateMany_No_Seed_Name_UserName() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();

            var generateManyA = g1.GenerateSequence(g => g.String(), Count);
            var generateManyB = g2.GenerateSequence(g => g.String(), Count);
            Assert.AreNotEqual(generateManyA, generateManyB);
        }

        [Test]
        public void GenerateMany_No_Seed_Params() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(g => g.CustomCollection(list), Count);
            var result = g2.GenerateSequence(g => g.CustomCollection(list), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_PhoneNumber() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();
            var expected = g1.GenerateSequence(g => g.NumberByLength(10), Count);
            var result = g2.GenerateSequence(g => g.NumberByLength(10), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_SecurityNumber_Formated_False() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();

            const int age = 20;
            var expected = g1.GenerateSequence(g => g.SocialSecurityNumber(g.DateByAge(age), false), Count);
            var result = g2.GenerateSequence(g => g.SocialSecurityNumber(g.DateByAge(age), false), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_No_Seed_SecurityNumber_Formated_True() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator();

            const int age = 20;
            var expected = g1.GenerateSequence(g => g.SocialSecurityNumber(g.DateByAge(age)), Count);
            var result = g2.GenerateSequence(g => g.SocialSecurityNumber(g.DateByAge(age)), Count);
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_Bool() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(g => g.Bool(), Count);
            var result = g2.GenerateSequence(g => g.Bool(), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_CustomCollection() {
            var list = new List<string> {"Foo", "Bar", "John", "Doe"};
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(g => g.CustomCollection(list), Count);
            var result = g2.GenerateSequence(g => g.CustomCollection(list), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_DateByAge() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(generator => generator.DateByAge(20), Count);
            var result = g2.GenerateSequence(generator => generator.DateByAge(20), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_DateByYear() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(generator => generator.DateByAge(2000), Count);
            var result = g2.GenerateSequence(generator => generator.DateByAge(2000), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_Double_No_Arg() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.GenerateSequence(g => g.Double(), Count);
            var result = g1.GenerateSequence(g => g.Double(), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Seed_Double_One_Arg() {
            const double max = 3.3;
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.GenerateSequence(g => g.Double(max), Count);
            var result = g1.GenerateSequence(g => g.Double(max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Seed_Double_Two_Args() {
            const double max = 3.3;
            const double min = 1.3;
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(g => g.Double(min, max), Count);
            var result = g2.GenerateSequence(g => g.Double(min, max), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_Integer_No_Arg() {
            var generator = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var generator2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = generator2.GenerateSequence(g => g.Integer(), Count);
            var result = generator.GenerateSequence(g => g.Integer(), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Seed_Integer_One_Arg() {
            const int max = 100;
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.GenerateSequence(g => g.Integer(max), Count);
            var result = g1.GenerateSequence(g => g.Integer(max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Seed_Integer_Two_Args() {
            const int max = 100;
            const int min = 20;
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.GenerateSequence(g => g.Integer(min, max), Count);
            var result = g1.GenerateSequence(g => g.Integer(min, max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Seed_Long_No_Arg() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g2.GenerateSequence(g => g.Long(), Count);
            var result = g1.GenerateSequence(g => g.Long(), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Seed_Long_One_Arg() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            const long max = long.MaxValue - 3923329;
            var expected = g2.GenerateSequence(g => g.Long(max), Count);
            var result = g1.GenerateSequence(g => g.Long(max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Seed_Long_Two_Args() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.GenerateSequence(g => g.Long(min, max), Count);
            var result = g1.GenerateSequence(g => g.Long(min, max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Seed_MailAddress() {
            const string name = "bob";
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(generator => generator.MailAddress(name), Count);
            var result = g2.GenerateSequence(generator => generator.MailAddress(name), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_Name_AnyName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var expected = g1.GenerateSequence(g => g.Name(NameType.Any), Count);
            var result = g2.GenerateSequence(g => g.Name(NameType.Any), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_Name_FemaleFirstName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var expected = g1.GenerateSequence(g => g.Name(NameType.FemaleFirstName), Count);
            var result = g2.GenerateSequence(g => g.Name(NameType.FemaleFirstName), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_Name_FirstName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var expected = g1.GenerateSequence(g => g.Name(NameType.FirstName), Count);
            var result = g2.GenerateSequence(g => g.Name(NameType.FirstName), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_Name_LastName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(g => g.Name(NameType.LastName), Count);
            var result = g2.GenerateSequence(g => g.Name(NameType.LastName), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_Name_MaleFirstName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var generateManyA = g1.GenerateSequence(g => g.Name(NameType.MaleFirstName), Count);
            var generateManyB = g2.GenerateSequence(g => g.Name(NameType.MaleFirstName), Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void GenerateMany_Seed_Name_UserName() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            var generateManyA = g1.GenerateSequence(g => g.String(), Count);
            var generateManyB = g2.GenerateSequence(g => g.String(), Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void GenerateMany_Seed_Params() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(g => g.Params("Foo", "Bar", "John", "Doe"), Count);
            var result = g2.GenerateSequence(g => g.Params("Foo", "Bar", "John", "Doe"), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_PhoneNumber() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));
            var expected = g1.GenerateSequence(g => g.NumberByLength(10), Count);
            var result = g2.GenerateSequence(g => g.NumberByLength(10), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_SecurityNumber_Formated_False() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            const int age = 20;
            var expected = g1.GenerateSequence(g => g.SocialSecurityNumber(g.DateByAge(age), false), Count);
            var result = g2.GenerateSequence(g => g.SocialSecurityNumber(g.DateByAge(age), false), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Seed_SecurityNumber_Formated_True() {
            var g1 = new Sharpy.Generator(new Random(TestSeed));
            Thread.Sleep(SleepDuration);
            var g2 = new Sharpy.Generator(new Random(TestSeed));

            const int age = 20;
            var expected = g1.GenerateSequence(g => g.SocialSecurityNumber(g.DateByAge(age)), Count);
            var result = g2.GenerateSequence(g => g.SocialSecurityNumber(g.DateByAge(age)), Count);
            Assert.AreEqual(expected, result);
        }
    }
}