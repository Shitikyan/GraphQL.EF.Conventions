using AutoMapper;

namespace GraphQL.EF.Conventions.Mapers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Data.Models.Actor, GraphAPI.Schema.Actor>();
            CreateMap<Data.Models.Movie, GraphAPI.Schema.Movie>();
        }
    }
}
