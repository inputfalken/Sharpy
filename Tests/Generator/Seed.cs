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
        /// <para>The seed given to all Generator instance created in the tests.</para>
        /// </summary>
        private const int TestSeed = 100;

        /// <summary>
        /// <para>The ammount generated from GenrateMany Tests.</para>
        /// </summary>
        private const int Count = 100;

        [Test]
        public void Compare_Generator_Seed_Default_Default() {
            var g1 = new Sharpy.Generator();
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator();
            Assert.AreEqual(g1.Seed, g2.Seed);
        }

        [Test]
        public void Compare_Generator_Seed_Default_Set() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator();
            Assert.AreNotEqual(g1.Seed, g2.Seed);
        }

        [Test]
        public void Compare_Generator_Seed_Set_Set() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            Assert.AreEqual(g1.Seed, g2.Seed);
        }

        [Test]
        public void Generate_Bool() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.Generate(g => g.Bool());
            var result = g2.Generate(g => g.Bool());
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_DateByAge() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.Generate(g => g.DateByAge(20));
            var result = g2.Generate(g => g.DateByAge(20));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_DateByYear() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.Generate(g => g.DateByAge(2000));
            var result = g2.Generate(g => g.DateByAge(2000));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Double_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.Generate(g => g.Double());
            var result = g1.Generate(g => g.Double());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Double_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const double max = 3.3;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.Generate(g => g.Double(max));
            var result = g1.Generate(g => g.Double(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Double_Two_Args() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const double max = 3.3;
            const double min = 1.3;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.Generate(g => g.Double(min, max));
            var result = g2.Generate(g => g.Double(min, max));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_Integer_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.Generate(g => g.Integer());
            var result = g1.Generate(g => g.Integer());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Integer_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.Generate(g => g.Integer(max));
            var result = g1.Generate(g => g.Integer(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Integer_Two_Args() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            const int min = 20;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.Generate(g => g.Integer(min, max));
            var result = g1.Generate(g => g.Integer(min, max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Long_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.Generate(g => g.Long());
            var result = g1.Generate(g => g.Long());
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Long_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const long max = long.MaxValue - 3923329;
            var expected = g2.Generate(g => g.Long(max));
            var result = g1.Generate(g => g.Long(max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_Long_Two_Args() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.Generate(g => g.Long(min, max));
            var result = g1.Generate(g => g.Long(min, max));
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Generate_MailAddress() {
            const string name = "bob";
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.Generate(generator => generator.MailAddress(name));
            var result = g2.Generate(generator => generator.MailAddress(name));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_PhoneNumber() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.Generate(g => g.PhoneNumber(10));
            var result = g2.Generate(g => g.PhoneNumber(10));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_SecurityNumber_Formated_True() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            const int age = 20;
            var expected = g1.Generate(g => g.SocialSecurityNumber(g.DateByAge(age)));
            var result = g2.Generate(g => g.SocialSecurityNumber(g.DateByAge(age)));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_SecurityNumber_Formated_False() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            const int age = 20;
            var expected = g1.Generate(g => g.SocialSecurityNumber(g.DateByAge(age), false));
            var result = g2.Generate(g => g.SocialSecurityNumber(g.DateByAge(age), false));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_String_AnyName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.Generate(g => g.String(StringType.AnyName));
            var result = g2.Generate(g => g.String(StringType.AnyName));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_String_FemaleFirstName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.Generate(g => g.String(StringType.FemaleFirstName));
            var result = g2.Generate(g => g.String(StringType.FemaleFirstName));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_String_FirstName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.Generate(g => g.String(StringType.FirstName));
            var result = g2.Generate(g => g.String(StringType.FirstName));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_String_LastName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.Generate(g => g.String(StringType.LastName));
            var result = g2.Generate(g => g.String(StringType.LastName));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_String_MaleFirstName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var generateManyA = g1.Generate(g => g.String(StringType.MaleFirstName));
            var generateManyB = g2.Generate(g => g.String(StringType.MaleFirstName));
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void Generate_String_UserName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var generateManyA = g1.GenerateMany(g => g.String(StringType.UserName), Count);
            var generateManyB = g2.GenerateMany(g => g.String(StringType.UserName), Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void GenerateMany_Bool() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(g => g.Bool(), Count);
            var result = g2.GenerateMany(g => g.Bool(), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_DateByAge() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(generator => generator.DateByAge(20), Count);
            var result = g2.GenerateMany(generator => generator.DateByAge(20), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_DateByYear() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(generator => generator.DateByAge(2000), Count);
            var result = g2.GenerateMany(generator => generator.DateByAge(2000), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Double_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.GenerateMany(g => g.Double(), Count);
            var result = g1.GenerateMany(g => g.Double(), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Double_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const double max = 3.3;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.GenerateMany(g => g.Double(max), Count);
            var result = g1.GenerateMany(g => g.Double(max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Double_Two_Args() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const double max = 3.3;
            const double min = 1.3;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(g => g.Double(min, max), Count);
            var result = g2.GenerateMany(g => g.Double(min, max), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Integer_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var generator2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = generator2.GenerateMany(g => g.Integer(), Count);
            var result = generator.GenerateMany(g => g.Integer(), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Integer_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.GenerateMany(g => g.Integer(max), Count);
            var result = g1.GenerateMany(g => g.Integer(max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Integer_Two_Args() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            const int min = 20;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.GenerateMany(g => g.Integer(min, max), Count);
            var result = g1.GenerateMany(g => g.Integer(min, max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Long_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g2.GenerateMany(g => g.Long(), Count);
            var result = g1.GenerateMany(g => g.Long(), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Long_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const long max = long.MaxValue - 3923329;
            var expected = g2.GenerateMany(g => g.Long(max), Count);
            var result = g1.GenerateMany(g => g.Long(max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Long_Two_Args() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.GenerateMany(g => g.Long(min, max), Count);
            var result = g1.GenerateMany(g => g.Long(min, max), Count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_MailAddress() {
            const string name = "bob";
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(generator => generator.MailAddress(name), Count);
            var result = g2.GenerateMany(generator => generator.MailAddress(name), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_PhoneNumber() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(g => g.PhoneNumber(10), Count);
            var result = g2.GenerateMany(g => g.PhoneNumber(10), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_SecurityNumber_Formated_True() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            const int age = 20;
            var expected = g1.GenerateMany(g => g.SocialSecurityNumber(g.DateByAge(age)), Count);
            var result = g2.GenerateMany(g => g.SocialSecurityNumber(g.DateByAge(age)), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_SecurityNumber_Formated_False() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            const int age = 20;
            var expected = g1.GenerateMany(g => g.SocialSecurityNumber(g.DateByAge(age), false), Count);
            var result = g2.GenerateMany(g => g.SocialSecurityNumber(g.DateByAge(age), false), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_AnyName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.GenerateMany(g => g.String(StringType.AnyName), Count);
            var result = g2.GenerateMany(g => g.String(StringType.AnyName), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_FemaleFirstName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.GenerateMany(g => g.String(StringType.FemaleFirstName), Count);
            var result = g2.GenerateMany(g => g.String(StringType.FemaleFirstName), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_FirstName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.GenerateMany(g => g.String(StringType.FirstName), Count);
            var result = g2.GenerateMany(g => g.String(StringType.FirstName), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_LastName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(g => g.String(StringType.LastName), Count);
            var result = g2.GenerateMany(g => g.String(StringType.LastName), Count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_MaleFirstName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var generateManyA = g1.GenerateMany(g => g.String(StringType.MaleFirstName), Count);
            var generateManyB = g2.GenerateMany(g => g.String(StringType.MaleFirstName), Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void GenerateMany_String_UserName() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var generateManyA = g1.GenerateMany(g => g.String(StringType.UserName), Count);
            var generateManyB = g2.GenerateMany(g => g.String(StringType.UserName), Count);
            Assert.AreEqual(generateManyA, generateManyB);
        }
    }
}