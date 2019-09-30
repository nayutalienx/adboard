using DataAccessLayer.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.StubImplementation
{
    public static class LayerInstaller
    {
        public static IServiceCollection InstallStubDataAccessLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAdvertRepository, AdvertRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<ICommentRepository, CommentRepository>();
            return serviceCollection;
        }
    }
}
