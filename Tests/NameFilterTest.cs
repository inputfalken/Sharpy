using System.Linq;
using DataGen.Types.Name;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    internal class NameFilterTest {
        #region Types

        [Test]
        public void NameFilter_ByType_FemaleFirstNames() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                lisa, rachel2, wilma, rachel, wilma2, johanna
            };
            var result = new NameFilter(enumerable).ByType(NameTypes.FemaleFirst);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByType_MaleFirstNames() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, jack, gustavo, james, albin, jens, bob, william
            };
            var result = new NameFilter(enumerable).ByType(NameTypes.MaleFirst);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByType_LastNames() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                fring, svensson, johnson, webb, willson, corigan, jenkins, jenkins2
            };
            var result = new NameFilter(enumerable).ByType(NameTypes.LastNames);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByType_MixedFirst() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, wilma, jens, bob, rachel,
                william, wilma2, johanna,
            };
            var result = new NameFilter(enumerable).ByType(NameTypes.MixedNames);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        #endregion

        #region ByRegion

        [Test]
        public void NameFilter_ByRegion_OneArg() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                gustavo, james, albin, wilma, jens, corigan, johanna
            };
            var result = new NameFilter(enumerable).ByRegion("region1");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_TwoArg() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                john, gustavo, james, lisa,
                albin, svensson,
                wilma, jens, william,
                willson, corigan,
                johanna, jenkins
            };
            var result = new NameFilter(enumerable).ByRegion("region1", "region3");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_ThreeArgs() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins
            };
            var result = new NameFilter(enumerable).ByRegion("region1", "region2", "region3");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_FourArgs() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var result = new NameFilter(enumerable).ByRegion("region1", "region2", "region3", "region4");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        #endregion

        #region  ByCountry

        [Test]
        public void NameFilter_ByCountry_OneArg() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, gustavo, james, rachel2, corigan, johanna
            };
            var result = new NameFilter(enumerable).ByCountry("country1");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_TwoArg() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, gustavo, james, lisa, rachel2,
                albin, fring, svensson, rachel, corigan, johanna
            };
            var result = new NameFilter(enumerable).ByCountry("country1", "country2");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_ThreeArgs() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                johnson, webb, wilma2, corigan, johanna, jenkins, jenkins2
            };
            var result = new NameFilter(enumerable).ByCountry("country1", "country2", "country3");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_FourArgs() {
            var vladimir = new Name(2, "country1", "region2", "Vladimir");
            var john = new Name(2, "country2", "region3", "John");
            var jack = new Name(2, "country3", "region2", "Jack");
            var gustavo = new Name(2, "country1", "region1", "gustavo");
            var james = new Name(2, "country1", "region1", "James");
            var lisa = new Name(1, "country2", "region3", "Lisa");
            var rachel2 = new Name(1, "country1", "region2", "Rachel");
            var albin = new Name(2, "country2", "region1", "Albin");
            var fring = new Name(3, "country2", "region2", "Fring");
            var svensson = new Name(3, "country2", "region3", "Svensson");
            var wilma = new Name(1, "country3", "region1", "Wilma");
            var jens = new Name(2, "country3", "region1", "Jens");
            var bob = new Name(2, "country3", "region2", "Bob");
            var rachel = new Name(1, "country2", "region2", "Rachel");
            var william = new Name(2, "country4", "region3", "William");
            var johnson = new Name(3, "country3", "region2", "Johnsson");
            var webb = new Name(3, "country3", "region2", "Webb");
            var wilma2 = new Name(1, "country3", "region2", "Wilma");
            var willson = new Name(3, "country4", "region3", "Willson");
            var corigan = new Name(3, "country1", "region1", "Corigan");
            var johanna = new Name(1, "country1", "region1", "johanna");
            var jenkins = new Name(3, "country3", "region3", "Jenkins");
            var jenkins2 = new Name(3, "country3", "region4", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var result = new NameFilter(enumerable).ByCountry("country1", "country2", "country3", "country4");
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        #endregion
    }
}