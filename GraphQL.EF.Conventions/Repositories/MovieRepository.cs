using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.EF.Conventions.Data.DbContexts;
using GraphQL.EF.Conventions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.EF.Conventions.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> FindMovie(int id);

        Task<IEnumerable<Movie>> GetMovies();
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _context;

        public MovieRepository(MovieDbContext movieDbContext)
        {
            _context = movieDbContext;
        }

        public async Task<Movie> FindMovie(int id)
        {
            return await _context.Movies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _context.Movies.AsNoTracking().ToListAsync();
        }
    }
}
