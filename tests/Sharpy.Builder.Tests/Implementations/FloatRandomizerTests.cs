﻿using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class FloatRandomizerTests
    {
        private const int Amount = 100000;
        private const int Repeats = 100;
        private const float MaxSupportedPrecision = 0.100_00f;

        private static readonly IFloatProvider FloatProvider = new FloatRandomizer(new Random());

        [Test, Repeat(Repeats)]
        public void No_Arg_All_Values_Are_Between_Zero_And_MaxValue()
        {
            var floats = new float[Amount];

            for (var i = 0; i < Amount; i++)
                floats[i] = FloatProvider.Float();

            floats.AssertNotAllValuesAreTheSame();
            Assert.True(
                floats.All(x => x >= 0 && x < float.MaxValue),
                "Floats.All(x => x >= 0 && x < Float.MaxValue)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Zero_And_Max()
        {
            var floats = new float[Amount];

            const float max = 200;
            for (var i = 0; i < Amount; i++)
                floats[i] = FloatProvider.Float(max);

            floats.AssertNotAllValuesAreTheSame();
            Assert.True(
                floats.All(x => x >= 0 && x < max),
                "Floats.All(x => x >= 0 && x < max)"
            );
        }

        [Test, Repeat(Repeats)]
        public void All_Values_Are_Between_Min_And_Max()
        {
            var floats = new float[Amount];

            const float min = 100;
            const float max = 200;
            for (var i = 0; i < Amount; i++)
                floats[i] = FloatProvider.Float(min, max);

            floats.AssertNotAllValuesAreTheSame();
            Assert.True(
                floats.All(x => x >= min && x < max),
                "Floats.All(x => x >= min && x < max)"
            );
        }

        [Test, Repeat(Repeats)]
        public void Inclusive_Min_Arg()
        {
            var floats = new float[Amount];

            const float arg = 100;
            for (var i = 0; i < Amount; i++)
                floats[i] = FloatProvider.Float(arg, arg);

            Assert.True(
                floats.All(x => x == arg),
                "Floats.All(x => x == arg)"
            );
        }

        [Test, Repeat(Repeats)]
        public void Exclusive_Max_Arg()
        {
            var floats = new float[Amount];
            const float max = 100;
            const float min = max - MaxSupportedPrecision;

            for (var i = 0; i < Amount; i++)
                floats[i] = FloatProvider.Float(min, max);


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

            Assert.DoesNotThrow(() => FloatProvider.Float(min, max));
        }

        [Test]
        public void Min_Greater_Than_Max_Does_Throw()
        {
            const float max = 100;
            const float min = max + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => FloatProvider.Float(min, max));
        }
    }
}