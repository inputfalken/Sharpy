using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    public class Params {
        [Test]
        public void WithString() {
            var randomGenerator = Productor.Yield(new Provider());
            var args = new[] {"hello", "there", "foo"};
            var generateMany =
                randomGenerator.Select(generator => generator.Params("hello", "there", "foo")).Take(10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}