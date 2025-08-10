using Moq;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieServices.Services;

namespace MovieServicesTests
{
    public class MovieServiceTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMovieRepository> movieRepoMock;
        private readonly MovieService movieService;

        public MovieServiceTests()
        {
            movieRepoMock = new Mock<IMovieRepository>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Movies).Returns(movieRepoMock.Object);
            movieService = new MovieService(unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_MovieExists_ReturnsMovieDto()
        {
            // Arrange
            int movieId = 1;

            var expectedMovie = new Movie
            {
                Id = movieId,
                Title = "Testen",
                Year = 1988,
                Duration = 90,
                GenreId = 5,
                Genre = new Genre { Name = "Dokumentär" },
            };

            movieRepoMock
                .Setup(repo => repo.GetAsync(movieId))
                .ReturnsAsync(expectedMovie);

            // Act
            var movieDto = await movieService.GetByIdAsync(movieId);

            // Assert
            Assert.NotNull(movieDto);
            Assert.Equal(expectedMovie.Id, movieDto.Id);
            Assert.Equal(expectedMovie.Title, movieDto.Title);
            Assert.Equal(expectedMovie.Year, movieDto.Year);
            Assert.Equal(expectedMovie.Genre?.Name, movieDto.Genre);
            Assert.Equal(expectedMovie.Duration, movieDto.Duration);
        }
    }

}
