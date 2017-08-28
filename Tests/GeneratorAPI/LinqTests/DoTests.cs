using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sharpy;
using Sharpy.Generator;
using Sharpy.Generator.Linq;

namespace Tests.GeneratorAPI.LinqTests {
    [TestFixture]
    public class DoTests {
        [SetUp]
        public void Initiate() {
            _generator = Factory.Incrementer(0);
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
            var generator = _generator
                .Do(s => invoked = true);
            // Not evaluated
            Assert.IsFalse(invoked);
            // Evaluated
            generator.Generate();
            Assert.IsTrue(invoked);
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
            var expected = Factory.Incrementer(0);

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