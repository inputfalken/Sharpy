using System;
using NUnit.Framework;

namespace Sharpy.Core.Tests.Implementations {
    [TestFixture]
    internal class GuidTests {
        [Test(
            Description = "Verify that Guid generator does not return null"
        )]
        public void Is_Not_Empty() {
            var result = Generator.Guid();
            Assert.AreNotEqual(result.Generate(), Guid.Empty);
        }

        [Test(
            Description = "Verify that Guid generator does not return null"
        )]
        public void Is_Not_Null() {
            var result = Generator.Guid();
            Assert.IsNotNull(result);
        }
    }
}