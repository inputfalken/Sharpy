using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    public class MovieDbFetcher : IMovieDbProvider {
        private readonly string _apiKey;
        private const int PagesCount = 1000;
        private IEnumerator<int> Pages { get; }
        private static readonly HttpClient Client = new HttpClient();
        private const string RemainingRequests = "X-RateLimit-Remaining";

        public MovieDbFetcher(string apiKey, Random random) {
            var rnd = random ?? throw new ArgumentNullException(nameof(random));
            _apiKey = string.IsNullOrEmpty(apiKey)
                ? throw new ArgumentNullException(nameof(apiKey))
                : apiKey;

            Pages = Enumerable
                .Range(1, PagesCount)
                .OrderBy(i => rnd.Next(i))
                .ToList()
                .GetEnumerator();
        }

        private static bool IsBelowRequestLimit(HttpResponseHeaders headers) {
            var remaining = headers
                .Where(x => x.Key == RemainingRequests)
                .SelectMany(x => x.Value)
                .SingleOrDefault()
                ?.Where(char.IsNumber)
                .ToArray();

            return remaining?.Length > 0
                   && int.TryParse(new string(remaining), out var remainingInvokations)
                   && remainingInvokations >= 0;
        }

        private static async Task<IList<Movie>> DeserializeMovies(Task<string> json) => JsonConvert
            .DeserializeAnonymousType(
                await json,
                new {results = new List<Movie>(), total_pages = PagesCount}
            ).results;

        // Docs https://developers.themoviedb.org/3/getting-started/request-rate-limiting
        public async Task<IReadOnlyList<Movie>> FetchMovies() {
            if (!Pages.MoveNext()) throw new Exception("Cannot obtain more movies.");
            var response =
                await Client.GetAsync(
                    $"https://api.themoviedb.org/3/discover/movie?api_key={_apiKey}&language=en-US&sort_by=original_title.asc&include_adult=false&include_video=false&page={Pages.Current}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new HttpRequestException(await response.Content.ReadAsStringAsync());

            if (IsBelowRequestLimit(response.Headers))
                return await DeserializeMovies(response.Content.ReadAsStringAsync()) as IReadOnlyList<Movie>;

            if (response.Headers.RetryAfter.Delta.HasValue)
                await Task.Delay(response.Headers.RetryAfter.Delta.Value.Milliseconds);
            else throw new HttpRequestException("Could not determine Retry-After header.");
            return await FetchMovies();
        }
    }
}