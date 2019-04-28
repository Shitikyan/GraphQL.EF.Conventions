using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GraphQL.EF.Conventions.Data.DbContexts
{
    public interface IDbContextProvider<TKey, TContext>
    {
        Dictionary<TKey, TContext> Contexts { get; set; }

        TContext GetContext(TKey key);
    }

    public class ProjectDbContextProvider : IDbContextProvider<int, ProjectDbContext>
    {
        private readonly IConfiguration _configuration;

        public ProjectDbContextProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            Contexts = new Dictionary<int, ProjectDbContext>();
        }

        public Dictionary<int, ProjectDbContext> Contexts { get; set; }

        public ProjectDbContext GetContext(int key)
        {
            if (this.Contexts.TryGetValue(key, out var ctx))
                return ctx;

            string conString = this.GetConnectionString(key);
            var dbOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            dbOptionsBuilder.UseSqlServer(conString);
            this.Contexts[key] = new ProjectDbContext(dbOptionsBuilder.Options);
            return this.Contexts[key];
        }

        private string GetConnectionString(int key)
        {
            switch (key)
            {
                case 1:
                    return this._configuration.GetConnectionString("Project1DBConnection");
                case 2:
                    return this._configuration.GetConnectionString("Project2DBConnection");
                default:
                    throw new InvalidOperationException($"Connection String for project with key={key} was not found.");
            }
        }
    }
}
