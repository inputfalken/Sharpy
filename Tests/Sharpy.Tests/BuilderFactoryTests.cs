using NUnit.Framework;

namespace Sharpy.Tests {
    [TestFixture]
    internal class BuilderFactoryTests {
        [Test]
        public void Enumerable_Is_Not_Null() => Assert.IsNotNull(BuilderFactory.Enumerable(b => b, 20));

        [Test]
        public void Enumerable_Supply_Builder_Is_Not_Null() =>
            Assert.IsNotNull(new Builder.Builder().Enumerable(m => m, 20));

        [Test]
        public void Enumerable_With_Counter_Is_Not_Null() =>
            Assert.IsNotNull(BuilderFactory.Enumerable((b, i) => b, 20));

        [Test]
        public void Generator_Is_Not_Null() => Assert.IsNotNull(BuilderFactory.Generator(b => b));

        [Test]
        public void Generator_Supply_Builder_Is_Not_Null() => Assert.IsNotNull(new Builder.Builder().Generator(m => m));

        [Test]
        public void Generator_With_Counter_Is_Not_Null() => Assert.IsNotNull(BuilderFactory.Generator((b, i) => b));
    }
}