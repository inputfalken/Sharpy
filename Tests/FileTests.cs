using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Sharpy.Implementation.DataObjects;
using Sharpy.Properties;

namespace Tests {
    [TestFixture]
    internal class FileTests {
        [Test]
        public void Name_Contains_No_Numbers() {
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin));
            var noNumber = deserializeObject.Select(name => name.Data).All(s => s.All(c => !char.IsNumber(c)));
            Assert.IsTrue(noNumber);
        }

        [Test]
        public void Name_Contains_No_Symbol() {
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin));
            var noSymbol = deserializeObject.Select(name => name.Data).All(s => s.All(c => !char.IsSymbol(c)));
            Assert.IsTrue(noSymbol);
        }

        [Test]
        public void Name_Contains_No_Symbols() {
            var noSymbols = Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                .All(s => s.All(c => !char.IsSymbol(c)));
            Assert.IsTrue(noSymbols);
        }

        [Test]
        public void User_Name_Contains_No_Numbers() {
            var noNumber = Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                .All(s => s.All(c => !char.IsNumber(c)));
            Assert.IsTrue(noNumber);
        }

        [Test]
        public void User_Name_Contains_No_Symbols() {
            var noSymbols = Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                .All(s => s.All(c => !char.IsSymbol(c)));
            Assert.IsTrue(noSymbols);
        }
    }
}