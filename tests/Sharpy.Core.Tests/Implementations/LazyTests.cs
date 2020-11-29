using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Sharpy.Core.Tests.Implementations
{
    [TestFixture]
    public class LazyTests
    {
        [Test(
            Description = "Verify that Generator.Lazy use the same instance"
        )]
        public void Generate_Same_Instance()
        {
            var generator = Generator.Lazy(() => new Randomizer());
            Assert.AreSame(generator.Generate(), generator.Generate());
        }

        [Test(
            Description = "Verify that Generator.Lazy is not used before generate is invoked"
        )]
        public void Is_Invoked_After_Take_Is_Invoked()
        {
            var invoked = false;
            var generator = Generator.Lazy(() =>
            {
                invoked = true;
                return new Randomizer();
            });
            Assert.IsFalse(invoked);
            generator.Generate();
            Assert.IsTrue(invoked);
        }
    }
}