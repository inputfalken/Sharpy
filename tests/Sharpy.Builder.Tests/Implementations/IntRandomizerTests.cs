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
        private static readonly IIntProvider IntProvider = new IntRandomizer(new Random());

        [Test]
        public void No_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new IntRandomizer(new Random(i)), x => x.Int());
        }

        [Test]
        public void No_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new IntRandomizer(new Random(i)), x => x.Int());
        }

        [Test]
        public void Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new IntRandomizer(new Random(i)), x => x.Int(50));
        }

        [Test]
        public void Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new IntRandomizer(new Random(i)), x => x.Int(50));
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new IntRandomizer(new Random(i)), x => x.Int(0, 50));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new IntRandomizer(new Random(i)), x => x.Int(0, 50));
        }

        [Test]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var ints = new int[Assertion.Amount];

            for (var i = 0; i < Assertion.Amount; i++)
                ints[i] = IntProvider.Int();

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
                ints[i] = IntProvider.Int(max);

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
                ints[i] = IntProvider.Int(min, max);

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
                ints[i] = IntProvider.Int(arg, arg);

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
                ints[i] = IntProvider.Int(min, max);


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

            Assert.DoesNotThrow(() => IntProvider.Int(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const int max = 100;
            const int min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => IntProvider.Int(min, max));
        }
    }
}