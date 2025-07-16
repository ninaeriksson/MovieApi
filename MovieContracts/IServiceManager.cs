using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieContracts
{
    public interface IServiceManager
    {
        IMovieService MovieService { get; }
        IReviewService ReviewService { get; }
        IActorService ActorService { get; }
    }
}
