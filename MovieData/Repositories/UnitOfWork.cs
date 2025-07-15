using MovieCore.DomainContracts;
using MovieData.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieData.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieApiContext context;

        public IMovieRepository Movies { get; }
        public IReviewRepository Reviews { get; }
        public IActorRepository Actors { get; }

        public UnitOfWork(MovieApiContext context)
        {
            this.context = context;
            Movies = new MovieRepository(context);
            Reviews = new ReviewRepository(context);
            Actors = new ActorRepository(context);
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
