using System.Linq;
using GeneratorAPI;
using NUnit.Framework;
using Sharpy;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class Params {
        [Test]
        public void WithString() {
            var randomGenerator = Generator.Factory.Provider(new Provider());
            var args = new[] {"hello", "there", "foo"};
            var generateMany =
                randomGenerator.Select(generator => generator.Params("hello", "there", "foo")).Take(10);
            Assert.IsTrue(generateMany.All(s => args.Contains(s)));
        }
    }
}