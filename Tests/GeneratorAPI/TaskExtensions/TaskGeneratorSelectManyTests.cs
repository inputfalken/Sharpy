using System.Threading.Tasks;
using GeneratorAPI;
using GeneratorAPI.Linq;
using GeneratorAPI.Linq.TaskExtensions;
using NUnit.Framework;

namespace Tests.GeneratorAPI.TaskExtensions {
    [TestFixture]
    internal class TaskGeneratorSelectManyTests {
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
        public async Task Flat_Maps_Tasks() {
            var selectMany = _generator
                .SelectMany(i => Task.Run(() => i + 1))
                .SelectMany(i => Task.Run(() => i + 1));

            Assert.AreEqual(2, await selectMany.Generate());
            Assert.AreEqual(3, await selectMany.Generate());
            Assert.AreEqual(4, await selectMany.Generate());
            Assert.AreEqual(5, await selectMany.Generate());
        }

        [Test]
        public async Task Flat_Maps_Tasks_With_ResultSelector() {
            var selectMany = _generator
                .SelectMany(i => Task.Run(() => i + 1), (i, i1) => i + i1)
                .SelectMany(i => Task.Run(() => i + 1), (i, i1) => i + i1);

            Assert.AreEqual(3, await selectMany.Generate());
            Assert.AreEqual(7, await selectMany.Generate());
            Assert.AreEqual(11, await selectMany.Generate());
            Assert.AreEqual(15, await selectMany.Generate());
        }

        [Test]
        public async Task Is_Evaluated_When_Awaited() {
            var invoked = false;
            var generator = _generator.SelectMany(i => {
                invoked = true;
                return Task.Run(() => i);
            });
            Assert.IsFalse(invoked);
            var generate = generator.Generate();
            Assert.IsFalse(invoked);
            await generate;
            Assert.IsTrue(invoked);
        }
    }
}