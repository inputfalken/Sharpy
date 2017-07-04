using System;
using System.Linq;
using System.Threading.Tasks;
using GeneratorAPI;
using GeneratorAPI.Linq;
using GeneratorAPI.Linq.TaskExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.GeneratorAPI.TaskExtensions {
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
            Description = "Verify that async Do throws exception with null action"
        )]
        public void Do_Null_Action() {
            var generator = Generator.Create(Task.Run(async () => {
                await Task.Delay(500);
                return true;
            }));
            Assert.Throws<ArgumentNullException>(() => generator.Do(task: null));
        }

        [Test(
            Description = "Verify that async Do throws exception with null generator"
        )]
        public void Do_Null_Generator() {
            IGenerator<Task<int>> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Do(task: i => { }));
        }


        [Test(
            Description = "Verify that Select can use <T> of IGenerator<Task<T>>"
        )]
        public async Task Map_From_Task() {
            var generator = Generator
                .Create(Task.Run(() => "hello"))
                .Select(async s => {
                    await Task.Delay(500);
                    return s.Length;
                });
            Assert.AreEqual(5, await await generator.Generate());
        }

        [Test(
            Description = "Verify that SelectMany can flat nested task"
        )]
        public async Task FlatMapTask_Flat_Nested_Task() {
            var generator = Generator
                .Create(Task.Run(() => "hello"))
                .SelectMany(async s => {
                    await Task.Delay(500);
                    return s.Length;
                });
            Assert.AreEqual(5, await generator.Generate());
        }


        [Test(
            Description = "Verify that SelectMany can flat nested task"
        )]
        public async Task FlatMapTask_Flat_Task() {
            var generator = Generator
                .Create(Task.Run(() => "Hello"))
                .SelectMany(async s => {
                    await Task.Delay(500);
                    return s.Length;
                });
            Assert.AreEqual(5, await generator.Generate());
        }


        [Test(
            Description = "Zip called from task generator and zipped with regular generator"
        )]
        public async Task Zip_First_Async_Generator() {
            var generator = Generator
                .Create(Task.Run(async () => {
                    await Task.Delay(500);
                    return "hello";
                }))
                .Zip(Generator.Create("world"), (string s, string s1) => s + s1);

            Assert.AreEqual("helloworld", await generator.Generate());
        }


        [Test(
            Description = "Zip called with two task generators"
        )]
        public async Task Zip_Both_Async_Generator() {
            var generator = Generator
                .Create(Task.Run(async () => {
                    await Task.Delay(500);
                    return "hello";
                }));
            var zip = Generator.Create(Task.Run(() => "world")).Zip(generator, (string s, string s1) => s + s1);
            Assert.AreEqual("worldhello", await zip.Generate());
        }

        [Test(
            Description = "Verify that Where works with Tasks"
        )]
        public async Task Filter() {
            var generator = Generator
                .Create(new Randomizer())
                .Select(async g => {
                    await Task.Delay(50);
                    return g.Next(20, 50);
                })
                .Where(i => i % 2 == 0)
                .Take(200);

            Assert.IsTrue((await Task.WhenAll(generator)).All(i => i % 2 == 0));
        }

        [Test(
            Description = "Verify that Where works with Tasks"
        )]
        public void Filter_Bad_Predicate() {
            Func<int, bool> predicate = i => false;
            var generator = Generator
                .Create(Task.Run(() => 1))
                .Where(predicate)
                .Take(200);

            var task = Task.WhenAll(generator);
            Assert.ThrowsAsync<ArgumentException>(async () => await task);
        }

        [Test(
            Description = "Verify that Where works with Tasks"
        )]
        public void Filter_Null_Predicate() {
            Func<Randomizer, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => Generator
                .Create(Task.Run(() => new Randomizer()))
                .Where(predicate));
        }

        [Test(
            Description = "Verify that Where works with Tasks"
        )]
        public void Filter_Null_Generator() {
            IGenerator<Task<Randomizer>> rndGenerator = null;

            Assert.Throws<ArgumentNullException>(() => rndGenerator.Where(i => i.Next() % 2 == 0));
        }
    }
}