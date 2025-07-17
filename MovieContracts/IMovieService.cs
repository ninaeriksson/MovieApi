using MovieCore.Models.Dtos;

namespace MovieContracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task<MovieDto?> GetMovieByIdAsync(int id);
        Task<MovieDetailDto?> GetMovieDetailsAsync(int id);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto dto);
        Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto);
        Task<bool> DeleteMovieAsync(int id);
        Task<bool> AnyAsync(int id);
    }
}
