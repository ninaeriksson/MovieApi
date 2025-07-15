using MovieCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCore.DomainContracts
{
    public interface IActorRepository
    {
        Task<Actor?> GetAsync(int id);
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<bool> AnyAsync(int id);
        void Add(Actor actor);
        void Update(Actor actor);
        void Remove(Actor actor);
    }
}
