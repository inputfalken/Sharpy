using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sharpy.Builder.IProviders {
    public interface IMovieDbProvider {
        Task<IReadOnlyList<Movie>> FetchMovies();
        
    }

    public class Movie {
        private Movie() {
            Genres = GenreIds.Select(x => (Genre) x).ToList();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("release date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("original_language")]
        public string Language { get; set; }

        [JsonProperty("genre_ids")]
        private IReadOnlyList<int> GenreIds { get; set; } = new List<int>();

        public IReadOnlyList<Genre> Genres { get; }
        

        public enum Genre {
            Action = 28,
            Adventure = 12,
            Animation = 16,
            Comedy = 35,
            Crime = 80,
            Documentary = 99,
            Drama = 18,
            Family = 10751,
            Fantasy = 14,
            History = 36,
            Horror = 27,
            Music = 10402,
            Mystery = 9648,
            Romance = 10749,
            Science_Fiction = 878,
            Thriller = 53,
            TV_Movie = 10770,
            War = 10752,
            Western = 37
        }
    }
}