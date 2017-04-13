using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Integration {
    [TestFixture]
    public class Config {
        [Test]
        public void DoubleProvider_Set_To_Null() {
            var configurement = new Configurement {
                DoubleProvider = null
            };
            var generator = Productor.Yield(new Provider(configurement));
            //Long
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)).Take());

            //Double
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double()).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double(10)).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double(1, 10)).Take());

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)).Take());


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()).Take());
        }

        [Test]
        public void IntegerProvider_Set_To_Null() {
            var configurement = new Configurement {
                IntegerProvider = null
            };
            var generator = Productor.Yield(new Provider(configurement));
            //Integer
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer()).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer(10)).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer(1, 10)).Take());

            //Long
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)).Take());

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)).Take());


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()).Take());
        }

        [Test]
        public void LongProvider_Set_To_Null() {
            var configurement = new Configurement {
                LongProvider = null
            };
            var generator = Productor.Yield(new Provider(configurement));
            //Long
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long()).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long(10)).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long(1, 10)).Take());

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)).Take());

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)).Take());


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()).Take());
        }

        [Test]
        public void NameProvider_Set_To_Null() {
            var configurement = new Configurement {
                NameProvider = null
            };
            var generator = Productor.Yield(new Provider(configurement));
            //Name
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName()).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName(Gender.Female)).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName(Gender.Male)).Take());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.LastName()).Take());

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)).Take());

            //Long
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)).Take());

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)).Take());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)).Take());
        }
    }
}
