using System.Threading.Tasks;
using AutoMapper;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using GraphQL.EF.Conventions.Repositories;

namespace GraphQL.EF.Conventions.GraphAPI.Schema
{
    public sealed class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public async Task<Actor[]> Actors([Inject] IActorRepository actorRepository, [Inject] DataLoaderContext dataLoaderContext)
        {
            var loader = dataLoaderContext.GetOrAddCollectionBatchLoader<int, Data.Models.Actor>("Movie_Actors", actorRepository.GetActorsPerMovie);
            return Mapper.Map<Actor[]>(await loader.LoadAsync(Id));
        }
    }
}
