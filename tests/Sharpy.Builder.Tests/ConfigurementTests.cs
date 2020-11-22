using System;
using NUnit.Framework;

namespace Sharpy.Builder.Tests
{
    [TestFixture]
    public class ConfigurementTests
    {
        [Test]
        public void Default_Configurement_Does_Not_Throw() =>
            Assert.DoesNotThrow(() => new Builder(new Configurement()));

        [Test]
        public void Null_GuidProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {GuidProvider = null}));

        [Test]
        public void Null_DecimalProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {DecimalProvider = null}));

        [Test]
        public void Null_TimeSpanProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {TimeSpanProvider = null}));

        [Test]
        public void Null_ArgumentProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {ArgumentProvider = null}));

        [Test]
        public void Null_BoolProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {BoolProvider = null}));

        [Test]
        public void Null_DateTimeOffsetProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {DateTimeOffSetProvider = null}));

        [Test]
        public void Null_FloatProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {FloatProvider = null}));

        [Test]
        public void Null_CharProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {CharProvider = null}));

        [Test]
        public void Null_DateTimeProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {DateTimeProvider = null}));

        [Test]
        public void Null_DoubleProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {DoubleProvider = null}));

        [Test]
        public void Null_IntegerProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {IntegerProvider = null}));

        [Test]
        public void Null_ListElementPicker_Throws() =>
            Assert.Throws<ArgumentNullException>(() =>
                new Builder(new Configurement {ListCollectionElementPicker = null}));

        [Test]
        public void Null_LongProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {LongProvider = null}));

        [Test]
        public void Null_MailProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {MailProvider = null}));

        [Test]
        public void Null_NameProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {NameProvider = null}));

        [Test]
        public void Null_PhoneNumberProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {PhoneNumberProvider = null}));

        [Test]
        public void Null_SecurityNumberProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {SecurityNumberProvider = null}));

        [Test]
        public void Null_UsernameProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {UserNameProvider = null}));
    }
}