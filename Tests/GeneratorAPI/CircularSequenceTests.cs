using System;
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
    }
}