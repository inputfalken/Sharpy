using System.Threading.Tasks;
using GeneratorAPI;
using NUnit.Framework;

namespace Tests.GeneratorAPI {
    internal class TaskExtensionTests {
        [Test(
            Description = "Verify that bool isInvoked gets assigned once the generation is awaited"
        )]
        public async Task Do_Task_Gets_Invoked_When_Awaited() {
            var isInvoked = false;
            var generator = Generator.Create(Task.Run(async () => {
                    await Task.Delay(500);
                    return true;
                }))
                .Do(b => isInvoked = b);
            Assert.IsFalse(isInvoked);
            await generator.Generate();
            Assert.IsTrue(isInvoked);
        }

        [Test(
            Description = "Verify that bool isInvoked gets assigned once the generation is awaited"
        )]
        public void Do_Task_Does_Not_Finish_When_Generate() {
            var isInvoked = false;
            var generator = Generator.Create(Task.Run(async () => {
                    await Task.Delay(500);
                    return true;
                }))
                .Do(b => isInvoked = b);
            Assert.IsFalse(isInvoked);
            generator.Generate();
            Assert.IsFalse(isInvoked);
        }

        [Test(
            Description = "Verify that Select can <T> of IGenerator<Task<T>>"
        )]
        public async Task Select_TSource_Task() {
            var generator = Generator
                .Create(Task.Run(async () => {
                    await Task.Delay(500);
                    return "hello";
                }))
                .Select(s => s.Length);
            Assert.AreEqual(5, await generator.Generate());
        }

        [Test(
            Description = "Verify that SelectMany can use <T> of IGenerator<T> Flatmap into Task<TResult>"
        )]
        public async Task SelectMany_TResult_Task() {
            var generator = Generator
                .Create("hello")
                .SelectMany(async s => {
                    await Task.Delay(500);
                    return s.Length;
                });
            Assert.AreEqual(5, await generator.Generate());
        }

        [Test(
            Description = "Verify that SelectMany can <T> IGenerator<Task<T>> and Flatmap into Task<TResult>"
        )]
        public async Task SelectMany_TSource_Task_TResult_Task() {
            var generator = Generator
                .Create(Task.Run(() => "hello"))
                .SelectMany(async s => {
                    await Task.Delay(500);
                    return s.Length;
                });
            Assert.AreEqual(5, await generator.Generate());
        }
    }
}