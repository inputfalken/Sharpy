using System;
using System.Net.Http;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations {
    public class MovieDbRandomizerTests {
        private static readonly string ApiKey ;

        static MovieDbRandomizerTests() {
            const string theMovieDbApiKey = "THE_MOVIE_DB_API_KEY";
            var variable = Environment.GetEnvironmentVariable(theMovieDbApiKey, EnvironmentVariableTarget.Machine);
            ApiKey = !string.IsNullOrEmpty(variable)
                ? variable
                : throw new ArgumentException(
                    $"Could not obtain environmental variable with the name '{theMovieDbApiKey}'");
        }

        [Test]
        public void Empty_ApiKey() {
            var movieDbRandomizer = new MovieDbRandomizer(apiKey: "", random: new Random());
            Assert.ThrowsAsync<ArgumentException>(() => movieDbRandomizer.RandomMovies(),
                "You need to supply a valid apikey for 'www.themoviedb.org'.");
        }

        [Test]
        public void Random_String_Api_Key() {
            var movieDbRandomizer = new MovieDbRandomizer(apiKey: "foobar", random: new Random());
            Assert.ThrowsAsync<HttpRequestException>(() => movieDbRandomizer.RandomMovies(),
                "You need to supply a valid apikey for 'www.themoviedb.org'.");
        }

        [Test]
        public void Valid_Api_Key() {
            var movieDbRandomizer = new MovieDbRandomizer(ApiKey, new Random());
            Assert.DoesNotThrowAsync(() => movieDbRandomizer.RandomMovies());
        }
    }
}