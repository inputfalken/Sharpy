using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.source {
    [TestFixture]
    public class Iteration {
        [Test]
        public void IteratorWithEnumerable() {
            var iteration = 0;
            var generator = Generator.Create();
            var result = generator.GenerateMany((sourcer, i) => iteration++ == i, 20).ToArray();
            Assert.IsTrue(result.All(b => b));
        }
    }
}