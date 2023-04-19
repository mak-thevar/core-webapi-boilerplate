using CoreWebApiBoilerPlate.Core;
using CoreWebApiBoilerPlate.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

//This will Register all the dependencies like the interfaces, Database, Automapper, etc
builder.RegisterProjectDependencies();




builder.Host.UseSerilog((context, services, config) =>
{
    config.WriteTo.Console();
});
var app = builder.Build();

//Auto Migration
using (var serviceScope = app.Services.CreateScope())
{
    var service = serviceScope.ServiceProvider;
    var dbContext = service.GetRequiredService<DefaultDBContext>();
    await dbContext.Database.MigrateAsync();

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyOrigin();
    opt.AllowAnyMethod();
});

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.RegisterProjectMiddleWares();

app.MapControllers();

app.Run();
