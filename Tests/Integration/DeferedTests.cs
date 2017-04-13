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
                .Deferred(() => "Hej")
                .Generate(s => s.Length)
                .Take();
            Assert.AreEqual(3, result);
        }

        [Test(
            Author = "Robert",
            Description = "If you can zip two defered"
        )]
        public void Return_Zip_Return() {
            var result = Productor
                .Deferred(() => "Hej")
                .GenerateZip(Productor.Deferred(() => 20), (s, i) => s.Length + i)
                .GetProvider();
            Assert.AreEqual(23, result);
        }

        [Test(
            Author = "Robert",
            Description = "Checks if zip random works like a wrapped random"
        )]
        public void Defered_Zip_Defered_Random() {
            const int seed = 10;
            var result = Productor
                .Deferred(() => new Random(seed))
                .GenerateZip(Productor.Deferred(() => new Random(seed)), (rand, zipedRand) => rand.Next() == zipedRand.Next())
                .ToArray(40);
            Assert.IsTrue(result.All(b => b));
        }

        [Test(
            Author = "Robert",
            Description = "If you can zip IEnumerable with a YieldDeferred"
        )]
        public void Return_Zip_Enumerable() {
            var result = Productor
                .Deferred(() => "hej")
                .GenerateZip(Enumerable.Range(0, 10), (s, i) => s + i)
                .Take(10)
                .ToArray();
            var expected = new[] {"hej0", "hej1", "hej2", "hej3", "hej4", "hej5", "hej6", "hej7", "hej8", "hej9"};
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "If you can zip sequence with a YieldDeferred"
        )]
        public void Return_Zip_Sequence() {
            var result = Productor
                .Deferred(() => "hej")
                .GenerateZip(Productor.Sequence(Enumerable.Range(0, 10)), (s, i) => s + i)
                .Take(10)
                .ToArray();
            var expected = new[] {"hej0", "hej1", "hej2", "hej3", "hej4", "hej5", "hej6", "hej7", "hej8", "hej9"};
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "If you can zip Deferred with a YieldFunction"
        )]
        public void Return_Zip_Defered() {
            var result = Productor
                .Deferred(() => "hej")
                .GenerateZip(Productor.Deferred(() => 10), (s, i) => s + i)
                .GetProvider();
            Assert.AreEqual("hej10", result);
        }
    }
}