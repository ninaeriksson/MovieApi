using Microsoft.EntityFrameworkCore;
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


        public async Task AddActorToMovieAsync(int actorId, int movieId)
        {
            var movie = await unitOfWork.Movies
                .GetAll()
                .Include(m => m.Genre)
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException($"Filmen med id {movieId} finns inte.");

            if (movie.Genre?.Name == "Dokumentär" && movie.Actors.Count >= 10)
                throw new InvalidOperationException("En dokumentärfilm får inte ha fler än 10 skådespelare.");

            var actor = await unitOfWork.Actors.GetAsync(actorId);
            if (actor == null)
                throw new KeyNotFoundException($"Skådespelaren med id {actorId} finns inte.");

            if (movie.Actors.Any(a => a.Id == actorId))
                throw new InvalidOperationException("Skådespelaren är redan kopplad till filmen.");

            movie.Actors.Add(actor);
            await unitOfWork.CompleteAsync();
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
