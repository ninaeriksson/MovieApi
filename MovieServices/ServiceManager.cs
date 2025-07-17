using MovieContracts;
using MovieCore.DomainContracts;
using MovieServices.Services;

namespace MovieServices
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork unitOfWork;

        private IMovieService? movieService;
        private IReviewService? reviewService;
        private IActorService? actorService;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IMovieService MovieService =>
            movieService ??= new MovieService(unitOfWork);

        public IReviewService ReviewService =>
            reviewService ??= new ReviewService(unitOfWork);

        public IActorService ActorService =>
            actorService ??= new ActorService(unitOfWork);
    }
}