using System.Linq;
using NUnit.Framework;

namespace Sharpy.Builder.Tests.Implementations
{
    internal class UserNameTests
    {
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