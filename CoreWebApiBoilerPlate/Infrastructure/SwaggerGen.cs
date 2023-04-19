using Microsoft.OpenApi.Models;

namespace CoreWebApiBoilerPlate.Core
{
    public static class SwaggerGen
    {
        public static void AddSwaggerGenWithJWTSecurity(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reporting API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT <strong>Authorization</strong> header using the Bearer scheme. <br/> 
                      Enter your token in the text input below.
                      <br/>Example: <i>'12345abcdef'</i>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
