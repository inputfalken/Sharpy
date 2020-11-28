﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class UniqueSecurityFormatedBuilderTests
    {
        /// <summary>
        ///     This number is maximum amount of possible number per date.
        /// </summary>
        private const int Limit = 10000;

        [Test]
        public void All_Are_Unique()
        {
            var dateTime = new DateTime(2000, 10, 10);
            var uniqueSecurityBuilder = new UniqueFormattedSecurityBuilder(new Random());
            var list = new List<string>();
            for (var i = 0; i < Limit; i++) list.Add(uniqueSecurityBuilder.SecurityNumber(dateTime));
            Assert.IsTrue(list.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void All_Got_Same_Length()
        {
            var dateTime = new DateTime(2000, 10, 10);
            var uniqueSecurityBuilder = new UniqueFormattedSecurityBuilder(new Random());
            var list = new List<string>();
            for (var i = 0; i < Limit; i++) list.Add(uniqueSecurityBuilder.SecurityNumber(dateTime));
            Assert.IsTrue(list.All(s => s.Length == 11));
        }

        [Test]
        public void Contains_Dash_At_Sixth_Index()
        {
            var dateTime = new DateTime(2000, 10, 10);
            var uniqueSecurityBuilder = new UniqueFormattedSecurityBuilder(new Random());
            var list = new List<string>();
            for (var i = 0; i < Limit; i++) list.Add(uniqueSecurityBuilder.SecurityNumber(dateTime));
            Assert.IsTrue(list.All(s => s[6] == '-'));
        }

        [Test]
        public void Crate_More_Max_Combination_Throws()
        {
            var dateTime = new DateTime(2000, 10, 10);
            var uniqueSecurityBuilder = new UniqueFormattedSecurityBuilder(new Random());
            var list = new List<string>();
            for (var i = 0; i < Limit; i++) list.Add(uniqueSecurityBuilder.SecurityNumber(dateTime));
            Assert.Throws<Exception>(() => uniqueSecurityBuilder.SecurityNumber(dateTime));
        }
    }
}