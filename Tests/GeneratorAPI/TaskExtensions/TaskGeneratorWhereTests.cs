using System;
using System.Threading.Tasks;
using GeneratorAPI;
using GeneratorAPI.Linq;
using GeneratorAPI.Linq.TaskExtensions;
using NUnit.Framework;

namespace Tests.GeneratorAPI.TaskExtensions {
    [TestFixture]
    internal class TaskGeneratorWhereTests {
        [SetUp]
        public void Initiate() {
            _generator = Generator.Factory.Incrementer(0)
                .Select(async i => {
                    await Task.Delay(100);
                    return i;
                });
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<Task<int>> _generator;

        [Test]
        public void Bad_Predicate_Throws() {
            _generator = Generator.Factory.Incrementer(0)
                .Select(i => Task.Run(() => i));
            Func<int, bool> predicate = i => false;
            Assert.ThrowsAsync<ArgumentException>(() => _generator.Where(predicate).Generate());
        }

        [Test(
            Description = "Verify to see that where only returns data fiting the predicate"
        )]
        public async Task Int_Dividable_By_Two() {
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = _generator.Where(predicate);

            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
            Assert.AreEqual(true, predicate(await result.Generate()));
        }

        [Test(
            Description = "Verify to see that where only returns data fiting the predicate"
        )]
        public async Task Int_Dividable_By_Two_Without_Filter() {
            Func<int, bool> predicate = i => i % 2 == 0;

            Assert.AreEqual(true, predicate(await _generator.Generate()));
            Assert.AreEqual(false, predicate(await _generator.Generate()));
            Assert.AreEqual(true, predicate(await _generator.Generate()));
            Assert.AreEqual(false, predicate(await _generator.Generate()));
            Assert.AreEqual(true, predicate(await _generator.Generate()));
            Assert.AreEqual(false, predicate(await _generator.Generate()));
            Assert.AreEqual(true, predicate(await _generator.Generate()));
            Assert.AreEqual(false, predicate(await _generator.Generate()));
            Assert.AreEqual(true, predicate(await _generator.Generate()));
            Assert.AreEqual(false, predicate(await _generator.Generate()));
            Assert.AreEqual(true, predicate(await _generator.Generate()));
        }

        [Test]
        public async Task Is_Evaluated_When_Awaited() {
            var invoked = false;
            var generator = _generator.Where((int i) => invoked = true);
            Assert.IsFalse(invoked);
            var generate = generator.Generate();
            Assert.IsFalse(invoked);
            await generate;
            Assert.IsTrue(invoked);
        }

        [Test]
        public void Null_Generator() {
            _generator = null;
            Func<int, bool> predicate = i => i * 2 == 0;
            Assert.Throws<ArgumentNullException>(() => _generator.Where(predicate));
        }

        [Test]
        public void Null_Predicate() {
            Func<int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => _generator.Where(predicate));
        }
    }
}