using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Implementation;
using DataAccessLayer.Abstraction;
using DataAccessLayer.StubImplementation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.DependencyInjection
{
    public static class Installer
    {
        public static IServiceCollection Install(this IServiceCollection serviceCollection) {
            serviceCollection.AddTransient<IAdvertManager, AdvertManager>()
                .AddTransient<IUserManager, UserManager>()
                .AddTransient<ICommentManager, CommentManager>();

            serviceCollection.AddSingleton<IAdvertRepository, AdvertRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<ICommentRepository, CommentRepository>();
            return serviceCollection;

        }
    }
}
