using AutoMapper;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using GraphQL.EF.Conventions.Data.DbContexts;
using GraphQL.EF.Conventions.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.EF.Conventions
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<ActorDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ActorDBConnection")));
            services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MovieDBConnection")));
            services.AddDbContext<MasterDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MasterDBConnection")));

            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IDatasourceRepository, DatasourceRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddSingleton<IDbContextProvider<int, ProjectDbContext>, ProjectDbContextProvider>();

            services.AddSingleton(provider => new GraphQLEngine()
                .WithFieldResolutionStrategy(FieldResolutionStrategy.Normal)
                .BuildSchema(typeof(SchemaDefinition<GraphAPI.Query>)));

            services.AddScoped<IDependencyInjector, Injector>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<Query>();

            services.AddScoped<DataLoaderContext>();
            Mapper.Initialize(config => config.AddProfile<Mapers.Mapper>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
