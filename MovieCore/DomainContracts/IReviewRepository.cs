using MovieCore.Models.Dtos;
using MovieCore.Models.Entities;
using MovieCore.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCore.DomainContracts
{
    public interface IReviewRepository
    {
        Task<Review> GetAsync(int id);
        Task<IEnumerable<Review>> GetAllAsync();
        IQueryable<Review> GetByMovieId(int movieId);
        IQueryable<Review> GetAll();
        Task<bool> AnyAsync(int id);
        void Add(Review review);
        void Update(Review review);
        void Remove(Review review);
    }
}
