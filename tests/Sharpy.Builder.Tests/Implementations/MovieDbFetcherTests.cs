using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Sharpy.Builder.Implementation;

namespace Sharpy.Builder.Tests.Implementations {
    public class MovieDbFetcherTests {
        private static readonly string ApiKey;

        static MovieDbFetcherTests() {
            const string environmentVariableName = "THE_MOVIE_DB_API_KEY";
            var variable = Environment.GetEnvironmentVariable(environmentVariableName);
            ApiKey = !string.IsNullOrEmpty(variable)
                ? variable
                : throw new ArgumentException(
                    $"Could not obtain environmental variable with the name '{environmentVariableName}'");
        }

        [Test]
        public void Empty_ApiKey_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new MovieDbFetcher(apiKey: "", random: new Random()));

        [Test]
        public void Null_ApiKey_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new MovieDbFetcher(apiKey: "", random: new Random()));

        [Test]
        public void Null_Random_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new MovieDbFetcher(ApiKey, null));

        [Test]
        public void Random_String_Api_Key_FetchMovies_Throws() {
            var movieDbRandomizer = new MovieDbFetcher(apiKey: "foobar", random: new Random());
            Assert.ThrowsAsync<HttpRequestException>(async () => await movieDbRandomizer.FetchMovies(),
                message: "You need to supply a valid apikey for 'www.themoviedb.org'.");
        }

        [Test]
        public void Valid_Api_Key_Does_Not_Throw() =>
            Assert.DoesNotThrow(() => new MovieDbFetcher(ApiKey, new Random()));

        [Test]
        public void Valid_Api_Key_FetchMovies_Does_Not_Throw() =>
            Assert.DoesNotThrowAsync(() => new MovieDbFetcher(ApiKey, new Random()).FetchMovies());

        [Test]
        public async Task Movies_Fetched_Got_Unique_Titles() {
            var movieDbRandomizer = new MovieDbFetcher(ApiKey, new Random());
            var movies1 = await movieDbRandomizer.FetchMovies();
            var movies2 = await movieDbRandomizer.FetchMovies();

            Assert.True(
                movies1.SelectMany(_ => movies2, (x, y) => (result1: x, result2: y))
                    .Any(x => x.result1.Title != x.result2.Title), "Movie title should not match.");
        }

        [Test(Description =
            "Request Rate Limiting: https://developers.themoviedb.org/3/getting-started/request-rate-limiting")]
        public void Make_40_Requests() {
            var movieDbRandomizer = new MovieDbFetcher(ApiKey, new Random());
            Assert.DoesNotThrowAsync(async () => {
                for (var i = 0; i < 40; i++) {
                    await movieDbRandomizer.FetchMovies();
                }
            });
        }

        [Test]
        public async Task Make_40_Requestx() {
            var movieDbFetcher = new MovieDbFetcher(ApiKey, new Random());
            var movieDbRandomizer = await movieDbFetcher.FetchMovies();
            var movieDbRandomizer2 = await movieDbFetcher.FetchMovies();
            Console.WriteLine(movieDbRandomizer);
        }

        [Test(Description =
            "Request Rate Limiting: https://developers.themoviedb.org/3/getting-started/request-rate-limiting")]
        public void Make_80_Requests() {
            var movieDbRandomizer = new MovieDbFetcher(ApiKey, new Random());
            Assert.DoesNotThrowAsync(async () => {
                for (var i = 0; i < 80; i++) {
                    await movieDbRandomizer.FetchMovies();
                }
            });
        }
    }
}