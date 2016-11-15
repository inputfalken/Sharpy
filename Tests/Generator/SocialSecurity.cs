using System.Linq;
using NodaTime;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class SocialSecurity {
        [Test]
        public void AllContainsDashAtSameIndex() {
            var generator = Sharpy.Generator.Create();

            var generateMany = generator.GenerateMany(generatorr =>
                    generatorr.SocialSecurityNumber(generatorr.DateByAge(generatorr.Integer(19, 20))), 10000).ToArray();

            Assert.IsTrue(generateMany.All(s => s[6] == '-'));
        }

        [Test]
        public void AllSameLength() {
            var generator = Sharpy.Generator.Create();

            var generateMany = generator.GenerateMany(generatorr =>
                    generatorr.SocialSecurityNumber(generatorr.DateByAge(generatorr.Integer(19, 20))), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 11));
        }

        [Test]
        public void AllUnique() {
            var generator = Sharpy.Generator.Create();

            var generateMany = generator.GenerateMany(generatorr =>
                    generatorr.SocialSecurityNumber(generatorr.DateByAge(generatorr.Integer(19, 20))), 10000);
            // Will look for repeats and expected behaviour is that it should only contain 1 repeat per grouping.
            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void CheckCombination() {
            var generateMany =
                Sharpy.Generator.Create()
                    .GenerateMany(generator => generator.SocialSecurityNumber(new LocalDate(2000, 10, 10)), 10000);
            //The test checks that it works like the following algorithm (10^(4)) and that all strings got same length.
            Assert.IsTrue(
                generateMany.GroupBy(s => s).All(grouping => (grouping.Count() == 1) && (grouping.Key.Length == 11)));
        }

        [Test]
        public void WithNoFormating() {
            var generator = Sharpy.Generator.Create();

            var generateMany = generator.GenerateMany(generatorr =>
                        generatorr.SocialSecurityNumber(generatorr.DateByAge(generatorr.Integer(19, 20)), false), 10000)
                .ToArray();

            Assert.IsTrue(generateMany.All(s => s.All(char.IsNumber)));
        }
    }
}