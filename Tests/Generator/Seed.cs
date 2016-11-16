using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Generator {
    /// <summary>
    ///     <para>These tests check that the result will be the same when setting a seed.</para>
    ///     <para>Thread.Sleep is for making sure a new seed would be given if the Seed given does not work.</para>
    /// </summary>
    [TestFixture]
    public class Seed {
        private const int TestSeed = 100;

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
        public void Generate_DateByAge() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.Generate(generator => generator.DateByAge(20));
            var result = g2.Generate(generator => generator.DateByAge(20));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_DateByYear() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.Generate(generator => generator.DateByAge(2000));
            var result = g2.Generate(generator => generator.DateByAge(2000));
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

            var generateManyA = g1.GenerateMany(g => g.String(StringType.UserName));
            var generateManyB = g2.GenerateMany(g => g.String(StringType.UserName));
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void GenerateMany_Bool() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(g => g.Bool());
            var result = g2.GenerateMany(g => g.Bool());
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Double_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const int count = 1000;
            var expected = g2.GenerateMany(g => g.Double(), count);
            var result = g1.GenerateMany(g => g.Double(), count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Double_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const double max = 3.3;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const int count = 1000;
            var expected = g2.GenerateMany(g => g.Double(max), count);
            var result = g1.GenerateMany(g => g.Double(max), count);
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
            const int count = 1000;
            var expected = g1.GenerateMany(g => g.Double(min, max), count);
            var result = g2.GenerateMany(g => g.Double(min, max), count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_Integer_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var generator = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var generator2 = new Sharpy.Generator {Seed = TestSeed};
            const int count = 1000;
            var expected = generator2.GenerateMany(g => g.Integer(), count);
            var result = generator.GenerateMany(g => g.Integer(), count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Integer_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            const int max = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const int count = 1000;
            var expected = g2.GenerateMany(g => g.Integer(max), count);
            var result = g1.GenerateMany(g => g.Integer(max), count);
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
            const int count = 1000;
            var expected = g2.GenerateMany(g => g.Integer(min, max), count);
            var result = g1.GenerateMany(g => g.Integer(min, max), count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Long_No_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const int count = 1000;
            var expected = g2.GenerateMany(g => g.Long(), count);
            var result = g1.GenerateMany(g => g.Long(), count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Long_One_Arg() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const int count = 1000;
            const long max = long.MaxValue - 3923329;
            var expected = g2.GenerateMany(g => g.Long(max), count);
            var result = g1.GenerateMany(g => g.Long(max), count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_Long_Two_Args() {
            //This test will make sure that the generator does not do anything with the Random type. and that i get the numbers expected
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            const int count = 1000;
            const long max = long.MaxValue - 3923329;
            const long min = long.MinValue + 3923329;
            var expected = g2.GenerateMany(g => g.Long(min, max), count);
            var result = g1.GenerateMany(g => g.Long(min, max), count);
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GenerateMany_DateByAge() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(generator => generator.DateByAge(20));
            var result = g2.GenerateMany(generator => generator.DateByAge(20));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_DateByYear() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(generator => generator.DateByAge(2000));
            var result = g2.GenerateMany(generator => generator.DateByAge(2000));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_PhoneNumber() {
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(g => g.PhoneNumber(10));
            var result = g2.GenerateMany(g => g.PhoneNumber(10));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_SecurityNumber_Formated_True() {
            const int count = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            const int age = 20;
            var expected = g1.GenerateMany(g => g.SocialSecurityNumber(g.DateByAge(age)), count);
            var result = g2.GenerateMany(g => g.SocialSecurityNumber(g.DateByAge(age)), count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_AnyName() {
            const int count = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.GenerateMany(g => g.String(StringType.AnyName), count);
            var result = g2.GenerateMany(g => g.String(StringType.AnyName), count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_FemaleFirstName() {
            const int count = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.GenerateMany(g => g.String(StringType.FemaleFirstName), count);
            var result = g2.GenerateMany(g => g.String(StringType.FemaleFirstName), count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_FirstName() {
            const int count = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var expected = g1.GenerateMany(g => g.String(StringType.FirstName), count);
            var result = g2.GenerateMany(g => g.String(StringType.FirstName), count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_LastName() {
            const int count = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};
            var expected = g1.GenerateMany(g => g.String(StringType.LastName), count);
            var result = g2.GenerateMany(g => g.String(StringType.LastName), count);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GenerateMany_String_MaleFirstName() {
            const int count = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var generateManyA = g1.GenerateMany(g => g.String(StringType.MaleFirstName), count);
            var generateManyB = g2.GenerateMany(g => g.String(StringType.MaleFirstName), count);
            Assert.AreEqual(generateManyA, generateManyB);
        }

        [Test]
        public void GenerateMany_String_UserName() {
            const int count = 100;
            var g1 = new Sharpy.Generator {Seed = TestSeed};
            Thread.Sleep(20);
            var g2 = new Sharpy.Generator {Seed = TestSeed};

            var generateManyA = g1.GenerateMany(g => g.String(StringType.UserName), count);
            var generateManyB = g2.GenerateMany(g => g.String(StringType.UserName), count);
            Assert.AreEqual(generateManyA, generateManyB);
        }
    }
}