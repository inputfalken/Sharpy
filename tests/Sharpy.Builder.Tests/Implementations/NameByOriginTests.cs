using System;
using System.Linq;
using NUnit.Framework;
using Sharpy.Builder.Enums;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations
{
    [TestFixture]
    public class NameByOriginTests
    {
        [Test]
        public void All_Origins_Are_Supported()
        {
            var values = Enum.GetValues(typeof(Origin));
            foreach (var value in values)
                Assert.DoesNotThrow(() => { new NameByOrigin((Origin) value).FirstName(); });
        }

        [Test]
        public void Female_First_Name_Not_Null_Or_White_Space()
        {
            var builder = new NameByOrigin();
            var names = Enumerable.Range(0, Assertion.Amount / 1000).Select(i => builder.FirstName(Gender.Female)).ToList();
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void First_Name_Not_Null_Or_White_Space()
        {
            var builder = new NameByOrigin();
            var names = Enumerable.Range(0, Assertion.Amount / 1000).Select(i => builder.FirstName()).ToList();
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void Last_Name_Not_Null_Or_White_Space()
        {
            var builder = new NameByOrigin();
            var names = Enumerable.Range(0, Assertion.Amount / 1000).Select(i => builder.LastName()).ToList();
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void Male_First_Name_Not_Null_Or_White_Space()
        {
            var builder = new NameByOrigin();
            var names = Enumerable.Range(0, Assertion.Amount / 1000).Select(i => builder.FirstName(Gender.Male)).ToList();
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));
        }

        [Test]
        public void Origin_Restricted_Constructor_With_One_Country()
        {
            var builder = new NameByOrigin(Origin.Sweden);
            var names = Enumerable.Range(0, Assertion.Amount / 1000).Select(i => builder.FirstName()).ToList();
            var allFinishNames = NameByOrigin.GetCollection(Origin.Finland);
            var allSwedishNames = NameByOrigin.GetCollection(Origin.Sweden);
            Assert.IsTrue(names.All(s => allSwedishNames.Contains(s)));
            Assert.IsFalse(names.All(s => allFinishNames.Contains(s)));
        }

        [Test]
        public void Origin_Restricted_Constructor_With_One_Country_And_One_Region()
        {
            var builder = new Builder(
                new Configurement
                {
                    NameProvider = new NameByOrigin(Origin.Sweden, Origin.NorthAmerica)
                }
            );
            var names = Enumerable.Range(0, Assertion.Amount / 1000).Select(i => builder.FirstName()).ToList();
            var allSwedishAndNorthAmericanNames = NameByOrigin.GetCollection(Origin.Sweden, Origin.NorthAmerica);
            var allDanishNames = NameByOrigin.GetCollection(Origin.Denmark);
            Assert.IsTrue(names.All(s => allSwedishAndNorthAmericanNames.Contains(s)));
            Assert.IsFalse(names.All(s => allDanishNames.Contains(s)));
        }

        [Test]
        public void Origin_Restricted_Constructor_With_Two_Countries()
        {
            var builder = new Builder(
                new Configurement
                {
                    NameProvider = new NameByOrigin(Origin.Sweden, Origin.Denmark)
                }
            );
            var names = Enumerable.Range(0, Assertion.Amount / 1000).Select(i => builder.FirstName()).ToList();
            var allFinishNames = NameByOrigin.GetCollection(Origin.Finland);
            var allSvDkNames = NameByOrigin.GetCollection(Origin.Sweden, Origin.Denmark);
            Assert.IsTrue(names.All(s => allSvDkNames.Contains(s)));
            Assert.IsFalse(names.All(s => allFinishNames.Contains(s)));
        }

        [Test]
        public void Origin_Restricted_Constructor_With_Two_Regions()
        {
            var builder = new Builder(
                new Configurement
                {
                    NameProvider = new NameByOrigin(Origin.Europe, Origin.NorthAmerica)
                }
            );
            var names = Enumerable.Range(0, Assertion.Amount / 1000).Select(i => builder.FirstName()).ToList();
            var allEuropeanAndNorthAmericanNames = NameByOrigin.GetCollection(Origin.Europe, Origin.NorthAmerica);
            var allBrazilianNames = NameByOrigin.GetCollection(Origin.Brazil);
            Assert.IsTrue(names.All(s => allEuropeanAndNorthAmericanNames.Contains(s)));
            Assert.IsFalse(names.All(s => allBrazilianNames.Contains(s)));
        }
    }
}