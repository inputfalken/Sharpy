using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Implementation;

namespace Tests.Generator {
    [TestFixture]
    public class Name {
        private const int Count = 100;

        [Test]
        public void All_Origins_Are_Supported() {
            var values = Enum.GetValues(typeof(Origin));
            foreach (var value in values)
                Assert.DoesNotThrow(() => {
                    Configurement configurement = new Configurement {
                        NameProvider = new NameByOrigin((Origin) value)
                    };
                    new Sharpy.Generator(configurement).Generate(g => g.FirstName());
                });
        }


        [Test]
        public void Female_First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateSequence(g => g.FirstName(Gender.Female), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.FirstName(Gender.Female));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateSequence(g => g.FirstName(), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.FirstName());
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Last_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateSequence(g => g.LastName(), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.LastName());
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Male_First_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
            //Many
            var names = gen.GenerateSequence(g => g.FirstName(Gender.Male), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.FirstName(Gender.Male));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void User_Name_Not_Null_Or_White_Space() {
            var gen = new Sharpy.Generator();
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