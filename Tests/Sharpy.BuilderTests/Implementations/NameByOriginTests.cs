using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Enums;
using Sharpy.Builder.Implementation;

namespace Tests.Sharpy.BuilderTests.Implementations {
    [TestFixture]
    internal class NameByOriginTests {
        private const int Amount = 10000;

        [Test]
        public void FirstName_No_Argument_Is_Not_Null_Or_Empty() {
            var nameByOrigin = new NameByOrigin(new Random());
            var list = new List<string>();
            for (var i = 0; i < Amount; i++) list.Add(nameByOrigin.FirstName());
            Assert.IsFalse(list.Any(string.IsNullOrEmpty));
        }

        [Test]
        public void FirstName_With_Female_Argument_Is_Not_Null_Or_Empty() {
            var nameByOrigin = new NameByOrigin(new Random());
            var list = new List<string>();
            for (var i = 0; i < Amount; i++) list.Add(nameByOrigin.FirstName(Gender.Female));
            Assert.IsFalse(list.Any(string.IsNullOrEmpty));
        }

        [Test]
        public void FirstName_With_Male_Argument_Is_Not_Null_Or_Empty() {
            var nameByOrigin = new NameByOrigin(new Random());
            var list = new List<string>();
            for (var i = 0; i < Amount; i++) list.Add(nameByOrigin.FirstName(Gender.Male));
            Assert.IsFalse(list.Any(string.IsNullOrEmpty));
        }

        [Test]
        public void LastName_Is_Not_Null_Or_Empty() {
            var nameByOrigin = new NameByOrigin(new Random());
            var list = new List<string>();
            for (var i = 0; i < Amount; i++) list.Add(nameByOrigin.LastName());
            Assert.IsFalse(list.Any(string.IsNullOrEmpty));
        }
    }
}