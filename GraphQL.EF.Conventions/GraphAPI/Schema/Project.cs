using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using GraphQL.EF.Conventions.Repositories;

namespace GraphQL.EF.Conventions.GraphAPI.Schema
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public async Task<IEnumerable<Datasource>> Datasource([Inject] IDatasourceRepository repository, [Inject] DataLoaderContext dataLoaderContext)
        {
            var loader = dataLoaderContext.GetOrAddCollectionBatchLoader<int, Data.Models.Datasource>("Projects_Datasources", repository.GetDatasourcesPerProject);
            return Mapper.Map<Datasource[]>(await loader.LoadAsync(Id));
        }
    }
}
