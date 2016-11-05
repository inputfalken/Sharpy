using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.source {
    [TestFixture]
    public class Params {
        [Test]
        public void WithString() {
            var randomGenerator = SharpyGenerator.Create();
            var args = new[] {"hello", "there", "foo"};
            var generateMany =
                randomGenerator.GenerateMany(source => source.Params("hello", "there", "foo"));
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}