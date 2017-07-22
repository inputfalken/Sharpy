using System;
using System.Linq;
using GeneratorAPI;
using GeneratorAPI.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class Integer {
        private const int Length = 1000000;


        [Test]
        public void NoArgument() {
            var ints = Generator.Create(new Provider())
                .Select(generator => generator.Integer())
                .Take(Length);
            Assert.IsTrue(ints.All(l => l > int.MinValue && l < int.MaxValue));

            var intInstance = Generator.Create(new Provider())
                .Select(generator => generator.Integer())
                .Generate();
            Assert.IsTrue(intInstance > int.MinValue && intInstance < int.MaxValue);
        }

        [Test]
        public void NotDefaultValue() {
            var generator = Generator.Create(new Provider());
            //many
            Assert.IsFalse(
                generator.Select(generatorr => generatorr.Integer(1, 100)).Take(100).All(i => i == 0)
            );
            //Single
            Assert.IsFalse(generator.Select(generatorr => generatorr.Integer(1, 100)).Generate() == 0);
        }

        [Test]
        public void One_Arg_MaxValue() {
            var ints = Generator.Create(new Provider())
                .Select(generator => generator.Integer(int.MaxValue))
                .Take(Length);
            Assert.IsTrue(ints.All(l => l >= 0));

            var intInstance =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(int.MaxValue))
                    .Generate();
            Assert.IsTrue(intInstance >= 0);
        }

        [Test]
        public void One_Arg_minusOne() {
            var ints =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(-1))
                    .Take(Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => Generator.Create(new Provider())
                    .Select(generator => generator.Integer(-1))
                    .Generate());
        }

        [Test]
        public void One_Arg_Thousand() {
            const int max = 1000;
            var ints =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(max))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l >= 0 && l < max));

            var intInstance = Generator.Create(new Provider())
                .Select(generator => generator.Integer(max))
                .Generate();
            Assert.IsTrue(intInstance >= 0 && intInstance < max);
        }


        [Test]
        public void Two_Args_MinusThousand_And_MinusTwoThousand() {
            const int min = -1000;
            const int max = -2000;
            var ints =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Take(Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    Generator.Create(new Provider())
                        .Select(generator => generator.Integer(min, max))
                        .Generate());
        }

        [Test]
        public void Two_Args_MinusThousand_And_TwoThousand() {
            const int min = -1000;
            const int max = 2000;
            var ints =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l >= min && l < max));

            var intInstance =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Generate();
            Assert.IsTrue(intInstance >= min && intInstance < max);
        }

        [Test]
        public void Two_Args_MinusTwoThousand_And_MinusThousand() {
            const int min = -2000;
            const int max = -1000;
            var ints =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l >= min && l < max));

            var intInstance =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Generate();
            Assert.IsTrue(intInstance >= min && intInstance < max);
        }

        [Test]
        public void Two_Args_MinValue_And_Zero() {
            var ints =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(int.MinValue, 0))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l < 0));
            var intInstance =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(int.MinValue, 0))
                    .Generate();

            Assert.IsTrue(intInstance < 0);
        }


        [Test]
        public void Two_Args_Thousand_And_TwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var ints =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l >= min && l < max));

            var intInstance =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Generate();
            Assert.IsTrue(intInstance >= min && intInstance < max);
        }

        [Test]
        public void Two_Args_Zero_And_MaxValue() {
            var ints =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(0, int.MaxValue))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l > 0));

            var intInstance =
                Generator.Create(new Provider())
                    .Select(generator => generator.Integer(0, int.MaxValue))
                    .Generate();
            Assert.IsTrue(intInstance > 0);
        }
    }
}