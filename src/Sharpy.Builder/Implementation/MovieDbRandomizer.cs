using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    public class MovieDbRandomizer : IMovieDbProvider {
        private readonly string _apiKey;
        private readonly Random _random;

        public MovieDbRandomizer(string apiKey, Random random) {
            _apiKey = apiKey;
            _random = random;
        }

        public async Task<IReadOnlyList<Movie>> RandomMovies() {
            if (string.IsNullOrEmpty(_apiKey))
                throw new ArgumentException("You need to supply a valid apikey for 'www.themoviedb.org'.");
            var httpClient = new HttpClient();
            var request = await httpClient.GetStringAsync(
                $"https://api.themoviedb.org/3/discover/movie?api_key={_apiKey}&language=en-US&sort_by=original_title.asc&include_adult=false&include_video=false&page={_random.Next(1, 1000)}");
            var deserialize = JsonConvert.DeserializeAnonymousType(request, new { results = new List<Movie>()});
            return deserialize.results;
        }
    }
}