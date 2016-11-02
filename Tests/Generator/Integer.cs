using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Integer {
        [Test]
        public void NotDefaultValue() {
            var generator = RandomGenerator.Create();
            //many
            Assert.IsFalse(generator.GenerateMany(randomizer => randomizer.Integer(1, 100), 100).All(i => i == 0));

            //Single
            Assert.IsFalse(generator.Generate(randomizer => randomizer.Integer(1, 100)) == 0);
        }
    }
}