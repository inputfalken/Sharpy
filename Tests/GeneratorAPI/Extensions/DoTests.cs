using System;
using System.Collections.Generic;
using GeneratorAPI;
using GeneratorAPI.Linq;
using NUnit.Framework;

namespace Tests.GeneratorAPI.Extensions {
    [TestFixture]
    public class DoTests {
        [SetUp]
        public void Initiate() {
            _generator = Generator.Factory.Incrementer(0);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<int> _generator;


        [Test(
            Description = "Verify that Do is invoked if Generate is invoked"
        )]
        public void Is_Evaluated_After_Generate_Is_invoked() {
            var invoked = false;
            _generator
                .Do(s => invoked = true)
                .Generate();

            Assert.IsTrue(invoked);
        }

        [Test(
            Description = "Verify that Do is only invoked if Generate is Invoked"
        )]
        public void Is_Not_Evaluated_Before_Take_Is_Invoked() {
            var invoked = false;
            _generator.Do(s => invoked = true);
            Assert.IsFalse(invoked);
        }

        [Test(
            Description = "Verify that Do throws exception if the Action<T> is null"
        )]
        public void Null_Argument_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Do(null));
        }

        [Test(
            Description = "Verify that Do with null Generator and argument throws exception"
        )]
        public void Null_Generator_And_Arg_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Do(null));
        }

        [Test(
            Description = "Verify that Do with null Generator throws exception"
        )]
        public void Null_Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Do(s => { }));
        }

        [Test(
            Description = "Verify that Do gets various elements and not the same element"
        )]
        public void Various_Elements_Gets_Sent_Through() {
            var container = new List<int>();
            var result = _generator
                .Do(container.Add);
            var expected = Generator
                .Factory.Incrementer(0);

            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
            Assert.AreEqual(expected.Generate(), result.Generate());
        }
    }
}