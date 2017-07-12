using System.Threading.Tasks;
using GeneratorAPI;
using GeneratorAPI.Linq;
using GeneratorAPI.Linq.TaskExtensions;
using NUnit.Framework;
using Sharpy;

namespace Tests.GeneratorAPI.TaskExtensions {
    [TestFixture]
    internal class TaskGeneratorDoTests {
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
        public async Task Is_Evaluated_When_Awaited() {
            var invoked = false;
            var generator = _generator.Do((int i) => invoked = true);
            Assert.IsFalse(invoked);
            var generate = generator.Generate();
            Assert.IsFalse(invoked);
            await generate;
            Assert.IsTrue(invoked);
        }
    }
}