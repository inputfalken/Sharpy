﻿using System;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;

namespace Tests.Generator {
    [TestFixture]
    public class Config {
        [Test]
        public void NameProvider_Set_To_Null() {
            var generator = Sharpy.Generator.Create(new Configurement {
                NameProvider = null
            });
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
            var generator = Sharpy.Generator.Create(new Configurement {
                IntegerProvider = null
            });
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
            var generator = Sharpy.Generator.Create(new Configurement {
                DoubleProvider = null
            });
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
            var generator = Sharpy.Generator.Create(new Configurement {
                LongProvider = null
            });
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