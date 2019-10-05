using AutoMapper;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Objects.AutoMapperProfiles;
using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.DependencyInjection
{
    public static class Installer
    {
        public static IServiceCollection Install(this IServiceCollection serviceCollection) {
            var mapperConfiguration = new MapperConfiguration(config => {
                config.AddProfile(new UserProfile());
                config.AddProfile(new AdvertProfile());
                config.AddProfile(new CommentProfile());
            });

            serviceCollection.AddTransient<IAdvertManager, AdvertManager>()
                .AddTransient<IUserManager, UserManager>()
                .AddTransient<ICommentManager, CommentManager>()
                .AddScoped<IAdvertRepository, AdvertRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICommentRepository, CommentRepository>()
                .AddDbContext<AdboardContext>(ServiceLifetime.Scoped)
                .AddSingleton(mapperConfiguration.CreateMapper());
            return serviceCollection;

        }
    }
}
