﻿using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class PhoneNumber {
        [Test]
        public void PhoneNumberChangeHigherLength() {
            var randomGenerator = Sharpy.Generator.Create();
            var generateMany = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 7));
            var generateMany2 = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 8));
        }

        [Test]
        public void PhoneNumberChangeLowerLength() {
            var randomGenerator = Sharpy.Generator.Create();
            var generateMany = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 8));
            var generateMany2 = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 7));
        }


        [Test]
        [Repeat(10)]
        public void PhoneNumberGotSameLengthAllUniqueWithOutPrefix() {
            var randomGenerator = Sharpy.Generator.Create();
            var generateMany = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(5), 10000);

            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        [Repeat(10)]
        public void PhoneNumberGotSameLengthAllUniqueWithPrefix() {
            var randomGenerator = Sharpy.Generator.Create();
            var generateMany = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(5, "07"), 10000);

            Assert.IsTrue(generateMany.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        [Repeat(10)]
        public void PhoneNumberGotSameLengthNoPrefix() {
            var randomGenerator = Sharpy.Generator.Create();
            var generateMany = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(7), 10000);

            Assert.IsTrue(generateMany.All(s => s.Length == 7));
        }

        [Test]
        public void PhoneNumberGotSameLengthWithPrefix() {
            var randomGenerator = Sharpy.Generator.Create();
            var generateMany = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(5, "07"), 10000);

            Assert.IsTrue(generateMany.All(s => s.Length == 7));
        }

        [Test]
        public void PhoneNumberSameValue() {
            var randomGenerator = Sharpy.Generator.Create();
            var generateMany = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 8));
            var generateMany2 = GeneratorExtensions.GenerateMany(randomGenerator, generatorr => generatorr.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 8));
        }
    }
}