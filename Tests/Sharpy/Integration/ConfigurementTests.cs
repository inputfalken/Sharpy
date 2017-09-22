using System;
using NUnit.Framework;
using Sharpy;
using static NUnit.Framework.Assert;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class ConfigurementTests {
        [Test]
        public void Default_Configurement_Does_Not_Throw() => DoesNotThrow(() => new Builder(new Configurement()));

        [Test]
        public void Null_LongProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {LongProvider = null}));

        [Test]
        public void Null_IntegerProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {IntegerProvider = null}));

        [Test]
        public void Null_ListElementPicker_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {ListElementPicker = null}));

        [Test]
        public void Null_NameProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {NameProvider = null}));

        [Test]
        public void Null_PostalCodeProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {PostalCodeProvider = null}));

        [Test]
        public void Null_BoolProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {BoolProvider = null}));

        [Test]
        public void Null_DoubleProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {DoubleProvider = null}));

        [Test]
        public void Null_SecurityNumberProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {SecurityNumberProvider = null}));

        [Test]
        public void Null_DateProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {DateProvider = null}));

        [Test]
        public void Null_MailProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {MailProvider = null}));

        [Test]
        public void Null_UsernameProvider_Throws() =>
            Throws<ArgumentNullException>(() => new Builder(new Configurement {UserNameProvider = null}));
    }
}