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


        public async Task<bool> AddActorToMovieAsync(int actorId, int movieId)
        {
            var movie = await unitOfWork.Movies.GetAsync(movieId);
            if (movie == null)
                return false;

            var actor = await unitOfWork.Actors.GetAsync(actorId);
            if (actor == null)
                return false;

            if (movie.Actors.Any(a => a.Id == actorId))
                return false;

            movie.Actors.Add(actor);
            await unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<IEnumerable<ActorDto>> GetAllAsync()
        {
            var actors = await unitOfWork.Actors.GetAllAsync();

            var dtos = actors.Select(a => new ActorDto
            {
                Name = a.Name,
                BirthYear = a.BirthYear
            });
            return dtos;
        }


        public async Task<ActorDto?> GetByIdAsync(int id)
        {
            var actor = await unitOfWork.Actors.GetAsync(id);

            if (actor == null)
                return null;

            return new ActorDto
            {
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };
        }

    }
}
