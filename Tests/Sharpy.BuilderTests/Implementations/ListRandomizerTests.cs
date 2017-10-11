﻿using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Tests.Sharpy.BuilderTests.Implementations {
    [TestFixture]
    public class ListRandomizerTests {
        [Test]
        [Repeat(100)]
        public void Array() {
            var randomGenerator = new ListRandomizer(new Random());
            var args = new[] {"hello", "there", "foo"};

            Assert.IsTrue(args.Contains(randomGenerator.Element(args)));
        }

        [Test]
        [Repeat(100)]
        public void List() {
            var randomGenerator = new ListRandomizer(new Random());
            var args = new[] {"hello", "there", "foo"};
            Assert.IsTrue(args.Contains(randomGenerator.Element(args)));
        }
    }
}