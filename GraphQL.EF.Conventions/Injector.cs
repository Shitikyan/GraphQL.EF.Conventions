using System;
using System.Reflection;
using GraphQL.Conventions;

namespace GraphQL.EF.Conventions
{
    public class Injector : IDependencyInjector
    {
        private readonly IServiceProvider _serviceProvider;

        public Injector(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object Resolve(TypeInfo typeInfo)
        {
            return _serviceProvider.GetService(typeInfo);
        }
    }
}
