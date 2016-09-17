using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Types.Name;

namespace Tests {
    [TestFixture]
    internal class NameFilterTest {
        [Test]
        public void NameFilter_ByCountry_FourArgs() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
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
            var result = new NameFilter(enumerable).ByCountry(Country.Belgium, Country.Sweden, Country.UnitedStates,
                Country.Finland);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_OneArg() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, gustavo, james, rachel2, corigan, johanna
            };
            var result = new NameFilter(enumerable).ByCountry(Country.Belgium);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_ThreeArgs() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
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
            var result = new NameFilter(enumerable).ByCountry(Country.Belgium, Country.Sweden, Country.UnitedStates);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByCountry_TwoArg() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, gustavo, james, lisa, rachel2,
                albin, fring, svensson, rachel, corigan, johanna
            };
            var result = new NameFilter(enumerable).ByCountry(Country.Belgium, Country.Sweden);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_FourArgs() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
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
            var result = new NameFilter(enumerable).ByRegion(Region.Europe, Region.SouthAmerica, Region.NorthAmerica,
                Region.CentralAmerica);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_OneArg() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                gustavo, james, albin, wilma, jens, corigan, johanna
            };
            var result = new NameFilter(enumerable).ByRegion(Region.Europe);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_ThreeArgs() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
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
            var result = new NameFilter(enumerable).ByRegion(Region.Europe, Region.SouthAmerica, Region.NorthAmerica);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByRegion_TwoArg() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
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
            var result = new NameFilter(enumerable).ByRegion(Region.Europe, Region.NorthAmerica);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByType_FemaleFirstNames() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                lisa, rachel2, wilma, rachel, wilma2, johanna
            };
            var result = new NameFilter(enumerable).ByType(NameType.FemaleFirstName);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByType_LastNames() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                fring, svensson, johnson, webb, willson, corigan, jenkins, jenkins2
            };
            var result = new NameFilter(enumerable).ByType(NameType.LastName);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByType_MaleFirstNames() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, jack, gustavo, james, albin, jens, bob, william
            };
            var result = new NameFilter(enumerable).ByType(NameType.MaleFirstName);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [Test]
        public void NameFilter_ByType_MixedFirst() {
            var vladimir = new Name(2, "Belgium", "SouthAmerica", "Vladimir");
            var john = new Name(2, "Sweden", "NorthAmerica", "John");
            var jack = new Name(2, "UnitedStates", "SouthAmerica", "Jack");
            var gustavo = new Name(2, "Belgium", "Europe", "gustavo");
            var james = new Name(2, "Belgium", "Europe", "James");
            var lisa = new Name(1, "Sweden", "NorthAmerica", "Lisa");
            var rachel2 = new Name(1, "Belgium", "SouthAmerica", "Rachel");
            var albin = new Name(2, "Sweden", "Europe", "Albin");
            var fring = new Name(3, "Sweden", "SouthAmerica", "Fring");
            var svensson = new Name(3, "Sweden", "NorthAmerica", "Svensson");
            var wilma = new Name(1, "UnitedStates", "Europe", "Wilma");
            var jens = new Name(2, "UnitedStates", "Europe", "Jens");
            var bob = new Name(2, "UnitedStates", "SouthAmerica", "Bob");
            var rachel = new Name(1, "Sweden", "SouthAmerica", "Rachel");
            var william = new Name(2, "Finland", "NorthAmerica", "William");
            var johnson = new Name(3, "UnitedStates", "SouthAmerica", "Johnsson");
            var webb = new Name(3, "UnitedStates", "SouthAmerica", "Webb");
            var wilma2 = new Name(1, "UnitedStates", "SouthAmerica", "Wilma");
            var willson = new Name(3, "Finland", "NorthAmerica", "Willson");
            var corigan = new Name(3, "Belgium", "Europe", "Corigan");
            var johanna = new Name(1, "Belgium", "Europe", "johanna");
            var jenkins = new Name(3, "UnitedStates", "NorthAmerica", "Jenkins");
            var jenkins2 = new Name(3, "UnitedStates", "CentralAmerica", "Jenkins");
            var enumerable = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, fring, svensson, wilma, jens, bob, rachel,
                william, johnson, webb, wilma2, willson, corigan, johanna, jenkins, jenkins2
            };
            var expected = new[] {
                vladimir, john, jack, gustavo, james, lisa, rachel2,
                albin, wilma, jens, bob, rachel,
                william, wilma2, johanna
            };
            var result = new NameFilter(enumerable).ByType(NameType.MixedFirstName);
            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}