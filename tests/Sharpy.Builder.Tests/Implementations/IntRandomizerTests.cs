using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations {
    [TestFixture]
    public class IntRandomizerTests {
        private const int Amount = 100000;

        [Test]
        public void Double_Argument_NotDefaultValue() {
            var intRandomizer = new IntegerRandomizer(new Random());
            var list = new List<int>(100);
            for (var i = 0; i < 100; i++) list.Add(intRandomizer.Integer(0, 100));
            Assert.IsFalse(list.All(i => i == 0));
        }

        [Test]
        public void No_Argument_NotDefaultValue() {
            var intRandomizer = new IntegerRandomizer(new Random());
            var list = new List<int>(100);
            for (var i = 0; i < 100; i++) list.Add(intRandomizer.Integer());
            Assert.IsFalse(list.All(i => i == 0));
        }

        [Test]
        [Repeat(Amount)]
        public void NoArgument() {
            var intRandomizer = new IntegerRandomizer(new Random());
            var result = intRandomizer.Integer();
            Assert.IsTrue(result > int.MinValue && result < int.MaxValue);
        }

        [Test]
        [Repeat(Amount)]
        public void One_Arg() {
            const int max = 1000;
            var intRandomizer = new IntegerRandomizer(new Random());
            var result = intRandomizer.Integer(max);
            Assert.IsTrue(result >= 0 && result < max);
        }

        [Test]
        [Repeat(Amount)]
        public void One_Arg_MaxValue() {
            var intRandomizer = new IntegerRandomizer(new Random());
            var result = intRandomizer.Integer(int.MaxValue);
            Assert.IsTrue(result > 0 && result < int.MaxValue);
        }

        [Test]
        public void One_Arg_minusOne_Throws() {
            var intRandomizer = new IntegerRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => intRandomizer.Integer(-1));
        }

        [Test]
        public void One_Argument_NotDefaultValue() {
            var intRandomizer = new IntegerRandomizer(new Random());
            var list = new List<int>(100);
            for (var i = 0; i < 100; i++) list.Add(intRandomizer.Integer(100));
            Assert.IsFalse(list.All(i => i == 0));
        }

        [Test]
        [Repeat(Amount)]
        public void Two_Args() {
            const int min = -1000;
            const int max = 2000;
            var intRandomizer = new IntegerRandomizer(new Random());
            var result = intRandomizer.Integer(min, max);
            Assert.IsTrue(result >= min && result < max);
        }

        [Test]
        public void Two_Args_Maxx_Less_Than_Min_Throws() {
            const int min = -1000;
            const int max = -2000;
            var intRandomizer = new IntegerRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => intRandomizer.Integer(min, max));
        }

        [Test]
        [Repeat(Amount)]
        public void Two_Args_MinValue_Zero() {
            var intRandomizer = new IntegerRandomizer(new Random());
            var result = intRandomizer.Integer(int.MinValue, 0);
            Assert.IsTrue(result < 0);
        }

        [Test]
        [Repeat(Amount)]
        public void Two_Args_Thousand_And_TwoThousand() {
            const int min = 1000;
            const int max = 2000;
            var intRandomizer = new IntegerRandomizer(new Random());
            var result = intRandomizer.Integer(min, max);
            Assert.IsTrue(result >= min && result < max);
        }

        [Test]
        [Repeat(Amount)]
        public void Two_Args_Zero_And_MaxValue() {
            var intRandomizer = new IntegerRandomizer(new Random());
            var result = intRandomizer.Integer(0, int.MaxValue);
            Assert.IsTrue(result > 0 && result < int.MaxValue);
        }
    }
}