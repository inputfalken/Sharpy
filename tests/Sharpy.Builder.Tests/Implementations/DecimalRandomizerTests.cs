using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class DecimalRandomizerTests
    {
        private const int Amount = 100000;
        private const int Repeats = 100;
        private const decimal MaxSupportedPrecision = 0.000_000_000_000_000_010m;

        private static readonly IDecimalProvider DecimalProvider = new DecimalRandomizer(new Random());

        [Test, Repeat(Repeats)]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var decimals = new decimal[Amount];

            for (var i = 0; i < Amount; i++)
                decimals[i] = DecimalProvider.Decimal();

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x > 0 && x < decimal.MaxValue),
                "decimals.All(x => x > 0 && x < decimal.MaxValue)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var decimals = new decimal[Amount];

            const decimal max = 200;
            for (var i = 0; i < Amount; i++)
                decimals[i] = DecimalProvider.Decimal(max);

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x > 0 && x < max),
                "decimals.All(x => x > 0 && x < max)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var decimals = new decimal[Amount];

            const decimal min = 100;
            const decimal max = 200;
            for (var i = 0; i < Amount; i++)
                decimals[i] = DecimalProvider.Decimal(min, max);

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x > min && x < max),
                "decimals.All(x => x > min && x < max)"
            );
        }

        [Test, Repeat(Repeats)]
        public void Inclusive_Min_Arg()
        {
            var decimals = new decimal[Amount];

            const decimal arg = 100;
            for (var i = 0; i < Amount; i++)
                decimals[i] = DecimalProvider.Decimal(arg, arg);

            Assert.True(
                decimals.All(x => x == arg),
                "decimals.All(x => x == arg)"
            );
        }

        [Test, Repeat(Repeats)]
        public void Exclusive_Max_Arg()
        {
            var decimals = new decimal[Amount];

            const decimal arg = 100;
            for (var i = 0; i < Amount; i++)
                decimals[i] = DecimalProvider.Decimal(arg - MaxSupportedPrecision, arg);
            

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x < arg),
                "decimals.All(x => x < arg)"
            );
        }
    }
}