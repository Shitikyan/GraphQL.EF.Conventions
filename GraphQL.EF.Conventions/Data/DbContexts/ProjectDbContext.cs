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
                new Datasource { Id = 1, Name = "Datasource from DB 1", ProjectId = 1, Type = 1 },
                new Datasource { Id = 2, Name = "Datasource from DB 2", ProjectId = 2, Type = 1 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
