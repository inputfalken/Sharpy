using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class DoubleRandomizerTests
    {
        private const int Amount = 100000;
        private const int Repeats = 100;

        private static readonly IDoubleProvider DoubleProvider = new DoubleRandomizer(new Random());

        [Test, Repeat(Repeats)]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var doubles = new double[Amount];

            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double();

            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x > 0 && x < double.MaxValue),
                "doubles.All(x => x > 0 && x < double.MaxValue)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var doubles = new double[Amount];

            const double max = 200;
            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double(max);

            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x > 0 && x < max),
                "doubles.All(x => x > 0 && x < max)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var doubles = new double[Amount];

            const double min = 100;
            const double max = 200;
            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double(min, max);

            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x > min && x < max),
                "doubles.All(x => x > min && x < max)"
            );
        }

        [Test, Repeat(Repeats)]
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

        [Test, Repeat(Repeats)]
        public void Exclusive_Max_Arg()
        {
            var doubles = new double[Amount];

            const double arg = 100;
            for (var i = 0; i < Amount; i++)
                doubles[i] = DoubleProvider.Double(arg - 0000000.1d, arg);


            doubles.AssertNotAllValuesAreTheSame();
            Assert.True(
                doubles.All(x => x < arg),
                "doubles.All(x => x < arg)"
            );
        }
    }
}