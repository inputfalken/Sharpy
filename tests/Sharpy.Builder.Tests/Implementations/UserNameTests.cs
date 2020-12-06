using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations
{
    internal class UserNameTests
    {
        [Test]
        public void No_Arg_Is_Deterministic_With_Seed()
        {
            Assertion.IsDeterministic(i => new UserNameRandomizer(new Random(i)), x => x.UserName());
        }

        [Test]
        public void No_Arg_Is_Not_Deterministic_With_Different_Seed()
        {
            Assertion.IsNotDeterministic(i => new UserNameRandomizer(new Random(i)), x => x.UserName());
        }

        [Test]
        public void User_Name_Not_Null_Or_White_Space()
        {
            var builder = new Builder();
            var names = Enumerable.Range(0, Assertion.Amount / 10000).Select(i => builder.UserName()).ToList();
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void User_Name_Contains_AtLeast_One_Char()
        {
            var builder = new Builder();
            var names = Enumerable.Range(0, Assertion.Amount / 10000).Select(i => builder.UserName()).ToList();

            Assert.IsTrue(names.All(x => x.Length > 0));
        }
    }
}