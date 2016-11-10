using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Integer {
        [Test]
        public void NotDefaultValue() {
            var generator = Sharpy.Generator.Create();
            //many
            Assert.IsFalse(generator.GenerateMany(generatorr => generatorr.Integer(1, 100), 100).All(i => i == 0));

            //Single
            Assert.IsFalse(generator.Generate(generatorr => generatorr.Integer(1, 100)) == 0);
        }

        private const int Length = 1000000;

        [Test]
        public void DoubleArgumentMinusThousandAndMinusTwoThousand() {
            const int min = -1000;
            const int max = -2000;
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(min, max), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => Sharpy.Generator.Create().Generate(generator => generator.Integer(min, max)));
        }

        [Test]
        public void DoubleArgumentMinusThousandAndTwoThousand() {
            const int min = -1000;
            const int max = 2000;
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(min, max), Length);
            Assert.IsTrue(ints.All(l => (l >= min) && (l < max)));

            var intInstance = Sharpy.Generator.Create().Generate(generator => generator.Integer(min, max));
            Assert.IsTrue((intInstance >= min) && (intInstance < max));
        }

        [Test]
        public void DoubleArgumentMinusTwoThousandAndMinusThousand() {
            const int min = -2000;
            const int max = -1000;
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(min, max), Length);
            Assert.IsTrue(ints.All(l => (l >= min) && (l < max)));

            var intInstance = Sharpy.Generator.Create().Generate(generator => generator.Integer(min, max));
            Assert.IsTrue((intInstance >= min) && (intInstance < max));
        }

        [Test]
        public void DoubleArgumentMinValueAndZero() {
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(int.MinValue, 0), Length);
            Assert.IsTrue(ints.All(l => l < 0));
            var intInstance = Sharpy.Generator.Create().Generate(generator => generator.Integer(int.MinValue, 0));

            Assert.IsTrue(intInstance < 0);
        }


        [Test]
        public void DoubleArgumentThousandAndTwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(min, max), Length);
            Assert.IsTrue(ints.All(l => (l >= min) && (l < max)));

            var intInstance = Sharpy.Generator.Create().Generate(generator => generator.Integer(min, max));
            Assert.IsTrue((intInstance >= min) && (intInstance < max));
        }

        [Test]
        public void DoubleArgumentZeroMaxValue() {
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(0, int.MaxValue), Length);
            Assert.IsTrue(ints.All(l => l > 0));

            var intInstance = Sharpy.Generator.Create().Generate(generator => generator.Integer(0, int.MaxValue));
            Assert.IsTrue(intInstance > 0);
        }

        [Test]
        public void NoArgument() {
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(), Length);
            Assert.IsTrue(ints.All(l => (l > int.MinValue) && (l < int.MaxValue)));

            var intInstance = Sharpy.Generator.Create().Generate(generator => generator.Integer());
            Assert.IsTrue((intInstance > int.MinValue) && (intInstance < int.MaxValue));
        }

        [Test]
        public void SingleArgumentLessThanZero() {
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(-1), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => Sharpy.Generator.Create().Generate(generator => generator.Integer(-1)));
        }

        [Test]
        public void SingleArgumentMaxValue() {
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(int.MaxValue), Length);
            Assert.IsTrue(ints.All(l => l >= 0));

            var intInstance = Sharpy.Generator.Create().Generate(generator => generator.Integer(int.MaxValue));
            Assert.IsTrue(intInstance >= 0);
        }

        [Test]
        public void SingleArgumentThousand() {
            const int max = 1000;
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(max), Length);
            Assert.IsTrue(ints.All(l => (l >= 0) && (l < max)));

            var intInstance = Sharpy.Generator.Create().Generate(generator => generator.Integer(max));
            Assert.IsTrue((intInstance >= 0) && (intInstance < max));
        }

        [Test]
        public void SingleArgumentZero() {
            var ints = Sharpy.Generator.Create().GenerateMany(generator => generator.Integer(0), Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => ints.ToArray());


            Assert.Throws<ArgumentOutOfRangeException>(
                () => Sharpy.Generator.Create().Generate(generator => generator.Integer(0)));
        }
    }
}