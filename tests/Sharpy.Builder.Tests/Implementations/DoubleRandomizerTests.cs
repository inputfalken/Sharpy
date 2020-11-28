using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class DoubleRandomizerTests
    {
        private const int Amount = 10000000;
        private const double MaxSupportedPrecision = 0.000_001_000_000_000d;

        private static readonly IDoubleProvider DoubleProvider = new DoubleRandomizer(new Random());

        [Test]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var doubles = new double[Amount];

            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double();

            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x >= 0 && x < double.MaxValue),
                "doubles.All(x => x >= 0 && x < double.MaxValue)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var doubles = new double[Amount];

            const double max = 200;
            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double(max);

            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x >= 0 && x < max),
                "doubles.All(x => x >= 0 && x < max)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var doubles = new double[Amount];

            const double min = 100;
            const double max = 200;
            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double(min, max);

            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x >= min && x < max),
                "doubles.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var doubles = new double[Amount];

            const double arg = 100;
            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double(arg, arg);

            Assert.True(
                doubles.All(x => x == arg),
                "doubles.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var doubles = new double[Amount];
            const double max = 100;
            const double min = max - MaxSupportedPrecision;

            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double(min, max);


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

            Assert.DoesNotThrow(() => DoubleProvider.Double(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const double max = 100;
            const double min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => DoubleProvider.Double(min, max));
        }
    }
}