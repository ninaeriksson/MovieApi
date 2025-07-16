using MovieContracts;
using MovieCore.DomainContracts;
using MovieCore.Models.Dtos;

namespace MovieServices.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork unitOfWork;

        public ActorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> AddActorToMovieAsync(int actorId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ActorDto>> GetAllActorsAsync()
        {
            var actors = await unitOfWork.Actors.GetAllAsync();

            var dtos = actors.Select(a => new ActorDto
            {
                Name = a.Name,
                BirthYear = a.BirthYear
            });

            return dtos;
        }

        public Task<IEnumerable<ActorDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActorDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
