using AutoMapper;

namespace GraphQL.EF.Conventions.Mapers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Data.Models.Actor, GraphAPI.Schema.Actor>();
            CreateMap<Data.Models.Movie, GraphAPI.Schema.Movie>();
            CreateMap<Data.Models.Project, GraphAPI.Schema.Project>();
            CreateMap<Data.Models.Datasource, GraphAPI.Schema.Datasource>();
        }
    }
}
