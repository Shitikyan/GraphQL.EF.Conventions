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
        private readonly ProjectDbContext _context;

        public DatasourceRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<ILookup<int, Datasource>> GetDatasourcesPerProject(IEnumerable<int> projectIds)
        {
            return (await _context.Datasource.Where(x => projectIds.Contains(x.ProjectId)).ToListAsync()).ToLookup(x => x.ProjectId);
        }

        public async Task<IEnumerable<Datasource>> GetDatasources()
        {
            return await _context.Datasource.AsNoTracking().ToListAsync();
        }
    }
}
