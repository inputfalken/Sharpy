using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sharpy;
using static System.Linq.Enumerable;

namespace Tests.Integration {
    [TestFixture]
    internal class SequenceTests {
        [Test(
            Author = "Robert",
            Description = "An exception should be thrown if sequence is empty"
        )]
        public void Number_Empty_Sequence() {
            Assert.Throws<ArgumentException>
            (() => {
                var res = Productor.Sequence(Empty<int>()).Take(8).ToArray();
            });
        }

        [Test(
            Author = "Robert",
            Description =
                "Produce a number sequence containing more elements than main sequence, result should reset"
        )]
        public void Number_Sequence_More_Than_Length() {
            var result = Productor
                .Sequence(Range(0, 10))
                .Take(11);
            var expected = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "Map from a Sequence"
        )]
        public void Number_Sequence_Select_Times_Two() {
            var result = Productor
                .Sequence(Range(0, 10))
                .Select(i => i * 2)
                .Take(10);
            int[] expected = {0, 2, 4, 6, 8, 10, 12, 14, 16, 18};
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "Produce a number sequence containing less elements than main sequence"
        )]
        public void Number_Sequence_Take_Less_Than_Length() {
            var result = Productor
                .Sequence(Range(0, 10))
                .Take(7);

            Assert.AreEqual(result, Range(0, 7));
        }


        [Test(
            Author = "Robert",
            Description =
                "Check to see if TakeForever saves it's state as well as reset itself when all elements has been interated"
        )]
        public void Number_Sequence_TakeForever() {
            var takeForever = Productor
                .Sequence(Range(0, 10))
                .TakeForever();
            var take2 = takeForever.Take(2);
            var take3 = takeForever.Take(3);
            var take5 = takeForever.Take(5);
            var take10 = takeForever.Take(10);
            Assert.AreEqual(new[] {0, 1}, take2);
            Assert.AreEqual(new[] {2, 3, 4}, take3);
            Assert.AreEqual(new[] {5, 6, 7, 8, 9}, take5);
            Assert.AreEqual(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}, take10);
        }

        [Test(
            Author = "Robert",
            Description = "Zip a Sequence with a defered string"
        )]
        public void Number_Sequence_Zip_With_Defered_String() {
            var result = Productor
                .Sequence(Range(0, 10))
                .Zip(Productor.Defer(() => "test"), (i, s) => s + i)
                .Take(4);
            string[] expected = {"test0", "test1", "test2", "test3"};
            Assert.AreEqual(result, expected);
        }


        [Test(
            Author = "Robert",
            Description = "Zip a Sequence with IEnumerable"
        )]
        public void Number_Sequence_Zip_With_Enumerable() {
            const int count = 10;
            var result = Productor
                .Sequence(Range(0, 10))
                .Zip(Range(0, count), (i, i1) => i + i1)
                .Take(count);
            int[] expected = {0, 2, 4, 6, 8, 10, 12, 14, 16, 18};
            Assert.AreEqual(expected, result);
        }

        [Test(
            Author = "Robert",
            Description = "Zip a Sequence with a Return string"
        )]
        public void Number_Sequence_Zip_With_Return_String() {
            var result = Productor
                .Sequence(Range(0, 10))
                .Zip(Productor.Return("test"), (i, s) => s + i)
                .Take(4);
            string[] expected = {"test0", "test1", "test2", "test3"};
            Assert.AreEqual(result, expected);
        }

        [Test(
            Author = "Robert",
            Description = "Zip a Sequence with another sequence"
        )]
        public void Number_Sequence_Zip_With_Sequence() {
            var result = Productor
                .Sequence(Range(0, 10))
                .Zip(Productor.Sequence(Range(0, 10)), (i, i1) => i + i1)
                .Take(10);
            int[] expected = {0, 2, 4, 6, 8, 10, 12, 14, 16, 18};
            Assert.AreEqual(expected, result);
        }
    }
}