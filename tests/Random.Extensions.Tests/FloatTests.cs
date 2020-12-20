using System;
using System.Linq;
using NUnit.Framework;

namespace Random.Extensions.Tests
{
    [TestFixture]
    public class FloatTests
    {
        private const float MaxSupportedPrecision = 0.100_00f;
        private static readonly System.Random Random = new();

        [Test]
        public void Min_Max_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new System.Random(i), x => x.Float(0, 50));
        }

        [Test]
        public void Min_Max_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new System.Random(i), x => x.Float(0, 50));
        }

        [Test]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var floats = new float[Assertion.Amount];

            const float min = 100;
            const float max = 200;
            for (var i = 0; i < Assertion.Amount; i++)
                floats[i] = Random.Float(min, max);

            floats.AssertNotAllValuesAreTheSame();
            Assert.True(
                floats.All(x => x >= min && x < max),
                "Floats.All(x => x >= min && x < max)"
            );
        }

        [Test]
        public void Inclusive_Min_Arg()
        {
            var floats = new float[Assertion.Amount];

            const float arg = 100;
            for (var i = 0; i < Assertion.Amount; i++)
                floats[i] = Random.Float(arg, arg);

            Assert.True(
                floats.All(x => x == arg),
                "Floats.All(x => x == arg)"
            );
        }

        [Test]
        public void Exclusive_Max_Arg()
        {
            var floats = new float[Assertion.Amount];
            const float max = 100;
            const float min = max - MaxSupportedPrecision;

            for (var i = 0; i < Assertion.Amount; i++)
                floats[i] = Random.Float(min, max);


            floats.AssertNotAllValuesAreTheSame();
            Assert.True(
                floats.All(x => x < max),
                "Floats.All(x => x < max)"
            );
        }

        [Test]
        public void Min_Equal_To_Max_Does_Not_Throw()
        {
            const float max = 100;
            const float min = max;

            Assert.DoesNotThrow(() => Random.Float(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const float max = 100;
            const float min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => Random.Float(min, max));
        }

        [Test]
        public void MinValue_And_Max_Does_Not_Throw()
        {
            Assert.DoesNotThrow(() => Random.Float(float.MinValue, float.MaxValue));
        }
    }
}