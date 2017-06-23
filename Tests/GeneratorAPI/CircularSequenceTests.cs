using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneratorAPI;
using NUnit.Framework;

namespace Tests.GeneratorAPI {
    [TestFixture]
    public class CircularSequenceTests {
        [SetUp]
        public void Initiate() {
            _list = new List<string> {
                "test1",
                "test2",
                "test3",
                "test4",
                "test5",
                "test6",
                "test7",
                "test8",
                "test9",
                "test10"
            };
            _generator = Generator.CircularSequence(_list);
        }

        [TearDown]
        public void Dispose() {
            _generator = null;
        }

        private IGenerator<string> _generator;
        private List<string> _list;

        [Test(
            Description = "Verify that generator iterates through the list supplied"
        )]
        public void Iterates_Through_List() {
            Assert.IsTrue(_generator.Take(10).SequenceEqual(_list));
        }

        [Test(
            Description = "Verify that passing null when creating a circular sequence throws exception"
        )]
        public void Null_Enumerable() {
            Assert.Throws<ArgumentNullException>(() => Generator.CircularSequence<string>(null));
        }

        [Test(
            Description = "Verify that generator restarts when all elements has been iterated"
        )]
        public void Repeats_When_List_Ends() {
            var list = _generator.Take(40).ToList();
            //Verify that you get the correct ammount from take
            Assert.AreEqual(40, list.Count);
            // Verify that reseting list works like concat.
            Assert.IsTrue(list.SequenceEqual(_list.Concat(_list).Concat(_list).Concat(_list)));
        }

        [Test(
            Description = "Verify that it's possible to use ArrayList"
        )]
        public void Support_No_Generic_Enumerable_ArrayList() {
            var list = Generator
                .CircularSequence(new ArrayList {1, 2, 3})
                .Cast<int>();
            Assert.AreEqual(new[] {1, 2, 3}, list.ToArray(3));
        }

        [Test(
            Description = "Verify that it's possible to use Stack"
        )]
        public void Support_No_Generic_Enumerable_Stack() {
            var stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            var list = Generator
                .CircularSequence(stack)
                .Cast<int>();
            Assert.AreEqual(new[] {3, 2, 1}, list.ToArray(3));
        }

        [Test(
            Description = "Verify that it's possible to use Queue"
        )]
        public void Support_No_Generic_Enumerable_Queue() {
            var stack = new Queue();
            stack.Enqueue(1);
            stack.Enqueue(2);
            stack.Enqueue(3);
            var list = Generator
                .CircularSequence(stack)
                .Cast<int>();
            Assert.AreEqual(new[] {1, 2, 3}, list.ToArray(3));
        }
    }
}