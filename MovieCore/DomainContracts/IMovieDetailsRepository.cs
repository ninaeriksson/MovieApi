using MovieCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCore.DomainContracts
{
    public interface IMovieDetailsRepository
    {
        Task<MovieDetails> GetAsync(int id);
        Task<IEnumerable<MovieDetails>> GetAllAsync();
        Task<bool> AnyAsync(int id);
        void Add(MovieDetails movieDetails);
        void Update(MovieDetails movieDetails);
        void Remove(MovieDetails movieDetails);
    }
}
