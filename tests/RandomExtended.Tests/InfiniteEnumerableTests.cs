using System;
using System.Linq;
using NUnit.Framework;
using RandomExtended;

namespace RandomExtensions.Tests
{
    [TestFixture]
    public class InfiniteEnumerableTests
    {
        [Test]
        public void Test()
        {
            Assertion.IsDeterministic(i => new Random(i),
                x => x
                    .InfiniteEnumerable(y => new {Decimal = y.Decimal(0, 10), Integer = y.Int(1, 10)})
                    .Take(10),
                100
            );
        }
    }
}