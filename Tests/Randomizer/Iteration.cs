using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Randomizer {
    [TestFixture]
    public class Iteration {
        [Test]
        public void IteratorWithEnumerable() {
            var iteration = 0;
            var generator = SharpyGenerator.Create();
            var result = generator.GenerateMany((randomizerr, i) => iteration++ == i, 20).ToArray();
            Assert.IsTrue(result.All(b => b));
        }
    }
}