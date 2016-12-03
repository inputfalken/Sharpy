using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Generator {
    [TestFixture]
    public class Config {
        [Test]
        public void NameProvider_Set_To_Null() {
            Configurement configurement = new Configurement {
                NameProvider = null
            };
            var generator = new Sharpy.Generator(configurement);
            //Name
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName()));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName(Gender.Female)));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.FirstName(Gender.Male)));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.LastName()));

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)));

            //Long
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)));

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)));
        }

        [Test]
        public void IntegerProvider_Set_To_Null() {
            Configurement configurement = new Configurement {
                IntegerProvider = null
            };
            var generator = new Sharpy.Generator(configurement);
            //Integer
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer()));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer(10)));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Integer(1, 10)));

            //Long
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)));

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)));


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()));
        }

        [Test]
        public void DoubleProvider_Set_To_Null() {
            Configurement configurement = new Configurement {
                DoubleProvider = null
            };
            var generator = new Sharpy.Generator(configurement);
            //Long
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Long(1, 10)));

            //Double
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double()));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double(10)));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Double(1, 10)));

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)));


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()));
        }

        [Test]
        public void LongProvider_Set_To_Null() {
            Configurement configurement = new Configurement {
                LongProvider = null
            };
            var generator = new Sharpy.Generator(configurement);
            //Long
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long()));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long(10)));
            Assert.Throws<NullReferenceException>(() => generator.Generate(x => x.Long(1, 10)));

            //Double
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Double(1, 10)));

            //Integer
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(10)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.Integer(1, 10)));


            //Name
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName()));
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Female)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.FirstName(Gender.Male)));
            Assert.DoesNotThrow(() => generator.Generate(x => x.LastName()));
        }
    }
}