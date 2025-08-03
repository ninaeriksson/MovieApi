using MovieCore.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieContracts
{
    public interface IActorService
    {
        Task<ActorDto?> GetByIdAsync(int id);
        Task<IEnumerable<ActorDto>> GetAllAsync();
        Task AddActorToMovieAsync(int actorId, int movieId);
    }
}
