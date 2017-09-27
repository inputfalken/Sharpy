using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Sharpy.Core;

namespace Tests.Sharpy.Core.Tests.Implementations {
    [TestFixture]
    public class LazyTests {
        [Test(
            Description = "Verify that Generator.Lazy use the same instance"
        )]
        public void Generate_Same_Instance() {
            var generator = Generator.Lazy(() => new Randomizer());
            Assert.AreSame(generator.Generate(), generator.Generate());
        }

        [Test(
            Description = "Verify that Generator.Lazy is not used before generate is invoked"
        )]
        public void Is_Invoked_After_Take_Is_Invoked() {
            var invoked = false;
            var generator = Generator.Lazy(() => {
                invoked = true;
                return new Randomizer();
            });
            Assert.IsFalse(invoked);
            generator.Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that null argument to Lazy throw exceptions"
        )]
        public void With_Null_Arg_Throws() {
            Assert.Throws<ArgumentNullException>(() => Generator.Lazy<string>(lazy: null));
        }
    }
}