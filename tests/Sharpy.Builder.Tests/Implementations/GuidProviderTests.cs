using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class GuidProviderTests
    {
        /// <summary>
        ///     <para>The amount generated from GenrateMany Tests.</para>
        /// </summary>
        private const int Amount = 10000000;

        [Test]
        public void IsUnique()
        {
            var guidProvider = new GuidProvider();
            var guids = new Guid[Amount];

            for (var i = 0; i < Amount; i++) guids[i] = guidProvider.Guid();

            Assert.True(
                guids.GroupBy(x => x).All(x => x.Count() == 1),
                "guids.GroupBy(x => x).All(x => x.Count() == 1)"
            );
        }

        [Test]
        public void Formatting()
        {
            var guidProvider = new GuidProvider();

            Assert.DoesNotThrow(() => Guid.ParseExact(guidProvider.Guid(GuidFormat.DigitsOnly), "N"));
            Assert.DoesNotThrow(() => Guid.ParseExact(guidProvider.Guid(GuidFormat.DigitsWithHyphens), "D"));
            Assert.DoesNotThrow(() =>
                Guid.ParseExact(guidProvider.Guid(GuidFormat.DigitsWithHyphensWrappedInBrackets), "B"));
            Assert.DoesNotThrow(() =>
                Guid.ParseExact(guidProvider.Guid(GuidFormat.DigitsWithHyphensWrappedInParentheses), "P"));
            Assert.DoesNotThrow(() =>
                Guid.ParseExact(guidProvider.Guid(GuidFormat.FourHexadecimalWrappedInBrackets), "X"));
        }
    }
}