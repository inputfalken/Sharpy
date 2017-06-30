using GeneratorAPI;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Implementation;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class GeneratorExtensionTests {
        [Test]
        public void Username_Seed_Arg_Not_Null() {
            var generator = Generator.Factory.Username(20);
            Assert.IsNotNull(generator);
        }

        [Test]
        public void Username_No_Arg_Not_Null() {
            var generator = Generator.Factory.Username();
            Assert.IsNotNull(generator);
        }

        [Test]
        public void FirstName_No_Arg_Is_Not_Null() {
            var generator = Generator.Factory.FirstName();
            Assert.IsNotNull(generator);
        }

        [Test]
        public void Username_Seed_Arg_Not_Generation_Is_Not_Null_Or_Whitespace() {
            var username = Generator.Factory.Username(20);
            Assert.IsFalse(string.IsNullOrWhiteSpace(username.Generate()));
        }

        [Test]
        public void Username_No_Arg_Not_Generation_Is_Not_Null_Or_Whitespace() {
            var username = Generator.Factory.Username();
            Assert.IsFalse(string.IsNullOrWhiteSpace(username.Generate()));
        }

        [Test]
        public void FirstName_No_Arg_Is_Not_Generation_Is_Not_Null_Or_Whitespace() {
            var firstName = Generator.Factory.FirstName();
            Assert.IsFalse(string.IsNullOrWhiteSpace(firstName.Generate()));
        }

        [Test]
        public void FirstName_Gender_Arg_Is_Not_Generation_Is_Not_Null_Or_Whitespace() {
            var firstName = Generator.Factory.FirstName(Gender.Female);
            Assert.IsFalse(string.IsNullOrWhiteSpace(firstName.Generate()));
        }

        [Test]
        public void FirstName_INameProvider_Arg_Is_Not_Generation_Is_Not_Null_Or_Whitespace() {
            var firstName = Generator.Factory.FirstName(new NameByOrigin());
            Assert.IsFalse(string.IsNullOrWhiteSpace(firstName.Generate()));
        }

        [Test]
        public void FirstName_No_Arg_Is_Not_Null_() {
            var generator = Generator.Factory.FirstName();
            Assert.IsNotNull(generator);
        }

        [Test]
        public void FirstName_Gender_Arg_Is_Not_Null_() {
            var generator = Generator.Factory.FirstName(Gender.Female);
            Assert.IsNotNull(generator);
        }

        [Test]
        public void FirstName_INameProvider_Arg_Is_Not_Null() {
            var generator = Generator.Factory.FirstName(new NameByOrigin());
            Assert.IsNotNull(generator);
        }
    }
}