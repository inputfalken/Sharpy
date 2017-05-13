using System;
using GeneratorAPI;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.GeneratorAPI {
    [TestFixture]
    internal class GeneratorTests {
        [SetUp]
        public void Initiate() {
            _generator = new Generator<Randomizer>(new Randomizer(Seed));
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private Generator<Randomizer> _generator;
        private const int Seed = 100;

        [Test(
            Description = "Check that constructor throws exception if argument is null"
        )]
        public void Constructor_Null_Argument() {
            Assert.Throws<ArgumentNullException>(() => new Generator<Randomizer>(null));
        }

        [Test(
            Description = "Checks that the result from Generate is not null"
        )]
        public void Generate_Does_Not_Return_With_Valid_Arg() {
            var result = _generator.Generate(randomizer => randomizer.GetString());
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Checks that an exception is thrown when generate argument is null"
        )]
        public void Generate_With_Null() {
            Assert.Throws<ArgumentNullException>(() => _generator.Generate<string>(null));
        }

        [Test(
            Description = "Checks that provider is assigned in the constructor."
        )]
        public void Provider_Is_not_Null() {
            var result = _generator.Provider;
            Assert.IsNotNull(result);
        }
    }
}