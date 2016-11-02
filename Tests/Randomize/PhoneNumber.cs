using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Randomize {
    [TestFixture]
    public class PhoneNumber {
        [Test]
        public void PhoneNumberChangeHigherLength() {
            var randomGenerator = RandomGenerator.Create();
            var generateMany = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 7));
            var generateMany2 = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 8));
        }

        [Test]
        public void PhoneNumberChangeLowerLength() {
            var randomGenerator = RandomGenerator.Create();
            var generateMany = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 8));
            var generateMany2 = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 7));
        }


        [Test]
        [Repeat(10)]
        public void PhoneNumberGotSameLengthAllUniqueWithOutPrefix() {
            var randomGenerator = RandomGenerator.Create();
            var generateMany = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(5), 10000);

            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        [Repeat(10)]
        public void PhoneNumberGotSameLengthAllUniqueWithPrefix() {
            var randomGenerator = RandomGenerator.Create();
            var generateMany = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(5, "07"), 10000);

            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        [Repeat(10)]
        public void PhoneNumberGotSameLengthNoPrefix() {
            var randomGenerator = RandomGenerator.Create();
            var generateMany = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(7), 10000);

            Assert.IsTrue(generateMany.All(s => s.Length == 7));
        }

        [Test]
        [Repeat(10)]
        public void PhoneNumberGotSameLengthWithPrefix() {
            var randomGenerator = RandomGenerator.Create();
            var generateMany = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(5, "07"), 10000);

            Assert.IsTrue(generateMany.All(s => s.Length == 7));
        }

        [Test]
        public void PhoneNumberSameValue() {
            var randomGenerator = RandomGenerator.Create();
            var generateMany = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 8));
            var generateMany2 = randomGenerator.GenerateMany(randomizer => randomizer.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 8));
        }
    }
}