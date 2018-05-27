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

        private static readonly (MovieLanguage, IReadOnlyCollection<Genre>) Identity = (MovieLanguage.Any,
            new[] {Genre.Any});

        private readonly Dictionary<(MovieLanguage, IReadOnlyCollection<Genre>), IEnumerator<int>> _argumentHistory =
            new Dictionary<(MovieLanguage, IReadOnlyCollection<Genre>), IEnumerator<int>>();

        private static string BuildGenreString(IReadOnlyList<Genre> genres) =>
            genres.Any() ? $"&with_genres=[{string.Join(",", genres.Cast<int>())}]" : string.Empty;

        private static string BuildPageString(int page) => $"&page={page}";
        private static string BuildApiKeyString(string key) => $"?api_key={key}";

        private static string BuildLanguageString(MovieLanguage language) =>
            $"&with_original_language={MapLanguage(language)}";

        private static IEnumerator<int> RandomizeList(Random rnd, int count) {
            return Enumerable.Range(1, count).OrderBy(_ => rnd.Next()).ToList().GetEnumerator();
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

        private readonly Random _random;

        // Docs https://developers.themoviedb.org/3/getting-started/request-rate-limiting
        private async Task<IReadOnlyList<Movie>> RequestMovies(string uri,
            (MovieLanguage, IReadOnlyCollection<Genre>) arguments) {
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
                    return await DeserializeMovies(response.Content.ReadAsStringAsync(), arguments) as
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
            (MovieLanguage, IReadOnlyCollection<Genre>) arguments) {
            var deserializeAnonymousType = JsonConvert
                .DeserializeAnonymousType(
                    await json,
                    new {results = new List<Movie>(), total_pages = 0}
                );
            lock (_argumentHistory)
                _argumentHistory[arguments] =
                    RandomizeList(
                        _random,
                        deserializeAnonymousType.total_pages >= MaxPage ? MaxPage : deserializeAnonymousType.total_pages
                    );

            return deserializeAnonymousType.results;
        }

        public Task<IReadOnlyList<Movie>> FetchMovies() {
            lock (_argumentHistory) {
                // Issues with reference types, only works with Identity
                if (_argumentHistory.TryGetValue(Identity, out var pageEnumerator)) {
                    if (pageEnumerator.MoveNext())
                        return RequestMovies(
                            $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}&language=en-US&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(pageEnumerator.Current)}",
                            Identity);

                    throw new Exception("Cannot obtain more movies.");
                }
            }

            return RequestMovies(
                $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}&language=en-US&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(1)}",
                Identity);
        }

        public Task<IReadOnlyList<Movie>> FetchMovies(params Genre[] genres) {
            lock (_argumentHistory) {
                if (_argumentHistory.TryGetValue(Identity, out var pageEnumerator)) {
                    if (pageEnumerator.MoveNext())
                        return RequestMovies(
                            $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}&language=en-US&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(pageEnumerator.Current)}{BuildGenreString(genres)}",
                            (MovieLanguage.Any, genres));

                    throw new Exception("Cannot obtain more movies.");
                }
            }

            return RequestMovies(
                $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}&language=en-US&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(1)}{BuildGenreString(genres)}",
                (MovieLanguage.Any, genres));
        }

        public Task<IReadOnlyList<Movie>> FetchMovies(MovieLanguage language) {
            var argument = (language, new[] {Genre.Any});
            lock (_argumentHistory) {
                if (_argumentHistory.TryGetValue(argument, out var pageEnumerator)) {
                    if (pageEnumerator.MoveNext())
                        return RequestMovies(
                            $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}{BuildLanguageString(language)}&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(pageEnumerator.Current)}",
                            argument);

                    throw new Exception("Cannot obtain more movies.");
                }
            }

            return RequestMovies(
                $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}{BuildLanguageString(language)}&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(1)}",
                argument);
        }

        public Task<IReadOnlyList<Movie>> FetchMovies(MovieLanguage language, params Genre[] genres) {
            lock (_argumentHistory) {
                if (_argumentHistory.TryGetValue(Identity, out var pageEnumerator)) {
                    if (pageEnumerator.MoveNext())
                        return RequestMovies(
                            $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}{BuildLanguageString(language)}&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(pageEnumerator.Current)}{BuildGenreString(genres)}",
                            (language, genres));

                    throw new Exception("Cannot obtain more movies.");
                }
            }

            return RequestMovies(
                $"https://api.themoviedb.org/3/discover/movie{BuildApiKeyString(_apiKey)}{BuildLanguageString(language)}&sort_by=original_title.asc&include_adult=false&include_video=false{BuildPageString(1)}{BuildGenreString(genres)}",
                (language, genres));
        }
    }
}