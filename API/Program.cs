using API.Extensions;
using API.Middleware;

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

app.Run();
