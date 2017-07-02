using System;
using GeneratorAPI;
using GeneratorAPI.Extensions;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Sharpy.Integration {
    [TestFixture]
    public class Config {
        [Test]
        public void DoubleProvider_Set_To_Null() {
            var configurement = new Configurement {
                DoubleProvider = null
            };

            var generator = Generator.Factory.Provider(new Provider(configurement));
            //Long
            Assert.DoesNotThrow(() => generator.Select(x => x.Long()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Long(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Long(1, 10)).Generate());

            //Double
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Double()).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Double(10)).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Double(1, 10)).Generate());

            //Integer
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer(1, 10)).Generate());


            //Name
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName(Gender.Female)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName(Gender.Male)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.LastName()).Generate());
        }

        [Test]
        public void IntegerProvider_Set_To_Null() {
            var configurement = new Configurement {
                IntegerProvider = null
            };
            var generator = Generator.Factory.Provider(new Provider(configurement));
            //Integer
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Integer()).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Integer(10)).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Integer(1, 10)).Generate());

            //Long
            Assert.DoesNotThrow(() => generator.Select(x => x.Long()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Long(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Long(1, 10)).Generate());

            //Double
            Assert.DoesNotThrow(() => generator.Select(x => x.Double()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Double(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Double(1, 10)).Generate());


            //Name
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName(Gender.Female)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName(Gender.Male)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.LastName()).Generate());
        }

        [Test]
        public void LongProvider_Set_To_Null() {
            var configurement = new Configurement {
                LongProvider = null
            };
            var generator = Generator.Factory.Provider(new Provider(configurement));
            //Long
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Long()).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Long(10)).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.Long(1, 10)).Generate());

            //Double
            Assert.DoesNotThrow(() => generator.Select(x => x.Double()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Double(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Double(1, 10)).Generate());

            //Integer
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer(1, 10)).Generate());


            //Name
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName(Gender.Female)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.FirstName(Gender.Male)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.LastName()).Generate());
        }

        [Test]
        public void NameProvider_Set_To_Null() {
            var configurement = new Configurement {
                NameProvider = null
            };
            var generator = Generator.Factory.Provider(new Provider(configurement));
            //Name
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.FirstName()).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.FirstName(Gender.Female)).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.FirstName(Gender.Male)).Generate());
            Assert.Throws<NullReferenceException>(() => generator.Select(x => x.LastName()).Generate());

            //Integer
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Integer(1, 10)).Generate());

            //Long
            Assert.DoesNotThrow(() => generator.Select(x => x.Long()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Long(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Long(1, 10)).Generate());

            //Double
            Assert.DoesNotThrow(() => generator.Select(x => x.Double()).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Double(10)).Generate());
            Assert.DoesNotThrow(() => generator.Select(x => x.Double(1, 10)).Generate());
        }
    }
}