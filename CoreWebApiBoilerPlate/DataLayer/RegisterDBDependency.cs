using CoreWebApiBoilerPlate.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiBoilerPlate.DataLayer
{
    public static class RegisterDBDependency
    {
        public static void RegisterDatabaseContext(this IServiceCollection services, string connectionString)
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
