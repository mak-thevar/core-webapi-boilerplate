using CoreWebApiBoilerPlate.Application.DI;
using CoreWebApiBoilerPlate.DAL.DI;
using CoreWebApiBoilerPlate.WebApi.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace CoreWebApiBoilerPlate.WebApi.DI
{
    public static class DependencyInjection
    {
        public static void RegisterProjectDependencies(this WebApplicationBuilder builder)
        {

            builder.Services.AddDataAccessLayer(builder.Configuration.GetConnectionString("DefaultCon"));
            builder.Services.AddApplicationLayer();
            

            //Fix JSON Self Referencing Loop Exceptions
            builder.Services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddJsonOptions(config =>
                {
                    config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? builder.Configuration["JWT:Key"];
            builder.Services.RegisterJWTAuthentication(jwtKey);
            builder.Services.AddSwaggerGenWithJWTSecurity();

        }


        public static void RegisterProjectMiddleWares(this IApplicationBuilder builder)
        {
            builder.UseSerilogRequestLogging();

            builder.UseErrorHandler();
        }

        public static void RegisterJWTAuthentication(this IServiceCollection services, string key)
        {
            //Add authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        SaveSigninToken = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
                    };

                });

        }
    }
}
