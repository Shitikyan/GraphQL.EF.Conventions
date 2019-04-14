using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GraphQL.Conventions;
using GraphQL.EF.Conventions.Data.Models;
using GraphQL.EF.Conventions.Repositories;

namespace GraphQL.EF.Conventions.GraphAPI
{
    public sealed class Query
    {
        public async Task<Schema.Actor> Actor([Inject] IActorRepository actorRepository, int id)
        {
            return Mapper.Map<Schema.Actor>(await actorRepository.FindActor(id));
        }

        public async Task<IEnumerable<Schema.Actor>> Actors([Inject] IActorRepository actorRepository)
        {
            return Mapper.Map<Schema.Actor[]>(await actorRepository.GetActors());
        }

        public async Task<IEnumerable<Schema.Movie>> Movies([Inject] IMovieRepository movieRepository)
        {
            return Mapper.Map<IEnumerable<Schema.Movie>>(await movieRepository.GetMovies());
        }
    }
}
