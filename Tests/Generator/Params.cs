using System.Linq;
using NUnit.Framework;

namespace Tests.Generator {
    [TestFixture]
    public class Params {
        [Test]
        public void WithString() {
            var randomGenerator = Sharpy.Generator.Create();
            var args = new[] {"hello", "there", "foo"};
            var generateMany =
                randomGenerator.GenerateMany(source => source.Params("hello", "there", "foo"));
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}