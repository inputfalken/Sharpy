using System.Threading.Tasks;
using GeneratorAPI;
using NUnit.Framework;

namespace Tests.GeneratorAPI {
    internal class TaskExtensionTests {
        [Test(
            Description = "Verify that bool isInvoked gets assigned once the generation is awaited"
        )]
        public async Task Do_Finsh_When_Result_Is_Awaited() {
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
        public void Do_Does_Not_Finish_With_Generate() {
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
            Description = "Verify that Select can use <T> of IGenerator<Task<T>>"
        )]
        public async Task Select() {
            var generator = Generator
                .Create("hello")
                .Select(async s => {
                    await Task.Delay(500);
                    return s.Length;
                });
            Assert.AreEqual(5, await generator.Generate());
        }

        [Test(
            Description = "Verify that Selectmany can flat nested task"
        )]
        public async Task SelectMany_Flat_Nested_Task() {
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