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

    }
}
