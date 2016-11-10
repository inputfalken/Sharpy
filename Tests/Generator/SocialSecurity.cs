using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class SocialSecurity {
        [Test]
        public void SocialSecurityNumberAllContainsDashAtSameIndex() {
            var generator = Sharpy.Generator.Create();

            var generateMany = generator.GenerateMany(generatorr =>
                    generatorr.SocialSecurityNumber(generatorr.DateByAge(generatorr.Integer(19, 20))), 10000).ToArray();

            Assert.IsTrue(generateMany.All(s => s[6] == '-'));
        }

        [Test]
        public void SocialSecurityNumberAllSameLength() {
            var generator = Sharpy.Generator.Create();

            var generateMany = generator.GenerateMany(generatorr =>
                    generatorr.SocialSecurityNumber(generatorr.DateByAge(generatorr.Integer(19, 20))), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 11));
        }

        [Test]
        public void SocialSecurityNumberAllUnique() {
            var generator = Sharpy.Generator.Create();

            var generateMany = generator.GenerateMany(generatorr =>
                    generatorr.SocialSecurityNumber(generatorr.DateByAge(generatorr.Integer(19, 20))), 10000);
            // Will look for repeats and expected behaviour is that it should only contain 1 repeat per grouping.
            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void SocialSecurityOnlyContainsNumberWithNoFormating() {
            var generator = Sharpy.Generator.Create();

            var generateMany = generator.GenerateMany(generatorr =>
                        generatorr.SocialSecurityNumber(generatorr.DateByAge(generatorr.Integer(19, 20)), false), 10000)
                .ToArray();

            Assert.IsTrue(generateMany.All(s => s.All(char.IsNumber)));
        }
    }
}