using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class Params {
        [Test]
        public void WithString() {
            var randomGenerator = Generator.Create(new Builder());
            var args = new[] {"hello", "there", "foo"};
            var generateMany =
                randomGenerator.Select(generator => generator.Params("hello", "there", "foo")).Take(10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}