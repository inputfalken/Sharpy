using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Generator {
    [TestFixture]
    public class Name {
        private const int Count = 100;

        [Test]
        public void Any_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator(new Random());
            //Many
            var names = gen.GenerateSequence(g => g.Name(NameType.Any), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.Name(NameType.Any));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Female_First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator(new Random());
            //Many
            var names = gen.GenerateSequence(g => g.Name(NameType.FemaleFirstName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.Name(NameType.FemaleFirstName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator(new Random());
            //Many
            var names = gen.GenerateSequence(g => g.Name(NameType.FirstName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.Name(NameType.FirstName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Last_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator(new Random());
            //Many
            var names = gen.GenerateSequence(g => g.Name(NameType.LastName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.Name(NameType.LastName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Male_First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator(new Random());
            //Many
            var names = gen.GenerateSequence(g => g.Name(NameType.MaleFirstName), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.Name(NameType.MaleFirstName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void User_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator(new Random());
            //Many
            var userNames = gen.GenerateSequence(g => g.UserName(), Count).ToArray();
            Assert.IsFalse(userNames.All(string.IsNullOrEmpty));
            Assert.IsFalse(userNames.All(string.IsNullOrWhiteSpace));

            //Single
            var userName = gen.Generate(g => g.UserName());
            Assert.IsFalse(string.IsNullOrEmpty(userName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(userName));
        }
    }
}