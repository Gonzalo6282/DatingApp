using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

//static class allows to use methods inside this class without having to create a new instance of this class
public static class ApplicationServiceExtensions
{
    //add AddApplicationServices to the IServiceCollection 
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)//say what it is what we are extending with "this" keyword and call it services 
    {
        services.AddControllers();
        //add dbcontext and pass class name DataContext from datacontext.cs file
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        //add CORS 
        services.AddCors();
        //add Token service by adding interface ITokenService and implementation class TokenService
        services.AddScoped<ITokenService, TokenService>();
        //add UserRepository service by adding interface IUserRepository and implementation class UserRepository
        services.AddScoped<IUserRepository, UserRepository>();
        //add ILikesRepository
        services.AddScoped<ILikesRepository, LikesRepository>();
        //add IPhotoService
        services.AddScoped<IPhotoService, PhotoService>();
        //add LogUserActivity
        services.AddScoped<LogUserActivity>();
        //add AddAutoMapper service
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //add cloudinary service
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        

        return services;
    }

}
