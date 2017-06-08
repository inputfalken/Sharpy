﻿using System.Linq;
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
        public async Task Where_Predicate_Task_Value() {
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
    }
}