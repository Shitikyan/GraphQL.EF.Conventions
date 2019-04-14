using System.Collections.Generic;
using GraphQL.EF.Conventions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.EF.Conventions.Data.DbContexts
{
    public class ActorDbContext : DbContext
    {
        public ActorDbContext(DbContextOptions<ActorDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, Name = "Leonardo Dicaprio", MovieId = 1 },
                new Actor { Id = 2, Name = "Robert De Niro", MovieId = 2 },
                new Actor { Id = 3, Name = "Adrien Brody", MovieId = 3 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
