﻿using System;
using NUnit.Framework;
using Sharpy.Builder;

namespace Tests.Sharpy.BuilderTests {
    [TestFixture]
    public class ConfigurementTests {
        [Test]
        public void Default_Configurement_Does_Not_Throw() =>
            Assert.DoesNotThrow(() => new Builder(new Configurement()));

        [Test]
        public void Null_ArgumentProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {ArgumentProvider = null}));

        [Test]
        public void Null_BoolProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {BoolProvider = null}));

        [Test]
        public void Null_DateProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {DateProvider = null}));

        [Test]
        public void Null_DoubleProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {DoubleProvider = null}));

        [Test]
        public void Null_IntegerProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {IntegerProvider = null}));

        [Test]
        public void Null_ListElementPicker_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {ListElementPicker = null}));

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
        public void Null_PostalCodeProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {PostalCodeProvider = null}));

        [Test]
        public void Null_SecurityNumberProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {SecurityNumberProvider = null}));

        [Test]
        public void Null_UsernameProvider_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Builder(new Configurement {UserNameProvider = null}));
    }
}