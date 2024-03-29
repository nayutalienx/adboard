﻿using AutoMapper;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Implementation.AutoMapperProfiles;
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
                config.AddProfile(new AdvertProfile());
                config.AddProfile(new CommentProfile());
                config.AddProfile(new CategoryProfile());
                config.AddProfile(new PhotoProfile());
                config.AddProfile(new AddressProfile());
            });

            serviceCollection.AddTransient<IAdvertManager, AdvertManager>()
                .AddTransient<ICategoryManager, CategoryManager>()
                .AddTransient<IAdvertRepository, AdvertRepository>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<IAddressRepository, AddressRepository>()
                .AddTransient<ICommentRepository, CommentRepository>()
                .AddTransient<IPhotoRepository, PhotoRepository>()
                //.AddDbContext<AdboardContext>(ServiceLifetime.Transient)
                .AddSingleton(mapperConfiguration.CreateMapper());
            return serviceCollection;

        }
     
    }
}
