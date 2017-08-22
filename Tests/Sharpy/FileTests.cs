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
        private static readonly IEnumerable<Name> GetNames =
            JsonConvert.DeserializeObject<IEnumerable<Name>>(Resources.NamesByOrigin);

        [Test(
            Description = "Verify that no name contains number"
        )]
        public void Name_Contains_No_Numbers() {
            var deserializeObject = GetNames;
            var containsNumber = deserializeObject.Select(name => name.Data).All(s => s.Any(char.IsNumber));
            Assert.IsFalse(containsNumber);
        }

        [Test(
            Description = "Verify that no name contains puncatuation"
        )]
        public void Name_Contains_No_Punctuations() {
            var deserializeObject = GetNames;
            var containsPuncation = deserializeObject.Select(name => name.Data).All(s => s.Any(char.IsPunctuation));
            Assert.IsFalse(containsPuncation);
        }

        [Test(
            Description = "Verify that no name contains separators"
        )]
        public void Name_Contains_No_Separator() {
            var deserializeObject = GetNames;
            var containsSeperator = deserializeObject
                .Select(name => name.Data)
                .All(s => s.Any(char.IsSeparator));
            Assert.IsFalse(containsSeperator);
        }

        [Test(
            Description = "Verify that no name contains symbol"
        )]
        public void Name_Contains_No_Symbols() {
            var deserializeObject = GetNames;
            var containsSymbols = deserializeObject
                .Select(name => name.Data)
                .Any(s => s.Any(char.IsSymbol));
            Assert.IsFalse(containsSymbols);
        }

        [Test(
            Description = "Verify that no name contains white space."
        )]
        public void Name_Contains_No_White_Space() {
            var deserializeObject = GetNames;
            var containsWhiteSpace = deserializeObject
                .Select(name => name.Data)
                .Any(s => s.Any(char.IsWhiteSpace));
            Assert.IsFalse(containsWhiteSpace);
        }

        [Test(
            Description = "Verify that all names starts with upper case"
        )]
        public void Name_Starts_With_Capital_Letter() {
            var deserializeObject = GetNames;
            var startsWithUpperCase = deserializeObject
                .Select(name => name.Data)
                .All(s => char.IsUpper(s.First()));
            Assert.IsTrue(startsWithUpperCase);
        }

        [Test(
            Description = "Verify that no username contains number."
        )]
        public void User_Name_Contains_No_Numbers() {
            var containsNumber = Resources.usernames
                .Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                .Any(s => s.Any(char.IsNumber));
            Assert.IsFalse(containsNumber);
        }

        [Test(
            Description = "Verify that no username contains punctuation."
        )]
        public void User_Name_Contains_No_Punctuations() {
            var containsPunctuations =
                Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                    .All(s => s.Any(char.IsPunctuation));
            Assert.IsFalse(containsPunctuations);
        }

        [Test(
            Description = "Verify that no username contains separator."
        )]
        public void User_Name_Contains_No_Separator() {
            var containsSeparator =
                Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                    .Any(s => s.Any(char.IsSeparator));
            Assert.IsFalse(containsSeparator);
        }

        [Test(
            Description = "Verify that no username contains symbol."
        )]
        public void User_Name_Contains_No_Symbols() {
            var containsSymbols = Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                .Any(s => s.Any(char.IsSymbol));
            Assert.IsFalse(containsSymbols);
        }

        [Test(
            Description = "Verify that no username contains white space."
        )]
        public void User_Name_Contains_No_White_Space() {
            var containsWhitespace =
                Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)
                    .Any(s => s.Any(char.IsWhiteSpace));
            Assert.IsFalse(containsWhitespace);
        }
    }
}