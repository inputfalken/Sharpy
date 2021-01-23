using System;
using System.Security.Cryptography;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    public class EnumTests
    {
        private enum TestEnum
        {
            One,
            Two,
            Three
        }

        private enum Empty
        {
        }

        private static readonly Random Random = new();

        [Test]
        public void IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Enum<TestEnum>(),
                x => Assert.True(x.Count == 3, "x.Count == 3")
            );
        }

        [Test]
        public void IsDeterministic()
        {
            Assertion.IsDeterministic(x => new Random(x), x => x.Enum<TestEnum>());
        }

        [Test]
        public void EmptyEnum_Throws()
        {
            Assert.Throws<ArgumentException>(() => Random.Enum<Empty>());
        }

        [Test]
        public void IsNotDeterministic()
        {
            Assertion.IsNotDeterministic(x => new Random(x), x => x.Enum<TestEnum>());
        }
    }
}