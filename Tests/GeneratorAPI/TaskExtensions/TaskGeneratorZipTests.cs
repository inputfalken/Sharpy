using System.Threading.Tasks;
using GeneratorAPI;
using GeneratorAPI.Linq;
using GeneratorAPI.Linq.TaskExtensions;
using NUnit.Framework;

namespace Tests.GeneratorAPI.TaskExtensions {
    [TestFixture]
    internal class TaskGeneratorZipTests {
        [SetUp]
        public void Initiate() {
            _generator = Generator.Factory
                .Incrementer(0)
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
        public async Task Double_Task_Generator_Is_Evaluated_When_Awaited() {
            var invoked = false;
            var second = Generator.Factory
                .Incrementer(0)
                .Select(async i => {
                    await Task.Delay(100);
                    return i;
                });
            var generator = _generator
                .Zip(second, (int i, int i1) => {
                    invoked = true;
                    return invoked;
                });
            Assert.IsFalse(invoked);
            var generate = generator.Generate();
            Assert.IsFalse(invoked);
            await generate;
            Assert.IsTrue(invoked);
        }

        [Test]
        public async Task Double_Task_Generator_Multiply() {
            var second = Generator.Factory
                .Incrementer(0)
                .Select(async i => {
                    await Task.Delay(100);
                    return i;
                });
            var generator = _generator
                .Zip(second, (i, i1) => i * i1);

            Assert.AreEqual(0, await generator.Generate()); // 0 * 0
            Assert.AreEqual(1, await generator.Generate()); // 1 * 1
            Assert.AreEqual(4, await generator.Generate()); // 2 * 2
            Assert.AreEqual(9, await generator.Generate()); // 3 * 3
            Assert.AreEqual(16, await generator.Generate()); // 4 * 4
        }

        [Test]
        public async Task Single_Task_Generator_Is_Evaluated_When_Awaited() {
            var second = Generator.Factory
                .Incrementer(0);
            var invoked = false;
            var generator = _generator
                .Zip(second, (int i, int i1) => {
                    invoked = true;
                    return invoked;
                });
            Assert.IsFalse(invoked);
            var generate = generator.Generate();
            Assert.IsFalse(invoked);
            await generate;
            Assert.IsTrue(invoked);
        }

        [Test]
        public async Task Single_Task_Generator_Multiply() {
            var second = Generator.Factory
                .Incrementer(0);
            var generator = _generator
                .Zip(second, (i, i1) => i * i1);

            Assert.AreEqual(0, await generator.Generate()); // 0 * 0
            Assert.AreEqual(1, await generator.Generate()); // 1 * 1
            Assert.AreEqual(4, await generator.Generate()); // 2 * 2
            Assert.AreEqual(9, await generator.Generate()); // 3 * 3
            Assert.AreEqual(16, await generator.Generate()); // 4 * 4
        }
    }
}