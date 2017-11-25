using NUnit.Framework;
using Sharpy;
using Sharpy.Builder;
using static NUnit.Framework.Assert;
using static Sharpy.BuilderFactory;

//namespace Tests.Sharpy.Tests {
//    [TestFixture]
//    internal class BuilderFactoryTests {
//        [Test]
//        public void Generator_Is_Not_Null() => IsNotNull(Generator(b => b));

//        [Test]
//        public void Generator_With_Counter_Is_Not_Null() => IsNotNull(Generator((b, i) => b));

//        [Test]
//        public void Enumerable_Is_Not_Null() => IsNotNull(Enumerable(b => b, 20));

//        [Test]
//        public void Enumerable_With_Counter_Is_Not_Null() => IsNotNull(Enumerable((b, i) => b, 20));

//        [Test]
//        public void Enumerable_Supply_Builder_Is_Not_Null() => IsNotNull(new Builder().Enumerable(m => m, 20));

//        [Test]
//        public void Generator_Supply_Builder_Is_Not_Null() => IsNotNull(new Builder().Generator(m => m));
//    }
//}