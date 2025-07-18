using MovieCore.Models.Dtos;
using MovieCore.Models.Paging;

namespace MovieContracts
{
    public interface IMovieService
    {
        Task<PagedResult<MovieDto>> GetAllAsync(PagingParameters paging);
        Task<MovieDto?> GetByIdAsync(int id);
        Task<MovieDetailDto?> GetMovieDetailsAsync(int id);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto dto);
        Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto);
        Task<bool> DeleteMovieAsync(int id);
        Task<bool> AnyAsync(int id);
    }
}
