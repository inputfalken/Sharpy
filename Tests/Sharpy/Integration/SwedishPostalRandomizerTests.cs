using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Implementation;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class SwedishPostalRandomizerTests {
        [Test]
        public void No_Argument_Does_Not_Throw() {
            var swedishPostal = new SwePostalCodeRandomizer(new Random());
            Assert.DoesNotThrow(() => swedishPostal.PostalCode());
        }

        [Test]
        public void Single_Valid_Argument_Does_Not_Throw() {
            var swedishPostal = new SwePostalCodeRandomizer(new Random());
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Stockholm"));
        }

        [Test]
        public void Ignore_Case() {
            var swedishPostal = new SwePostalCodeRandomizer(new Random());
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("stoCkHolm"));
        }

        [Test]
        public void Invalid_Arg_Throws() {
            var swedishPostal = new SwePostalCodeRandomizer(new Random());
            Assert.Throws<ArgumentException>(() => swedishPostal.PostalCode("Foo"));
        }

        [Test]
        public void No_Valid_Argument_Throws() {
            var swedishPostal = new SwePostalCodeRandomizer(new Random());
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Stockholm"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Västerbotten"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Norrbotten"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Uppsala"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Södermanland"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Östergötland"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Jönköping"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Kronoberg"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Kalmar"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Gotland"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Blekinge"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Skåne"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Halland"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Västra Götaland"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Värmland"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Örebro"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Västmanland"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Dalarna"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Gävleborg"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Västernorrland"));
            Assert.DoesNotThrow(() => swedishPostal.PostalCode("Jämtland"));
        }
    }
}
