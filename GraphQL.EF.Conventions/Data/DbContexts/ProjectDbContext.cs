using GraphQL.EF.Conventions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.EF.Conventions.Data.DbContexts
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Datasource> Datasource { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Datasource>().HasData(
                new Datasource { Id = 1, Name = "Datasource1", ProjectId = 1, Type = 1 },
                new Datasource { Id = 2, Name = "Datasource2", ProjectId = 2, Type = 1 },
                new Datasource { Id = 3, Name = "Datasource3", ProjectId = 3, Type = 1 },
                new Datasource { Id = 4, Name = "Datasource4", ProjectId = 4, Type = 1 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
