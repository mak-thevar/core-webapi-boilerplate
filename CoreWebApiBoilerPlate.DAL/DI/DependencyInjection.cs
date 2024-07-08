using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Context;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Impl;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApiBoilerPlate.DAL.DI
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            // Register database context
            services.RegisterSqliteDatabaseContext(connectionString);

            // Register repository wrapper
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }


        public static void RegisterSqlServerDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DefaultDBContext>(options =>
            {
                options.UseSqlServer(connectionString, options =>
                {
                    options.EnableRetryOnFailure(3);
                    options.MigrationsAssembly(typeof(DefaultDBContext).Assembly.FullName);
                });
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
        }

        public static void RegisterSqliteDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DefaultDBContext>(options =>
            {
                options.UseSqlite(connectionString, options =>
                {
                    options.MigrationsAssembly(typeof(DefaultDBContext).Assembly.FullName);
                });
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
        }

    }
}
