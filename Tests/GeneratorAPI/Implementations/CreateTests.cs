using NUnit.Framework;
using NUnit.Framework.Internal;
using Sharpy.Generator;

namespace Tests.GeneratorAPI.Implementations {
    [TestFixture]
    public class CreateTests {
        [Test(
            Description = "Verifies that Generator.Create uses same instance"
        )]
        public void Generate_Uses_Same_Instance() {
            var generator = Generator.Create(new Randomizer());
            Assert.AreSame(generator.Generate(), generator.Generate());
        }

        [Test(
            Description = "Veirfy that Create does not throw exception when the argument is null" +
                          "Since the argument is just T you can't be sure if the consumer was using the default value of int(0) intentionally"
        )]
        public void With_Null_Arg_Throws() {
            Assert.DoesNotThrow(() => Generator.Create<string>(null));
        }
    }
}