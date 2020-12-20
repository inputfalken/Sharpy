using NUnit.Framework;

namespace Random.Extensions.Tests
{
    [TestFixture]
    public class ArgumentTests
    {
        private static readonly System.Random Random = new();

        [Test]
        public void Two_Arguments_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(
                i => new System.Random(i), x => x.Argument(1, 2)
            );
        }

        [Test]
        public void Two_Arguments_Is_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new System.Random(i), x => x.Argument(1, 2));
        }

        [Test]
        public void Three_Arguments_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new System.Random(i), x => x.Argument(1, 2, 3));
        }

        [Test]
        public void Three_Arguments_Is_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => (new System.Random(i)), x => x.Argument(1, 2, 3));
        }

        [Test]
        public void Four_Arguments_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => (new System.Random(i)), x => x.Argument(1, 2, 3, 4));
        }

        [Test]
        public void Four_Arguments_Is_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => (new System.Random(i)), x => x.Argument(1, 2, 3, 4));
        }

        [Test]
        public void Two_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2),
                x => Assert.AreEqual(2, x.Count)
            );
        }

        [Test]
        public void Three_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2, 3),
                x => Assert.AreEqual(3, x.Count)
            );
        }

        [Test]
        public void Four_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2, 3, 4),
                x => Assert.AreEqual(4, x.Count)
            );
        }

        [Test]
        public void Five_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2, 3, 4, 5),
                x => Assert.AreEqual(5, x.Count)
            );
        }

        [Test]
        public void Six_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2, 3, 4, 5, 6),
                x => Assert.AreEqual(6, x.Count)
            );
        }

        [Test]
        public void Seven_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2, 3, 4, 5, 6, 7),
                x => Assert.AreEqual(7, x.Count)
            );
        }

        [Test]
        public void Eight_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2, 3, 4, 5, 6, 7, 8),
                x => Assert.AreEqual(8, x.Count)
            );
        }

        [Test]
        public void Nine_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2, 3, 4, 5, 6, 7, 8, 9),
                x => Assert.AreEqual(9, x.Count)
            );
        }

        [Test]
        public void Ten_Arguments_IsDistributed()
        {
            Assertion.IsDistributed(
                Random,
                x => x.Argument(1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
                x => Assert.AreEqual(10, x.Count)
            );
        }
    }
}