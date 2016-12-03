using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Long {
        private const int Length = 1000000;

        [Test]
        public void NoArgument() {
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Long(),
                    Length);
            Assert.IsTrue(longs.All(l => (l > long.MinValue) && (l < long.MaxValue)));

            var longInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Long());
            Assert.IsTrue((longInstance > long.MinValue) && (longInstance < long.MaxValue));
        }

        [Test]
        public void One_Arg_MaxValue() {
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                    generator => generator.Long(long.MaxValue),
                    Length);
            Assert.IsTrue(longs.All(l => l >= 0));

            var longInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(
                    generator => generator.Long(long.MaxValue));
            Assert.IsTrue(longInstance >= 0);
        }

        [Test]
        public void One_Arg_MinusOne() {
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Long(-1),
                    Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Long(-1)));
        }

        [Test]
        public void One_Arg_Thousand() {
            const int max = 1000;
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                    generator => generator.Long(max), Length);
            Assert.IsTrue(longs.All(l => (l >= 0) && (l < max)));

            var longInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Long(max));
            Assert.IsTrue((longInstance >= 0) && (longInstance < max));
        }

        [Test]
        public void One_Arg_Zero() {
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Long(0),
                    Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());


            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Long(0)));
        }

        [Test]
        public void Two_Args_MinusThousand_And_MinusTwoThousand() {
            const int min = -1000;
            const int max = -2000;
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                    generator => generator.Long(min, max),
                    Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => longs.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    new Sharpy.Generator(new Configurement(new Random())).Generate(
                        generator => generator.Long(min, max)));
        }

        [Test]
        public void Two_Args_MinusThousand_And_TwoThousand() {
            const int min = -1000;
            const int max = 2000;
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                    generator => generator.Long(min, max),
                    Length);
            Assert.IsTrue(longs.All(l => (l >= min) && (l < max)));

            var longInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Long(min, max));
            Assert.IsTrue((longInstance >= min) && (longInstance < max));
        }

        [Test]
        public void Two_Args_MinusTwoThousand_And_MinusThousand() {
            const int min = -2000;
            const int max = -1000;
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                    generator => generator.Long(min, max),
                    Length);
            Assert.IsTrue(longs.All(l => (l >= min) && (l < max)));

            var longInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Long(min, max));
            Assert.IsTrue((longInstance >= min) && (longInstance < max));
        }

        [Test]
        public void Two_Args_MinValue_And_Zero() {
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                    generator => generator.Long(long.MinValue, 0),
                    Length);
            Assert.IsTrue(longs.All(l => l < 0));
            var longInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(
                    generator => generator.Long(long.MinValue, 0));

            Assert.IsTrue(longInstance < 0);
        }


        [Test]
        public void Two_Args_Thousand_And_TwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                    generator => generator.Long(min, max),
                    Length);
            Assert.IsTrue(longs.All(l => (l >= min) && (l < max)));

            var longInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Long(min, max));
            Assert.IsTrue((longInstance >= min) && (longInstance < max));
        }

        [Test]
        public void Two_Args_Zero_And_MaxValue() {
            var longs =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                    generator => generator.Long(0, long.MaxValue),
                    Length);
            Assert.IsTrue(longs.All(l => l > 0));

            var longInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(
                    generator => generator.Long(0, long.MaxValue));
            Assert.IsTrue(longInstance > 0);
        }
    }
}