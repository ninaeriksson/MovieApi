using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCore.DomainContracts
{
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
        IGenreRepository Genres { get; }
        IReviewRepository Reviews { get; }
        IActorRepository Actors { get; }
        Task CompleteAsync();
    }
}
