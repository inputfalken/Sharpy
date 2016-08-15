using System.Linq;
using DataGen.Types.Name;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    internal class NameFilterTest {
        #region ByRegion

        [Test]
        public void NameFilter_ByRegion_OneArg() {
            var vladimir = new Name(1, "", "region1", "Vladimir");
            var john = new Name(1, "", "region2", "John");
            var jack = new Name(1, "", "region3", "Jack");
            var gustavo = new Name(1, "", "region1", "gustavo");
            var james = new Name(1, "", "region1", "James");
            var lisa = new Name(1, "", "region2", "Lisa");
            var albin = new Name(1, "", "region2", "Albin");
            var wilma = new Name(1, "", "region3", "wilma");
            var jens = new Name(1, "", "region3", "jens");
            var bob = new Name(1, "", "region4", "bob");
            var william = new Name(1, "", "region4", "william");
            var jenkins = new Name(1, "", "region4", "jenkins");
            var enumerable = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var expected = new[] {
                vladimir, gustavo, james
            };
            var result = new NameFilter(enumerable).ByRegion("region1");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_TwoArg() {
            var vladimir = new Name(1, "", "region1", "Vladimir");
            var john = new Name(1, "", "region2", "John");
            var jack = new Name(1, "", "region3", "Jack");
            var gustavo = new Name(1, "", "region1", "gustavo");
            var james = new Name(1, "", "region1", "James");
            var lisa = new Name(1, "", "region2", "Lisa");
            var albin = new Name(1, "", "region2", "Albin");
            var wilma = new Name(1, "", "region3", "wilma");
            var jens = new Name(1, "", "region3", "jens");
            var bob = new Name(1, "", "region4", "bob");
            var william = new Name(1, "", "region4", "william");
            var jenkins = new Name(1, "", "region4", "jenkins");
            var enumerable = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var expected = new[] {
                vladimir, jack, gustavo,
                james, wilma, jens
            };
            var result = new NameFilter(enumerable).ByRegion("region1", "region3");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_ThreeArgs() {
            var vladimir = new Name(1, "", "region1", "Vladimir");
            var john = new Name(1, "", "region2", "John");
            var jack = new Name(1, "", "region3", "Jack");
            var gustavo = new Name(1, "", "region1", "gustavo");
            var james = new Name(1, "", "region1", "James");
            var lisa = new Name(1, "", "region2", "Lisa");
            var albin = new Name(1, "", "region2", "Albin");
            var wilma = new Name(1, "", "region3", "wilma");
            var jens = new Name(1, "", "region3", "jens");
            var bob = new Name(1, "", "region4", "bob");
            var william = new Name(1, "", "region4", "william");
            var jenkins = new Name(1, "", "region4", "jenkins");
            var enumerable = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var expected = new[] {
                vladimir, john, jack,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var result = new NameFilter(enumerable).ByRegion("region1", "region2", "region3");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_FourArgs() {
            var vladimir = new Name(1, "", "region1", "Vladimir");
            var john = new Name(1, "", "region2", "John");
            var jack = new Name(1, "", "region3", "Jack");
            var gustavo = new Name(1, "", "region1", "gustavo");
            var james = new Name(1, "", "region1", "James");
            var lisa = new Name(1, "", "region2", "Lisa");
            var albin = new Name(1, "", "region2", "Albin");
            var wilma = new Name(1, "", "region3", "wilma");
            var jens = new Name(1, "", "region3", "jens");
            var bob = new Name(1, "", "region4", "bob");
            var william = new Name(1, "", "region4", "william");
            var jenkins = new Name(1, "", "region4", "jenkins");
            var enumerable = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var expected = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var result = new NameFilter(enumerable).ByRegion("region1", "region2", "region3", "region4");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        #endregion

        #region  ByCountry

        [Test]
        public void NameFilter_ByCountry_OneArg() {
            var vladimir = new Name(1, "country1", "", "Vladimir");
            var john = new Name(1, "country2", "", "John");
            var jack = new Name(1, "country3", "", "Jack");
            var gustavo = new Name(1, "country1", "", "gustavo");
            var james = new Name(1, "country1", "", "James");
            var lisa = new Name(1, "country2", "", "Lisa");
            var albin = new Name(1, "country2", "", "Albin");
            var wilma = new Name(1, "country3", "", "wilma");
            var jens = new Name(1, "country3", "", "jens");
            var bob = new Name(1, "country4", "", "bob");
            var william = new Name(1, "country4", "", "william");
            var jenkins = new Name(1, "country4", "", "jenkins");
            var enumerable = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var expected = new[] {
                vladimir, gustavo, james
            };
            var result = new NameFilter(enumerable).ByCountry("country1");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_TwoArg() {
            var vladimir = new Name(1, "country1", "", "Vladimir");
            var john = new Name(1, "country2", "", "John");
            var jack = new Name(1, "country3", "", "Jack");
            var gustavo = new Name(1, "country1", "", "gustavo");
            var james = new Name(1, "country1", "", "James");
            var lisa = new Name(1, "country2", "", "Lisa");
            var albin = new Name(1, "country2", "", "Albin");
            var wilma = new Name(1, "country3", "", "wilma");
            var jens = new Name(1, "country3", "", "jens");
            var bob = new Name(1, "country4", "", "bob");
            var william = new Name(1, "country4", "", "william");
            var jenkins = new Name(1, "country4", "", "jenkins");
            var enumerable = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var expected = new[] {
                vladimir, john, gustavo,
                james, lisa, albin
            };
            var result = new NameFilter(enumerable).ByCountry("country1", "country2");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_ThreeArgs() {
            var vladimir = new Name(1, "country1", "", "Vladimir");
            var john = new Name(1, "country2", "", "John");
            var jack = new Name(1, "country3", "", "Jack");
            var gustavo = new Name(1, "country1", "", "gustavo");
            var james = new Name(1, "country1", "", "James");
            var lisa = new Name(1, "country2", "", "Lisa");
            var albin = new Name(1, "country2", "", "Albin");
            var wilma = new Name(1, "country3", "", "wilma");
            var jens = new Name(1, "country3", "", "jens");
            var bob = new Name(1, "country4", "", "bob");
            var william = new Name(1, "country4", "", "william");
            var jenkins = new Name(1, "country4", "", "jenkins");
            var enumerable = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var expected = new[] {
                vladimir, john, jack,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var result = new NameFilter(enumerable).ByCountry("country1", "country2", "country3");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_FourArgs() {
            var vladimir = new Name(1, "country1", "", "Vladimir");
            var john = new Name(1, "country2", "", "John");
            var jack = new Name(1, "country3", "", "Jack");
            var gustavo = new Name(1, "country1", "", "gustavo");
            var james = new Name(1, "country1", "", "James");
            var lisa = new Name(1, "country2", "", "Lisa");
            var albin = new Name(1, "country2", "", "Albin");
            var wilma = new Name(1, "country3", "", "wilma");
            var jens = new Name(1, "country3", "", "jens");
            var bob = new Name(1, "country4", "", "bob");
            var william = new Name(1, "country4", "", "william");
            var jenkins = new Name(1, "country4", "", "jenkins");
            var enumerable = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var expected = new[] {
                vladimir, john, jack,
                bob, william, jenkins,
                gustavo, james, lisa,
                albin, wilma, jens
            };
            var result = new NameFilter(enumerable).ByCountry("country1", "country2", "country3", "country4");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        #endregion
    }
}