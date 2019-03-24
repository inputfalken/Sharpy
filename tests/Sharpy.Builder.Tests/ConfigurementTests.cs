using System;
using NUnit.Framework;

namespace Sharpy.Builder.Tests {
    [TestFixture]
    public class ConfigurementTests {
        [Test]
        public void Default_Configurement_Does_Not_Throw() =>
            Assert.DoesNotThrow(() => new Builder(new Configuration()));

        [Test]
        public void Null_ArgumentProvider_Throws() {
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {ArgumentProvider = null}));
        }

        [Test]
        public void Null_BoolProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {BoolProvider = null}));

        [Test]
        public void Null_DateProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {DateProvider = null}));

        [Test]
        public void Null_DoubleProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {DoubleProvider = null}));

        [Test]
        public void Null_IntegerProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {IntegerProvider = null}));

        [Test]
        public void Null_ListElementPicker_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {ListElementPicker = null}));

        [Test]
        public void Null_LongProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {LongProvider = null}));

        [Test]
        public void Null_MailProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {MailProvider = null}));

        [Test]
        public void Null_NameProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {NameProvider = null}));

        [Test]
        public void Null_PhoneNumberProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {PhoneNumberProvider = null}));

        [Test]
        public void Null_PostalCodeProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {PostalCodeProvider = null}));

        [Test]
        public void Null_SecurityNumberProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {SecurityNumberProvider = null}));

        [Test]
        public void Null_UsernameProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {UserNameProvider = null}));
        
        [Test]
        public void Null_MovieDb_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configuration {MovieDbProvider = null}));
        
    }
}