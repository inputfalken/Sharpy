using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorAPI;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Tests.GeneratorAPI {
    [TestFixture]
    public class GeneratorFactoryTests {
        [Test]
        public void Incrementer_No_Arg() {
            var result = Generator
                .Factory
                .Incrementer()
                .Take(500);
            var expected = Enumerable.Range(0, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_Start_Twenty() {
            var result = Generator
                .Factory
                .Incrementer(20)
                .Take(500);
            var expected = Enumerable.Range(20, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_Start_Minus_Twenty() {
            var result = Generator
                .Factory
                .Incrementer(-20)
                .Take(500);
            var expected = Enumerable.Range(-20, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_Start_Int_MaxValue_Throws() {
            var result = Generator
                .Factory
                .Incrementer(int.MaxValue)
                .Take(500);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Incrementer_Int_MinValue_Does_Not_Throw() {
            var result = Generator
                .Factory
                .Incrementer(int.MinValue)
                .Take(500);
            Assert.DoesNotThrow(() => result.ToArray());
        }

        [Test]
        public void Incrementer_Start_Below_Int_MinValue_Throws() {
            int start = int.MinValue;
            var result = Generator
                .Factory
                .Incrementer(start - 1)
                .Take(1);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }

        [Test]
        public void Incrementer_To_Int_Max_Value() {
            const int start = int.MaxValue - 500;
            var result = Generator
                .Factory
                .Incrementer(start)
                .Take(500);
            var expected = Enumerable.Range(start, 500);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Incrementer_To_More_Than_Int_Max_Value() {
            const int start = int.MaxValue - 500;
            var result = Generator
                .Factory
                .Incrementer(start)
                .Take(501);
            Assert.Throws<OverflowException>(() => result.ToArray());
        }
    }
}