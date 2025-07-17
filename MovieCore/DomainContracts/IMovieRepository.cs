using MovieCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCore.DomainContracts
{
    public interface IMovieRepository
    {
        Task<Movie> GetAsync(int id);
        IQueryable<Movie> GetAll();
        Task<bool> AnyAsync(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Remove(Movie movie);
    }
}
