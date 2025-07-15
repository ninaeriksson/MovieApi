using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieData.Repositories
{
    internal class ReviewRepository : IReviewRepository
    {
        private readonly MovieApiContext context;

        public ReviewRepository(MovieApiContext context)
        {
            this.context = context;
        }

        public void Add(Review review)
        {
            context.Reviews.Add(review);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await context.Reviews.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await context.Reviews
                .Include(r => r.Movie)
                .ToListAsync();
        }

        public async Task<Review> GetAsync(int id)
        {
            return await context.Reviews
                .Include(r => r.Movie)
                .FirstAsync(m => m.Id == id);
        }

        public void Remove(Review review)
        {
            context.Reviews.Remove(review);
        }

        public void Update(Review review)
        {
            context.Reviews.Update(review);
        }
    }
}
