using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    public class Integer {
        private const int Length = 1000000;


        [Test]
        public void NoArgument() {
            var ints = Productor.Yield(new Provider())
                .Select(generator => generator.Integer())
                .Take(Length);
            Assert.IsTrue(ints.All(l => l > int.MinValue && l < int.MaxValue));

            var intInstance = Productor.Yield(new Provider()).Select(generator => generator.Integer()).Produce();
            Assert.IsTrue(intInstance > int.MinValue && intInstance < int.MaxValue);
        }

        [Test]
        public void NotDefaultValue() {
            var generator = Productor.Yield(new Provider());
            //many
            Assert.IsFalse(
                generator.Select(generatorr => generatorr.Integer(1, 100)).Take(100).All(i => i == 0)
            );

            //Single
            Assert.IsFalse(generator.Select(generatorr => generatorr.Integer(1, 100)).Produce() == 0);
        }

        [Test]
        public void One_Arg_MaxValue() {
            var ints = Productor.Yield(new Provider())
                .Select(generator => generator.Integer(int.MaxValue))
                .Take(Length);
            Assert.IsTrue(ints.All(l => l >= 0));

            var intInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Integer(int.MaxValue)).Produce();
            Assert.IsTrue(intInstance >= 0);
        }

        [Test]
        public void One_Arg_minusOne() {
            var ints =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Integer(-1))
                    .Take(Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => Productor.Yield(new Provider()).Select(generator => generator.Integer(-1)).Produce());
        }

        [Test]
        public void One_Arg_Thousand() {
            const int max = 1000;
            var ints =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Integer(max))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l >= 0 && l < max));

            var intInstance = Productor.Yield(new Provider()).Select(generator => generator.Integer(max)).Produce();
            Assert.IsTrue(intInstance >= 0 && intInstance < max);
        }


        [Test]
        public void Two_Args_MinusThousand_And_MinusTwoThousand() {
            const int min = -1000;
            const int max = -2000;
            var ints =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Take(Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    Productor.Yield(new Provider()).Select(generator => generator.Integer(min, max)).Produce());
        }

        [Test]
        public void Two_Args_MinusThousand_And_TwoThousand() {
            const int min = -1000;
            const int max = 2000;
            var ints =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l >= min && l < max));

            var intInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Integer(min, max)).Produce();
            Assert.IsTrue(intInstance >= min && intInstance < max);
        }

        [Test]
        public void Two_Args_MinusTwoThousand_And_MinusThousand() {
            const int min = -2000;
            const int max = -1000;
            var ints =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l >= min && l < max));

            var intInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Integer(min, max)).Produce();
            Assert.IsTrue(intInstance >= min && intInstance < max);
        }

        [Test]
        public void Two_Args_MinValue_And_Zero() {
            var ints =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Integer(int.MinValue, 0))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l < 0));
            var intInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Integer(int.MinValue, 0)).Produce();

            Assert.IsTrue(intInstance < 0);
        }


        [Test]
        public void Two_Args_Thousand_And_TwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var ints =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Integer(min, max))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l >= min && l < max));

            var intInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Integer(min, max)).Produce();
            Assert.IsTrue(intInstance >= min && intInstance < max);
        }

        [Test]
        public void Two_Args_Zero_And_MaxValue() {
            var ints =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Integer(0, int.MaxValue))
                    .Take(Length);
            Assert.IsTrue(ints.All(l => l > 0));

            var intInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Integer(0, int.MaxValue)).Produce();
            Assert.IsTrue(intInstance > 0);
        }
    }
}