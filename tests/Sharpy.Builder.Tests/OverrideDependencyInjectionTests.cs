using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Tests {
    [TestFixture]
    public class OverrideDependencyInjectionTests {
        [Test]
        public void Override_IntProvider_Does_Not_Give_Default() {
            var provider = new Configuration {IntegerProvider = new IntegerProvider()}.BuildServiceProvider();
            provider.VerifyServiceProvide<IIntegerProvider, IntegerProvider>();
        }

        private class IntegerProvider : IIntegerProvider {
            public int Integer(int max) => 2;

            public int Integer(int min, int max) => throw new NotImplementedException();

            public int Integer() => throw new NotImplementedException();
        }
    }
}