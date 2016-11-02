using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Params {
        [Test]
        public void WithString() {
            var randomGenerator = RandomGenerator.Create();
            var args = new[] {"hello", "there", "foo"};
            var generateMany =
                randomGenerator.GenerateMany(randomize => randomize.Params("hello", "there", "foo"));
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}