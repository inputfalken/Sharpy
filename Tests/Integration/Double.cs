using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    public class Double {
        [Test]
        public void No_Arguments() {
            var generator = Productor.Yield(new Provider());
            var generateMany = generator.Generate(generator1 => generator1.Double()).Take(20);
            Assert.IsTrue(generateMany.All(d => d < 1 && d > 0));
        }


        [Test]
        public void One_Arg_Eleven_Point_Two() {
            const double max = 11.2;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator.Generate(generator1 => generator1.Double(max)).Take(20);
            Assert.IsTrue(generateMany.All(d => d < max));
        }

        [Test]
        public void One_Arg_Minus_Eleven_Point_Two() {
            const double max = -11.2;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator
                .Generate(generator1 => generator1.Double(max))
                .Take(20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d < max));
        }

        [Test]
        public void One_Arg_Zero() {
            const double max = 0;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator
                .Generate(generator1 => generator1.Double(max))
                .Take(20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d < max));
        }

        [Test]
        public void Two_Args_Eleven_Point_Two_And_Eleven_Point_Two() {
            const double min = 11.2;
            const double max = 11.2;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator
                .Generate(generator1 => generator1.Double(min, max))
                .Take(20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Args_Eleven_Point_Two_And_Ten_Point_Four() {
            const double min = 11.2;
            const double max = 10.4;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator
                .Generate(generator1 => generator1.Double(min, max))
                .Take(20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Args_Minus_Eleven_Point_Two_And_Minus_Ten_Point_Four() {
            const double min = -11.4;
            const double max = -10.2;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator
                .Generate(generator1 => generator1.Double(min, max))
                .Take(20);
            Assert.IsTrue(generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Args_Minus_Ten_Point_Two_And_Minus_Eleven_Point_Four() {
            const double min = -10.2;
            const double max = -11.4;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator
                .Generate(generator1 => generator1.Double(min, max))
                .Take(20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Args_One_Point_Two_And_Three_Point_Four() {
            const double min = 1.2;
            const double max = 3.4;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator
                .Generate(generator1 => generator1.Double(min, max))
                .Take(20);

            Assert.IsTrue(generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Args_Ten_Point_Two_And_Eleven_Point_Four() {
            const double min = 10.2;
            const double max = 11.4;
            var generator = Productor.Yield(new Provider());
            var generateMany = generator
                .Generate(generator1 => generator1.Double(min, max))
                .Take(20);

            Assert.IsTrue(generateMany.All(d => d > min && d < max));
        }
    }
}