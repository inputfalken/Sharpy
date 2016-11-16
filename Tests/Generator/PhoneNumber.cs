using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class PhoneNumber {
        [Test]
        public void Change_Higher_Length() {
            var gen = new Sharpy.Generator();
            var generateMany = gen.GenerateMany(g => g.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 7));
            var generateMany2 = gen.GenerateMany(g => g.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 8));
        }

        [Test]
        public void Change_Lower_Length() {
            var gen = new Sharpy.Generator();
            var generateMany = gen.GenerateMany(g => g.PhoneNumber(6, "07"), 10000);
            Assert.IsTrue(generateMany.All(s => s.Length == 8));
            var generateMany2 = gen.GenerateMany(g => g.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(generateMany2.All(s => s.Length == 7));
        }

        [Test]
        public void Create_Max_Ammount_Does_Not_Throw() {
            var gen = new Sharpy.Generator();
            var res = gen.GenerateMany(generator => generator.PhoneNumber(3), 1000);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.DoesNotThrow(() => res.ToArray());
        }

        [Test]
        public void Create_More_Than_Max_Ammount_Throw() {
            var gen = new Sharpy.Generator();
            var res = gen.GenerateMany(generator => generator.PhoneNumber(3), 1001);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.Throws<Exception>(() => res.ToArray());
        }

        [Test]
        public void Got_Same_Length_No_Prefix() {
            var gen = new Sharpy.Generator();
            var res = gen.GenerateMany(g => g.PhoneNumber(5), 10000);
            Assert.IsTrue(res.All(s => s.Length == 5));
        }

        [Test]
        public void Got_Same_Length_With_Prefix() {
            var gen = new Sharpy.Generator();
            var res = gen.GenerateMany(g => g.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(res.All(s => s.Length == 7));
        }

        [Test]
        public void All_Unique_With_Prefix() {
            var gen = new Sharpy.Generator();
            var res = gen.GenerateMany(g => g.PhoneNumber(5, "07"), 10000);
            Assert.IsTrue(res.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void All_Unique_No_Prefix() {
            var gen = new Sharpy.Generator();
            var res = gen.GenerateMany(g => g.PhoneNumber(5), 10000);
            Assert.IsTrue(res.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }
    }
}