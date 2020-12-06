using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class LongRandomizerTests
    {
        private static readonly ILongProvider LongProvider = new LongRandomizer(new Random());

        [Test]
        public void No_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new LongRandomizer(new Random(i)), x => x.Long());
        }

        [Test]
        public void No_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new LongRandomizer(new Random(i)), x => x.Long());
        }

        [Test]
        public void Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new LongRandomizer(new Random(i)), x => x.Long(50));
        }

        [Test]
        public void Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new LongRandomizer(new Random(i)), x => x.Long(50));
        }

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new LongRandomizer(new Random(i)), x => x.Long(0, 50));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new LongRandomizer(new Random(i)), x => x.Long(0, 50));
        }
        [Test]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var longs = new long[Assertion.Amount];

            for (var i = 0; i < Assertion.Amount; i++)
                longs[i] = LongProvider.Long();

            longs.AssertNotAllValuesAreTheSame();
            Assert.True(
                longs.All(x => x > 0 && x < long.MaxValue),
                "longs.All(x => x > 0 && x < long.MaxValue)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var longs = new long[Assertion.Amount];

            const long max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                longs[i] = LongProvider.Long(max);

            longs.AssertNotAllValuesAreTheSame();
            Assert.True(
                longs.All(x => x >= 0 && x < max),
                "longs.All(x => x > 0 && x < max)"
            );
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var longs = new long[Assertion.Amount];

            const long min = 100;
            const long max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                longs[i] = LongProvider.Long(min, max);

            longs.AssertNotAllValuesAreTheSame();
            Assert.True(
                longs.All(x => x >= min && x < max),
                "longs.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var longs = new long[Assertion.Amount];

            const long arg = 100;
            for (var i = 0; i < Assertion.Amount; i++)
                longs[i] = LongProvider.Long(arg, arg);

            Assert.True(
                longs.All(x => x == arg),
                "longs.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var longs = new long[Assertion.Amount];

            const long max = 100;
            const long min = max - 1;
            for (var i = 0; i < Assertion.Amount; i++)
                longs[i] = LongProvider.Long(min, max);


            Assert.True(
                longs.All(x => x == min),
                "longs.All(x => x == min)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const long max = 100;
            const long min = max;

            Assert.DoesNotThrow(() => LongProvider.Long(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const long max = 100;
            const long min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => LongProvider.Long(min, max));
        }
    }
}