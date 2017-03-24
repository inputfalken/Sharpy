using System;
using System.Linq;
using NUnit.Framework;
using Sharpy;
using Sharpy.Enums;
using Sharpy.Implementation;

namespace Tests.Integration {
    [TestFixture]
    public class NameProvider {
        private const int Count = 100;

        [Test]
        public void All_Origins_Are_Supported() {
            var values = Enum.GetValues(typeof(Origin));
            foreach (var value in values)
                Assert.DoesNotThrow(() => {
                    var configurement = new Configurement {
                        NameProvider = new NameByOrigin((Origin) value)
                    };
                    new Generator(configurement).Generate(g => g.FirstName());
                });
        }

        [Test]
        public void Female_First_Name_Not_Null_Or_White_Space() {
            var gen = new Generator();
            //Many
            var names = gen.GenerateSequence(g => g.FirstName(Gender.Female), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.FirstName(Gender.Female));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void First_Name_Not_Null_Or_White_Space() {
            var gen = new Generator();
            //Many
            var names = gen.GenerateSequence(g => g.FirstName(), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.FirstName());
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Last_Name_Not_Null_Or_White_Space() {
            var gen = new Generator();
            //Many
            var names = gen.GenerateSequence(g => g.LastName(), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.LastName());
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Male_First_Name_Not_Null_Or_White_Space() {
            var gen = new Generator();
            //Many
            var names = gen.GenerateSequence(g => g.FirstName(Gender.Male), Count).ToArray();
            Assert.IsFalse(names.All(string.IsNullOrEmpty));
            Assert.IsFalse(names.All(string.IsNullOrWhiteSpace));

            //Single
            var name = gen.Generate(g => g.FirstName(Gender.Male));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(name));
        }

        [Test]
        public void Origin_Restricted_Constructor_With_One_Country() {
            var swedishNameGenerator = new Generator(new Configurement {NameProvider = new NameByOrigin(Origin.Sweden)});
            var swedishNames = swedishNameGenerator
                .GenerateSequence(g => g.FirstName(), Count)
                .ToArray();
            var allFinishNames = NameByOrigin.GetCollection(Origin.Finland);
            var allSwedishNames = NameByOrigin.GetCollection(Origin.Sweden);
            Assert.IsTrue(swedishNames.All(s => allSwedishNames.Contains(s)));
            Assert.IsFalse(swedishNames.All(s => allFinishNames.Contains(s)));
        }

        [Test]
        public void Origin_Restricted_Constructor_With_One_Country_And_One_Region() {
            var svDkGenerator = new Generator(
                new Configurement {
                    NameProvider = new NameByOrigin(Origin.Sweden, Origin.NorthAmerica)
                }
            );
            var swedishNorthAmericanNames = svDkGenerator
                .GenerateSequence(g => g.FirstName(), Count)
                .ToArray();
            var allSwedishAndNorthAmericanNames = NameByOrigin.GetCollection(Origin.Sweden, Origin.NorthAmerica);
            var allDanishNames = NameByOrigin.GetCollection(Origin.Denmark);
            Assert.IsTrue(swedishNorthAmericanNames.All(s => allSwedishAndNorthAmericanNames.Contains(s)));
            Assert.IsFalse(swedishNorthAmericanNames.All(s => allDanishNames.Contains(s)));
        }

        [Test]
        public void Origin_Restricted_Constructor_With_Two_Countries() {
            var svDkGenerator = new Generator(
                new Configurement {
                    NameProvider = new NameByOrigin(Origin.Sweden, Origin.Denmark)
                }
            );
            var svDkNames = svDkGenerator
                .GenerateSequence(g => g.FirstName(), Count)
                .ToArray();
            var allFinishNames = NameByOrigin.GetCollection(Origin.Finland);
            var allSvDkNames = NameByOrigin.GetCollection(Origin.Sweden, Origin.Denmark);
            Assert.IsTrue(svDkNames.All(s => allSvDkNames.Contains(s)));
            Assert.IsFalse(svDkNames.All(s => allFinishNames.Contains(s)));
        }

        [Test]
        public void Origin_Restricted_Constructor_With_Two_Regions() {
            var svDkGenerator = new Generator(
                new Configurement {
                    NameProvider = new NameByOrigin(Origin.Europe, Origin.NorthAmerica)
                }
            );
            var europeanAndNorthAmericanNames = svDkGenerator
                .GenerateSequence(g => g.FirstName(), Count)
                .ToArray();
            var allEuropeanAndNorthAmericanNames = NameByOrigin.GetCollection(Origin.Europe, Origin.NorthAmerica);
            var allBrazilianNames = NameByOrigin.GetCollection(Origin.Brazil);
            Assert.IsTrue(europeanAndNorthAmericanNames.All(s => allEuropeanAndNorthAmericanNames.Contains(s)));
            Assert.IsFalse(europeanAndNorthAmericanNames.All(s => allBrazilianNames.Contains(s)));
        }

        [Test]
        public void User_Name_Not_Null_Or_White_Space() {
            var gen = new Generator();
            //Many
            var userNames = gen.GenerateSequence(g => g.UserName(), Count).ToArray();
            Assert.IsFalse(userNames.All(string.IsNullOrEmpty));
            Assert.IsFalse(userNames.All(string.IsNullOrWhiteSpace));

            //Single
            var userName = gen.Generate(g => g.UserName());
            Assert.IsFalse(string.IsNullOrEmpty(userName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(userName));
        }
    }
}