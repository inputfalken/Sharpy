using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sharpy;

namespace Tests.Integration {
    [TestFixture]
    internal class DeferedTests {
        [Test(
            Author = "Robert",
            Description = "Check to see if select succeeds in mapping as from defer"
        )]
        public void Defer_Select() {
            var result = Productor
                .Function(() => "Hej")
                .Generate(s => s.Length)
                .Produce();
            Assert.AreEqual(3, result);
        }

        [Test(
            Author = "Robert",
            Description = "If you can zip two defered"
        )]
        public void Return_Zip_Return() {
            var result = Productor
                .Function(() => "Hej")
                .Zip(Productor.Function(() => 20), (s, i) => s.Length + i)
                .Produce();
            Assert.AreEqual(23, result);
        }

        [Test(
            Author = "Robert",
            Description = "Checks if zip random works like a wrapped random"
        )]
        public void Defered_Zip_Defered_Random() {
            const int seed = 10;
            var result = Productor
                .Function(() => new Random(seed))
                .Zip(Productor.Function(() => new Random(seed)), (rand, zipedRand) => rand.Next() == zipedRand.Next())
                .ToArray(40);
            Assert.IsTrue(result.All(b => b));
        }

        [Test(
            Author = "Robert",
            Description = "If you can zip IEnumerable with a YieldFunction"
        )]
        public void Return_Zip_Enumerable() {
            var result = Productor
                .Function(() => "hej")
                .Zip(Enumerable.Range(0, 10), (s, i) => s + i)
                .Take(10)
                .ToArray();
            var expected = new[] {"hej0", "hej1", "hej2", "hej3", "hej4", "hej5", "hej6", "hej7", "hej8", "hej9"};
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "If you can zip sequence with a YieldFunction"
        )]
        public void Return_Zip_Sequence() {
            var result = Productor
                .Function(() => "hej")
                .Zip(Productor.Sequence(Enumerable.Range(0, 10)), (s, i) => s + i)
                .Take(10)
                .ToArray();
            var expected = new[] {"hej0", "hej1", "hej2", "hej3", "hej4", "hej5", "hej6", "hej7", "hej8", "hej9"};
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "If you can zip Function with a YieldFunction"
        )]
        public void Return_Zip_Defered() {
            var result = Productor
                .Function(() => "hej")
                .Zip(Productor.Function(() => 10), (s, i) => s + i)
                .Produce();
            Assert.AreEqual("hej10", result);
        }
    }
}