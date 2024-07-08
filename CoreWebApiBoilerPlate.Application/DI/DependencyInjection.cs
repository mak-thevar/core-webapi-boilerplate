using CoreWebApiBoilerPlate.Application.Mappings;
using CoreWebApiBoilerPlate.Application.Services;
using CoreWebApiBoilerPlate.Application.Services.Interfaces;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Impl;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApiBoilerPlate.Application.DI
{
    public static class DependencyInjection
    {

        public static void AddApplicationLayer(this IServiceCollection services)
        {
            // Add application services here
            services.AddScoped<IServiceManager, ServiceManager>();

            // Register AutoMapper profiles
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }
    }
}
