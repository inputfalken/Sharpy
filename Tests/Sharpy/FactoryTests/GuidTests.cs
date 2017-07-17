using System;
using NUnit.Framework;
using Sharpy;

namespace Tests.Sharpy.FactoryTests {
    [TestFixture]
    internal class GuidTests {
        [Test(
            Description = "Verify that Guid generator does not return null"
        )]
        public void Is_Not_Empty() {
            var result = Factory.Guid();
            Assert.AreNotEqual(result.Generate(), Guid.Empty);
        }

        [Test(
            Description = "Verify that Guid generator does not return null"
        )]
        public void Is_Not_Null() {
            var result = Factory.Guid();
            Assert.IsNotNull(result);
        }
    }
}