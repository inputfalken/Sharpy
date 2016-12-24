using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class NumberByLength {
        [Test]
        public void Not_Unique() {
            var configurement = new Configurement {
                UniqueNumbers = false
            };
            var gen = new Sharpy.Generator(configurement);
            var res = gen.GenerateSequence(g => g.NumberByLength(5), 10000);
            Assert.IsFalse(res.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void Unique() {
            var configurement = new Configurement {
                UniqueNumbers = true
            };
            var gen = new Sharpy.Generator(configurement);
            var res = gen.GenerateSequence(g => g.NumberByLength(5), 10000);
            Assert.IsTrue(res.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void Unique_Create_Max_Ammount_Does_Not_Throw() {
            var configurement = new Configurement {
                UniqueNumbers = true
            };
            var gen = new Sharpy.Generator(configurement);
            var res = gen.GenerateSequence(generator => generator.NumberByLength(3), 1000);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.DoesNotThrow(() => res.ToArray());
        }

        [Test]
        public void Not_Unique_Create_More_Than_Max_Ammount_Does_Not_Throw() {
            var configurement = new Configurement {
                UniqueNumbers = false
            };
            var gen = new Sharpy.Generator(configurement);
            var res = gen.GenerateSequence(generator => generator.NumberByLength(3), 1001);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.DoesNotThrow(() => res.ToArray());
        }

        [Test]
        public void Unique_Create_More_Than_Max_Ammount_Does_Throw() {
            var configurement = new Configurement {
                UniqueNumbers = true
            };
            var gen = new Sharpy.Generator(configurement);
            var res = gen.GenerateSequence(generator => generator.NumberByLength(3), 1001);
            //The test checks that it works like the following algorithm 10^length and that all got same length.
            Assert.Throws<Exception>(() => res.ToArray());
        }

        [Test]
        public void Not_Unique_Got_Same_Length() {
            var configurement = new Configurement {
                UniqueNumbers = false
            };
            var gen = new Sharpy.Generator(configurement);
            var res = gen.GenerateSequence(g => g.NumberByLength(5), 10000);
            Assert.IsTrue(res.All(s => s.Length == 5));
        }

        [Test]
        public void Unique_Got_Same_Length() {
            var configurement = new Configurement {
                UniqueNumbers = true
            };
            var gen = new Sharpy.Generator(configurement);
            var res = gen.GenerateSequence(g => g.NumberByLength(5), 10000);
            Assert.IsTrue(res.All(s => s.Length == 5));
        }
    }
}