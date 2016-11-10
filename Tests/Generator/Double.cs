using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class Double {
        [Test]
        public void Two_Arguments_One_Point_Two_And_Three_Point_Four() {
            const double min = 1.2;
            const double max = 3.4;
            var generator = Sharpy.Generator.Create();
            var generateMany = generator.GenerateMany(generator1 => generator1.Double(min, max), 20);

            Assert.IsTrue(generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Arguments_Ten_Point_Two_And_Eleven_Point_Four() {
            const double min = 10.2;
            const double max = 11.4;
            var generator = Sharpy.Generator.Create();
            var generateMany = generator.GenerateMany(generator1 => generator1.Double(min, max), 20);

            Assert.IsTrue(generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Arguments_Minus_Ten_Point_Two_And_Minus_Eleven_Point_Four() {
            const double min = -10.2;
            const double max = -11.4;
            var generator = Sharpy.Generator.Create();
            var generateMany = generator.GenerateMany(generator1 => generator1.Double(min, max), 20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Arguments_Minus_Eleven_Point_Two_And_Minus_Ten_Point_Four() {
            const double min = -11.4;
            const double max = -10.2;
            var generator = Sharpy.Generator.Create();
            var generateMany = generator.GenerateMany(generator1 => generator1.Double(min, max), 20);
            Assert.IsTrue(generateMany.All(d => d > min && d < max));
        }

        [Test]
        public void Two_Arguments_Eleven_Point_Two_And_Ten_Point_Four() {
            const double min = 11.2;
            const double max = 10.4;
            var generator = Sharpy.Generator.Create();
            var generateMany = generator.GenerateMany(generator1 => generator1.Double(min, max), 20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d > min && d < max));
        }


        [Test]
        public void One_Arguments_Eleven_Point_Two() {
            const double max = 11.2;
            var generator = Sharpy.Generator.Create();
            var generateMany = generator.GenerateMany(generator1 => generator1.Double(max), 20);
            Assert.IsTrue(generateMany.All(d => d < max));
        }

        [Test]
        public void One_Arguments_Minus_Eleven_Point_Two() {
            const double max = -11.2;
            var generator = Sharpy.Generator.Create();
            var generateMany = generator.GenerateMany(generator1 => generator1.Double(max), 20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d < max));
        }

        [Test]
        public void One_Arguments_Zero() {
            const double max = 0;
            var generator = Sharpy.Generator.Create();
            var generateMany = generator.GenerateMany(generator1 => generator1.Double(max), 20);
            Assert.Throws<ArgumentOutOfRangeException>(() => generateMany.All(d => d < max));
        }
    }
}