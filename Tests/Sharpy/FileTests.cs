using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Sharpy.Implementation.DataObjects;
using Sharpy.Properties;

namespace Tests.Sharpy {
    [TestFixture]
    internal class FileTests {
        [Test]
        public void Name_Contains_No_Numbers() {
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin));
            var containsNumber = deserializeObject.Select(name => name.Data).All(s => s.Any(char.IsNumber));
            Assert.IsFalse(containsNumber);
        }

        [Test]
        public void Name_Contains_No_Punctuations() {
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin));
            var containsPuncation = deserializeObject.Select(name => name.Data).All(s => s.Any(char.IsPunctuation));
            Assert.IsFalse(containsPuncation);
        }

        [Test]
        public void Name_Contains_No_Separator() {
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin));
            var containsSeperator = deserializeObject.Select(name => name.Data).All(s => s.Any(char.IsSeparator));
            Assert.IsFalse(containsSeperator);
        }

        [Test]
        public void Name_Contains_No_Symbols() {
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin));
            var containsSymbols = deserializeObject.Select(name => name.Data).All(s => s.Any(char.IsSymbol));
            Assert.IsFalse(containsSymbols);
        }

        [Test]
        public void Name_Contains_No_White_Space() {
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin));
            var containsWhiteSpace = deserializeObject.Select(name => name.Data).All(s => s.Any(char.IsWhiteSpace));
            Assert.IsFalse(containsWhiteSpace);
        }

        [Test]
        public void Name_Starts_With_Capital_Letter() {
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin));
            var startsWithUpperCase = deserializeObject.Select(name => name.Data).All(s => char.IsUpper(s.First()));
            Assert.IsTrue(startsWithUpperCase);
        }

        [Test]
        public void User_Name_Contains_No_Numbers() {
            var containsNumber = Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                .All(s => s.Any(char.IsNumber));
            Assert.IsFalse(containsNumber);
        }


        [Test]
        public void User_Name_Contains_No_Punctuations() {
            var containsPunctuations =
                Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                    .All(s => s.Any(char.IsPunctuation));
            Assert.IsFalse(containsPunctuations);
        }

        [Test]
        public void User_Name_Contains_No_Separator() {
            var containsSeparator =
                Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                    .All(s => s.Any(char.IsSeparator));
            Assert.IsFalse(containsSeparator);
        }

        [Test]
        public void User_Name_Contains_No_Symbols() {
            var containsSymbols = Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                .All(s => s.Any(char.IsSymbol));
            Assert.IsFalse(containsSymbols);
        }

        [Test]
        public void User_Name_Contains_No_White_Space() {
            var containsWhitespace =
                Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                    .All(s => s.Any(char.IsWhiteSpace));
            Assert.IsFalse(containsWhitespace);
        }
    }
}