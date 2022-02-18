using CoreWebApiBoilerPlate.Infrastructure;
using CoreWebApiBoilerPlate.Infrastructure.Auth;
using CoreWebApiBoilerPlate.Infrastructure.Middlewares;
using HelpDeskBBQN.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate
{
    public class Startup
    {
        private readonly string SwaggerTitle;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var aName = Assembly.GetExecutingAssembly().FullName;
            SwaggerTitle = aName.Substring(0, aName.IndexOf(',', aName.IndexOf(',') + 1)).Trim();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbc = services.AddDbContext<DefaultContext>(options => {
                options.UseSqlite(@"DataSource=corewebapi.db",opt=>opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
                options.EnableSensitiveDataLogging();
                
            });
            services.AddControllers();
            services.AddInfrastructure();

            services.AddAutoMapper(typeof(Startup));

            #region Authentication Setup

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWTKey"]))
                };
            });
            services.AddScoped<IJWTAuth, JWTAuth>();

            #endregion

            #region Logger Setup
            var logConfig = new LoggerConfiguration();
            logConfig.ReadFrom.Configuration(Configuration);
            logConfig.Enrich.WithProperty("ApplicationName", this.GetType().Assembly.FullName);
            Log.Logger = logConfig.CreateLogger();
            services.AddSingleton(Log.Logger);
            #endregion

            #region Swaggger Setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = SwaggerTitle, Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
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

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging(config => { config.Logger = Log.Logger; });

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", SwaggerTitle);
            });

            app.UseRequestResponseLogging();
            app.UseErrorHandler();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Automigration, Comment the below 4 lines to disable auto migration
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DefaultContext>();
                context.Database.Migrate();
            }

        }
    }
}
