using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.EF.Conventions.Data.DbContexts;
using GraphQL.EF.Conventions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.EF.Conventions.Repositories
{
    public interface IDatasourceRepository
    {
        Task<IEnumerable<Datasource>> GetDatasources();

        Task<ILookup<int, Datasource>> GetDatasourcesPerProject(IEnumerable<int> projectIds);
    }

    public class DatasourceRepository : IDatasourceRepository
    {
        private readonly IDbContextProvider<int, ProjectDbContext> _contextProvider;

        public DatasourceRepository(IDbContextProvider<int, ProjectDbContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public async Task<ILookup<int, Datasource>> GetDatasourcesPerProject(IEnumerable<int> projectIds)
        {
            List<Datasource> datasources = new List<Datasource>();

            foreach (var projectId in projectIds.Distinct())
            {
                var context = _contextProvider.GetContext(projectId);
                var result = await context.Datasource.Where(x => projectId == x.ProjectId).ToListAsync();
                datasources.AddRange(result);
            }

            return datasources.ToLookup(x => x.ProjectId);
        }

        public async Task<IEnumerable<Datasource>> GetDatasources()
        {
            var context = _contextProvider.Contexts.FirstOrDefault();
            return await context.Value?.Datasource.AsNoTracking().ToListAsync();
        }
    }
}
