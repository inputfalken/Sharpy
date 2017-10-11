using System;
using NUnit.Framework;
using Sharpy.Core;
using Sharpy.Core.Linq;

namespace Tests.Sharpy.CoreTests.ExtensionTests {
    [TestFixture]
    public class WhereTests {
        [SetUp]
        public void Initiate() {
            _generator = Generator.Incrementer(0);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<int> _generator;

        [Test(
            Description = "Verify that bad predicates do not run forever."
        )]
        public void Bad_Predicate_Throws() {
            var result = _generator
                .Where(s => false);

            Assert.Throws<ArgumentException>(() => result.Generate());
        }

        [Test(
            Description = "Verify that where does not return null"
        )]
        public void Does_Not_Return_Null() {
            var result = _generator.Where(s => true);
            Assert.IsNotNull(result);
        }

        [Test(
            Description = "Verify that Where with null Generator and Argument throws exception"
        )]
        public void Filter_Null_Generator_And_Arg_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Where(null));
        }

        [Test(
            Description = "Verify that Where with null Generator throws exception"
        )]
        public void Filter_Null_Generator_Throws() {
            IGenerator<string> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Where(s => s.Length == 0));
        }

        [Test(
            Description = "Verify that passing null does not work and throws exception"
        )]
        public void Filter_Null_Param_Throws() {
            Assert.Throws<ArgumentNullException>(() => _generator.Where(null));
        }

        [Test(
            Description = "Verify to see that where only returns data fiting the predicate"
        )]
        public void Int_Dividable_By_Two() {
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = _generator.Where(predicate);

            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
            Assert.AreEqual(true, predicate(result.Generate()));
        }

        [Test(
            Description = "Verify to see that where only returns data fiting the predicate"
        )]
        public void Int_Dividable_By_Two_Without_Filter() {
            Func<int, bool> predicate = i => i % 2 == 0;

            Assert.AreEqual(true, predicate(_generator.Generate()));
            Assert.AreEqual(false, predicate(_generator.Generate()));
            Assert.AreEqual(true, predicate(_generator.Generate()));
            Assert.AreEqual(false, predicate(_generator.Generate()));
            Assert.AreEqual(true, predicate(_generator.Generate()));
            Assert.AreEqual(false, predicate(_generator.Generate()));
            Assert.AreEqual(true, predicate(_generator.Generate()));
            Assert.AreEqual(false, predicate(_generator.Generate()));
            Assert.AreEqual(true, predicate(_generator.Generate()));
            Assert.AreEqual(false, predicate(_generator.Generate()));
            Assert.AreEqual(true, predicate(_generator.Generate()));
        }

        [Test(
            Description = "Verifys that the Func is only invoked if Generate is invoked"
        )]
        public void Is_Evaluated_After_Take_Is_Invoked() {
            var invoked = false;
            var generator = _generator.Where(s => {
                invoked = true;
                return true;
            });
            // Not evaluated
            Assert.IsFalse(invoked);
            // Evaluated
            generator.Generate();
            Assert.IsTrue(invoked);
        }
    }
}