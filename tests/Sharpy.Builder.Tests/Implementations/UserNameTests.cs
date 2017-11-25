using System.Linq;
using NUnit.Framework;

namespace Sharpy.Builder.Tests.Implementations {
    class UserNameTests {
        private const int Count = 100;

        [Test]
        public void User_Name_Not_Null_Or_White_Space() {
            var builder = new Builder();
            var names = Enumerable.Range(0, Count).Select(i => builder.UserName()).ToList();
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
        }
    }
}