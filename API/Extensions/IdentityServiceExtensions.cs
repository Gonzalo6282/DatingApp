using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

//static class allows to use methods inside this class without having to create a new instance of this class
public static class IdentityServiceExtensions
{

    //add AddApplicationServices to the IServiceCollection
    public static IServiceCollection AddIdentityervices(this IServiceCollection services, IConfiguration config)//say what it is what we are extending with "this" keyword and call it services 
    {
        //add Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {   //If not tokenkey  throw exception
            var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                //this will accept any token regardless has been signed or not
                ValidateIssuerSigningKey = true,
                //what the issuersigningkey is validating against
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,//we are not passing this to our token
                ValidateAudience = false//we are not passing this to our token
            };
        });
        return services;
    }
}
