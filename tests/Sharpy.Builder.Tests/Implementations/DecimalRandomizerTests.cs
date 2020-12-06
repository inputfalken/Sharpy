using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class DecimalRandomizerTests
    {
        private const decimal MaxSupportedPrecision = 0.000_000_000_000_000_010m;

        private static readonly IDecimalProvider DecimalProvider = new DecimalRandomizer(new Random());

        [Test]
        public void No_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new DecimalRandomizer(new Random(i)), x => x.Decimal());
        }

        [Test]
        public void No_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new DecimalRandomizer(new Random(i)), x => x.Decimal());
        }

        [Test]
        public void Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new DecimalRandomizer(new Random(i)), x => x.Decimal(50));
        }

        [Test]
        public void Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new DecimalRandomizer(new Random(i)), x => x.Decimal(50));
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new DecimalRandomizer(new Random(i)), x => x.Decimal(0, 50));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new DecimalRandomizer(new Random(i)), x => x.Decimal(0, 50));
        }

        [Test]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var decimals = new decimal[Assertion.Amount];

            for (var i = 0; i < Assertion.Amount; i++)
                decimals[i] = DecimalProvider.Decimal();

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x >= 0 && x < decimal.MaxValue),
                "decimals.All(x => x >= 0 && x < decimal.MaxValue)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var decimals = new decimal[Assertion.Amount];

            const decimal max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                decimals[i] = DecimalProvider.Decimal(max);

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x >= 0 && x < max),
                "decimals.All(x => x >= 0 && x < max)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var decimals = new decimal[Assertion.Amount];

            const decimal min = 100;
            const decimal max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                decimals[i] = DecimalProvider.Decimal(min, max);

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x >= min && x < max),
                "decimals.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var decimals = new decimal[Assertion.Amount];

            const decimal arg = 100;
            for (var i = 0; i < Assertion.Amount; i++)
                decimals[i] = DecimalProvider.Decimal(arg, arg);

            Assert.True(
                decimals.All(x => x == arg),
                "decimals.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var decimals = new decimal[Assertion.Amount];

            const decimal max = 100;
            const decimal min = max - MaxSupportedPrecision;
            for (var i = 0; i < Assertion.Amount; i++)
                decimals[i] = DecimalProvider.Decimal(min, max);

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x < max),
                "decimals.All(x => x < max)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const decimal max = 100;
            const decimal min = max;

            Assert.DoesNotThrow(() => DecimalProvider.Decimal(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const decimal max = 100;
            const decimal min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => DecimalProvider.Decimal(min, max));
        }
    }
}