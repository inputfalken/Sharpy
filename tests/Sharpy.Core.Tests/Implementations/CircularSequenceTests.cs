using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Core.Linq;

namespace Sharpy.Core.Tests.Implementations {
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
            Description = "Verify that it's possible to use ArrayList"
        )]
        public void Generic_List() {
            var list = new List<int> {1, 2, 3};
            var generator = Generator
                .CircularSequence(list);

            Assert.AreEqual(new[] {1, 2, 3}, generator.ToArray(3));
        }

        [Test(
            Description = "Verify that generator iterates through the list supplied"
        )]
        public void Generic_List_Iterates_All_Elements() {
            Assert.IsTrue(_generator.Take(10).SequenceEqual(_list));
        }

        [Test(
            Description = "Verify that generator restarts when all elements has been iterated"
        )]
        public void Generic_List_Repeats() {
            var list = _generator.Take(40).ToList();
            //Verify that you get the correct ammount from take
            Assert.AreEqual(40, list.Count);
            // Verify that reseting list works like concat.
            Assert.IsTrue(list.SequenceEqual(_list.Concat(_list).Concat(_list).Concat(_list)));
        }

        [Test(
            Description = "Verify that it's possible to use Queue"
        )]
        public void Generic_Queue() {
            var stack = new Queue<int>();
            stack.Enqueue(1);
            stack.Enqueue(2);
            stack.Enqueue(3);
            var generator = Generator
                .CircularSequence(stack);

            Assert.AreEqual(new[] {1, 2, 3}, generator.ToArray(3));
        }

        [Test(
            Description = "Verify that it's possible to use Stack"
        )]
        public void Generic_Stack() {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            var generator = Generator
                .CircularSequence(stack);

            Assert.AreEqual(new[] {3, 2, 1}, generator.ToArray(3));
        }

        [Test(
            Description = "Verify that passing null when creating a circular sequence throws exception"
        )]
        public void Null_Enumerable() {
            Assert.Throws<ArgumentNullException>(() => Generator.CircularSequence<string>(null));
        }
    }
}