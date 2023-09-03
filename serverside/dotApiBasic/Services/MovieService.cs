using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using dotApiBasic.Model;
using Newtonsoft.Json;

namespace dotApiBasic.Services {
    public class MovieService  {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> GetAsync<T>(string requestUrl, HttpMethod? method = null,string? query = null) {
            HttpRequestMessage request;
            
            if (method != null) {
                 request = new HttpRequestMessage(method, $"{BaseUrl}{requestUrl}");
            }else {
                 request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}{requestUrl}");
            }

            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {Token}");

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var movieResponse = JsonConvert.DeserializeObject<MovieResponse?>(body);
            return movieResponse?.Results ?? new List<Movie>();
        }  
        
        public async Task<MovieDetails> GetMovieAsync<T>(string requestUrl, HttpMethod? method = null,string? query = null) {
            HttpRequestMessage request;
            
            if (method != null) {
                 request = new HttpRequestMessage(method, $"{BaseUrl}{requestUrl}");
            }else {
                 request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}{requestUrl}");
            }

            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {Token}");
            request.Headers.Add("jsonNamingPolicy", "SnakeCaseLower");

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var movieResponse = JsonConvert.DeserializeObject<MovieDetails?>(body);
            return movieResponse ?? new MovieDetails();
        }

        public virtual  async Task<List<Movie>> GetPopularMoviesAsync() {
            return await GetAsync<MovieResponse>("movie/popular?language=en-US&page=1");
        }
        
        public virtual  async Task<List<Movie>> GetMovieByQueryAsync(string query) {
            return await GetAsync<MovieResponse>($"search/movie?query={query}");
        }
        
        public virtual  async Task<MovieDetails> GetMovieByIdAsync(int id) {
            var movieResponse = await GetMovieAsync<MovieDetails>($"movie/{id}?language=en-US");
            return movieResponse;
        }

        private const string Token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJkOGI1YTE4YTMwODBjMDk2ODNhYjJkNzVjMWE4OTI1NSIsInN1YiI6IjY0ZWM1YWUyMDZmOTg0MDBjYTU2YjhhYiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.BU0YslRTJSubjpQpSXmZLyXWd8jm-Nh5oKd0gESpskM";
        private const string BaseUrl = "https://api.themoviedb.org/3/";
    }

    public class MovieResponse {
        public int page { get; set; }
        public List<Movie>? Results { get; set; }
    }
}
