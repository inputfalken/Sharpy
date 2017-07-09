using GeneratorAPI;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.IProviders;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class GeneratorExtensionTests {
        [Test]
        public void FirstName_Gender_Arg_Generation_Is_Not_Null_Or_Whitespace() {
            var firstName = GeneratorFactory.FirstName(Gender.Female);
            Assert.IsFalse(string.IsNullOrWhiteSpace(firstName.Generate()));
        }

        [Test]
        public void FirstName_Gender_Arg_Is_Not_Null_() {
            var generator = GeneratorFactory.FirstName(Gender.Female);
            Assert.IsNotNull(generator);
        }

        [Test]
        public void FirstName_INameProvider_Arg_Generation_Is_Not_Null_Or_Whitespace() {
            var firstName = GeneratorFactory.FirstName();
            Assert.IsFalse(string.IsNullOrWhiteSpace(firstName.Generate()));
        }

        [Test]
        public void FirstName_INameProvider_Arg_Is_Not_Null() {
            var generator = GeneratorFactory.FirstName();
            Assert.IsNotNull(generator);
        }

        [Test]
        public void FirstName_No_Arg_Generation_Is_Not_Null_Or_Whitespace() {
            var firstName = GeneratorFactory.FirstName();
            Assert.IsFalse(string.IsNullOrWhiteSpace(firstName.Generate()));
        }

        [Test]
        public void FirstName_No_Arg_Is_Not_Null() {
            var generator = GeneratorFactory.FirstName();
            Assert.IsNotNull(generator);
        }

        [Test]
        public void FirstName_No_Arg_Is_Not_Null_() {
            var generator = GeneratorFactory.FirstName();
            Assert.IsNotNull(generator);
        }

        [Test]
        public void LastName_INameProvider_Arg_Generation_Is_Not_Null_Or_Whitespace() {
            var generator = GeneratorFactory.LastName();
            Assert.IsFalse(string.IsNullOrWhiteSpace(generator.Generate()));
        }

        [Test]
        public void LastName_INameProvider_Arg_Is_Not_Null() {
            var generator = GeneratorFactory.LastName(new NameByOrigin());
            Assert.IsNotNull(generator);
        }

        [Test]
        public void LastName_No_Arg_Is_Not_Null() {
            var generator = GeneratorFactory.LastName();
            Assert.IsNotNull(generator);
        }

        [Test]
        public void LastName_No_INameProvider_Arg_Generation_Is_Not_Null_Or_Whitespace() {
            var generator = GeneratorFactory.LastName();
            Assert.IsFalse(string.IsNullOrWhiteSpace(generator.Generate()));
        }

        [Test]
        public void Username_No_Arg_Generation_Is_Not_Null_Or_Whitespace() {
            var username = GeneratorFactory.Username();
            Assert.IsFalse(string.IsNullOrWhiteSpace(username.Generate()));
        }

        [Test]
        public void Username_No_Arg_Not_Null() {
            var generator = GeneratorFactory.Username();
            Assert.IsNotNull(generator);
        }

        [Test]
        public void Username_Seed_Arg_Generation_Is_Not_Null_Or_Whitespace() {
            var username = GeneratorFactory.Username(20);
            Assert.IsFalse(string.IsNullOrWhiteSpace(username.Generate()));
        }

        [Test]
        public void Username_Seed_Arg_Not_Null() {
            var generator = GeneratorFactory.Username(20);
            Assert.IsNotNull(generator);
        }
    }
}