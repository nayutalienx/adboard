using BusinessLogicLayer.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation
{
    public static class LayerInstaller
    {
        public static IServiceCollection InstallBusinessLogic(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAdvertManager, AdvertManager>()
                .AddTransient<IUserManager, UserManager>()
                .AddTransient<ICommentManager, CommentManager>();
            return serviceCollection;
        }
    }
}
