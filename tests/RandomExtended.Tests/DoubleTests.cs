using System;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class DoubleTests
    {
        private const double MaxSupportedPrecision = 0.000_001_000_000_000d;

        private static readonly Random Random = new();

        [Test]
        public void Is_Distributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Double(double.MinValue, double.MaxValue),
                x => Assert.IsTrue(x.Count > Assertion.Amount / 2, "x.Count > Assertion.Amount / 2")
            );
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new Random(i), x => x.Double(0, 50));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new Random(i), x => x.Double(0, 50));
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var doubles = new double[Assertion.Amount];

            const double min = 100;
            const double max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                doubles[i] = Random.Double(min, max);

            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x >= min && x < max),
                "doubles.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var doubles = new double[Assertion.Amount];

            const double arg = 100;
            for (var i = 0; i < Assertion.Amount; i++)
                doubles[i] = Random.Double(arg, arg);

            Assert.True(
                doubles.All(x => x == arg),
                "doubles.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var doubles = new double[Assertion.Amount];
            const double max = 100;
            const double min = max - MaxSupportedPrecision;

            for (var i = 0; i < Assertion.Amount; i++)
                doubles[i] = Random.Double(min, max);


            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x < max),
                "doubles.All(x => x < max)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const double max = 100;
            const double min = max;

            Assertion.DoesNotThrow(() => Random.Double(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const double max = 100;
            const double min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Double(min, max));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Throw()
        {
            Assertion.DoesNotThrow(() => Random.Double(double.MinValue, double.MaxValue));
        }

        [Test]
        public void MinValue_And_MaxValue_Does_Not_Produce_Same_Values()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Double(double.MinValue, double.MaxValue),
                _ => { }
            );
        }
    }
}