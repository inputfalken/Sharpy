using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class PhoneNumber {
        [Test]
        public void ChangeHigherLength() {
            var randomGenerator = new Sharpy.Generator();
            var generateMany = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 7));
            var generateMany2 = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 8));
        }

        [Test]
        public void ChangeLowerLength() {
            var randomGenerator = new Sharpy.Generator();
            var generateMany = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 8));
            var generateMany2 = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 7));
        }

        [Test]
        public void CheckCombination() {
            var generateMany = new Sharpy.Generator().GenerateMany(generator => generator.PhoneNumber(3), 1000);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.IsTrue(generateMany.GroupBy(s => s)
                .All(grouping => (grouping.Count() == 1) && (grouping.Key.Length == 3)));
        }


        [Test]
        public void GotSameLengthAllUniqueWithOutPrefix() {
            var randomGenerator = new Sharpy.Generator();
            var generateMany = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(5), 10000);

            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void GotSameLengthAllUniqueWithPrefix() {
            var randomGenerator = new Sharpy.Generator();
            var generateMany = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(5, "07"), 10000);

            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void GotSameLengthNoPrefix() {
            var randomGenerator = new Sharpy.Generator();
            var generateMany = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(7), 10000);

            Assert.IsTrue(generateMany.All(s => s.Length == 7));
        }

        [Test]
        public void GotSameLengthWithPrefix() {
            var randomGenerator = new Sharpy.Generator();
            var generateMany = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(5, "07"), 10000);

            Assert.IsTrue(generateMany.All(s => s.Length == 7));
        }

        [Test]
        public void SameValue() {
            var randomGenerator = new Sharpy.Generator();
            var generateMany = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 8));
            var generateMany2 = randomGenerator.GenerateMany(generatorr => generatorr.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 8));
        }
    }
}