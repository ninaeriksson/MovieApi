using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieContracts;
using MovieCore.DomainContracts;
using MovieCore.Models.Dtos;
using MovieCore.Models.Entities;
using MovieCore.Models.Paging;

namespace MovieServices.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ReviewDto>> GetReviewsByMovieIdAsync(int movieId, PagingParameters paging)
        {
            var query = unitOfWork.Reviews.GetByMovieId(movieId);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((paging.Page - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Select(r => new ReviewDto
                {
                    ReviewerName = r.ReviewerName,
                    Rating = r.Rating,
                    Comment = r.Comment
                })
                .ToListAsync();

            return new PagedResult<ReviewDto>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = paging.Page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)paging.PageSize),
                PageSize = paging.PageSize
            };
        }

        public async Task<ReviewDto> AddReviewAsync(ReviewCreateDto dto)
        {
            var movie = await unitOfWork.Movies
                .GetAll()
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == dto.MovieId);

            if (movie == null)
                throw new ArgumentException($"Filmen med id {dto.MovieId} finns inte.");

            if (movie.Reviews.Count >= 10)
                throw new InvalidOperationException("En film får max ha 10 recensioner.");

            var review = new Review
            {
                MovieId = dto.MovieId,
                ReviewerName = dto.ReviewerName,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            unitOfWork.Reviews.Add(review);
            await unitOfWork.CompleteAsync();

            return new ReviewDto
            {
                Id = review.Id,
                ReviewerName = review.ReviewerName,
                Rating = review.Rating,
                Comment = review.Comment
            };
        }


    }
}
