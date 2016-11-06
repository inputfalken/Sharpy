using System.Linq;
using NUnit.Framework;

namespace Tests.Generator {
    [TestFixture]
    public class Iteration {
        [Test]
        public void IteratorWithEnumerable() {
            var iteration = 0;
            var generator = Sharpy.Generator.Create();
            var result = generator.GenerateMany((sourcer, i) => iteration++ == i, 20).ToArray();
            Assert.IsTrue(result.All(b => b));
        }
    }
}