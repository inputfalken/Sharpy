using GeneratorAPI;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.GeneratorAPI {
    [TestFixture]
    internal class GeneratorTests {
        [SetUp]
        public void Initiate() {
            var randomizer = new Randomizer(Seed);
            _generator = Generator.Function(randomizer.GetString);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<string> _generator;
        private const int Seed = 100;


        [Test(
            Description = "Verify that Generate gives expected result"
        )]
        public void Generate_Expected_Result() {
            var result = _generator.Generate();
            var expected = new Randomizer(Seed).GetString();
            Assert.AreEqual(expected, result);
        }

        [Test(
            Description = "Verify that does not return null"
        )]
        public void Generate_Not_Null() {
            var result = _generator.Generate();
            Assert.IsNotNull(result);
        }
    }
}