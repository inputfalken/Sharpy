using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Integer {
        private const int Length = 1000000;

        [Test]
        public void NoArgument() {
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(), Length);
            Assert.IsTrue(ints.All(l => (l > int.MinValue) && (l < int.MaxValue)));

            var intInstance = new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer());
            Assert.IsTrue((intInstance > int.MinValue) && (intInstance < int.MaxValue));
        }

        [Test]
        public void NotDefaultValue() {
            var generator = new Sharpy.Generator(new Configurement(new Random()));
            //many
            Assert.IsFalse(generator.GenerateSequence(generatorr => generatorr.Integer(1, 100), 100).All(i => i == 0));

            //Single
            Assert.IsFalse(generator.Generate(generatorr => generatorr.Integer(1, 100)) == 0);
        }

        [Test]
        public void One_Arg_MaxValue() {
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(
                generator => generator.Integer(int.MaxValue), Length);
            Assert.IsTrue(ints.All(l => l >= 0));

            var intInstance = new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(int.MaxValue));
            Assert.IsTrue(intInstance >= 0);
        }

        [Test]
        public void One_Arg_minusOne() {
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(-1), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(-1)));
        }

        [Test]
        public void One_Arg_Thousand() {
            const int max = 1000;
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(max), Length);
            Assert.IsTrue(ints.All(l => (l >= 0) && (l < max)));

            var intInstance = new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(max));
            Assert.IsTrue((intInstance >= 0) && (intInstance < max));
        }

        [Test]
        public void One_Arg_Zero() {
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(0), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(0)));
        }

        [Test]
        public void Two_Args_MinusThousand_And_MinusTwoThousand() {
            const int min = -1000;
            const int max = -2000;
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(min, max),
                Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(min, max)));
        }

        [Test]
        public void Two_Args_MinusThousand_And_TwoThousand() {
            const int min = -1000;
            const int max = 2000;
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(min, max),
                Length);
            Assert.IsTrue(ints.All(l => (l >= min) && (l < max)));

            var intInstance = new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(min, max));
            Assert.IsTrue((intInstance >= min) && (intInstance < max));
        }

        [Test]
        public void Two_Args_MinusTwoThousand_And_MinusThousand() {
            const int min = -2000;
            const int max = -1000;
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(min, max),
                Length);
            Assert.IsTrue(ints.All(l => (l >= min) && (l < max)));

            var intInstance = new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(min, max));
            Assert.IsTrue((intInstance >= min) && (intInstance < max));
        }

        [Test]
        public void Two_Args_MinValue_And_Zero() {
            var ints =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(int.MinValue, 0),
                    Length);
            Assert.IsTrue(ints.All(l => l < 0));
            var intInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(int.MinValue, 0));

            Assert.IsTrue(intInstance < 0);
        }


        [Test]
        public void Two_Args_Thousand_And_TwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var ints = new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(min, max),
                Length);
            Assert.IsTrue(ints.All(l => (l >= min) && (l < max)));

            var intInstance = new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(min, max));
            Assert.IsTrue((intInstance >= min) && (intInstance < max));
        }

        [Test]
        public void Two_Args_Zero_And_MaxValue() {
            var ints =
                new Sharpy.Generator(new Configurement(new Random())).GenerateSequence(generator => generator.Integer(0, int.MaxValue),
                    Length);
            Assert.IsTrue(ints.All(l => l > 0));

            var intInstance =
                new Sharpy.Generator(new Configurement(new Random())).Generate(generator => generator.Integer(0, int.MaxValue));
            Assert.IsTrue(intInstance > 0);
        }
    }
}