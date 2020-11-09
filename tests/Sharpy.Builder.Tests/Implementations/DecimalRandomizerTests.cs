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

        private readonly IDecimalProvider _decimalProvider = new DecimalRandomizer(new Random());

        [Test]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var decimals = new decimal[Amount];

            for (var i = 0; i < Amount; i++)
                decimals[i] = _decimalProvider.Decimal();

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x > 0 && x < decimal.MaxValue),
                "decimals.All(x => x > 0 && x < decimal.MaxValue)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var decimals = new decimal[Amount];

            const decimal max = 200;
            for (var i = 0; i < Amount; i++)
                decimals[i] = _decimalProvider.Decimal(max);

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x > 0 && x < max),
                "decimals.All(x => x > 0 && x < max)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var decimals = new decimal[Amount];

            const decimal min = 100;
            const decimal max = 200;
            for (var i = 0; i < Amount; i++)
                decimals[i] = _decimalProvider.Decimal(min, max);

            decimals.AssertNotAllValuesAreTheSame();
            Assert.True(
                decimals.All(x => x > min && x < max),
                "decimals.All(x => x > min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var decimals = new decimal[Amount];

            const decimal arg = 100;
            for (var i = 0; i < Amount; i++)
                decimals[i] = _decimalProvider.Decimal(arg, arg);

            Assert.True(
                decimals.All(x => x == arg),
                "decimals.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var decimals = new decimal[Amount];

            const decimal arg = 100;
            for (var i = 0; i < Amount; i++)
                decimals[i] = _decimalProvider.Decimal(arg, arg + 1);

            Assert.True(
                decimals.All(x => x == arg),
                "decimals.All(x => x == arg)"
            );
        }
    }
}