using System.Collections.Generic;
using System.Threading.Tasks;
using dotApiBasic.Model;
using dotApiBasic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace dotApiBasic.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller {
        private readonly ILogger<MovieController> _logger;
        private readonly MovieService _movieService;

        public MovieController(ILogger<MovieController> logger, MovieService movieService) {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet("api/popular")]
        public async Task<ActionResult<List<Movie>>> GetPopularMovies()
        {
            _logger.LogInformation("Getting popular movies from TMDB");
            var response = await _movieService.GetPopularMoviesAsync();
            return Ok(response);
        }


        [HttpGet("api/search")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies(string query) {
            _logger.LogInformation("Searching for movies by title on TMDB");
            var response = await _movieService.GetMovieByQueryAsync(query)!;
            return Ok(response);
        }

        [HttpGet("api/movie/{id}")]
        public async Task<ActionResult<MovieDetails>> GetMovieById(int id) {
            _logger.LogInformation("Getting movie by id from TMDB");
            var response = await _movieService.GetMovieByIdAsync(id);
            return Ok(response);
        }
    }
}