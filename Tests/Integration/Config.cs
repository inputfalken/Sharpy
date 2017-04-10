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
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)).Produce());

            //Double
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double()).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double(10)).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double(1, 10)).Produce());

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)).Produce());


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()).Produce());
        }

        [Test]
        public void IntegerProvider_Set_To_Null() {
            var configurement = new Configurement {
                IntegerProvider = null
            };
            var generator = Productor.Yield(new Provider(configurement));
            //Integer
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer()).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer(10)).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer(1, 10)).Produce());

            //Long
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)).Produce());

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)).Produce());


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()).Produce());
        }

        [Test]
        public void LongProvider_Set_To_Null() {
            var configurement = new Configurement {
                LongProvider = null
            };
            var generator = Productor.Yield(new Provider(configurement));
            //Long
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long()).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long(10)).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long(1, 10)).Produce());

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)).Produce());

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)).Produce());


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()).Produce());
        }

        [Test]
        public void NameProvider_Set_To_Null() {
            var configurement = new Configurement {
                NameProvider = null
            };
            var generator = Productor.Yield(new Provider(configurement));
            //Name
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName()).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName(Gender.Female)).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName(Gender.Male)).Produce());
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.LastName()).Produce());

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)).Produce());

            //Long
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)).Produce());

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)).Produce());
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)).Produce());
        }
    }
}
