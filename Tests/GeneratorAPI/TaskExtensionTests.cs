using System;
using System.Linq;
using System.Threading.Tasks;
using GeneratorAPI;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
            Description = "Verify that async Do throws exception with null action"
        )]
        public void Do_Null_Action() {
            var generator = Generator.Create(Task.Run(async () => {
                await Task.Delay(500);
                return true;
            }));
            Assert.Throws<ArgumentNullException>(() => generator.Do(actionTask: null));
        }
        [Test(
            Description = "Verify that async Do throws exception with null generator"
        )]
        public void Do_Null_Generator() {
            IGenerator<Task<int>> generator = null;
            Assert.Throws<ArgumentNullException>(() => generator.Do(actionTask:i => {}));
        }


        [Test(
            Description = "Verify that Select can use <T> of IGenerator<Task<T>>"
        )]
        public async Task Select_From_Task() {
            var generator = Generator
                .Create(Task.Run(() => "hello"))
                .Select(async s => {
                    await Task.Delay(500);
                    return s.Length;
                });
            Assert.AreEqual(5, await await generator.Generate());
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

        [Test(
            Description = "Verify that Selectmany with compose can flat nested task"
        )]
        public async Task SelectMany_Flat_Nested_Task_Compose() {
            var generator = Generator
                .Create(Task.Run(() => "hello"))
                .SelectMany(async s => {
                    await Task.Delay(500);
                    return s.Length;
                }, (s, i) => s + i);
            Assert.AreEqual("hello5", await generator.Generate());
        }

        [Test(
            Description = "Verify that Selectmany can flat nested task"
        )]
        public async Task SelectMany_Flat_Task() {
            var generator = Generator
                .Create("Hello")
                .SelectMany(async s => {
                    await Task.Delay(500);
                    return s.Length;
                });
            Assert.AreEqual(5, await generator.Generate());
        }

        [Test(
            Description = "Verify that Selectmany with compose can flat nested task"
        )]
        public async Task SelectMany_Flat_Task_Compose() {
            var generator = Generator
                .Create("hello")
                .SelectMany(async s => {
                    await Task.Delay(500);
                    return s.Length;
                }, (s, i) => s + i);
            Assert.AreEqual("hello5", await generator.Generate());
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
            Description = "Zip called from regular generator and zipped with a task generator"
        )]
        public async Task Zip_Second_Async_Generator() {
            var generator = Generator
                .Create(Task.Run(async () => {
                    await Task.Delay(500);
                    return "hello";
                }));
            var zip = Generator.Create("world").Zip(generator, (string s, string s1) => s + s1);
            Assert.AreEqual("worldhello", await zip.Generate());
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
        public async Task Where() {
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
        public void Where_Bad_Predicate() {
            var generator = Generator
                .Create(Task.Run(() => 1))
                .Where(asyncPredicate: i => false)
                .Take(200);

            var task = Task.WhenAll(generator);
            Assert.ThrowsAsync<ArgumentException>(async () => await task);
        }

        [Test(
            Description = "Verify that Where works with Tasks"
        )]
        public void Where_Null_Predicate() {
            Assert.Throws<ArgumentNullException>(() => Generator
                .Create(Task.Run(() => new Randomizer()))
                .Where(asyncPredicate: null));
        }

        [Test(
            Description = "Verify that Where works with Tasks"
        )]
        public void Where_Null_Generator() {
            IGenerator<Task<Randomizer>> rndGenerator = null;

            Assert.Throws<ArgumentNullException>(() => rndGenerator.Where(i => i.Next() % 2 == 0));
        }
    }
}