using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class IntRandomizerTests
    {
        private const int Amount = 100000;
        private const int Repeats = 100;

        private static readonly IIntegerProvider IntegerProvider = new IntegerRandomizer(new Random());

        [Test, Repeat(Repeats)]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var ints = new int[Amount];

            for (var i = 0; i < Amount; i++)
                ints[i] = IntegerProvider.Integer();

            ints.AssertNotAllValuesAreTheSame();
            Assert.True(
                ints.All(x => x > 0 && x < int.MaxValue),
                "ints.All(x => x > 0 && x < int.MaxValue)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var ints = new int[Amount];

            const int max = 200;
            for (var i = 0; i < Amount; i++)
                ints[i] = IntegerProvider.Integer(max);

            ints.AssertNotAllValuesAreTheSame();
            Assert.True(
                ints.All(x => x >= 0 && x < max),
                "ints.All(x => x > 0 && x < max)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var ints = new int[Amount];

            const int min = 100;
            const int max = 200;
            for (var i = 0; i < Amount; i++)
                ints[i] = IntegerProvider.Integer(min, max);

            ints.AssertNotAllValuesAreTheSame();
            Assert.True(
                ints.All(x => x >= min && x < max),
                "ints.All(x => x >= min && x < max)"
            );
        }

        [Test, Repeat(Repeats)]
        public void Inclusive_Min_Arg()
        {
            var ints = new int[Amount];

            const int arg = 100;
            for (var i = 0; i < Amount; i++)
                ints[i] = IntegerProvider.Integer(arg, arg);

            Assert.True(
                ints.All(x => x == arg),
                "ints.All(x => x == arg)"
            );
        }

        [Test, Repeat(Repeats)]
        public void Exclusive_Max_Arg()
        {
            var ints = new int[Amount];

            const int max = 100;
            const int min = max -1;
            for (var i = 0; i < Amount; i++)
                ints[i] = IntegerProvider.Integer(min, max);


            Assert.True(
                ints.All(x => x == min),
                "ints.All(x => x == min)"
            );
        }
    }
}