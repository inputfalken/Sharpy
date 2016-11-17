using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Generator {
    [TestFixture]
    public class String {
        private const int Count = 100;

        [Test]
        public void Any_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateMany(g => g.String(StringType.AnyName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.String(StringType.AnyName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Female_First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateMany(g => g.String(StringType.FemaleFirstName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.String(StringType.FemaleFirstName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void First_Name_Bad_Config_Throws() {
            var gen = new Sharpy.Generator {
                Countries = new[] {Country.Sweden},
                Regions = new[] {Region.CentralAmerica}
            };
            //Many
            Assert.Throws<Exception>(() => gen.GenerateMany(g => g.String(StringType.FirstName), Count).ToArray());
            //Single
            Assert.Throws<Exception>(() => gen.Generate(g => g.String(StringType.FirstName)));
        }

        [Test]
        public void First_Name_Bad_Config_Throws_Different_Config_Order() {
            var gen = new Sharpy.Generator {
                Regions = new[] {Region.CentralAmerica},
                Countries = new[] {Country.Sweden}
            };
            //Many
            Assert.Throws<Exception>(() => gen.GenerateMany(g => g.String(StringType.FirstName), Count).ToArray());
            //Single
            Assert.Throws<Exception>(() => gen.Generate(g => g.String(StringType.FirstName)));
        }

        [Test]
        public void First_Name_Good_Config_Throws() {
            var gen = new Sharpy.Generator {
                Countries = new[] {Country.Sweden},
                Regions = new[] {Region.Europe}
            };
            //Many
            Assert.DoesNotThrow(() => gen.GenerateMany(g => g.String(StringType.FirstName), Count).ToArray());
            //Single
            Assert.DoesNotThrow(() => gen.Generate(g => g.String(StringType.FirstName)));
        }

        [Test]
        public void First_Name_Good_Config_Throws_Different_Config_Order() {
            var gen = new Sharpy.Generator {
                Regions = new[] {Region.Europe},
                Countries = new[] {Country.Sweden}
            };
            //Many
            Assert.DoesNotThrow(() => gen.GenerateMany(g => g.String(StringType.FirstName), Count).ToArray());
            //Single
            Assert.DoesNotThrow(() => gen.Generate(g => g.String(StringType.FirstName)));
        }

        [Test]
        public void First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateMany(g => g.String(StringType.FirstName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.String(StringType.FirstName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Last_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateMany(g => g.String(StringType.LastName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.String(StringType.LastName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Male_First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateMany(g => g.String(StringType.MaleFirstName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.String(StringType.MaleFirstName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void User_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var userNames = gen.GenerateMany(g => g.String(StringType.UserName), Count).ToArray();
            Assert.IsFalse(userNames.All(string.IsNullOrEmpty));
            Assert.IsFalse(userNames.All(string.IsNullOrWhiteSpace));

            //Single
            var userName = gen.Generate(g => g.String(StringType.UserName));
            Assert.IsFalse(string.IsNullOrEmpty(userName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(userName));
        }
    }
}