using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Randomize {
    [TestFixture]
    public class Iteration {
        [Test]
        public void IteratorWithEnumerable() {
            var iteration = 0;
            var generator = RandomGenerator.Create();
            var result = generator.GenerateMany((randomizer, i) => iteration++ == i, 20).ToArray();
            Assert.IsTrue(result.All(b => b));
        }
    }
}