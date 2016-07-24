using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.Name;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGeneratorTests.Types.Name {
    [TestClass]
    public class NameFactoryTests {
        #region Exception Handling

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LastNameCollection_ExceptionHandling() {
            const string countryQuery = "foobar";
            NameFactory.LastNameCollection(countryQuery);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FirstNameCollection_ExceptionHandling() {
            const string stringQuery = "foobar";
            NameFactory.FirstNameCollection(stringQuery);
        }

        #endregion

        #region Countries

        #region Sweden

        [TestMethod]
        public void FirstNameCollection_Args_Sweden() {
            const string stringQuery = "Sweden";
            var result = NameFactory.FirstNameCollection(stringQuery);

            #region Expected 

            IEnumerable<string> expected = new List<string> {
                "Julia",
                "Emma",
                "Wilma",
                "Hanna",
                "Elin",
                "Linnéa",
                "Amanda",
                "Ida",
                "Matilda",
                "Moa",
                "Maja",
                "Sara",
                "Ebba",
                "Felicia",
                "Emilia",
                "Klara",
                "Josefine",
                "Johanna",
                "Emelie",
                "Linn",
                "Sofia",
                "Frida",
                "Anna",
                "Ellen",
                "Alice",
                "Alva",
                "Isabelle",
                "Olivia",
                "Rebecca",
                "Lisa",
                "Lovisa",
                "Nathalie",
                "Jennifer",
                "Tilda",
                "Kajsa",
                "Fanny",
                "Filippa",
                "Sandra",
                "Alexandra",
                "Saga",
                "Lina",
                "Tilde",
                "Evelina",
                "Agnes",
                "Ella",
                "Victoria",
                "Malin",
                "Elsa",
                "Nora",
                "Isabella",
                "Sanna",
                "Louise",
                "Alma",
                "Emmy",
                "Jenny",
                "Madeleine",
                "Cornelia",
                "Sofie",
                "Nellie",
                "Mikaela",
                "Alicia",
                "Maria",
                "Erika",
                "Tova",
                "Ronja",
                "My",
                "Jasmine",
                "Ellinor",
                "Elvira",
                "Jessica",
                "Stina",
                "Jonna",
                "Caroline",
                "Tove",
                "Nicole",
                "Thea",
                "Elina",
                "Cecilia",
                "Vendela",
                "Annie",
                "Astrid",
                "Gabriella",
                "Molly",
                "Andrea",
                "Carolina",
                "Selma",
                "Linda",
                "Michelle",
                "Tuva",
                "Karin",
                "Cassandra",
                "Therese",
                "Melissa",
                "Daniella",
                "Hilda",
                "Miranda",
                "Vanessa",
                "Angelica",
                "Beatrice",
                "Vera",
                "Filip",
                "Oscar",
                "William",
                "Viktor",
                "Simon",
                "Anton",
                "Erik",
                "Alexander",
                "Emil",
                "Lucas",
                "Jonathan",
                "Linus",
                "Adam",
                "Marcus",
                "Jacob",
                "Albin",
                "Gustav",
                "Isak",
                "Sebastian",
                "David",
                "Daniel",
                "Hugo",
                "Rasmus",
                "Carl",
                "Elias",
                "Samuel",
                "Hampus",
                "Kevin",
                "Oliver",
                "Axel",
                "Johan",
                "Jesper",
                "Ludvig",
                "Felix",
                "Max",
                "Robin",
                "Joel",
                "Mattias",
                "Martin",
                "Andreas",
                "Pontus",
                "Christoffer",
                "Fredrik",
                "Gabriel",
                "Edvin",
                "Tobias",
                "Casper",
                "Dennis",
                "Tim",
                "Johannes",
                "Joakim",
                "Arvid",
                "Benjamin",
                "Niklas",
                "Nils",
                "Noah",
                "Elliot",
                "Hannes",
                "Alex",
                "Fabian",
                "Olle",
                "Henrik",
                "Christian",
                "Leo",
                "John",
                "Mikael",
                "Jonas",
                "Mohamed",
                "Rickard",
                "Josef",
                "Adrian",
                "Liam",
                "Alfred",
                "André",
                "Theodor",
                "Melker",
                "Wilhelm",
                "Patrik",
                "Kalle",
                "Måns",
                "August",
                "Theo",
                "Kim",
                "Love",
                "Melvin",
                "Petter",
                "Robert",
                "Vincent",
                "Ahmed",
                "Douglas",
                "Ali",
                "Emanuel",
                "Herman",
                "Albert",
                "Eddie",
                "Leon",
                "Julius",
                "Aron",
                "Thomas",
                "Jack",
                "Tom"
            };

            #endregion

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void FirstNameCollection_Args_SwedenFemale() {
            const string stringQuery = "Sweden";
            var result = NameFactory.FirstNameCollection(stringQuery, Gender.Female);

            #region Expected 

            IEnumerable<string> expected = new List<string> {
                "Julia",
                "Emma",
                "Wilma",
                "Hanna",
                "Elin",
                "Linnéa",
                "Amanda",
                "Ida",
                "Matilda",
                "Moa",
                "Maja",
                "Sara",
                "Ebba",
                "Felicia",
                "Emilia",
                "Klara",
                "Josefine",
                "Johanna",
                "Emelie",
                "Linn",
                "Sofia",
                "Frida",
                "Anna",
                "Ellen",
                "Alice",
                "Alva",
                "Isabelle",
                "Olivia",
                "Rebecca",
                "Lisa",
                "Lovisa",
                "Nathalie",
                "Jennifer",
                "Tilda",
                "Kajsa",
                "Fanny",
                "Filippa",
                "Sandra",
                "Alexandra",
                "Saga",
                "Lina",
                "Tilde",
                "Evelina",
                "Agnes",
                "Ella",
                "Victoria",
                "Malin",
                "Elsa",
                "Nora",
                "Isabella",
                "Sanna",
                "Louise",
                "Alma",
                "Emmy",
                "Jenny",
                "Madeleine",
                "Cornelia",
                "Sofie",
                "Nellie",
                "Mikaela",
                "Alicia",
                "Maria",
                "Erika",
                "Tova",
                "Ronja",
                "My",
                "Jasmine",
                "Ellinor",
                "Elvira",
                "Jessica",
                "Stina",
                "Jonna",
                "Caroline",
                "Tove",
                "Nicole",
                "Thea",
                "Elina",
                "Cecilia",
                "Vendela",
                "Annie",
                "Astrid",
                "Gabriella",
                "Molly",
                "Andrea",
                "Carolina",
                "Selma",
                "Linda",
                "Michelle",
                "Tuva",
                "Karin",
                "Cassandra",
                "Therese",
                "Melissa",
                "Daniella",
                "Hilda",
                "Miranda",
                "Vanessa",
                "Angelica",
                "Beatrice",
                "Vera"
            };

            #endregion

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void FirstNameCollection_Args_SwedenMale() {
            const string stringQuery = "Sweden";
            var result = NameFactory.FirstNameCollection(stringQuery, Gender.Male);

            #region Expected 

            IEnumerable<string> expected = new List<string> {
                "Filip",
                "Oscar",
                "William",
                "Viktor",
                "Simon",
                "Anton",
                "Erik",
                "Alexander",
                "Emil",
                "Lucas",
                "Jonathan",
                "Linus",
                "Adam",
                "Marcus",
                "Jacob",
                "Albin",
                "Gustav",
                "Isak",
                "Sebastian",
                "David",
                "Daniel",
                "Hugo",
                "Rasmus",
                "Carl",
                "Elias",
                "Samuel",
                "Hampus",
                "Kevin",
                "Oliver",
                "Axel",
                "Johan",
                "Jesper",
                "Ludvig",
                "Felix",
                "Max",
                "Robin",
                "Joel",
                "Mattias",
                "Martin",
                "Andreas",
                "Pontus",
                "Christoffer",
                "Fredrik",
                "Gabriel",
                "Edvin",
                "Tobias",
                "Casper",
                "Dennis",
                "Tim",
                "Johannes",
                "Joakim",
                "Arvid",
                "Benjamin",
                "Niklas",
                "Nils",
                "Noah",
                "Elliot",
                "Hannes",
                "Alex",
                "Fabian",
                "Olle",
                "Henrik",
                "Christian",
                "Leo",
                "John",
                "Mikael",
                "Jonas",
                "Mohamed",
                "Rickard",
                "Josef",
                "Adrian",
                "Liam",
                "Alfred",
                "André",
                "Theodor",
                "Melker",
                "Wilhelm",
                "Patrik",
                "Kalle",
                "Måns",
                "August",
                "Theo",
                "Kim",
                "Love",
                "Melvin",
                "Petter",
                "Robert",
                "Vincent",
                "Ahmed",
                "Douglas",
                "Ali",
                "Emanuel",
                "Herman",
                "Albert",
                "Eddie",
                "Leon",
                "Julius",
                "Aron",
                "Thomas",
                "Jack",
                "Tom"
            };

            #endregion

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void LastNameCollection_Args_Sweden() {
            const string stringQuery = "Sweden";
            var result = NameFactory.LastNameCollection(stringQuery);

            #region Expected 

            IEnumerable<string> expected = new List<string> {
                "Andersson",
                "Johansson",
                "Karlsson",
                "Nilsson",
                "Eriksson",
                "Larsson",
                "Olsson",
                "Persson",
                "Svensson",
                "Gustafsson",
                "Pettersson",
                "Jonsson",
                "Jansson",
                "Hansson",
                "Bengtsson",
                "Jönsson",
                "Lindberg",
                "Jakobsson",
                "Magnusson",
                "Olofsson"
            };

            #endregion

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        #endregion

        #region Norway

        [TestMethod]
        public void FirstNameCollection_Args_Norway() {
            var result = NameFactory.FirstNameCollection("Norway");

            #region Expected

            IEnumerable<string> expected = new List<string> {
                "Ida",
                "Emilie",
                "Julie",
                "Thea",
                "Sara",
                "Ingrid",
                "Malin",
                "Maria",
                "Marte",
                "Nora",
                "Markus",
                "Kristian",
                "Martin",
                "Sander",
                "Kristoffer",
                "Mathias",
                "Andreas",
                "Jonas",
                "Henrik",
                "Daniel"
            };

            #endregion

            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public void FirstNameCollection_Args_NorwayFemale() {
            var result = NameFactory.FirstNameCollection("Norway", Gender.Female);

            #region Expected

            IEnumerable<string> expected = new List<string> {
                "Ida",
                "Emilie",
                "Julie",
                "Thea",
                "Sara",
                "Ingrid",
                "Malin",
                "Maria",
                "Marte",
                "Nora",
            };

            #endregion

            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public void FirstNameCollection_Args_NorwayMale() {
            var result = NameFactory.FirstNameCollection("Norway", Gender.Male);

            #region Expected

            IEnumerable<string> expected = new List<string> {
                "Markus",
                "Kristian",
                "Martin",
                "Sander",
                "Kristoffer",
                "Mathias",
                "Andreas",
                "Jonas",
                "Henrik",
                "Daniel"
            };

            #endregion

            Assert.IsTrue(result.SequenceEqual(expected));
        }

        public void LastNameCollection_Args_Norway() {
            var result = NameFactory.LastNameCollection("Norway");

            #region Expected

            IEnumerable<string> expected = new List<string> {
                "Hansen",
                "Johansen",
                "Olsen",
                "Larsen",
                "Andersen",
                "Pedersen",
                "Nilsen",
                "Kristiansen",
                "Jensen",
                "Karlsen",
                "Johnsen",
                "Pettersen",
                "Eriksen",
                "Berg",
                "Haugen",
                "Hagen",
                "Johannessen",
                "Andreassen",
                "Jacobsen",
                "Dahl",
                "Jørgensen",
                "Halvorsen",
                "Henriksen",
                "Lund"
            };

            #endregion

            Assert.IsTrue(result.SequenceEqual(expected));
        }

        #endregion

        #endregion
    }
}