using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Implementation;

namespace Tests.Integration {
    [TestFixture]
    public class GenerateBySequence {
        [Test]
        public void Same_Count() {
            var generator = new Generator();
            var sequence = new List<string> {
                "Foo",
                "Bar",
                "John",
                "Doe"
            };
            var result = generator.GenerateBySequence(sequence, (g, s) => s);
            Assert.AreEqual(result.Count(), sequence.Count);
        }

        [Test]
        public void Does_Not_Mutate_Sequence() {
            var generator = new Generator();
            var sequence = new List<string> {
                "Foo",
                "Bar",
                "John",
                "Doe"
            };
            var result = generator.GenerateBySequence(sequence, (g, s) => s);
            Assert.AreEqual(result, sequence);
        }

        [Test]
        public void Works_With_Iterator_Integer() {
            var generator = new Generator();
            var sequence = new List<string> {
                "Foo",
                "Bar",
                "John",
                "Doe"
            };

            var result = generator.GenerateBySequence(sequence, (g, s, i) => s + i).ToArray();
            Assert.AreEqual("Foo0", result[0]);
            Assert.AreEqual("Bar1", result[1]);
            Assert.AreEqual("John2", result[2]);
            Assert.AreEqual("Doe3", result[3]);
        }

        [Test]
        public void Works_With_Generator() {
            var generator = new Generator();
            var ages = new List<int> {
                20,
                21,
                30,
                45
            };
            var dates = generator.GenerateBySequence(ages, (gen, age) => gen.DateByAge(age)).ToArray();

            Assert.AreEqual(DateGenerator.CurrentLocalDate.Year - 20, dates[0].Year);
            Assert.AreEqual(DateGenerator.CurrentLocalDate.Year - 21, dates[1].Year);
            Assert.AreEqual(DateGenerator.CurrentLocalDate.Year - 30, dates[2].Year);
            Assert.AreEqual(DateGenerator.CurrentLocalDate.Year - 45, dates[3].Year);
        }
    }
}