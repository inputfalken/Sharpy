using System;
using GeneratorAPI;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.GeneratorAPI.Implementations {
    [TestFixture]
    public class LazyTests {
        [Test(
            Description = "Verify that Generator.Lazy uses same instance"
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
            Generator.Lazy(() => {
                invoked = true;
                return new Randomizer();
            }).Generate();
            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that Generator.Lazy is not used before generate is invoked"
        )]
        public void Is_Not_Invoked_Before_Take_Is_Invoked() {
            var invoked = false;
            Generator.Lazy(() => {
                invoked = true;
                return new Randomizer();
            });
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that null argument to Lazy throw exceptions"
        )]
        public void With_Null_Arg_Throws() {
            Assert.Throws<ArgumentNullException>(() => Generator.Lazy<string>(lazy: null));
        }
    }
}