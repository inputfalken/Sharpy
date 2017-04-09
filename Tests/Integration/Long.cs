using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    public class Long {
        private const int Length = 1000000;

        [Test]
        public void NoArgument() {
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long())
                    .Take(Length);
            Assert.IsTrue(longs.All(l => l > long.MinValue && l < long.MaxValue));

            var longInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Long()).Produce();
            Assert.IsTrue(longInstance > long.MinValue && longInstance < long.MaxValue);
        }

        [Test]
        public void One_Arg_MaxValue() {
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(long.MaxValue))
                    .Take(Length);
            Assert.IsTrue(longs.All(l => l >= 0));

            var longInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Long(long.MaxValue)).Produce();
            Assert.IsTrue(longInstance >= 0);
        }

        [Test]
        public void One_Arg_MinusOne() {
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(-1))
                    .Take(Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => Productor.Yield(new Provider()).Select(generator => generator.Long(-1)).Produce());
        }

        [Test]
        public void One_Arg_Thousand() {
            const int max = 1000;
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(max))
                    .Take(Length);
            Assert.IsTrue(longs.All(l => l >= 0 && l < max));

            var longInstance =Productor.Yield(new Provider()).Select(generator => generator.Long(max)).Produce();
            Assert.IsTrue(longInstance >= 0 && longInstance < max);
        }

        [Test]
        public void One_Arg_Zero() {
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(0))
                    .Take(Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());


            Assert.Throws<ArgumentOutOfRangeException>(
                () => Productor.Yield(new Provider()).Select(generator => generator.Long(0)).Produce());
        }

        [Test]
        public void Two_Args_MinusThousand_And_MinusTwoThousand() {
            const int min = -1000;
            const int max = -2000;
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(min, max))
                    .Take(Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    Productor.Yield(new Provider()).Select(generator => generator.Long(min, max)).Produce());
        }

        [Test]
        public void Two_Args_MinusThousand_And_TwoThousand() {
            const int min = -1000;
            const int max = 2000;
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(min, max))
                    .Take(Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance =Productor.Yield(new Provider()).Select(generator => generator.Long(min, max)).Produce();
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void Two_Args_MinusTwoThousand_And_MinusThousand() {
            const int min = -2000;
            const int max = -1000;
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(min, max))
                    .Take(Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Long(min, max)).Produce();
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void Two_Args_MinValue_And_Zero() {
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(long.MinValue, 0))
                    .Take(Length);
            Assert.IsTrue(longs.All(l => l < 0));
            var longInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Long(long.MinValue, 0)).Produce();

            Assert.IsTrue(longInstance < 0);
        }


        [Test]
        public void Two_Args_Thousand_And_TwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(min, max))
                    .Take(Length);
            Assert.IsTrue(longs.All(l => l >= min && l < max));

            var longInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Long(min, max)).Produce();
            Assert.IsTrue(longInstance >= min && longInstance < max);
        }

        [Test]
        public void Two_Args_Zero_And_MaxValue() {
            var longs =
                Productor.Yield(new Provider())
                    .Select(generator => generator.Long(0, long.MaxValue))
                    .Take(Length);
            Assert.IsTrue(longs.All(l => l > 0));

            var longInstance =
                Productor.Yield(new Provider()).Select(generator => generator.Long(0, long.MaxValue)).Produce();
            Assert.IsTrue(longInstance > 0);
        }
    }
}