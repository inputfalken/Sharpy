using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.source {
    [TestFixture]
    public class SocialSecurity {
        [Test]
        [Repeat(10)]
        public void SocialSecurityNumberAllContainsDashAtSameIndex() {
            var generator = Generator.Create();

            var generateMany = generator.GenerateMany(sourcer =>
                    sourcer.SocialSecurityNumber(sourcer.DateByAge(sourcer.Integer(19, 20))), 10000).ToArray();

            Assert.IsTrue(generateMany.All(s => s[6] == '-'));
        }

        [Test]
        [Repeat(10)]
        public void SocialSecurityNumberAllSameLength() {
            var generator = Generator.Create();

            var generateMany = generator.GenerateMany(sourcer =>
                    sourcer.SocialSecurityNumber(sourcer.DateByAge(sourcer.Integer(19, 20))), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 11));
        }

        [Test]
        [Repeat(10)]
        public void SocialSecurityNumberAllUnique() {
            var generator = Generator.Create();

            var generateMany = generator.GenerateMany(sourcer =>
                    sourcer.SocialSecurityNumber(sourcer.DateByAge(sourcer.Integer(19, 20))), 10000);
            // Will look for repeats and expected behaviour is that it should only contain 1 repeat per grouping.
            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        [Repeat(10)]
        public void SocialSecurityOnlyContainsNumberWithNoFormating() {
            var generator = Generator.Create();

            var generateMany = generator.GenerateMany(sourcer =>
                        sourcer.SocialSecurityNumber(sourcer.DateByAge(sourcer.Integer(19, 20)), false), 10000)
                .ToArray();

            Assert.IsTrue(generateMany.All(s => s.All(char.IsNumber)));
        }
    }
}