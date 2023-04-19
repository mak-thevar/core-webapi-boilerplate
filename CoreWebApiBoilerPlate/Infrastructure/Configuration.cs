using CoreWebApiBoilerPlate.Core.Middlewares;
using CoreWebApiBoilerPlate.DataLayer.Repository.Impl;
using CoreWebApiBoilerPlate.DataLayer.Repository.Interfaces;
using CoreWebApiBoilerPlate.Infrastructure;

namespace CoreWebApiBoilerPlate.Core
{
    public static class Configuration
    {
        public static void RegisterProjectDependencies(this WebApplicationBuilder builder)
        {

            //Fix JSON Self Referencing Loop Exceptions
            builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.RegisterSqliteDatabaseContext(builder.Configuration.GetConnectionString("DefaultCon"));
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddAutoMapper(typeof(Program));
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? builder.Configuration["JWT:Key"];
            builder.Services.RegisterJWTAuthentication(jwtKey);
            builder.Services.AddSwaggerGenWithJWTSecurity();
        }

        public static void RegisterProjectMiddleWares(this IApplicationBuilder builder)
        {
            builder.UseSerilogRequestLogging(opt =>
            {
                opt.Logger = Log.Logger;
            });

            builder.UseErrorHandler();
        }
    }
}