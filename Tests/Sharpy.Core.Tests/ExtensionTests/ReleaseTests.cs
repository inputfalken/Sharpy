using System;
using NUnit.Framework;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.GeneratorAPI.LinqTests {
    [TestFixture]
    internal class ReleaseTests {
        [Test(
            Description = "Verify that release will Immediately release the elements"
        )]
        public void Is_Not_Lazy_Evaluated() {
            var invoked = false;
            Generator.Incrementer(0).Do(actual => { invoked = true; })
                .Release(1);
            Assert.AreEqual(true, invoked);
        }

        [Test(
            Description = "Verify that calling release with negative number throws exception"
        )]
        public void Negative_Number_Throws() {
            Assert.Throws<ArgumentException>(() => { Generator.Incrementer(0).Release(-5); });
        }

        [Test(
            Description = "Verify that calling release with null generator throws exception"
        )]
        public void Null_Generator_Throws() {
            Assert.Throws<ArgumentNullException>(() => {
                IGenerator<int> generator = null;
                generator.Release(1);
            });
        }

        [Test(
            Description = "Verify that release will Immediately release the elements"
        )]
        public void Release_Elements_Immediately() {
            var expected = 0;
            Generator.Incrementer(0).Do(actual => {
                    Assert.AreEqual(expected, actual);
                    expected++;
                })
                .Release(5);
        }

        [Test(
            Description = "Verify that release returns the same instance of IGenerator<T>"
        )]
        public void Same_Generator() {
            var expected = Generator.Incrementer(0);
            var actual = expected.Release(5);
            Assert.AreSame(expected, actual);
        }

        [Test(
            Description = "Verify that invoking release with zero does nothing"
        )]
        public void Zero_Elements_Does_Nothing() {
            var invoked = false;
            Generator.Incrementer(0).Release(0)
                .Do(i => invoked = true);
            Assert.IsFalse(invoked);
        }
    }
}