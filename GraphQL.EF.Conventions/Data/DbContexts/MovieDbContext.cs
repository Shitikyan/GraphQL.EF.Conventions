using GraphQL.EF.Conventions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.EF.Conventions.Data.DbContexts
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Movie1" },
                new Movie { Id = 2, Title = "Movie2" },
                new Movie { Id = 3, Title = "Movie3" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
