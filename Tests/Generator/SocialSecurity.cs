﻿using System;
using System.Linq;
using NodaTime;
using NUnit.Framework;
using Sharpy;

namespace Tests.Generator {
    [TestFixture]
    public class SocialSecurity {
        /// <summary>
        ///     This number is maxium ammount of possible number per date.
        /// </summary>
        private const int Limit = 10000;

        [Test]
        public void All_Are_Unique() {
            var gen = new Sharpy.Generator();
            var result = gen.GenerateSequence(g => g.SocialSecurityNumber(new LocalDate(2000, 10, 10)), Limit);
            Assert.IsTrue(result.GroupBy(s => s).All(grouping => grouping.Count() == 1));
        }

        [Test]
        public void All_Contains_Dash_At_Same_Index_When_Formated() {
            var gen = new Sharpy.Generator();
            var result = gen.GenerateSequence(g => g.SocialSecurityNumber(new LocalDate(2000, 10, 10)), Limit);
            Assert.IsTrue(result.All(s => s[6] == '-'));
        }

        [Test]
        public void All_Got_Same_Length() {
            var gen = new Sharpy.Generator();
            var result = gen.GenerateSequence(g => g.SocialSecurityNumber(new LocalDate(2000, 10, 10)), Limit);
            Assert.IsTrue(result.All(s => s.Length == 11));
        }

        [Test]
        public void Crate_More_Max_Combination_Throws() {
            var gen = new Sharpy.Generator();
            var result = gen.GenerateSequence(g => g.SocialSecurityNumber(new LocalDate(2000, 10, 10)), Limit + 1);
            Assert.Throws<Exception>(() => result.ToArray());
        }

        [Test]
        public void Create_Max_Ammount_Not_Throw() {
            var gen = new Sharpy.Generator();
            var result = gen.GenerateSequence(g => g.SocialSecurityNumber(new LocalDate(2000, 10, 10)), Limit);
            Assert.DoesNotThrow(() => result.ToArray());
        }


        [Test]
        public void No_Dash_With_False_Formating() {
            var gen = new Sharpy.Generator();
            var result = gen.GenerateSequence(g => g.SocialSecurityNumber(new LocalDate(2000, 10, 10), false), Limit);
            Assert.IsTrue(result.All(s => s.All(char.IsNumber)));
        }
    }
}