using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieData.Repositories
{
    internal class GenreRepository : IGenreRepository
    {
        private readonly MovieApiContext context;

        public GenreRepository(MovieApiContext context)
        {
            this.context = context;
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await context.Genres.FindAsync(id);
        }
    }
}
