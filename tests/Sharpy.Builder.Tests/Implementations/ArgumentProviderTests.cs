using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class ArgumentProviderTests
    {
        private readonly IProviders.IArgumentProvider _argumentProvider = new ArgumentRandomizer(new Random());
        private const int Iterations = 1000;
        [Test]
        public void Two_Arguments()
        {

            var arr = new int[Iterations];
            for (var i = 0; i < Iterations; i++) arr[i] = _argumentProvider.Argument(1, 2);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
        }

        [Test]
        public void Three_Arguments()
        {
            var arr = new int[Iterations];
            for (var i = 0; i < Iterations; i++) arr[i] = _argumentProvider.Argument(1, 2, 3);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
        }
        
        [Test]
        public void Four_Arguments()
        {
            var arr = new int[Iterations];
            for (var i = 0; i < Iterations; i++) arr[i] = _argumentProvider.Argument(1, 2, 3, 4);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
        }
        
        [Test]
        public void Five_Arguments()
        {
            var arr = new int[Iterations];
            for (var i = 0; i < Iterations; i++) arr[i] = _argumentProvider.Argument(1, 2, 3, 4, 5);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.AreEqual(5, grouping[4].Key);
            
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
            Assert.IsNotEmpty(grouping[4]);
        }
        
        [Test]
        public void Six_Arguments()
        {
            var arr = new int[Iterations];
            for (var i = 0; i < Iterations; i++) arr[i] = _argumentProvider.Argument(1, 2, 3, 4, 5, 6);

            var grouping = arr
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            Assert.AreEqual(1, grouping[0].Key);
            Assert.AreEqual(2, grouping[1].Key);
            Assert.AreEqual(3, grouping[2].Key);
            Assert.AreEqual(4, grouping[3].Key);
            Assert.AreEqual(5, grouping[4].Key);
            Assert.AreEqual(6, grouping[5].Key);
            Assert.IsNotEmpty(grouping[0]);
            Assert.IsNotEmpty(grouping[1]);
            Assert.IsNotEmpty(grouping[2]);
            Assert.IsNotEmpty(grouping[3]);
            Assert.IsNotEmpty(grouping[4]);
            Assert.IsNotEmpty(grouping[5]);
        }
    }
}