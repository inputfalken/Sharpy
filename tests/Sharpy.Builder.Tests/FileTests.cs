using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Implementation.DataObjects;

namespace Sharpy.Builder.Tests {
    [TestFixture]
    internal class FileTests {
        private static readonly IEnumerable<Name> GetNames = Data.GetNames;
        private static readonly string[] GetUserNames = Data.GetUserNames;

        [Test(
            Description = "Verify that no name contains number"
        )]
        public void Name_Contains_No_Numbers() {
            var deserializeObject = GetNames;
            var containsNumber = deserializeObject.Select(name => name.Data).All(s => s.Any(char.IsNumber));
            Assert.IsFalse(containsNumber);
        }

        [Test(
            Description = "Verify that no name contains punctuation"
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
            Description = "All characters are numeric (space excluded)."
        )]
        public void Postal_Code_Contains_Numbers_Only() => Assert.IsTrue(Data.GetPostalcodes
            .Select(code => code.Postalcode.Remove(3, 1))
            .All(s => s.All(char.IsNumber)));

        [Test(
            Description = "All postal codes contains 6 characters (space included)."
        )]
        public void Postal_Code_Got_Same_Length() =>
            Assert.IsTrue(Data.GetPostalcodes.All(code => code.Postalcode.Length == 6));

        [Test(
            Description = "Third index is a space."
        )]
        public void Postal_Codes_Use_Space_On_Third_Index() =>
            Assert.IsTrue(Data.GetPostalcodes.All(code => code.Postalcode[3] == ' '));

        [Test(
            Description = "Verify that no user name contains number."
        )]
        public void User_Name_Contains_No_Numbers() {
            var containsNumber =
                GetUserNames
                    .Any(s => s.Any(char.IsNumber));
            Assert.IsFalse(containsNumber);
        }

        [Test(
            Description = "Verify that no user name contains punctuation."
        )]
        public void User_Name_Contains_No_Punctuations() {
            var containsPunctuations =
                GetUserNames.All(s => s.Any(char.IsPunctuation));
            Assert.IsFalse(containsPunctuations);
        }

        [Test(
            Description = "Verify that no user name contains separator."
        )]
        public void User_Name_Contains_No_Separator() {
            var containsSeparator =
                GetUserNames
                    .Any(s => s.Any(char.IsSeparator));
            Assert.IsFalse(containsSeparator);
        }

        [Test(
            Description = "Verify that no user name contains symbol."
        )]
        public void User_Name_Contains_No_Symbols() {
            var containsSymbols = GetUserNames
                .Any(s => s.Any(char.IsSymbol));
            Assert.IsFalse(containsSymbols);
        }

        [Test(
            Description = "Verify that no user name contains white space."
        )]
        public void User_Name_Contains_No_White_Space() {
            var containsWhitespace =
                GetUserNames
                    .Any(s => s.Any(char.IsWhiteSpace));
            Assert.IsFalse(containsWhitespace);
        }
    }
}