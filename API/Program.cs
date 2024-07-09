using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);//inject from file AddApplicationServices.cs

builder.Services.AddIdentityervices(builder.Configuration);//inject from file AddIdentityervices.cs

var app = builder.Build();

// Configure the HTTP request pipeline.ent())

app.UseMiddleware<ExceptionMiddleware>();//pass middleware from ExceptionMiddleware

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200","http://localhost:4200"));//add CORS middleware 
//we nee to authentificate before we can authorise
app.UseAuthentication();
//Once we have authentificate we can authorise
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();//create scope that once use will be disposed off
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();//migrate
    await Seed.SeedUsers(context);//seed databse
}
catch (Exception ex)
{
    
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error ocurred during migration");
}

app.Run();
