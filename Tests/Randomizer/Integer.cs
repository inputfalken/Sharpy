using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Randomizer {
    [TestFixture]
    public class Integer {
        [Test]
        public void NotDefaultValue() {
            var generator = RandomGenerator.Create();
            //many
            Assert.IsFalse(generator.GenerateMany(randomizerr => randomizerr.Integer(1, 100), 100).All(i => i == 0));

            //Single
            Assert.IsFalse(generator.Generate(randomizerr => randomizerr.Integer(1, 100)) == 0);
        }
    }
}