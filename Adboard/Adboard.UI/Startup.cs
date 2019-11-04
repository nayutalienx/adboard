using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Adboard.UI.Clients;
using Adboard.UI.Models.AutoMapperProfiles;
using Adboard.UI.Options;

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adboard.UI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ApiClientOptions _apiClientOptions;
        private readonly IdentityClientOptions _identityClientOptions;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiClientOptions = _configuration.GetSection("ApiClient").Get<ApiClientOptions>();
            _identityClientOptions = _configuration.GetSection("IdentityClient").Get<IdentityClientOptions>();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            services.Configure<ApiClientOptions>(_configuration.GetSection("ApiClient"));
            services.Configure<AdvertApiClientOptions>(_configuration.GetSection("AdvertApiClient"));
            services.Configure<CategoryApiClientOptions>(_configuration.GetSection("CategoryApiClient"));
            services.Configure<IdentityClientOptions>(_configuration.GetSection("IdentityClient"));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie(options => 
            {
                options.Cookie.Name = "dashboard-app";
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "http://localhost:5005";
                options.RequireHttpsMetadata = false;
                
                options.ClientId = "dashboard-app";
                options.ClientSecret = "dashboard-app";
                options.ResponseType = "code";

                options.SaveTokens = true;
                

                options.Scope.Add("dashboard-api");
                options.Scope.Add("role");
                options.Scope.Add("offline_access");
            });

            services.AddHttpClient<IAdvertApiClient, AdvertApiClient>(options =>
            {
                options.Timeout = TimeSpan.FromMinutes(1);
                options.BaseAddress = new Uri(_apiClientOptions.BaseUrl);
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddHttpClient<ICategoryApiClient, CategoryApiClient>(options =>
            {
                options.Timeout = TimeSpan.FromMinutes(1);
                options.BaseAddress = new Uri(_apiClientOptions.BaseUrl);
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddHttpClient<IIdentityClient, IdentityClient>(options =>
            {
                options.Timeout = TimeSpan.FromMinutes(1);
                options.BaseAddress = new Uri(_identityClientOptions.BaseUrl);
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });


            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.AddProfile(new AdvertProfile());
            }).CreateMapper());


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
