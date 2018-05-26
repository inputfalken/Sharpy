using System;
using System.Net.Http;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations {
    public class MovieDbRandomizerTests {
        private static readonly string ApiKey;

        static MovieDbRandomizerTests() {
            const string environmentVariableName = "THE_MOVIE_DB_API_KEY";
            var variable = Environment.GetEnvironmentVariable(environmentVariableName);
            ApiKey = !string.IsNullOrEmpty(variable)
                ? variable
                : throw new ArgumentException(
                    $"Could not obtain environmental variable with the name '{environmentVariableName}'");
        }

        [Test]
        public void Empty_ApiKey() => Assert.Throws<ArgumentNullException>(() => new MovieDbRandomizer(apiKey: "", random: new Random()));

        [Test]
        public void Null_ApiKey() => Assert.Throws<ArgumentNullException>(() => new MovieDbRandomizer(apiKey: "", random: new Random()));

        [Test]
        public void Null_Random() => Assert.Throws<ArgumentNullException>(() => new MovieDbRandomizer(ApiKey, null));

        [Test]
        public void Random_String_Api_Key() {
            var movieDbRandomizer = new MovieDbRandomizer(apiKey: "foobar", random: new Random());
            Assert.ThrowsAsync<HttpRequestException>(() => movieDbRandomizer.RandomMovies(),
                "You need to supply a valid apikey for 'www.themoviedb.org'.");
        }

        [Test]
        public void Valid_Api_Key() => Assert.DoesNotThrow(() => new MovieDbRandomizer(ApiKey, new Random()));
    }
}