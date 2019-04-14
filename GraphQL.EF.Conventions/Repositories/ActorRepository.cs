using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.EF.Conventions.Data.DbContexts;
using GraphQL.EF.Conventions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.EF.Conventions.Repositories
{
    public interface IActorRepository
    {
        Task<Actor> FindActor(int id);

        Task<IEnumerable<Actor>> GetActors();

        Task<ILookup<int, Actor>> GetActorsPerMovie(IEnumerable<int> movieIds);
    }

    public class ActorRepository : IActorRepository
    {
        private readonly ActorDbContext _context;

        public ActorRepository(ActorDbContext context)
        {
            _context = context;
        }

        public async Task<Actor> FindActor(int id)
        {
            return await _context.Actors.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            return await _context.Actors.AsNoTracking().ToListAsync();
        }

        public async Task<ILookup<int, Actor>> GetActorsPerMovie(IEnumerable<int> movieIds)
        {
            return (await _context.Actors.AsNoTracking()
                                        .Where(x => movieIds.Contains(x.MovieId))
                                        .ToArrayAsync()).ToLookup(x => x.MovieId);
        }
    }
}
