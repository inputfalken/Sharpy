using System;
using NUnit.Framework;
using Sharpy.Core.Linq;

namespace Sharpy.Core.Tests.ExtensionTests {
    [TestFixture]
    internal class Skiptests {
        [Test(
            Description = "Verify that skip is lazy Evaluated"
        )]
        public void Is_Evaluated_After_Generate_Is_Invoked() {
            var invoked = false;
            var generator = Generator
                .Function<string>(() => {
                    invoked = true;
                    return null;
                })
                .Skip(50);
            // Not evaluated
            Assert.IsFalse(invoked);
            // Evaluated
            generator.Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that you can skip fifty elements"
        )]
        public void Skip_Fifty() {
            var generator = Generator.Incrementer(1).Skip(50);
            Assert.AreEqual(51, generator.Generate());
        }

        [Test(
            Description = "Verify that you can't skip negative numbers"
        )]
        public void Skip_Negative_Number_Throws() {
            var generator = Generator.Incrementer(1);
            Assert.Throws<ArgumentException>(() => generator.Skip(-1));
        }

        [Test(
            Description = "Verify that using skip with null generator throws"
        )]
        public void Skip_Null_Generator_Throws() {
            IGenerator<int> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Skip(1));
        }

        [Test(
            Description = "Verify that you can skip zero elements"
        )]
        public void Skip_Zero() {
            var generator = Generator.Incrementer(1).Skip(0);
            Assert.AreEqual(1, generator.Generate());
        }

        [Test(
            Description = "Verify that you can skip zero elements and it does nothing"
        )]
        public void Skip_Zero_Does_Nothing() {
            var result = Generator.Incrementer(1).Skip(0);

            var expected = Generator.Incrementer(1);
            Assert.AreEqual(expected.Generate(), result.Generate());
        }

        [Test(
            Description = "Verify that skipping 0 elements returns same instance"
        )]
        public void Skip_Zero_Returns_Same_instance() {
            var generator = Generator.Incrementer(1);

            Assert.AreSame(generator, generator.Skip(0));
            Assert.AreNotSame(generator, generator.Skip(1));
        }
    }
}