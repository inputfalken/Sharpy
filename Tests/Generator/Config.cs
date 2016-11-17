using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests.Generator {
    [TestFixture]
    public class Config {
        [Test]
        public void UniqueMailAddresses_Default_Value() {
            var gen = new Sharpy.Generator();
            Assert.AreEqual(true, gen.UniqueMailAddresses);
        }

        [Test]
        public void MailProviders_Default_Value() {
            var gen = new Sharpy.Generator();
            Assert.AreEqual(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, gen.MailProviders);
        }

        [Test]
        public void UniquePhoneNumber_Default_Value() {
            var gen = new Sharpy.Generator();
            Assert.AreEqual(true, gen.UniquePhoneNumbers);
        }

        [Test]
        public void Countries_Default_Value_Null() {
            var gen = new Sharpy.Generator();
            Assert.IsNull(gen.Countries);
        }

        [Test]
        public void Region_Default_Value_Null() {
            var gen = new Sharpy.Generator();
            Assert.IsNull(gen.Regions);
        }
    }
}