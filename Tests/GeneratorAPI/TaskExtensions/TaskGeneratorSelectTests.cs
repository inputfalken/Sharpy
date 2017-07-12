using System.Threading.Tasks;
using GeneratorAPI;
using GeneratorAPI.Linq;
using GeneratorAPI.Linq.TaskExtensions;
using NUnit.Framework;
using Sharpy;

namespace Tests.GeneratorAPI.TaskExtensions {
    [TestFixture]
    internal class TaskGeneratorSelectTests {
        [SetUp]
        public void Initiate() {
            _generator = Factory.Incrementer(0)
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
        public async Task Convert_To_String() {
            var taskGenerator = _generator.Select((int i) => i.ToString());

            Assert.AreEqual("0", await taskGenerator.Generate());
            Assert.AreEqual("1", await taskGenerator.Generate());
            Assert.AreEqual("2", await taskGenerator.Generate());
            Assert.AreEqual("3", await taskGenerator.Generate());
            Assert.AreEqual("4", await taskGenerator.Generate());
            Assert.AreEqual("5", await taskGenerator.Generate());
            Assert.AreEqual("6", await taskGenerator.Generate());
            Assert.AreEqual("7", await taskGenerator.Generate());
            Assert.AreEqual("8", await taskGenerator.Generate());
            Assert.AreEqual("9", await taskGenerator.Generate());
        }

        [Test]
        public async Task Is_Evaluated_When_Awaited() {
            var invoked = false;
            var generator = _generator.Select((int i) => {
                invoked = true;
                return i;
            });
            Assert.IsFalse(invoked);
            var generate = generator.Generate();
            Assert.IsFalse(invoked);
            await generate;
            Assert.IsTrue(invoked);
        }

        [Test]
        public async Task Multiply_With_Two() {
            var taskGenerator = _generator.Select(i => i * 2);

            Assert.AreEqual(0, await taskGenerator.Generate());
            Assert.AreEqual(2, await taskGenerator.Generate());
            Assert.AreEqual(4, await taskGenerator.Generate());
            Assert.AreEqual(6, await taskGenerator.Generate());
            Assert.AreEqual(8, await taskGenerator.Generate());
            Assert.AreEqual(10, await taskGenerator.Generate());
            Assert.AreEqual(12, await taskGenerator.Generate());
            Assert.AreEqual(14, await taskGenerator.Generate());
            Assert.AreEqual(16, await taskGenerator.Generate());
            Assert.AreEqual(18, await taskGenerator.Generate());
        }
    }
}