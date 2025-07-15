using MovieCore.Models.Entities;
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
        Task<bool> AnyAsync(int id);
        void Add(Review review);
        void Update(Review review);
        void Remove(Review review);
    }
}
