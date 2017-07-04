using System;
using System.Collections.Generic;
using GeneratorAPI;
using GeneratorAPI.Extensions;
using NUnit.Framework;

namespace Tests.GeneratorAPI.Extensions {
    [TestFixture]
    public class CastTests {
        [Test(
            Description = "Verify that invalid cast throws exception"
        )]
        public void Invalid_Cast_Throws() {
            var generator = Generator
                .CircularSequence(new List<int> {1, 2})
                .Cast<string>();
            Assert.Throws<InvalidCastException>(() => generator.Generate());
        }

        [Test(
            Description = "Verify that if the type is not the same, the cast is needed."
        )]
        public void Not_Same_Type_Casts() {
            IGenerator generator = Generator.CircularSequence(new List<object> {"foo", "bar"});
            Assert.AreNotSame(generator, generator.Cast<string>());
        }

        [Test(
            Description = ""
        )]
        public void Null_Generator() {
            IGenerator<int> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Cast<long>());
        }

        [Test(
            Description = "Verify that char casting works"
        )]
        public void Object_To_Char() {
            var generator = Generator
                .CircularSequence(new List<object> {'1', '2'})
                .Cast<char>();
            Assert.IsInstanceOf<char>(generator.Generate());
        }

        [Test(
            Description = "Verify that int casting works"
        )]
        public void Object_To_Int() {
            var generator = Generator
                .CircularSequence(new List<object> {1, 2, 3, 4, 5})
                .Cast<int>();
            Assert.IsInstanceOf<int>(generator.Generate());
        }

        [Test(
            Description = "Verify that string casting works"
        )]
        public void Object_To_String() {
            var generator = Generator
                .CircularSequence(new List<object> {"hello", "World"})
                .Cast<string>();
            Assert.IsInstanceOf<string>(generator.Generate());
        }

        [Test(
            Description = "Verify that if the type is not the same, the cast can be skipped."
        )]
        public void Same_Type_Does_Nothing() {
            IGenerator generator = Generator.CircularSequence(new List<string> {"foo", "bar"});
            Assert.AreSame(generator, generator.Cast<string>());
        }
    }
}