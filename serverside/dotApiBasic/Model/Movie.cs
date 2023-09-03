using System.Collections.Generic;
using Newtonsoft.Json;

namespace dotApiBasic.Model {
    public class Movie {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public string ReleaseDate { get; set; }
        public bool Adult { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
        public List<int> GenreIds { get; set; }
    }
}