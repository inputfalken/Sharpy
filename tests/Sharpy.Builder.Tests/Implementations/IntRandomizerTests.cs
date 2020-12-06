using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class IntRandomizerTests
    {
        private static readonly IIntegerProvider IntegerProvider = new IntegerRandomizer(new Random());

        [Test]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var ints = new int[Assertion.Amount];

            for (var i = 0; i < Assertion.Amount; i++)
                ints[i] = IntegerProvider.Integer();

            ints.AssertNotAllValuesAreTheSame();
            Assert.True(
                ints.All(x => x >= 0 && x < int.MaxValue),
                "ints.All(x => x >= 0 && x < int.MaxValue)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var ints = new int[Assertion.Amount];

            const int max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                ints[i] = IntegerProvider.Integer(max);

            ints.AssertNotAllValuesAreTheSame();
            Assert.True(
                ints.All(x => x >= 0 && x < max),
                "ints.All(x => x >= 0 && x < max)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var ints = new int[Assertion.Amount];

            const int min = 100;
            const int max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                ints[i] = IntegerProvider.Integer(min, max);

            ints.AssertNotAllValuesAreTheSame();
            Assert.True(
                ints.All(x => x >= min && x < max),
                "ints.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var ints = new int[Assertion.Amount];

            const int arg = 100;
            for (var i = 0; i < Assertion.Amount; i++)
                ints[i] = IntegerProvider.Integer(arg, arg);

            Assert.True(
                ints.All(x => x == arg),
                "ints.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var ints = new int[Assertion.Amount];

            const int max = 100;
            const int min = max - 1;
            for (var i = 0; i < Assertion.Amount; i++)
                ints[i] = IntegerProvider.Integer(min, max);


            Assert.True(
                ints.All(x => x == min),
                "ints.All(x => x == min)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const int max = 100;
            const int min = max;

            Assert.DoesNotThrow(() => IntegerProvider.Integer(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const int max = 100;
            const int min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => IntegerProvider.Integer(min, max));
        }
    }
}