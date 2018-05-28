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
        private const int MaxPage = 1000;

        private static readonly HttpClient Client = new HttpClient();
        private const string RemainingRequests = "X-RateLimit-Remaining";

        private readonly Dictionary<MovieDbArgument, IEnumerator<int>> _cache =
            new Dictionary<MovieDbArgument, IEnumerator<int>>(MovieDbArgument.Comparer);

        private static string BuildGenreString(IReadOnlyList<Genre> genres) =>
            genres.Any() ? $"&with_genres={string.Join(",", genres.Cast<int>())}" : string.Empty;

        private static string BuildPageString(int page) => $"&page={page}";
        private static string BuildApiKeyString(string key) => $"?api_key={key}";

        private static string BuildLanguageString(MovieLanguage language) =>
            $"&with_original_language={MapLanguage(language)}";

        private static IEnumerator<int> CreateEnumerator(Random rnd, int count) {
            var list = Enumerable.Range(2, count - 1).OrderBy(_ => rnd.Next()).ToList();
            return list.GetEnumerator();
        }

        private class MovieDbArgument {
            internal static IEqualityComparer<MovieDbArgument> Comparer { get; } = new MovieDbComparer();

            public MovieDbArgument(MovieLanguage language, params Genre[] genres) {
                _language = language;
                _genres = genres.Distinct().ToList();
            }

            private readonly MovieLanguage _language;
            private readonly IReadOnlyCollection<Genre> _genres;

            private class MovieDbComparer : IEqualityComparer<MovieDbArgument> {
                public bool Equals(MovieDbArgument x, MovieDbArgument y) =>
                    y._language == x._language && x._genres.SequenceEqual(y._genres);

                public int GetHashCode(MovieDbArgument obj) {
                    unchecked {
                        // TODO make it ignore order of elements, sorting does not solve this...
                        return obj._genres
                            .Aggregate(17,
                                (x, y) => x * 31 + y.GetHashCode(),
                                i => i * 31 + obj._language.GetHashCode()
                            );
                    }
                }
            }
        }

        private static string MapLanguage(MovieLanguage language) {
            switch (language) {
                case MovieLanguage.English:
                    return "en";
                case MovieLanguage.Spanish:
                    return "es";
                case MovieLanguage.Swedish:
                    return "sv";
                case MovieLanguage.Italian:
                    return "it";
                case MovieLanguage.German:
                    return "de";
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
        }

        private static int GetPage(IReadOnlyDictionary<MovieDbArgument, IEnumerator<int>> dictionary,
            MovieDbArgument argument) => dictionary.TryGetValue(argument, out var pageEnumerator)
            ? (pageEnumerator.MoveNext()
                ? pageEnumerator.Current
                : throw new Exception("Cannot obtain more movies."))
            : 1;

        private readonly Random _random;

        // Docs https://developers.themoviedb.org/3/getting-started/request-rate-limiting
        private async Task<IReadOnlyList<Movie>> RequestMovies(string uri,
            MovieDbArgument argument) {
            bool UnhandledStatusCode(HttpStatusCode statusCode) => new[] {
                HttpStatusCode.Unauthorized,
                HttpStatusCode.InternalServerError,
                HttpStatusCode.Forbidden,
                HttpStatusCode.BadGateway,
                HttpStatusCode.Conflict,
                HttpStatusCode.NotFound
            }.Any(x => x == statusCode);

            bool IsBelowRequestLimit(HttpResponseHeaders headers) {
                var remaining = headers
                    .Where(x => x.Key == RemainingRequests)
                    .SelectMany(x => x.Value)
                    .SingleOrDefault()
                    ?.Where(char.IsNumber)
                    .ToArray();

                return remaining?.Length > 0 && int.TryParse(new string(remaining), out var remainingInvokations) &&
                       remainingInvokations >= 0;
            }

            while (true) {
                var response = await Client.GetAsync(uri);
                if (UnhandledStatusCode(response.StatusCode))
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                if (IsBelowRequestLimit(response.Headers))
                    return await DeserializeMovies(response.Content.ReadAsStringAsync(), argument) as
                        IReadOnlyList<Movie>;
                if (response.Headers.RetryAfter.Delta.HasValue)
                    await Task.Delay(response.Headers.RetryAfter.Delta.Value.Milliseconds);
                else
                    throw new HttpRequestException("Could not determine Retry-After header.");
            }
        }

        public MovieDbFetcher(string apiKey, Random random) {
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _apiKey = string.IsNullOrEmpty(apiKey)
                ? throw new ArgumentNullException(nameof(apiKey))
                : apiKey;
        }

        private async Task<IList<Movie>> DeserializeMovies(Task<string> json,
            MovieDbArgument arguments) {
            var deserializeAnonymousType = JsonConvert
                .DeserializeAnonymousType(
                    await json,
                    new {results = new List<Movie>(), total_pages = 0}
                );

            if (deserializeAnonymousType.total_pages == 0)
                throw new ArgumentException("No movies could be found with the arguments supplied");

            lock (_cache)
                if (!_cache.ContainsKey(arguments))
                    _cache[arguments] =
                        CreateEnumerator(
                            _random,
                            deserializeAnonymousType.total_pages >= MaxPage
                                ? MaxPage
                                : deserializeAnonymousType.total_pages
                        );

            return deserializeAnonymousType.results;
        }

        public Task<IReadOnlyList<Movie>> FetchMovies() {
            var argument = new MovieDbArgument(MovieLanguage.Any, Genre.Any);
            lock (_cache)
                return RequestMovies(
                    $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}&language=en-US&sort_by=original_title.asc&include_adult=false&include_video=false{GetPage(_cache, argument)}",
                    argument);
        }

        public Task<IReadOnlyList<Movie>> FetchMovies(params Genre[] genres) {
            var argument = new MovieDbArgument(MovieLanguage.Any, genres);
            lock (_cache)
                return RequestMovies(
                    $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}&language=en-US&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(GetPage(_cache, argument))}{BuildGenreString(genres)}",
                    argument);
        }

        public Task<IReadOnlyList<Movie>> FetchMovies(MovieLanguage language) {
            var argument = new MovieDbArgument(language, Genre.Any);
            lock (_cache)
                return RequestMovies(
                    $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}{BuildLanguageString(language)}&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(GetPage(_cache, argument))}",
                    argument);
        }

        public Task<IReadOnlyList<Movie>> FetchMovies(MovieLanguage language, params Genre[] genres) {
            var argument = new MovieDbArgument(language, genres);
            lock (_cache)
                return RequestMovies(
                    $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}{BuildLanguageString(language)}&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(GetPage(_cache, argument))}{BuildGenreString(genres)}",
                    argument);
        }
    }
}