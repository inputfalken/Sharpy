using System;
using GeneratorAPI;
using NUnit.Framework;

namespace Tests.GeneratorAPI.Implementations {
    [TestFixture]
    internal class FunctionTests {
        [Test(
            Description = "Verify that Generator.Function does not use the same instance"
        )]
        public void Function_Not_Same_Instance() {
            var generator = Generator.Function(() => new Random());
            Assert.AreNotSame(generator.Generate(), generator.Generate());
        }

        [Test(
            Description = "Verify that passing null to Generator.Function throws exception"
        )]
        public void Function_Throw_Exception_When_Null() {
            Assert.Throws<ArgumentNullException>(() => Generator.Function<string>(null), "Argument cannot be null");
        }
    }
}