using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using dotApiBasic.Model;
using dotApiBasic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace dotApiBasic.Controllers.Tests {
    public class MovieControllerTests {
        private readonly MovieController _movieController;
        private readonly Mock<MovieService> _movieServiceMock;

        public MovieControllerTests() {
            Mock<ILogger<MovieController>> loggerMock = new();
            // Create an instance of HttpClient or use a real one if needed.
            Mock<HttpClient> movieHttpClient = new();
            _movieServiceMock = new(movieHttpClient.Object);
            _movieController = new(loggerMock.Object, _movieServiceMock.Object);
        }
        
        private static T? GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T) ((ObjectResult) result.Result!).Value!;
        }

        [Fact]
        public async Task GetPopularMovies_ReturnsListOfMovies() {
            // Arrange
            var movies = new List<Movie> {
                new() {Id = 1, Title = "Movie 1"},
                new() {Id = 2, Title = "Movie 2"}
            };
            _movieServiceMock.Setup(movieService => movieService.GetPopularMoviesAsync()).ReturnsAsync(movies);

            // Act
            var actionResult = await _movieController.GetPopularMovies();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = GetObjectResultContent(actionResult);
            Assert.Equal(movies, resultObject);
        }


        [Fact]
        public async Task SearchMovies_ReturnsListOfMoviesMatchingQuery() {
            // Arrange
            var movies = new List<Movie>();
            movies.Add(new Movie {Title = "The Shawshank Redemption"});
            _movieServiceMock.Setup(x => x.GetMovieByQueryAsync(It.IsAny<string>())).ReturnsAsync(movies);

            // Act
            var actionResult = await _movieController.SearchMovies("The Shawshank Redemption");

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = GetObjectResultContent(actionResult);
            Assert.Equal("The Shawshank Redemption", resultObject.First().Title);
          
        }
        
        [Fact]
        public async Task GetMovieById_ReturnsMovieById() {
            // Arrange
            var expectedMovie = new MovieDetails { Id = 1, Title = "The Shawshank Redemption" };
            _movieServiceMock.Setup(x => x.GetMovieByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedMovie);

            // Act
            var actionResult = await _movieController.GetMovieById(1);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultObject = GetObjectResultContent(actionResult);
            if (resultObject != null) Assert.Equal(1, resultObject.Id);
        }
    }
}
