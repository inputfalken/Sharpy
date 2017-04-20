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
            Author = "Robert",
            Description = "Checks that the result from Generate is not null"
        )]
        public void Test() {
            var result = _generator.Generate(randomizer => randomizer.GetString());
            Assert.IsNotNull(result);
        }

        [Test(
            Author = "Robert",
            Description = "Checks that provider is assigned in the constructor."
        )]
        public void Test_Provider_Is_not_Null() {
            Assert.IsNotNull(_generator);
        }
    }
}