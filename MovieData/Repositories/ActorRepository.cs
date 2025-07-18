using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Context;

namespace MovieData.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly MovieApiContext context;

        public ActorRepository(MovieApiContext context)
        {
            this.context = context;
        }

        public void Add(Actor actor)
        {
            context.Actors.Add(actor);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await context.Actors.AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await context.Actors
                .Include(a => a.Movies)
                .ToListAsync();
        }

        public async Task<Actor?> GetAsync(int id)
        {
            return await context.Actors
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Remove(Actor actor)
        {
            context.Actors.Remove(actor);
        }

        public void Update(Actor actor)
        {
            context.Actors.Update(actor);
        }
    }
}
