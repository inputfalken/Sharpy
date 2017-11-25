using System;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations {
    [TestFixture]
    public class DoubleRandomizerTests {
        [Test]
        [Repeat(100)]
        public void No_Arguments() {
            var doubleRandomizer = new DoubleRandomizer(new Random());
            var result = doubleRandomizer.Double();
            Assert.IsTrue(result < 1 && result > 0);
        }

        [Test]
        [Repeat(100)]
        public void One_Arg() {
            const double max = 11.2;
            var doubleRandomizer = new DoubleRandomizer(new Random());
            var result = doubleRandomizer.Double(max);
            Assert.IsTrue(result < max);
        }

        [Test]
        public void One_Arg_Minus_Throws() {
            const double max = -11.2;
            var doubleRandomizer = new DoubleRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => doubleRandomizer.Double(max));
        }

        [Test]
        public void One_Arg_Zero_Throws() {
            const double max = 0;
            var doubleRandomizer = new DoubleRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => doubleRandomizer.Double(max));
        }

        [Test]
        public void Two_Args_Min_More_Than_Max_Throws() {
            const double min = 11.2;
            const double max = 10.4;
            var doubleRandomizer = new DoubleRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => doubleRandomizer.Double(min, max));
        }

        [Test]
        [Repeat(100)]
        public void Two_Args_Minus_Eleven_Point_Two_And_Minus_Ten_Point_Four() {
            const double min = -11.4;
            const double max = -10.2;
            var doubleRandomizer = new DoubleRandomizer(new Random());
            var result = doubleRandomizer.Double(min, max);
            Assert.IsTrue(result > min && result < max);
        }

        [Test]
        public void Two_Args_Same_Value_Throws() {
            const double min = 11.2;
            const double max = 11.2;
            var doubleRandomizer = new DoubleRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => doubleRandomizer.Double(min, max));
        }

        [Test]
        public void Two_Negatve_Args_Throws() {
            const double min = -10.2;
            const double max = -11.4;
            var doubleRandomizer = new DoubleRandomizer(new Random());
            Assert.Throws<ArgumentOutOfRangeException>(() => doubleRandomizer.Double(min, max));
        }
    }
}