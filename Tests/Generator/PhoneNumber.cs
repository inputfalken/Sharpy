using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class PhoneNumber {
        [Test]
        public void All_Unique_No_Prefix_Not_Unique() {
            var gen = new Sharpy.Generator.Configurment {UniqueNumbers = false}.CreateGenerator();
            var res = gen.GenerateSequence(g => g.NumberByLength(5), 10000);
            Assert.IsFalse(res.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void All_Unique_No_Prefix_Unique() {
            var gen = new Sharpy.Generator.Configurment {UniqueNumbers = true}.CreateGenerator();
            var res = gen.GenerateSequence(g => g.NumberByLength(5), 10000);
            Assert.IsTrue(res.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void Create_Max_Ammount_Does_Not_Throw_Unique() {
            var gen = new Sharpy.Generator.Configurment {UniqueNumbers = true}.CreateGenerator();
            var res = gen.GenerateSequence(generator => generator.NumberByLength(3), 1000);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.DoesNotThrow(() => res.ToArray());
        }

        [Test]
        public void Create_More_Than_Max_Ammount_Throw_Not_Unique() {
            var gen = new Sharpy.Generator.Configurment {UniqueNumbers = false}.CreateGenerator();
            var res = gen.GenerateSequence(generator => generator.NumberByLength(3), 1001);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.DoesNotThrow(() => res.ToArray());
        }

        [Test]
        public void Create_More_Than_Max_Ammount_Throw_Unique() {
            var gen = new Sharpy.Generator.Configurment {UniqueNumbers = true}.CreateGenerator();
            var res = gen.GenerateSequence(generator => generator.NumberByLength(3), 1001);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.Throws<Exception>(() => res.ToArray());
        }

        [Test]
        public void Got_Same_Length_No_Prefix_Not_Unique() {
            var gen = new Sharpy.Generator.Configurment {UniqueNumbers = true}.CreateGenerator();
            var res = gen.GenerateSequence(g => g.NumberByLength(5), 10000);
            Assert.IsTrue(res.All(s => s.Length == 5));
        }

        [Test]
        public void Got_Same_Length_No_Prefix_Unique() {
            var gen = new Sharpy.Generator.Configurment {UniqueNumbers = true}.CreateGenerator();
            var res = gen.GenerateSequence(g => g.NumberByLength(5), 10000);
            Assert.IsTrue(res.All(s => s.Length == 5));
        }
    }
}